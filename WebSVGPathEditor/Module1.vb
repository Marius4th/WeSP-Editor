Imports System.Runtime.CompilerServices
Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions

Public Module Module1
    'Constants
    Const DEGS_PER_RAD As Double = 180 / Math.PI

    'Toggles
    Public placeBetweenClosest As Boolean = False
    Public snapToGrid As Boolean = False
    Public showPoints As Boolean = True
    Public mirrorHor As Boolean = False
    Public mirrorVert As Boolean = False
    Public optimizePath As Boolean = True

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
    Public decimalPlaces As Integer = 2

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
    Public historyLock As Boolean = True
    Public modsSinceLastBkp As Integer = -2
    Public modsSinceLastSave As Integer = -2

    Public Sub AddToHistory()
        If historyLock Then Return
        Dim latest As String = SVG.GetHtml(optimizePath)

        If historySelected < history.Count - 1 Then
            history.RemoveRange(historySelected + 1, history.Count - historySelected - 1)
        End If

        If history.Count <= 0 OrElse history.Last <> latest Then
            history.Add(latest)
            historySelected = history.Count - 1
            modsSinceLastBkp += 1
            modsSinceLastSave += 1
        End If
    End Sub

    Public Sub Undo()
        If history.Count <= 0 Then Return
        historyLock = True
        historySelected = Math.Max(historySelected - 1, 0)
        SVG.ParseString(history(historySelected))
        historyLock = False
        modsSinceLastSave += 1
    End Sub

    Public Sub Redo()
        If history.Count <= 0 Then Return
        historyLock = True
        historySelected = Math.Min(historySelected + 1, history.Count - 1)
        SVG.ParseString(history(historySelected))
        historyLock = False
        modsSinceLastSave += 1
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

    <Extension()>
    Public Function RemoveLetters(str As String) As String
        Static rx As New Regex("[A-Za-z]")
        Return rx.Replace(str, "")
    End Function

    <Extension()>
    Public Function GetNumbers(str As String) As String
        Static rx As New Regex("[^\d,.]")
        Return rx.Replace(str, "")
    End Function

    <Extension()>
    Public Function ToList(sellist As ListBox.SelectedIndexCollection) As List(Of Integer)
        Dim lst As New List(Of Integer)
        For Each indx In sellist
            lst.Add(indx)
        Next
        Return lst
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

    'Returns angle in degs
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

    Public Function AngleToPointf(rads As Single, len As Single) As PointF
        Return New PointF(Math.Cos(rads) * len,
                         -Math.Sin(rads) * len)
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

    Public Function Ceil(a As Double, digits As Integer) As Double
        digits = Math.Pow(10, digits)
        Return Math.Ceiling(a * digits) / digits
    End Function

    Public Function OptimizePathD(d As String) As String
        Dim figData As String()
        Dim ppType As PointType = PointType.moveto
        Dim ppRelative As Boolean = False
        Dim lastType As PointType = PointType.smoothCurveto
        Dim lastRelative As Boolean = False

        Dim optD As String = ""

        For Each fig As String In Split(d, "Z")
            If fig.Length <= 1 Then Continue For
            'dData = Regex.Split(d, "([A-Z]+)", RegexOptions.IgnoreCase)
            figData = Regex.Split(fig, "([A-Z]+)", RegexOptions.IgnoreCase)

            Dim lastHadDecimals As Boolean = True

            For Each dat As String In figData
                If dat.Length <= 0 Then Continue For

                Select Case dat.ToUpper
                    Case Chr(PointType.moveto), Chr(PointType.lineto), Chr(PointType.horizontalLineto), Chr(PointType.verticalLineto), Chr(PointType.curveto), Chr(PointType.smoothCurveto), Chr(PointType.quadraticBezierCurve), Chr(PointType.smoothQuadraticBezierCurveto), Chr(PointType.ellipticalArc)
                        ppType = Asc(dat.ToUpper)
                        If dat = dat.ToUpper Then
                            ppRelative = False
                        Else
                            ppRelative = True
                        End If

                        If ppType <> lastType AndAlso (lastType <> PointType.moveto OrElse ppType <> PointType.lineto) OrElse lastRelative <> ppRelative Then
                            optD = Regex.Replace(optD, "\s+$|,+$", "")
                            lastType = ppType
                            lastRelative = ppRelative
                            optD &= dat
                        End If

                    Case Else
                        Dim coords As New List(Of Single)
                        Dim numb As String = ""
                        Dim strLst As String() = dat.Split(New String() {" "}, StringSplitOptions.RemoveEmptyEntries)
                        Dim numLst As String()

                        'Parse the string to get all the numbers
                        For si As Integer = 0 To strLst.Length - 1
                            numLst = strLst(si).Split(New String() {","}, StringSplitOptions.RemoveEmptyEntries)
                            For ni As Integer = 0 To numLst.Length - 1
                                numb = numLst(ni)

                                If numb.StartsWith("0.") Then
                                    If lastHadDecimals AndAlso (optD.EndsWith(" ") OrElse optD.EndsWith(",")) Then optD = optD.Remove(optD.Length - 1, 1)
                                    optD &= numb.Remove(0, 1)
                                Else
                                    optD &= numb
                                End If

                                If ni < numLst.Length - 1 Then
                                    optD &= ","
                                End If

                                If numb.Contains(".") Then
                                    lastHadDecimals = True
                                Else
                                    lastHadDecimals = False
                                End If
                            Next

                            optD &= " "
                        Next

                        'optD &= dat
                End Select

            Next
            optD = Regex.Replace(optD, "\s+$|,+$", "")
            optD &= "Z"
        Next

        optD = optD.Replace(" -", "-")
        optD = optD.Replace(",-", "-")
        optD = Regex.Replace(optD, "\s\s+", " ")

        Return optD
    End Function

    Public Function CutDecimals(num As Double) As Double
        Return Math.Round(num, decimalPlaces)
    End Function

    Public Function CutDecimals(pt As PointF) As PointF
        Return New PointF(Math.Round(pt.X, decimalPlaces), Math.Round(pt.Y, decimalPlaces))
    End Function

    Public Function CutDecimals(sz As SizeF) As SizeF
        Return New SizeF(Math.Round(sz.Width, decimalPlaces), Math.Round(sz.Height, decimalPlaces))
    End Function

    Public Function Clamp(val As Double, min As Double, max As Double)
        Return Math.Min(Math.Max(val, min), max)
    End Function

    Public Function Sign2(val As Double, Optional zeroRet As Integer = 1) As Integer
        If val < 0 Then
            Return -1
        ElseIf val > 0 Then
            Return 1
        Else
            Return zeroRet
        End If
    End Function

    '--------------------------------------------------------------------------------------------------------------------------

    Public Const ZERO_TOLERANCE As Single = 0.00001F

    Public Sub EllipticArcToBezierCurves(center As PointF, radius As PointF, xAngle As Single, startAngle As Single,
             deltaAngle As Single, moveToStart As Boolean, ByRef path As Drawing2D.GraphicsPath)

        Dim endAngle As Double = startAngle + deltaAngle
        Dim sign As Single = Sign2(endAngle - startAngle)
        Dim remain = Math.Abs(endAngle - startAngle)

        Dim prev As PointF = EllipticArcPoint(center, radius, xAngle, startAngle)

        While (remain > ZERO_TOLERANCE)

            Dim stepp As Double = Math.Min(remain, Math.PI / 4)
            Dim signStep As Double = stepp * sign

            Dim curr As PointF = EllipticArcPoint(center, radius, xAngle, startAngle + signStep)

            Dim alphaT As Double = Math.Tan(signStep / 2)
            Dim alpha = Math.Sin(signStep) * (Math.Sqrt(4 + 3 * alphaT * alphaT) - 1) / 3
            Dim q1 = prev + CType(EllipticArcDerivative(center, radius, xAngle, startAngle), CPointF) * alpha
            Dim q2 = curr - CType(EllipticArcDerivative(center, radius, xAngle, startAngle + signStep), CPointF) * alpha

            path.AddBezier(prev, q1, q2, curr)

            startAngle += signStep
            remain -= stepp
            prev = curr
        End While
    End Sub

    'r may be enlarged
    Public Sub EndpointToCenterArcParams(p1 As PointF, p2 As PointF, ByRef r As CPointF, xAngle As Single,
                                         flagA As Boolean, flagS As Boolean, ByRef c As CPointF, ByRef angles As CPointF)
        'Make radii positive
        r.X = Math.Abs(r.X)
        r.Y = Math.Abs(r.Y)

        '(F.6.5.1)
        Dim mat1 As New Matrix({{Math.Cos(xAngle), Math.Sin(xAngle)},
                               {-Math.Sin(xAngle), Math.Cos(xAngle)}})

        Dim mat2 As New Matrix({{(p1.X - p2.X) / 2},
                                {(p1.Y - p2.Y) / 2}})

        Dim pPrime As PointF = mat1.Multiply(mat2).ToPointf

        'Adjust radii (make sure thei are large enough)
        Dim a As Double = (Math.Pow(pPrime.X, 2) / Math.Pow(r.X, 2)) + (Math.Pow(pPrime.Y, 2) / Math.Pow(r.Y, 2))
        If a > 1 Then
            r.X = Math.Sqrt(a) * r.X
            r.Y = Math.Sqrt(a) * r.Y
        End If

        '(F.6.5.2)
        Dim scalarRxRy As Double = (Math.Pow(r.X, 2) * Math.Pow(r.Y, 2))
        Dim scalarRxNy As Double = (Math.Pow(r.X, 2) * Math.Pow(pPrime.Y, 2))
        Dim scalarRyNx As Double = (Math.Pow(r.Y, 2) * Math.Pow(pPrime.X, 2))
        Dim scalarDend As Double = scalarRxRy - scalarRxNy - scalarRyNx
        Dim scalarDsor As Double = scalarRxNy + scalarRyNx
        Dim scalar As Double = Math.Sqrt(Math.Abs(scalarDend / scalarDsor))
        If flagA = flagS Then
            scalar = -scalar
        Else
            scalar = Math.Abs(scalar)
        End If
        Dim mat3 As New Matrix({{(r.X * pPrime.Y) / r.Y},
                              {-((r.Y * pPrime.X) / r.X)}})

        Dim cPrime As Matrix = mat3.Multiply(scalar)

        '(F.6.5.3)
        mat1 = New Matrix({{Math.Cos(xAngle), -Math.Sin(xAngle)},
                           {Math.Sin(xAngle), Math.Cos(xAngle)}})

        mat2 = New Matrix({{(p1.X + p2.X) / 2},
                           {(p1.Y + p2.Y) / 2}})

        Dim center As PointF = mat1.Multiply(cPrime).Add(mat2).ToPointf

        '(F.6.5.4)
        Dim vec1 As New System.Windows.Vector((pPrime.X - cPrime.ToPointf.X) / r.X, (pPrime.Y - cPrime.ToPointf.Y) / r.Y)
        Dim vec2 As New System.Windows.Vector((-pPrime.X - cPrime.ToPointf.X) / r.X, (-pPrime.Y - cPrime.ToPointf.Y) / r.Y)
        Dim theta As Double = VectorAngle(New System.Windows.Vector(1, 0), vec1)

        Dim delta As Double = RadsToDegs(VectorAngle(vec1, vec2)) Mod 360

        If flagS = False AndAlso delta > 0 Then
            delta -= 360
        ElseIf flagS = True AndAlso delta < 0 Then
            delta += 360
        End If

        delta = DegsToRads(delta)

        'Return Values
        'r = New PointF(rX, rY)
        c = center
        angles = New PointF(theta, delta)
    End Sub

    Public Function VectorAngle(u As System.Windows.Vector, v As System.Windows.Vector) As Double
        Dim ret As Double = Math.Acos(System.Windows.Vector.Multiply(u, v) / (u.Length * v.Length))
        Dim sign As Integer = Math.Sign((u.X * v.Y) - (u.Y * v.X))
        If sign < 0 Then
            If ret > 0 Then Return -ret
            Return ret
        End If

        Return Math.Abs(ret)
    End Function

    Public Function EllipticArcPoint(c As PointF, r As PointF, xAngle As Single, t As Single) As PointF
        Return New PointF(c.X + r.X * Math.Cos(xAngle) * Math.Cos(t) - r.Y * Math.Sin(xAngle) * Math.Sin(t),
                           c.Y + r.X * Math.Sin(xAngle) * Math.Cos(t) + r.Y * Math.Cos(xAngle) * Math.Sin(t))
    End Function

    Public Function EllipticArcDerivative(c As PointF, r As PointF, xAngle As Single, t As Single) As PointF
        Return New PointF(-r.X * Math.Cos(xAngle) * Math.Sin(t) - r.Y * Math.Sin(xAngle) * Math.Cos(t),
                           -r.X * Math.Sin(xAngle) * Math.Sin(t) + r.Y * Math.Cos(xAngle) * Math.Cos(t))
    End Function

    'Sticks a point to equal portions of specific angle degrees
    Public Function StickPointToAngles(pt As PointF, origin As PointF, portionDegs As Single) As PointF
        Dim angleRads As Double = DegsToRads(Math.Round(LineAngle(origin, pt) / portionDegs) * portionDegs)
        Dim dist As Single = LineLength(pt, origin)
        Dim newPos As New PointF
        'Calculate the new pos based on the angle and distance of the point (pt)
        newPos.X = origin.X + Math.Cos(angleRads) * dist
        newPos.Y = origin.Y - Math.Sin(angleRads) * dist
        Return newPos
    End Function

End Module
