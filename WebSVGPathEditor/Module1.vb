Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices

Public Module Module1
    'Constants
    Const DEGS_PER_RAD As Double = 180 / Math.PI

    'Toggles
    Public placeBetweenClosest As Boolean = False
    Public snapToGrid As Boolean = False
    Public showPoints As Boolean = True
    Public mirrorHor As Boolean = False
    Public mirrorVert As Boolean = False

    'Tools
    Public Enum Tool
        Selection
        Movement
        Drawing
    End Enum
    Public selectedTool As Tool = Tool.Drawing
    Public selectedType As PointType = PointType.lineto

    'Others
    Public grid As New SizeF(32, 32)
    Public gridZoomed As New SizeF(320, 320)
    Public movingPoints As Boolean = False
    Public mouseLastPos As PointF
    Public movingObject As Boolean = False
    Public movingCanvas As Boolean = False

    Public selectionRect As New RectangleF(0, 0, 0, 0)
    Public selStart As New PointF(0, 0)
    Public selEnd As New PointF(0, 0)

    Public pressedKey As Keys = Keys.None
    Public pressedMod As Keys = Keys.None
    Public pressedMButton As MouseButtons = MouseButtons.None
    Public mouseDelta As Integer = 0

    Public subCursor As Bitmap = My.Resources.move

    Public defFilePath As String = My.Application.Info.DirectoryPath & "\untitled.wsvg"
    Public filePath As String = defFilePath

    Public history As New List(Of String)
    Public historySelected As Integer = 0
    Public historyLock As Boolean = False

    Public Sub AddToHistory()
        If historyLock Then Return
        Dim latest As String = SVG.GetHtml

        If historySelected < history.Count - 1 Then
            history.RemoveRange(historySelected + 1, history.Count - historySelected - 1)
        End If

        If history.Count <= 0 OrElse history.Last <> latest Then
            history.Add(latest)
            historySelected = history.Count - 1
        End If
    End Sub

    Public Sub Undo()
        If history.Count <= 0 Then Return
        historyLock = True
        historySelected = Math.Max(historySelected - 1, 0)
        SVG.ParseString(history(historySelected))
        historyLock = False
    End Sub

    Public Sub Redo()
        If history.Count <= 0 Then Return
        historyLock = True
        historySelected = Math.Min(historySelected + 1, history.Count - 1)
        SVG.ParseString(history(historySelected))
        historyLock = False
    End Sub

    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    'APIs

    <DllImport("user32.dll", SetLastError:=True)>
    Public Function SetWindowPos(ByVal hWnd As IntPtr, ByVal hWndInsertAfter As IntPtr, ByVal X As Integer, ByVal Y As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal uFlags As Integer) As Boolean
    End Function

    'Constants for SetWindowPos.
    Public Const SWP_NOMOVE As Integer = &H2
    Public Const SWP_NOSIZE As Integer = &H1
    Public Const SWP_NOACTIVATE As Integer = &H10
    Public Const SWP_SHOWWINDOW As Integer = &H40
    Public Const HWND_BOTTOM As Integer = 1
    Public Const HWND_NOTOPMOST As Integer = -2
    Public Const HWND_TOP As Integer = 0
    Public Const HWND_TOPMOST As Integer = -1

    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    'Extensions

    <Extension()>
    Public Sub AddEllipticalArc(ByRef path As Drawing2D.GraphicsPath, startPoint As Point, endPoint As Point, size As Size, xAngle As Integer, largeArc As Boolean, sweep As Boolean)

    End Sub

    <Extension()>
    Public Sub DrawX(ByRef graphs As Graphics, pen As Pen, pos As Point, radius As Integer)
        Dim rc As New Rectangle(pos.X - radius, pos.Y - radius, pos.X + radius, pos.Y + radius)
        graphs.DrawLine(pen, New Point(rc.X, rc.Y), New Point(rc.Width, rc.Height))
        graphs.DrawLine(pen, New Point(rc.X, rc.Height), New Point(rc.Width, rc.Y))
    End Sub

    <Extension()>
    Public Sub DrawX(ByRef graphs As Graphics, pen As Pen, rc As Rectangle)
        graphs.DrawLine(pen, New Point(rc.X, rc.Y), New Point(rc.X + rc.Width, rc.Y + rc.Height))
        graphs.DrawLine(pen, New Point(rc.X, rc.Y + rc.Height), New Point(rc.X + rc.Width, rc.Y))
    End Sub

    <Extension()>
    Public Function ToRectangle(ByRef rcf As RectangleF) As Rectangle
        Return New Rectangle(rcf.X, rcf.Y, rcf.Width, rcf.Height)
    End Function

    <Extension()>
    Public Function ToPoint(ByRef ptf As PointF) As Point
        Return New Point(ptf.X, ptf.Y)
    End Function

    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    Public Function MakeDWord(ByVal low As Integer, ByVal high As Integer) As Integer
        MakeDWord = (high * &H10000) Or (low And &HFFFF)
    End Function

    Public Function IntMakeWord(ByVal loWord As Short, ByVal hiWord As Short) As Integer
        Return (CInt(hiWord) << 16) Or (CInt(loWord) And Short.MaxValue)
    End Function
    Public Function IntLoWord(ByVal word As Integer) As Short
        Return CShort(word And Short.MaxValue)
    End Function
    Public Function IntHiWord(ByVal word As Integer) As Short
        Return CShort((word >> 16))
    End Function

    Public Function LngMakeWord(ByVal loWord As Integer, ByVal hiWord As Integer) As Long
        Return CLng(hiWord) << 32 Or (CLng(loWord) And UInteger.MaxValue)
    End Function
    Public Function LngLoWord(ByVal word As Long) As Integer
        Return CInt(word And UInteger.MaxValue)
    End Function
    Public Function LngHiWord(ByVal word As Long) As Integer
        Return CInt(word >> 32)
    End Function

    '--------------------------------------------------------------------------------------------------------------------------

    Public Function DegsToRads(degs As Double) As Double
        Return degs / DEGS_PER_RAD
    End Function

    Public Function RadsToDegs(rads As Double) As Double
        Return rads * DEGS_PER_RAD
    End Function

    Public Function DistanceToLine(p As PointF, lp1 As PointF, lp2 As PointF) As Double
        Dim len1 As Double = LineLength(p, lp1)
        Dim len2 As Double = LineLength(p, lp2)
        Dim len12 As Double = LineLength(lp1, lp2)

        Dim part1 As Double = Math.Abs((lp2.Y - lp1.Y) * p.X - (lp2.X - lp1.X) * p.Y + lp2.X * lp1.Y - lp2.Y * lp1.X) 'Twice triangle area (p, lp1, lp2)
        'Dim part2 As Double = Math.Sqrt(Math.Pow(lp2.Y - lp1.Y, 2) + Math.Pow((lp2.X - lp1.X), 2))  'Line length (lp1, lp2)

        If Math.Max(len1, len2) > len12 Then
            Return Math.Min(len1, len2)
        End If

        Return Math.Abs(part1 / len12)
    End Function

    Public Function DistanceToLineInfinite(p As PointF, lp1 As PointF, lp2 As PointF) As Double
        Dim part1 As Double = Math.Abs((lp2.Y - lp1.Y) * p.X - (lp2.X - lp1.X) * p.Y + lp2.X * lp1.Y - lp2.Y * lp1.X) 'Twice triangle area (p, lp1, lp2)
        Dim part2 As Double = Math.Sqrt(Math.Pow(lp2.Y - lp1.Y, 2) + Math.Pow((lp2.X - lp1.X), 2)) 'Line length (lp1, lp2)
        Return Math.Abs(part1 / part2)
    End Function

    Public Function LineLength(p1 As PointF, p2 As PointF) As Single
        Return Math.Sqrt(Math.Pow(p2.Y - p1.Y, 2) + Math.Pow((p2.X - p1.X), 2))
    End Function

    Public Function LineToRectangle(x1 As Single, y1 As Single, x2 As Single, y2 As Single) As RectangleF
        Return New RectangleF(Math.Min(x1, x2), Math.Min(y1, y2), Math.Abs(x1 - x2), Math.Abs(y1 - y2))
    End Function

    Public Function LineToRectangle(p1 As Point, p2 As Point) As Rectangle
        Return New Rectangle(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y), Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y))
    End Function

    Public Function LineToRectangle(p1 As PointF, p2 As PointF) As RectangleF
        Return New RectangleF(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y), Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y))
    End Function

    Public Function LineToRectangleNotZero(p1 As PointF, p2 As PointF) As RectangleF
        Return New RectangleF(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y), Math.Max(1, Math.Abs(p1.X - p2.X)), Math.Max(1, Math.Abs(p1.Y - p2.Y)))
    End Function

    Public Function LineToCircle(p1 As PointF, p2 As PointF) As RectangleF
        Dim len As Integer = Math.Max(1, LineLength(p1, p2))
        Dim midp As PointF = Midpoint(p1, p2)
        Dim rc As New RectangleF(midp.X - (len / 2), midp.Y - (len / 2), len, len)

        Return rc
    End Function

    Public Function LineToSemiCircle(p1 As PointF, p2 As PointF, sweep As Boolean) As RectangleF
        Dim len As Integer = Math.Max(1, LineLength(p1, p2))
        Dim midp As PointF = Midpoint(p1, p2)
        Dim angle As Single = LineAngle(p1, p2)
        Dim rc As New RectangleF(midp.X - (len / 2), midp.Y - (len / 2), len, len)

        If sweep = True Then
            Select Case Math.Floor(angle / 90)
                Case 0
                    rc.Width = Math.Abs(p2.X - rc.X)
                    rc.Height = Math.Abs(p1.Y - rc.Y)
                Case 1
                    rc.Height = Math.Abs(rc.Bottom - p2.Y)
                    rc.Y = p2.Y
                    rc.Width = Math.Abs(p1.X - rc.X)
                Case 2
                    rc.Height = Math.Abs(rc.Bottom - p1.Y)
                    rc.Width = Math.Abs(rc.Right - p2.X)
                    rc.Y = p1.Y
                    rc.X = p2.X
                Case 3
                    rc.Width = Math.Abs(rc.Right - p1.X)
                    rc.X = p1.X
                    rc.Height = Math.Abs(p2.Y - rc.Y)
            End Select
        Else
            Select Case Math.Floor(angle / 90)
                Case 0
                    rc.Height = Math.Abs(rc.Bottom - p2.Y)
                    rc.Width = Math.Abs(rc.Right - p1.X)
                    rc.Y = p2.Y
                    rc.X = p1.X
                Case 1
                    rc.Width = Math.Abs(rc.Right - p2.X)
                    rc.X = p2.X
                    rc.Height = Math.Abs(p1.Y - rc.Y)
                Case 2
                    rc.Height = Math.Abs(p2.Y - rc.Y)
                    rc.Width = Math.Abs(p1.X - rc.X)
                Case 3
                    rc.Height = Math.Abs(rc.Bottom - p1.Y)
                    rc.Y = p1.Y
                    rc.Width = Math.Abs(p2.X - rc.X)
            End Select
        End If
        Return rc
    End Function

    Public Function LineAngle(p1 As PointF, p2 As PointF) As Double
        Dim vec As New PointF(p2.X - p1.X, (p2.Y - p1.Y) * -1)
        Dim ret As Double = 0
        If vec.Y > 0 Then
            ret = Math.Atan2(vec.Y, vec.X)
            'If ret < 0 Then ret += 180
        Else
            ret = Math.Atan2(vec.Y, vec.X) + (Math.PI * 2)
        End If

        Return ret * DEGS_PER_RAD
    End Function

    Public Function Midpoint(p1 As PointF, p2 As PointF) As PointF
        Return New PointF((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2)
    End Function

    Public Function ClampPoint(ByRef p As PointF, limits As RectangleF)
        If p.X < limits.X Then p.X = limits.X
        If p.X > limits.Width Then p.X = limits.Width
        If p.Y < limits.Y Then p.Y = limits.Y
        If p.Y > limits.Height Then p.Y = limits.Height

        Return Nothing
    End Function

    Public Sub SnapPointToGrid(ByRef p As PointF, gr As SizeF)
        p.X = Math.Round(p.X / gr.Width) * gr.Width
        p.Y = Math.Round(p.Y / gr.Height) * gr.Height
    End Sub

    '--------------------------------------------------------------------------------------------------------------------------

    Public Function GetMousePlacePos(ByRef ctrl As Control) As PointF
        Dim mpos As PointF = Cursor.Position
        mpos = ctrl.PointToClient(mpos.ToPoint)
        ClampPoint(mpos, New Rectangle(0, 0, SVG.CanvasSizeZoomed.Width, SVG.CanvasSizeZoomed.Height))
        If My.Computer.Keyboard.AltKeyDown Then SnapPointToGrid(mpos, New SizeF(SVG.CanvasZoom * SVG.StikyGrid.Width, SVG.CanvasZoom * SVG.StikyGrid.Width))

        Return SVG.UnZoom(mpos)
    End Function

    Public Function IsPPointSelected(ByRef ppoint As PathPoint) As Boolean
        Return SVG.selectedPoints.Contains(ppoint)
    End Function

    '--------------------------------------------------------------------------------------------------------------------------

    'Public Function PathPointToString(ppoint As PathPoint)
    '    Dim ret As String = Chr(ppoint.pointType)

    '    If Not ppoint.pos is Nothing Then
    '        ret &= ppoint.pos.X & " " & ppoint.pos.Y
    '    End If
    '    If Not ppoint.p1 is Nothing Then
    '        ret &= " " & ppoint.p1.X & " " & ppoint.p1.Y
    '    End If
    '    If Not ppoint.p2 is Nothing Then
    '        ret &= " " & ppoint.p2.X & " " & ppoint.p2.Y
    '    End If

    '    Return ret
    'End Function



    Public Sub DrawEllipticalArc(ByRef path As Drawing2D.GraphicsPath, startPoint As Point, endPoint As Point, size As Size, largeArc As Boolean, sweep As Boolean)

    End Sub

    Public Structure HSVColor
        Dim H As Integer '0-360
        Dim S As Single '0-255
        Dim V As Single '0-255
        Dim A As Integer '0-255

        Public Function ToColor() As Color
            Dim c, x, m, th, ts, tv As Single
            Dim ret As New Color

            ts = S / 255
            tv = V / 255
            th = H

            c = tv * ts
            x = c * (1 - Math.Abs((th / 60) Mod 2 - 1))
            m = tv - c

            Select Case Math.Floor(th / 60)
                Case 0
                    ret = Color.FromArgb(A, (c + m) * 255, (x + m) * 255, (m) * 255)
                Case 1
                    ret = Color.FromArgb(A, (x + m) * 255, (c + m) * 255, (m) * 255)
                Case 2
                    ret = Color.FromArgb(A, (m) * 255, (c + m) * 255, (x + m) * 255)
                Case 3
                    ret = Color.FromArgb(A, (m) * 255, (x + m) * 255, (c + m) * 255)
                Case 4
                    ret = Color.FromArgb(A, (x + m) * 255, (m) * 255, (c + m) * 255)
                Case 5
                    ret = Color.FromArgb(A, (c + m) * 255, (m) * 255, (x + m) * 255)
            End Select

            Return ret
        End Function

        Public Shared Function FromColor(col As Color) As HSVColor
            Dim min, max, r, g, b As Single
            Dim ret As HSVColor

            r = col.R / 255
            g = col.G / 255
            b = col.B / 255

            min = Math.Min(r, Math.Min(g, b))
            max = Math.Max(r, Math.Max(g, b))

            'Value (luminace)
            ret.V = (max + min) / 2

            'Saturation
            If min = max Then
                ret.S = 0
                ret.H = 0
            Else
                If ret.V <= 0.5 Then
                    ret.S = (max - min) / (max + min)
                Else
                    ret.S = (max - min) / (2.0 - max - min)
                End If

                'Hue
                If r = max Then
                    ret.H = (g - b) / (max - min) * 60
                ElseIf g = max Then
                    ret.H = (2.0 + (b - r) / (max - min)) * 60
                ElseIf b = max Then
                    ret.H = (4.0 + (r - g) / (max - min)) * 60
                End If

                If ret.H < 0 Then ret.H += 360
            End If

            ret.V *= 255
            ret.S *= 255

            ret.A = col.A

            Return ret
        End Function

    End Structure

    Public Function ColorToHexString(ByRef col As Color) As String
        Return Conversion.Hex(col.R).PadLeft(2, "0") & Conversion.Hex(col.G).PadLeft(2, "0") & Conversion.Hex(col.B).PadLeft(2, "0") & Conversion.Hex(col.A).PadLeft(2, "0")
    End Function

    Public Function IsStringHexColor(ByVal hex As String) As Boolean
        If hex.StartsWith("#") AndAlso (hex.Length = 9 OrElse hex.Length = 7) Then Return True
        Return False
    End Function

    Public Function HexStringToColor(ByVal hex As String) As Color
        Dim col As New Color
        hex = hex.Replace("#", "")
        If hex.Length >= 8 Then 'RGBA
            Return Color.FromArgb(Convert.ToInt32(hex.Substring(6, 2), 16), Convert.ToInt32(hex.Substring(0, 2), 16), Convert.ToInt32(hex.Substring(2, 2), 16), Convert.ToInt32(hex.Substring(4, 2), 16))
        ElseIf hex.Length >= 6 Then 'RGB
            Return Color.FromArgb(Convert.ToInt32(hex.Substring(0, 2), 16), Convert.ToInt32(hex.Substring(2, 2), 16), Convert.ToInt32(hex.Substring(4, 2), 16))
        End If
        Return col
    End Function

    Public Function ColorInverseSimple(col As Color) As Color
        Return Color.FromArgb(col.A, 255 - col.R, 255 - col.G, 255 - col.B)
    End Function
    Public Function ColorInverseValue(col As Color) As Color
        Dim colHSV As HSVColor = HSVColor.FromColor(col)
        colHSV.V = (colHSV.V + 128) Mod 255
        Return colHSV.ToColor
    End Function
    Public Function ColorRotate(col As Color) As Color
        Return Color.FromArgb(col.A, (col.R + 128) Mod 255, (col.G + 128) Mod 255, (col.B + 128) Mod 255)
    End Function

    Public Function ColorInverseHSV(col As Color) As Color
        Dim colHSV As HSVColor = HSVColor.FromColor(col)
        colHSV.H = (colHSV.H + 180) Mod 360
        colHSV.S = (colHSV.S + 128) Mod 255
        colHSV.V = (colHSV.V + 128) Mod 255
        Return colHSV.ToColor
    End Function

    Public Function ClosestPointToPos(ByRef pos As PointF, ptsList As CPointF()) As KeyValuePair(Of Integer, CPointF)
        Dim closestDist As Integer = 999999
        Dim closestPt As CPointF = Nothing
        Dim closestPtI As Integer = -1
        Dim dist As Single

        'No secondary points
        For i As Integer = 0 To ptsList.Count - 1
            Dim pt As CPointF = ptsList(i)
            dist = LineLength(pt, pos)
            If dist < closestDist Then
                closestDist = dist
                closestPt = pt
                closestPtI = i
            End If
        Next

        Return New KeyValuePair(Of Integer, CPointF)(closestPtI, closestPt)
    End Function


    Public Function GetCurveBounds(ax As Single, ay As Single, bx As Single, by As Single, cx As Single, cy As Single, dx As Single, dy As Single) As RectangleF
        Dim px, py, qx, qy, rx, ry, sx, sy, tx, ty,
            tobx, toby, tocx, tocy, todx, tody, toqx, toqy,
            torx, tory, totx, toty As Single
        Dim x, y, minx, miny, maxx, maxy As Single

        minx = Single.PositiveInfinity
        miny = Single.PositiveInfinity
        maxx = Single.NegativeInfinity
        maxy = Single.NegativeInfinity

        ' directions
        tobx = bx - ax
        toby = by - ay
        tocx = cx - bx
        tocy = cy - by
        todx = dx - cx
        tody = dy - cy
        Dim stepp As Single = 1 / 40      'precision
        For d As Single = 0 To 1.001 Step stepp

            px = ax + d * tobx
            py = ay + d * toby
            qx = bx + d * tocx
            qy = by + d * tocy
            rx = cx + d * todx
            ry = cy + d * tody
            toqx = qx - px
            toqy = qy - py
            torx = rx - qx
            tory = ry - qy

            sx = px + d * toqx
            sy = py + d * toqy
            tx = qx + d * torx
            ty = qy + d * tory
            totx = tx - sx
            toty = ty - sy

            x = sx + d * totx
            y = sy + d * toty
            minx = Math.Min(minx, x)
            miny = Math.Min(miny, y)
            maxx = Math.Max(maxx, x)
            maxy = Math.Max(maxy, y)

        Next
        Return New RectangleF(minx, miny, maxx - minx, maxy - miny)
    End Function

    Public Function GetHTMLAttributeValue(html As String, propName As String, Optional isNameAlone As Boolean = True) As String
        'html = html.Replace("'", """")
        'propName = propName.Replace("'", """")
        If isNameAlone Then propName &= "="""

        Dim subStart As Integer = html.IndexOf(propName)
        If subStart < 0 Then Return ""

        subStart += propName.Length
        Dim subEnd As Integer = html.IndexOf("""", subStart)
        Dim substr As String = html.Substring(subStart, subEnd - subStart)

        Return substr
    End Function

End Module
