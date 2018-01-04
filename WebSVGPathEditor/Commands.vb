Imports System.Runtime.CompilerServices
Imports System.ComponentModel

Public Module Commands

    Public Enum PointType As Integer
        moveto = Asc("M")
        lineto = Asc("L")
        horizontalLineto = Asc("H")
        verticalLineto = Asc("V")
        curveto = Asc("C")          'Cubic Bezier
        smoothCurveto = Asc("S")    'Reflexion of Cubic Bezier
        quadraticBezierCurve = Asc("Q")         'Quadratic Bezier
        smoothQuadraticBezierCurveto = Asc("T") 'Reflexion of Quadratic Bezier
        ellipticalArc = Asc("A")
        'closepath = Asc("Z")
    End Enum

    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    Public Enum Orientation
        Horizontal = 0
        Vertical = 1
    End Enum

    'Public MustInherit Class PathPoint
    Public Class PathPoint
        Public pointType As PointType
        Public pos As CPointF
        Public prevPPoint As PathPoint
        Public selPoint As CPointF
        Public mirroredPP As PathPoint      'Only mirror the position
        Public mirroredPos As PathPoint    'Mirror the econdary info
        Public mirrorOrient As Orientation
        Public isMirrorOrigin As Boolean

        Public locked As Boolean = False    'Can't be modified
        Public stickedTo As PathPoint = Nothing
        Public nonInteractve As Boolean = False 'Can't be interected with the mouse and the point won't be drawn

        Public parent As Figure

        Public Shared Event OnModified(ByRef sender As PathPoint)

        Public Sub New()
            pointType = PointType.moveto
            pos = Nothing
            prevPPoint = Nothing
            parent = Nothing
            mirroredPP = Nothing
            mirroredPos = Nothing
            isMirrorOrigin = False
        End Sub

        Public Sub New(ptype As PointType, ByRef position As CPointF, ByRef paFig As Figure)
            pointType = ptype
            pos = CType(position, PointF)
            selPoint = pos
            prevPPoint = Nothing
            parent = paFig
            mirroredPP = Nothing
            mirroredPos = Nothing
            isMirrorOrigin = False
        End Sub

        Public Overridable Function Clone(Optional pa As Figure = Nothing) As PathPoint
            If pa Is Nothing Then pa = Me.parent
            Dim dup As New PathPoint(Me.pointType, CType(Me.pos, PointF), pa)
            dup.RefreshPrevPPoint()
            Return dup
        End Function

        Public Overridable Sub SetMirrorPPoint(ByRef pp As PathPoint, orient As Orientation)
            'If mirroredPP Is Nothing AndAlso pointType <> PointType.moveto Then
            If pointType <> PointType.moveto Then
                Dim selPP As PathPoint = pp
                If pp.HasSecondaryPoints() Then
                    selPP = pp.prevPPoint
                End If

                pp.mirrorOrient = orient
                pp.mirroredPP = Me
                pp.mirroredPP.RefreshPrevPPoint()
                pp.isMirrorOrigin = True

                mirroredPP = pp  'For secondary data
                mirrorOrient = orient
                isMirrorOrigin = False
                mirroredPos = selPP  'For position
                If selPP.pointType = PointType.moveto Then nonInteractve = True

                selPP.mirroredPos = Me  'For position
                selPP.mirrorOrient = orient
                selPP.isMirrorOrigin = True

                If selPP IsNot pp Then selPP.RefreshMirror()
                pp.RefreshMirror()
            End If
        End Sub

        Public Overridable Sub SetMirrorPos(ByRef pp As PathPoint, orient As Orientation)
            'If mirroredPos Is Nothing AndAlso pointType <> PointType.moveto Then
            If pointType <> PointType.moveto Then
                pp.mirrorOrient = orient
                pp.isMirrorOrigin = True

                mirrorOrient = orient
                isMirrorOrigin = False
                mirroredPos = pp  'For position
                If pp.pointType = PointType.moveto Then nonInteractve = True

                pp.mirroredPos = Me  'For position
                pp.mirrorOrient = orient
                pp.isMirrorOrigin = True

                pp.RefreshMirror()
            End If
        End Sub

        Public Overridable Sub Mirror(orient As Orientation)
            If mirroredPP Is Nothing AndAlso pointType <> PointType.moveto Then
                Dim selPP As PathPoint = Me
                If Me.HasSecondaryPoints() Then
                    selPP = prevPPoint
                End If

                mirrorOrient = orient
                'If prevPPoint IsNot Nothing AndAlso prevPPoint.mirroredPP IsNot Nothing Then
                '    mirroredPP = parent.InsertPPoint(pointType, pos, prevPPoint.mirroredPos.GetIndex)
                'Else
                mirroredPP = parent.AddPPoint(pointType, pos)
                'End If

                mirroredPP.RefreshPrevPPoint()
                isMirrorOrigin = True

                mirroredPP.mirroredPP = Me  'For secondary data
                mirroredPP.mirrorOrient = orient
                mirroredPP.isMirrorOrigin = False
                mirroredPP.mirroredPos = selPP  'For position
                If selPP.pointType = PointType.moveto Then mirroredPP.nonInteractve = True

                selPP.mirroredPos = mirroredPP  'For position
                selPP.mirrorOrient = orient
                selPP.isMirrorOrigin = True

                'If prevPPoint IsNot Nothing AndAlso prevPPoint.mirroredPP IsNot Nothing Then
                '    mirroredPP.mirroredPos = prevPPoint
                '    prevPPoint.mirroredPos.mirroredPos = Me

                'End If

                If selPP IsNot Me Then selPP.RefreshMirror()
                RefreshMirror()
            End If
        End Sub

        Public Sub RefreshPosition()
            Dim moveto As PathPoint = parent.GetMoveto()

            If moveto IsNot Nothing AndAlso mirroredPos IsNot Nothing Then
                Dim mirPos As New CPointF()

                Select Case mirrorOrient
                    Case Orientation.Vertical
                        mirPos.X = mirroredPos.pos.X
                        mirPos.Y = moveto.pos.Y + (moveto.pos.Y - mirroredPos.pos.Y)
                    Case Orientation.Horizontal
                        mirPos.Y = mirroredPos.pos.Y
                        mirPos.X = moveto.pos.X + (moveto.pos.X - mirroredPos.pos.X)
                End Select

                SetPosition(mirPos, False)
            End If
        End Sub

        Public Overridable Sub RefreshSeccondaryData()
            RaiseEvent OnModified(Me)
        End Sub

        Public Overridable Sub RefreshMirror()
            If mirroredPos IsNot Nothing AndAlso Not SVG.selectedPoints.Contains(mirroredPos) Then
                mirroredPos.RefreshPosition()
                mirroredPos.RefreshSeccondaryData()
            End If
            If mirroredPP IsNot Nothing AndAlso Not SVG.selectedPoints.Contains(mirroredPP) Then
                mirroredPP.RefreshSeccondaryData()
            End If
        End Sub

        Public Function ReflectPoint(axis As CPointF, pt As CPointF, orient As Orientation) As PointF
            Dim newPt As PointF
            Select Case orient
                Case Orientation.Vertical
                    newPt.X = pt.X
                    newPt.Y = axis.Y + (axis.Y - pt.Y)
                Case Orientation.Horizontal
                    newPt.Y = pt.Y
                    newPt.X = axis.X + (axis.X - pt.X)
            End Select

            Return newPt
        End Function

        Public Sub Offset(xoff As Single, yoff As Single, Optional refMirror As Boolean = True)
            Offset(New PointF(xoff, yoff), refMirror)
            RaiseEvent OnModified(Me)
        End Sub

        Public Overridable Sub Offset(ByRef ammount As PointF, Optional refMirror As Boolean = True)
            If pos Is Nothing Then Return
            pos.X += ammount.X
            pos.Y += ammount.Y
            'asyc.AddTaskReseter(AddressOf OnPPointModifiedAsync, {Me}, Me)
            'enqueued = True
            If refMirror Then RefreshMirror()
            RaiseEvent OnModified(Me)
        End Sub

        'Offsets the internaly selected point (it can be the pos or any other ref point)
        Public Overridable Sub OffsetSelPoint(ByRef ammount As PointF, Optional refMirror As Boolean = True)
            If selPoint Is Nothing Then selPoint = pos
            selPoint.X += ammount.X
            selPoint.Y += ammount.Y
            'asyc.AddTaskReseter(AddressOf OnPPointModifiedAsync, {Me}, Me)
            'enqueued = True
            If refMirror Then RefreshMirror()
            RaiseEvent OnModified(Me)
        End Sub

        Public Overridable Sub SetPosition(ByRef position As PointF, Optional refMirror As Boolean = True)
            Me.Offset(position - pos, refMirror)
        End Sub

        Public Function GetIndex() As Integer
            Return parent.IndexOf(Me)
        End Function

        Public Function GetIndexInPath() As Integer
            Return parent.parent.FigureIndexToPath(Me)
        End Function

        Public Sub RefreshPrevPPoint()
            Dim myIndex As Integer = Me.GetIndex()
            If myIndex > 0 Then
                prevPPoint = parent(myIndex - 1)
            Else
                prevPPoint = Nothing
            End If
        End Sub

        Public Shared Function OptimizeString(ByRef str As String) As String
            Return str.Replace(" -", "-")
        End Function

        Public Overridable Function GetString(Optional optimize As Boolean = True) As String
            Dim strData As String = Math.Round(pos.X, 3) & "," & Math.Round(pos.Y, 3)
            If optimize = True Then
                If prevPPoint IsNot Nothing Then
                    If pointType = PointType.lineto Then
                        'Treat it as a Horizontal/Vertical Lineto
                        If prevPPoint.pos.Y = pos.Y Then
                            Return "H" & pos.X
                        ElseIf prevPPoint.pos.X = pos.X Then
                            Return "V" & pos.Y
                        End If
                    End If

                    If prevPPoint.pointType = pointType OrElse (prevPPoint.pointType = PointType.moveto AndAlso pointType = PointType.lineto) Then
                        strData = OptimizeString(strData)
                        Return " " & strData
                    End If
                End If
            End If

            Return Chr(pointType) & strData
        End Function

        Public Overridable Sub AddToPath(ByRef path As Drawing2D.GraphicsPath)
            If prevPPoint Is Nothing Then Return
            path.AddLine(SVG.ApplyZoom(prevPPoint.pos), SVG.ApplyZoom(pos))
        End Sub

        Public Overridable Sub DrawSecPoints(ByRef graphs As Graphics)
        End Sub

        Public Overridable Sub OnMoveStart(ByRef mpos As PointF)
            selPoint = pos
        End Sub

        Public Overridable Function GetClosestPoint(ByRef mpos As CPointF) As CPointF
            Return pos
        End Function

        Public Sub RefreshLBsValue(ByRef figLB As ListBox, ByRef pathLB As ListBox)
            figLB.Items.Item(parent.IndexOf(Me)) = Me.GetString(False)
            pathLB.Items.Item(parent.parent.FigureIndexToPath(Me)) = Me.GetString(False)
        End Sub

        Public Sub StickToGrid()
            'Dim off As CPointF = SVG.StickPointToCanvasGrid(pos) - pos
            'Me.Offset(off)
            SetPosition(SVG.StickPointToCanvasGrid(pos))
        End Sub

        Public Sub RoundPosition()
            SetPosition(New SizeF(Math.Round(pos.X), Math.Round(pos.Y)))
        End Sub

        Public Sub FloorPosition()
            SetPosition(New SizeF(Math.Floor(pos.X), Math.Floor(pos.Y)))
        End Sub

        Public Sub CeilPosition()
            SetPosition(New SizeF(Math.Ceiling(pos.X), Math.Ceiling(pos.Y)))
        End Sub

        Public Sub Delete()
            If parent IsNot Nothing Then
                parent.Remove(Me)
            End If
        End Sub

        'Protected Overrides Sub Finalize()
        '    If parent IsNot Nothing Then
        '        Dim index As Integer = Me.GetIndex
        '        If index + 1 < parent.Count AndAlso index > 0 Then
        '            parent(index + 1).prevPPoint = parent(index - 1)
        '        End If
        '        OnPPointDeleted(Me)
        '    End If

        '    MyBase.Finalize()
        'End Sub

        Public Sub StickToAngleToPoint(ByRef pt As PointF)
            If prevPPoint Is Nothing Then Return

            Dim angleRads As Double = DegsToRads(Math.Round(LineAngle(prevPPoint.pos, pt) / 45) * 45)
            Dim dist As Single = LineLength(pt, prevPPoint.pos)
            Dim newPos As New CPointF
            'Calculate the new pos based on the angle and distance of the point (pt)
            newPos.X = prevPPoint.pos.X + Math.Cos(angleRads) * dist
            newPos.Y = prevPPoint.pos.Y - Math.Sin(angleRads) * dist
            Me.SetPosition(newPos)
        End Sub

        Public Overridable Function HasSecondaryPoints() As Boolean
            Return False
        End Function

        Public Overridable Function GetLeft() As Single
            Return pos.X
        End Function

        Public Overridable Function GetRight() As Single
            Return pos.X
        End Function

        Public Overridable Function GetTop() As Single
            Return pos.Y
        End Function

        Public Overridable Function GetBottom() As Single
            Return pos.Y
        End Function

        Public Overridable Function GetBounds() As RectangleF
            If prevPPoint Is Nothing Then Return New RectangleF(pos.X, pos.Y, 0, 0)
            Return LineToRectangle(prevPPoint.pos.X, prevPPoint.pos.Y, pos.X, pos.Y)
        End Function

        'Converts from relative coords to absolute
        Public Overridable Sub RelativeToAbsolute()
            'In case there is no previous ppoint for some reason (mostly for movetos)
            Dim prevpp As PathPoint = SVGPath.GetPreviousPPoint(Me)
            If prevpp Is Nothing Then Return
            pos.X = prevpp.pos.X + pos.X
            pos.Y = prevpp.pos.Y + pos.Y
        End Sub

    End Class

    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    Public Class PPMoveto
        Inherits PathPoint

        Public Sub New(position As CPointF, ByRef paFig As Figure)
            MyBase.New(PointType.moveto, position, paFig)
            'pointType = ptye
            'pos = position
        End Sub

        Public Overrides Sub Offset(ByRef ammount As PointF, Optional refMirror As Boolean = True)
            If pos Is Nothing Then Return
            MyBase.Offset(ammount, refMirror)

            For Each pp As PathPoint In parent
                If pp Is Me OrElse SVG.selectedPoints.Contains(pp) Then Continue For
                pp.RefreshPosition()
            Next
        End Sub

        'Offsets the internaly selected point (it can be the pos or any other ref point)
        Public Overrides Sub OffsetSelPoint(ByRef ammount As PointF, Optional refMirror As Boolean = True)
            If selPoint Is Nothing Then selPoint = pos
            MyBase.OffsetSelPoint(ammount, refMirror)

            For Each pp As PathPoint In parent
                pp.RefreshMirror()
            Next
        End Sub

    End Class

    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    'Public Class PPClosePath
    '    Inherits PathPoint

    '    Public Sub New(ByRef paFig As Figure)
    '        MyBase.New(PointType.closepath, Nothing, paFig)
    '        'pointType = ptye
    '        'pos = position
    '    End Sub

    '    Public Overrides Function GetString(Optional optimize As Boolean = True) As String
    '        Return Chr(pointType)
    '    End Function

    '    Public Overrides Function GetBounds() As RectangleF
    '        Return New RectangleF()
    '    End Function

    'End Class

    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    Public Class PPLineto
        Inherits PathPoint

        Public Sub New(position As CPointF, ByRef paFig As Figure)
            MyBase.New(PointType.lineto, position, paFig)
            'pointType = ptye
            'pos = position
        End Sub

    End Class

    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    'Public Class PPHorLineto
    '    Inherits PathPoint

    '    Public Sub New(ByRef prevPP As PathPoint, x As Integer, ByRef paFig As Figure)
    '        MyBase.New(PointType.lineto, prevPP, New Point(prevPP), paFig)
    '        'pointType = ptye
    '        'pos = position
    '    End Sub

    '    Public Overrides Sub AddToPath(ByRef path As Drawing2D.GraphicsPath)
    '        path.AddLine(prevPPoint.pos, pos)
    '    End Sub

    'End Class

    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    'Just partially implemented. Will be fully implemented in the future.
    Public Class PPEllipticalArc
        Inherits PathPoint
        Public sweep As Boolean
        Public size As SizeF

        Public Sub New(position As CPointF, sweep As Boolean, ByRef paFig As Figure)
            MyBase.New(PointType.ellipticalArc, position, paFig)
            'pointType = ptye
            'pos = position
            Me.sweep = sweep
        End Sub

        Public Overrides Function Clone(Optional pa As Figure = Nothing) As PathPoint
            If pa Is Nothing Then pa = Me.parent
            Dim dup As New PPEllipticalArc(CType(Me.pos, PointF), Me.sweep, pa)
            dup.size = Me.size
            Return dup
        End Function

        Public Overrides Function GetString(Optional optimize As Boolean = True) As String
            Dim strData As String = Math.Round(size.Width, 3) / 2 & "," & Math.Round(size.Height, 3) / 2 & " 0 0," & Math.Abs(CInt(sweep)) & " " & Math.Round(pos.X, 3) & "," & Math.Round(pos.Y, 3)
            If optimize = True Then
                If prevPPoint IsNot Nothing Then
                    If prevPPoint.pointType = pointType Then
                        strData = OptimizeString(strData)
                        Return " " & strData
                    End If
                End If
            End If

            Return Chr(pointType) & strData
        End Function

        Public Overrides Sub AddToPath(ByRef path As Drawing2D.GraphicsPath)
            Dim maxSz As Integer = Math.Max(1, LineLength(prevPPoint.pos, pos))
            Dim rect As RectangleF = LineToCircle(SVG.ApplyZoom(prevPPoint.pos), SVG.ApplyZoom(pos))
            'rect.Size = New SizeF(maxSz, maxSz)
            'Dim sz As New SizeF(maxSz, maxSz)
            Dim t As Integer = sweep
            Dim angle As Double = (360 - LineAngle(prevPPoint.pos, pos)) + 180 '+ p2.p1.Y
            Dim apperture As Single = 180

            size = New SizeF(maxSz, maxSz)
            If sweep = False Then apperture *= -1

            path.AddArc(rect, angle, apperture)
        End Sub

        Public Overrides Sub RefreshSeccondaryData()
            If mirroredPP IsNot Nothing Then
                sweep = CType(mirroredPP, PPEllipticalArc).sweep
            End If
            MyBase.RefreshSeccondaryData()
        End Sub

        Public Overrides Sub OnMoveStart(ByRef mpos As PointF)
            'Dim dpos As Integer = LineLength(mpos, pos)
            'Dim dr1 As Integer = LineLength(mpos, refPoint)

            'If dpos < dr1 Then
            '    selPoint = pos
            'Else
            '    selPoint = refPoint
            'End If
            selPoint = GetClosestPoint(mpos)
            Dim rc As New RectangleF(pos.X + 1, pos.Y + 1, 1, 1)
            If rc.Contains(mpos.X, mpos.Y) Then
                sweep = Not sweep
            End If
        End Sub

        Public Overrides Sub DrawSecPoints(ByRef graphs As Graphics)
            'Sweep Flag
            Dim rc As New Rectangle(SVG.ApplyZoom(pos).X + 10, SVG.ApplyZoom(pos).Y + 10, 10, 10)
            Static penIn As New Pen(Color.Pink, 1)
            Static penOut As New Pen(Color.FromArgb(255, 40, 40, 40), 3)

            graphs.DrawEllipse(penOut, rc)
            graphs.DrawEllipse(penIn, rc)

            If sweep Then
                graphs.DrawX(penOut, rc)
                graphs.DrawX(penIn, rc)
            End If
        End Sub

        Public Overrides Function HasSecondaryPoints() As Boolean
            Return True
        End Function

        Public Overrides Function GetBounds() As RectangleF
            Return LineToSemiCircle(prevPPoint.pos, pos, sweep)
        End Function

    End Class

    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    Public Class PPBezier
        Inherits PathPoint
        Public refPoint As CPointF

        Public Sub New(ByRef position As CPointF, ByRef refp As CPointF, ByRef paFig As Figure)
            MyBase.New(PointType.quadraticBezierCurve, position, paFig)
            Me.refPoint = refp
        End Sub

        Public Overrides Function Clone(Optional pa As Figure = Nothing) As PathPoint
            If pa Is Nothing Then pa = Me.parent
            Dim dup As New PPBezier(CType(Me.pos, PointF), CType(Me.refPoint, PointF), pa)
            Return dup
        End Function

        Public Overrides Sub Offset(ByRef ammount As PointF, Optional refMirror As Boolean = True)
            refPoint.X += ammount.X
            refPoint.Y += ammount.Y

            MyBase.Offset(ammount, refMirror)
        End Sub

        Public Overrides Function GetString(Optional optimize As Boolean = True) As String
            Dim strData As String = Math.Round(refPoint.X, 3) & "," & Math.Round(refPoint.Y, 3) & " " & Math.Round(pos.X, 3) & "," & Math.Round(pos.Y, 3)
            If optimize = True Then
                If prevPPoint IsNot Nothing Then
                    If prevPPoint.pointType = pointType Then
                        strData = OptimizeString(strData)
                        Return " " & strData
                    End If
                End If
            End If
            Return Chr(pointType) & strData
        End Function

        Public Overrides Sub AddToPath(ByRef path As Drawing2D.GraphicsPath)
            path.AddBezier(SVG.ApplyZoom(prevPPoint.pos), SVG.ApplyZoom(refPoint), SVG.ApplyZoom(pos), SVG.ApplyZoom(pos))
            'path.AddBezier(prevPoint.pos, prevPoint.pos, refPoint, pos)
        End Sub

        Public Overrides Sub DrawSecPoints(ByRef graphs As Graphics)
            Dim rc As New Rectangle(SVG.ApplyZoom(refPoint).X - 4, SVG.ApplyZoom(refPoint).Y - 4, 8, 8)
            Static penIn As New Pen(Color.Pink, 1)
            Static penOut As New Pen(Color.FromArgb(255, 40, 40, 40), 3)

            graphs.DrawEllipse(penOut, rc)
            graphs.DrawEllipse(penIn, rc)
            graphs.DrawLine(penIn, SVG.ApplyZoom(pos), SVG.ApplyZoom(refPoint))
            graphs.DrawLine(penIn, SVG.ApplyZoom(prevPPoint.pos), SVG.ApplyZoom(refPoint))
        End Sub

        Public Overrides Sub OnMoveStart(ByRef mpos As PointF)
            'Dim dpos As Integer = LineLength(mpos, pos)
            'Dim dr1 As Integer = LineLength(mpos, refPoint)

            'If dpos < dr1 Then
            '    selPoint = pos
            'Else
            '    selPoint = refPoint
            'End If
            selPoint = GetClosestPoint(mpos)
        End Sub

        'Public Overrides Sub SelectClosestPoint(ByRef mpos As Point)
        '    selPoint = GetClosestPoint(mpos)
        'End Sub

        Public Overrides Function GetClosestPoint(ByRef mpos As CPointF) As CPointF
            Return ClosestPointToPos(mpos, {pos, refPoint}).Value
        End Function

        Public Overrides Sub RefreshSeccondaryData()
            If mirroredPP IsNot Nothing Then
                refPoint = ReflectPoint(parent.GetMoveto().pos, CType(mirroredPP, PPBezier).refPoint, mirrorOrient)
            End If
            MyBase.RefreshSeccondaryData()
        End Sub

        Public Overrides Function HasSecondaryPoints() As Boolean
            Return True
        End Function

        Public Overrides Function GetBounds() As RectangleF
            Return GetCurveBounds(pos.X, pos.Y, pos.X, pos.Y, refPoint.X, refPoint.Y, prevPPoint.pos.X, prevPPoint.pos.Y)
        End Function

        Public Overrides Sub RelativeToAbsolute()
            Dim prevpp As PathPoint = SVGPath.GetPreviousPPoint(Me)
            If prevpp Is Nothing Then Return
            pos.X = prevpp.pos.X + pos.X
            pos.Y = prevpp.pos.Y + pos.Y
            refPoint.X = prevpp.pos.X + refPoint.X
            refPoint.Y = prevpp.pos.Y + refPoint.Y
        End Sub

    End Class

    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    Public Class PPCurveto
        Inherits PathPoint
        Public refPoint1 As CPointF
        Public refPoint2 As CPointF

        Public Sub New(ByRef position As CPointF, ByRef refp1 As CPointF, ByRef refp2 As CPointF, ByRef paFig As Figure)
            MyBase.New(PointType.curveto, position, paFig)
            Me.refPoint1 = refp1
            Me.refPoint2 = refp2
        End Sub

        Public Overrides Function Clone(Optional pa As Figure = Nothing) As PathPoint
            If pa Is Nothing Then pa = Me.parent
            Dim dup As New PPCurveto(CType(Me.pos, PointF), CType(Me.refPoint1, PointF), CType(Me.refPoint2, PointF), pa)
            Return dup
        End Function

        Public Overrides Sub Offset(ByRef ammount As PointF, Optional refMirror As Boolean = True)
            refPoint1.X += ammount.X
            refPoint1.Y += ammount.Y
            refPoint2.X += ammount.X
            refPoint2.Y += ammount.Y

            MyBase.Offset(ammount, refMirror)
        End Sub

        Public Overrides Function GetString(Optional optimize As Boolean = True) As String
            Dim strData As String = Math.Round(refPoint1.X, 3) & "," & Math.Round(refPoint1.Y, 3) & " " &
                Math.Round(refPoint2.X, 3) & "," & Math.Round(refPoint2.Y, 3) & " " &
                Math.Round(pos.X, 3) & "," & Math.Round(pos.Y, 3)

            If optimize = True Then
                If prevPPoint IsNot Nothing Then
                    If prevPPoint.pointType = pointType Then
                        strData = OptimizeString(strData)
                        Return " " & strData
                    End If
                End If
            End If
            Return Chr(pointType) & strData
        End Function

        Public Overrides Sub AddToPath(ByRef path As Drawing2D.GraphicsPath)
            path.AddBezier(SVG.ApplyZoom(prevPPoint.pos), SVG.ApplyZoom(refPoint1), SVG.ApplyZoom(refPoint2), SVG.ApplyZoom(pos))
            'path.AddBezier(prevPoint.pos, prevPoint.pos, refPoint, pos)
        End Sub

        Public Overrides Sub DrawSecPoints(ByRef graphs As Graphics)
            Dim rc1 As New Rectangle(SVG.ApplyZoom(refPoint1).X - 4, SVG.ApplyZoom(refPoint1).Y - 4, 8, 8)
            Dim rc2 As New Rectangle(SVG.ApplyZoom(refPoint2).X - 4, SVG.ApplyZoom(refPoint2).Y - 4, 8, 8)
            Static penIn As New Pen(Color.Pink, 1)
            Static penOut As New Pen(Color.FromArgb(255, 40, 40, 40), 3)

            graphs.DrawEllipse(penOut, rc1)
            graphs.DrawEllipse(penIn, rc1)
            graphs.DrawLine(penIn, SVG.ApplyZoom(pos), SVG.ApplyZoom(refPoint1))
            graphs.DrawLine(penIn, SVG.ApplyZoom(prevPPoint.pos), SVG.ApplyZoom(refPoint1))

            graphs.DrawEllipse(penOut, rc2)
            graphs.DrawEllipse(penIn, rc2)
            graphs.DrawLine(penIn, SVG.ApplyZoom(pos), SVG.ApplyZoom(refPoint2))
            graphs.DrawLine(penIn, SVG.ApplyZoom(prevPPoint.pos), SVG.ApplyZoom(refPoint2))
        End Sub

        Public Overrides Sub OnMoveStart(ByRef mpos As PointF)
            'Dim dpos As Integer = LineLength(mpos, pos)
            'Dim dr1 As Integer = LineLength(mpos, refPoint)

            'If dpos < dr1 Then
            '    selPoint = pos
            'Else
            '    selPoint = refPoint
            'End If
            selPoint = GetClosestPoint(mpos)
        End Sub

        'Public Overrides Sub SelectClosestPoint(ByRef mpos As Point)
        '    selPoint = GetClosestPoint(mpos)
        'End Sub

        Public Overrides Function GetClosestPoint(ByRef mpos As CPointF) As CPointF
            Return ClosestPointToPos(mpos, {pos, refPoint1, refPoint2}).Value
        End Function

        Public Overrides Sub RefreshSeccondaryData()
            If mirroredPP IsNot Nothing Then
                refPoint1 = ReflectPoint(parent.GetMoveto().pos, CType(mirroredPP, PPCurveto).refPoint2, mirrorOrient)
                refPoint2 = ReflectPoint(parent.GetMoveto().pos, CType(mirroredPP, PPCurveto).refPoint1, mirrorOrient)
            End If
            MyBase.RefreshSeccondaryData()
        End Sub

        Public Overrides Function HasSecondaryPoints() As Boolean
            Return True
        End Function

        Public Overrides Function GetBounds() As RectangleF
            Return GetCurveBounds(pos.X, pos.Y, refPoint2.X, refPoint2.Y, refPoint1.X, refPoint1.Y, prevPPoint.pos.X, prevPPoint.pos.Y)
        End Function

        Public Overrides Sub RelativeToAbsolute()
            Dim prevpp As PathPoint = SVGPath.GetPreviousPPoint(Me)
            If prevpp Is Nothing Then Return
            pos.X = prevpp.pos.X + pos.X
            pos.Y = prevpp.pos.Y + pos.Y
            refPoint1.X = prevpp.pos.X + refPoint1.X
            refPoint1.Y = prevpp.pos.Y + refPoint1.Y
            refPoint2.X = prevpp.pos.X + refPoint2.X
            refPoint2.Y = prevpp.pos.Y + refPoint2.Y
        End Sub

    End Class

End Module
