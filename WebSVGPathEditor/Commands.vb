Imports System.Runtime.CompilerServices
Imports System.ComponentModel
Imports Microsoft.Win32.SafeHandles
Imports System.Runtime.InteropServices

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
        closepath = Asc("Z")
    End Enum

    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    Public Enum Orientation
        Horizontal = 0
        Vertical = 1
        None
    End Enum

    Public Enum MoveMods As Long
        StickToGrid = 1     '10000000
        StickTo45Degs = 2   '01000000
    End Enum

    'Public MustInherit Class PathPoint
    Public Class PathPoint
        Implements IDisposable

        Public pointType As PointType
        Protected _pos As CPointF
        Protected _prevPPoint As PathPoint
        Public selPoint As CPointF
        Public mirroredPP As PathPoint      'Only mirror the position
        Public mirroredPos As PathPoint    'Mirror the econdary info
        Public mirrorOrient As Orientation = Orientation.None
        Public isMirrorOrigin As Boolean

        Public locked As Boolean = False    'Can't be modified
        Public stickedTo As PathPoint = Nothing
        Public nonInteractve As Boolean = False 'Can't be interected with the mouse and the point won't be drawn

        Public parent As Figure


        Public ReadOnly Property Pos() As CPointF
            Get
                Return _pos
            End Get
        End Property

        Public ReadOnly Property PrevPPoint() As PathPoint
            Get
                Return _prevPPoint
            End Get
        End Property

        Public Shared Event OnModified(ByRef sender As PathPoint)

        Public Sub New()
            pointType = PointType.moveto
            _pos = Nothing
            _prevPPoint = Nothing
            parent = Nothing
            mirroredPP = Nothing
            mirroredPos = Nothing
            isMirrorOrigin = False
        End Sub

        Public Sub New(ptype As PointType, ByRef position As CPointF, ByRef paFig As Figure)
            pointType = ptype
            _pos = CType(position, PointF)
            selPoint = Pos
            _prevPPoint = Nothing
            parent = paFig
            mirroredPP = Nothing
            mirroredPos = Nothing
            isMirrorOrigin = False
        End Sub

        Dim disposed As Boolean = False

        ' Public implementation of Dispose pattern callable by consumers.
        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub

        ' Protected implementation of Dispose pattern.
        Protected Overridable Sub Dispose(disposing As Boolean)
            If disposed Then Return

            If disposing Then
                ' Free any other managed objects here.
                '
            End If

            ' Free any unmanaged objects here.
            '
            If mirroredPP IsNot Nothing Then
                'parent.numMirrored -= 1
                mirroredPP.mirroredPP = Nothing
                If mirroredPP.nonInteractve Then mirroredPP.nonInteractve = False
            End If
            If mirroredPos IsNot Nothing Then
                'If mirroredPP Is Nothing Then parent.numMirrored -= 1
                parent.NumMirrored -= 1
                mirroredPos.mirroredPos = Nothing
            End If

            mirroredPP = Nothing
            mirroredPos = Nothing

            disposed = True
        End Sub

        Public Overridable Sub SetPrevPPoint(ByRef prev As PathPoint)
            _prevPPoint = prev
        End Sub

        Public Overridable Function Clone(destIndex As Integer, Optional insertInPa As Boolean = True, Optional pa As Figure = Nothing) As PathPoint
            If pa Is Nothing Then pa = Me.parent
            Dim dup As New PathPoint(Me.pointType, CType(Me.Pos, PointF), pa)
            If insertInPa Then
                pa.Insert(destIndex, dup, False)
                dup.RefreshPrevPPoint()
            End If
            Return dup
        End Function

        Public Overridable Function Clone(Optional insertInPa As Boolean = True, Optional pa As Figure = Nothing) As PathPoint
            If pa Is Nothing Then pa = Me.parent
            Return Clone(pa.Count, insertInPa, pa)
        End Function

        Public Overridable Sub SetMirrorPPoint(ByRef mirror As PathPoint, orient As Orientation)
            If pointType <> PointType.moveto Then
                'If mirroredPos Is Nothing And mirroredPP Is Nothing Then parent.numMirrored += 1
                mirror.mirroredPP = Me
                mirror.mirrorOrient = orient
                mirror.isMirrorOrigin = True

                Me.mirroredPP = mirror
                Me.mirrorOrient = orient
                Me.isMirrorOrigin = True

                mirror.RefreshSeccondaryData()
                parent.mirrorOrient = orient
            End If
        End Sub

        Public Overridable Sub SetMirrorPos(ByRef mirror As PathPoint, orient As Orientation)
            If pointType <> PointType.moveto Then
                If mirroredPos Is Nothing OrElse mirror.mirroredPos Is Nothing Then parent.NumMirrored += 1
                mirror.mirroredPos = Me
                mirror.mirrorOrient = orient
                mirror.isMirrorOrigin = True

                Me.mirroredPos = mirror
                Me.mirrorOrient = orient
                Me.isMirrorOrigin = True

                mirror.RefreshPosition()
                parent.mirrorOrient = orient
            End If
        End Sub

        Public Overridable Sub Mirror(orient As Orientation)
            If mirroredPP Is Nothing AndAlso pointType <> PointType.moveto Then
                Dim clon As PathPoint ' = Me.Clone()
                If prevPPoint.mirroredPos IsNot Nothing Then
                    'Add the mirroring ppoint in prev ppoint's mirror's pos
                    clon = Me.Clone(PrevPPoint.mirroredPos.GetIndex + 1, True, Nothing)

                    mirroredPP = clon
                    mirroredPos = prevPPoint.mirroredPos
                    mirroredPos.mirroredPos = Me

                    clon.mirroredPP = Me
                    clon.mirroredPos = prevPPoint

                    prevPPoint.mirroredPos = clon

                    mirroredPos.RefreshPosition()
                    mirroredPos.RefreshSeccondaryData()
                    mirroredPos.nonInteractve = False
                Else
                    'Add the mirroring ppoint to the end of the figure
                    clon = Me.Clone(True)

                    mirroredPP = clon
                    mirroredPos = clon.prevPPoint

                    clon.mirroredPP = Me
                    clon.mirroredPos = Me.prevPPoint
                End If

                mirrorOrient = orient
                isMirrorOrigin = True
                clon.mirrorOrient = orient
                clon.isMirrorOrigin = False

                If Me.prevPPoint.pointType = PointType.moveto Then
                    clon.nonInteractve = True
                    Me.PrevPPoint.mirroredPos = clon
                End If

                clon.RefreshPosition()
                clon.RefreshSeccondaryData()
                parent.mirrorOrient = orient
                parent.numMirrored += 1
            End If
        End Sub

        Public Sub BreakMirror()
            If mirroredPos IsNot Nothing Then
                mirroredPos.mirroredPos = Nothing
                mirroredPos.nonInteractve = False
                mirroredPos = Nothing
                nonInteractve = False
            End If
            If mirroredPP IsNot Nothing Then
                mirroredPP.mirroredPP = Nothing
                mirroredPP = Nothing
            End If
        End Sub

        Public Sub RefreshPosition()
            'If mirroredPos.pointType = PointType.moveto Then
            '    SetPosition(mirroredPos.pos, False)
            '    Return
            'End If

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
            RefreshPrevPPoint()
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

        Public Sub Multiply(xscale As Single, yscale As Single, Optional refMirror As Boolean = True)
            Multiply(New PointF(xscale, yscale), refMirror)
        End Sub

        Public Overridable Sub Multiply(ByRef ammount As PointF, Optional refMirror As Boolean = True)
            If Pos Is Nothing Then Return
            Pos.X *= ammount.X
            Pos.Y *= ammount.Y
            If refMirror Then RefreshMirror()
            RaiseEvent OnModified(Me)
        End Sub

        Public Sub Offset(xoff As Single, yoff As Single, Optional refMirror As Boolean = True)
            Offset(New PointF(xoff, yoff), refMirror)
            'RaiseEvent OnModified(Me)
        End Sub

        Public Overridable Sub Offset(ByRef ammount As PointF, Optional refMirror As Boolean = True)
            If pos Is Nothing Then Return
            pos.X += ammount.X
            Pos.Y += ammount.Y
            If refMirror Then RefreshMirror()
            RaiseEvent OnModified(Me)
        End Sub

        'Offsets the internaly selected point (it can be the pos or any other ref point)
        Public Overridable Sub OffsetSelPoint(newPos As PointF, delta As PointF, mods As Long, Optional refMirror As Boolean = True)
            'If selPoint Is Nothing Then selPoint = pos
            If selPoint Is Nothing Then Return
            selPoint.X += delta.X
            selPoint.Y += delta.Y

            If (mods And MoveMods.StickToGrid) > 0 Then
                Dim off As PointF = SVG.StickPointToCanvasGrid(selPoint) - selPoint
                selPoint.X += off.X
                selPoint.Y += off.Y
            End If

            If (mods And MoveMods.StickTo45Degs) > 0 AndAlso PrevPPoint IsNot Nothing Then
                Dim off As PointF = StickPointToAngles(newPos, PrevPPoint.Pos, 45) - selPoint
                selPoint.X += off.X
                selPoint.Y += off.Y
            End If

            'asyc.AddTaskReseter(AddressOf OnPPointModifiedAsync, {Me}, Me)
            'enqueued = True
            If refMirror Then RefreshMirror()
            RaiseEvent OnModified(Me)
        End Sub

        Public Overridable Sub FlipSecondary(dir As Orientation)

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

        Public Overridable Sub RefreshPrevPPoint()
            Dim myIndex As Integer = Me.GetIndex()
            If myIndex > 0 Then
                SetPrevPPoint(parent(myIndex - 1))
            Else
                SetPrevPPoint(Nothing)
            End If
        End Sub

        Public Shared Function OptimizeStringMore(ByRef str As String) As String
            Return str.Replace(" -", "-")
        End Function

        Public Overridable Function GetString(Optional optimize As Boolean = True) As String
            If optimize = False Then
                Return GetStringAbsolute()
            Else
                Dim abs As String = OptimizePathD(GetStringAbsolute())
                Dim rel As String = OptimizePathD(GetStringRelative())

                If abs.Length > rel.Length Then Return rel
                Return abs
            End If
        End Function

        Public Overridable Function GetStringAbsolute() As String
            Dim cutPos As PointF = CutDecimals(Pos)

            If pointType = PointType.lineto Then
                'Treat it as a Horizontal/Vertical Lineto
                If PrevPPoint.Pos.Y = Pos.Y Then
                    Return "H" & cutPos.X
                ElseIf PrevPPoint.Pos.X = Pos.X Then
                    Return "V" & cutPos.Y
                End If
            End If

            Return Chr(pointType) & cutPos.X & "," & cutPos.Y
        End Function

        Public Overridable Function GetStringRelative() As String
            Dim relPos As PointF = CutDecimals(Pos)

            If PrevPPoint IsNot Nothing Then relPos = CutDecimals(Pos - PrevPPoint.Pos)

            If pointType = PointType.lineto Then
                'Treat it as a Horizontal/Vertical Lineto
                If PrevPPoint.Pos.Y = Pos.Y Then
                    Return "h" & relPos.X
                ElseIf PrevPPoint.Pos.X = Pos.X Then
                    Return "v" & relPos.Y
                End If
            End If

            Return Chr(pointType).ToString.ToLower & relPos.X & "," & relPos.Y
        End Function

        Public Overridable Sub AddToGxPath(ByRef path As Drawing2D.GraphicsPath)
            If PrevPPoint Is Nothing Then Return
            path.AddLine(SVG.ApplyZoom(PrevPPoint.Pos), SVG.ApplyZoom(Pos))
        End Sub

        Public Overridable Sub DrawSecPoints(ByRef graphs As Graphics)
        End Sub

        Public Overridable Sub OnMoveStart(ByRef mpos As PointF)
            selPoint = pos
        End Sub

        Public Overridable Function GetClosestPoint(ByRef mpos As CPointF) As CPointF
            Return pos
        End Function

        'Public Sub StickSelToGrid()
        '    If selPoint Is Nothing Then Return
        '    Dim np As PointF = SVG.StickPointToCanvasGrid(selPoint)
        '    Me.OffsetSelPoint(np, np - selPoint, MoveMods.StickToGrid)
        '    'SetPosition(SVG.StickPointToCanvasGrid(Pos))
        'End Sub

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

        'Public Sub StickPosTo45Degs(pt As PointF)
        '    If PrevPPoint Is Nothing Then Return

        '    Dim angleRads As Double = DegsToRads(Math.Round(LineAngle(PrevPPoint.Pos, pt) / 45) * 45)
        '    Dim dist As Single = LineLength(pt, PrevPPoint.Pos)
        '    Dim newPos As New CPointF
        '    'Calculate the new pos based on the angle and distance of the point (pt)
        '    newPos.X = PrevPPoint.Pos.X + Math.Cos(angleRads) * dist
        '    newPos.Y = PrevPPoint.Pos.Y - Math.Sin(angleRads) * dist
        '    Me.SetPosition(newPos)
        'End Sub

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
        Public Overridable Sub RelativeToAbsolute(onlyPos As Boolean)
            'In case there is no previous ppoint for some reason (mostly for movetos)
            Dim prevpp As PathPoint = SVGPath.GetPreviousPPoint(Me)
            If prevpp Is Nothing Then Return
            SetPosition(prevpp.Pos + Pos, False)
        End Sub

        Public Function GetNextPPoint() As PathPoint
            Dim indx As Integer = Me.GetIndex
            If parent.Count > indx + 1 Then
                Return parent(indx + 1)
            End If
            Return Nothing
        End Function

    End Class

    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    Public Class PPMoveto
        Inherits PathPoint

        Public Sub New(position As CPointF, ByRef paFig As Figure)
            MyBase.New(PointType.moveto, position, paFig)
            'pointType = ptye
            'pos = position
        End Sub

        Public Overrides Function Clone(destIndex As Integer,  Optional insertInPa As Boolean = True, Optional pa As Figure = Nothing) As PathPoint
            If pa Is Nothing Then pa = Me.parent
            Dim dup As New PPMoveto(CType(Me.Pos, PointF), pa)
            If insertInPa Then
                pa.Insert(destIndex, dup, False)
                dup.RefreshPrevPPoint()
            End If
            Return dup
        End Function

        Public Overrides Sub Multiply(ByRef ammount As PointF, Optional refMirror As Boolean = True)
            If Pos Is Nothing Then Return
            MyBase.Multiply(ammount, refMirror)

            For Each pp As PathPoint In parent
                If pp Is Me OrElse SVG.selectedPoints.Contains(pp) Then Continue For
                pp.RefreshPosition()
            Next
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
        Public Overrides Sub OffsetSelPoint(newPos As PointF, delta As PointF, mods As Long, Optional refMirror As Boolean = True)
            If selPoint Is Nothing Then selPoint = Pos
            MyBase.OffsetSelPoint(newPos, delta, mods, refMirror)

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

        Public Overrides Function Clone(destIndex As Integer, Optional insertInPa As Boolean = True, Optional pa As Figure = Nothing) As PathPoint
            If pa Is Nothing Then pa = Me.parent
            Dim dup As New PPLineto(CType(Me.Pos, PointF), pa)
            If insertInPa Then
                pa.Insert(destIndex, dup, False)
                dup.RefreshPrevPPoint()
            End If
            Return dup
        End Function

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

    Public Class PPEllipticalArc
        Inherits PathPoint
        Public flarge As Boolean
        Public fsweep As Boolean
        Public radii As PointF
        Public xangle As Single

        Private _secCenter As CPointF
        Private _secAngleRad As New CPointF
        Private _secHeight As New CPointF

        Public Sub New(position As CPointF, ByRef paFig As Figure)
            MyBase.New(PointType.ellipticalArc, position, paFig)
            Me.radii = New PointF(1, 1)
            Me.xangle = 0
            Me.flarge = False
            Me.fsweep = False

            _secCenter = New PointF(1, 1)
            _secAngleRad = Pos + AngleToPointf(xangle, radii.X)
            _secHeight = _secCenter + AngleToPointf(DegsToRads(360 - xangle + 90), radii.Y)
        End Sub

        Public Sub New(position As CPointF, radii As PointF, xangle As Single, flarge As Boolean, fsweep As Boolean, ByRef paFig As Figure)
            MyBase.New(PointType.ellipticalArc, position, paFig)
            Me.radii = radii
            Me.xangle = xangle
            Me.flarge = flarge
            Me.fsweep = fsweep

            _secCenter = New PointF(1, 1)
            _secAngleRad = Pos + AngleToPointf(360 - xangle, radii.X)
            _secHeight = _secCenter + AngleToPointf(DegsToRads(360 - xangle + 90), radii.Y)
        End Sub

        Public Overrides Sub SetPrevPPoint(ByRef prev As PathPoint)
            MyBase.SetPrevPPoint(prev)

            _secCenter = Midpoint(PrevPPoint.Pos, Pos)
            _secAngleRad = _secCenter + AngleToPointf(DegsToRads(360 - xangle), radii.X)
            _secHeight = _secCenter + AngleToPointf(DegsToRads(360 - xangle + 90), radii.Y)
        End Sub

        Public Overrides Function Clone(destIndex As Integer, Optional insertInPa As Boolean = True, Optional pa As Figure = Nothing) As PathPoint
            If pa Is Nothing Then pa = Me.parent
            Dim dup As New PPEllipticalArc(CType(Pos, PointF), radii, xangle, flarge, fsweep, pa)
            If insertInPa Then
                pa.Insert(destIndex, dup, False)
                dup.RefreshPrevPPoint()
            End If
            Return dup
        End Function

        Public Overrides Sub Multiply(ByRef ammount As PointF, Optional refMirror As Boolean = True)
            MyBase.Multiply(ammount, refMirror)

            radii.X *= ammount.X
            radii.Y *= ammount.Y

            _secCenter = Midpoint(PrevPPoint.Pos, Pos)
            _secAngleRad = CType(_secCenter, CPointF) + AngleToPointf(DegsToRads(360 - xangle), radii.X)
            _secHeight = CType(_secCenter, CPointF) + AngleToPointf(DegsToRads(360 - xangle + 90), radii.Y)
        End Sub

        Public Overrides Sub Offset(ByRef ammount As PointF, Optional refMirror As Boolean = True)
            MyBase.Offset(ammount, refMirror)

            _secCenter = Midpoint(PrevPPoint.Pos, Pos)
            _secAngleRad = CType(_secCenter, CPointF) + AngleToPointf(DegsToRads(360 - xangle), radii.X)
            _secHeight = CType(_secCenter, CPointF) + AngleToPointf(DegsToRads(360 - xangle + 90), radii.Y)
        End Sub

        'Offsets the internaly selected point (it can be the pos or any other ref point)
        Public Overrides Sub OffsetSelPoint(newPos As PointF, delta As PointF, mods As Long, Optional refMirror As Boolean = True)
            If selPoint Is Nothing Then Return

            If selPoint Is Pos Then
                MyBase.OffsetSelPoint(newPos, delta, mods, refMirror)
            Else
                MyBase.OffsetSelPoint(newPos, delta, Math.Max(0, mods - MoveMods.StickTo45Degs), refMirror)
            End If

            'Dim pivot As PointF = PrevPPoint.Pos
            'If selPoint IsNot Pos Then pivot = _secCenter
            'If (mods And MoveMods.StickTo45Degs) > 0 Then
            '    Dim off As PointF = StickPointToAngles(newPos, pivot, 45) - selPoint
            '    selPoint.X += off.X
            '    selPoint.Y += off.Y
            'End If

            If selPoint Is _secAngleRad Then
                xangle = 360 - LineAngle(_secCenter, _secAngleRad)
                'radii.Y = Math.Max(0.0001, LineLength(PrevPPoint.Pos, Pos) / 2)
                radii.X = Math.Max(0.0001, LineLength(_secAngleRad, _secCenter))
                If (mods And MoveMods.StickTo45Degs) > 0 Then radii.Y = radii.X
            ElseIf selPoint Is _secHeight Then
                radii.Y = LineLength(_secCenter, _secHeight)
                If (mods And MoveMods.StickTo45Degs) > 0 Then radii.X = radii.Y
            Else
                _secCenter = Midpoint(PrevPPoint.Pos, Pos)
                _secAngleRad = CType(_secCenter, CPointF) + AngleToPointf(DegsToRads(360 - xangle), radii.X)
                _secHeight = CType(_secCenter, CPointF) + AngleToPointf(DegsToRads(360 - xangle + 90), radii.Y)
            End If

            Dim tp As PointF = _secCenter + AngleToPointf(DegsToRads(360 - xangle + 90), radii.Y)
            _secHeight.X = tp.X
            _secHeight.Y = tp.Y
            tp = _secCenter + AngleToPointf(DegsToRads(360 - xangle), radii.X)
            _secAngleRad.X = tp.X
            _secAngleRad.Y = tp.Y

            'If refMirror = True Then RefreshMirror()
        End Sub

        Public Overrides Sub FlipSecondary(dir As Orientation)
            'flarge = Not flarge
            fsweep = Not fsweep
            selPoint = _secAngleRad

            Select Case dir
                Case Orientation.Horizontal
                    OffsetSelPoint(New PointF(0, 0), New PointF((_secCenter.X - _secAngleRad.X) * 2, 0), 0, False)
                Case Orientation.Vertical
                    OffsetSelPoint(New PointF(0, 0), New PointF(0, (_secCenter.Y - _secAngleRad.Y) * 2), 0, False)
            End Select
        End Sub

        Public Overrides Function GetStringAbsolute() As String
            Dim cutPos As PointF = CutDecimals(Pos)
            Dim cutRadii As PointF = CutDecimals(radii)

            Return Chr(pointType) & cutRadii.X & "," & cutRadii.Y & " " & CutDecimals(xangle) & " " & Math.Abs(CInt(flarge)) & "," & Math.Abs(CInt(fsweep)) & " " & cutPos.X & "," & cutPos.Y
        End Function

        Public Overrides Function GetStringRelative() As String
            Dim cutRadii As PointF = CutDecimals(radii)

            Dim relPos As PointF = CutDecimals(Pos)
            If PrevPPoint IsNot Nothing Then relPos = CutDecimals(Pos - PrevPPoint.Pos)

            Return Chr(pointType).ToString.ToLower & cutRadii.X & "," & cutRadii.Y & " " & CutDecimals(xangle) & " " & Math.Abs(CInt(flarge)) & "," & Math.Abs(CInt(fsweep)) & " " & relPos.X & "," & relPos.Y
        End Function

        Public Overrides Sub AddToGxPath(ByRef path As Drawing2D.GraphicsPath)
            '_secCenter = Midpoint(PrevPPoint.Pos, Pos)
            'xangle = 360 - LineAngle(_secCenter, _secAngleRad)
            'radii.Y = Math.Max(0.0001, LineLength(prevPPoint.pos, pos) / 2)
            'radii.X = Math.Max(0.0001, LineLength(_secAngleRad, _secCenter))

            'Dim posAngle As Single = (xangle + LineAngle(PrevPPoint.Pos, Pos)) Mod 360

            'If posAngle > 0 AndAlso posAngle < 180 Then
            '    fsweep = False
            'Else
            '    fsweep = True
            'End If

            Dim rads As Single = DegsToRads(xangle)
            Dim r As CPointF = radii
            Dim c As New CPointF
            Dim angles As New CPointF

            EndpointToCenterArcParams(PrevPPoint.Pos, Pos, r, rads, flarge, fsweep, c, angles)
            EllipticArcToBezierCurves(SVG.ApplyZoom(c), SVG.ApplyZoom(r), rads, angles.X, angles.Y, path)
        End Sub

        Public Overrides Sub RefreshSeccondaryData()
            If mirroredPP IsNot Nothing Then
                radii = CType(mirroredPP, PPEllipticalArc).radii
                'xangle = CType(mirroredPP, PPEllipticalArc).xangle
                fsweep = CType(mirroredPP, PPEllipticalArc).fsweep
                flarge = CType(mirroredPP, PPEllipticalArc).flarge

                _secCenter = ReflectPoint(parent.GetMoveto().Pos, CType(mirroredPP, PPEllipticalArc)._secCenter, mirrorOrient)
                selPoint = _secAngleRad
                Dim refl As PointF = ReflectPoint(parent.GetMoveto().Pos, CType(mirroredPP, PPEllipticalArc)._secAngleRad, mirrorOrient)
                OffsetSelPoint(refl, refl - _secAngleRad, 0, False)
            ElseIf PrevPPoint IsNot Nothing Then
                _secCenter = Midpoint(PrevPPoint.Pos, Pos)
                _secAngleRad = _secCenter + AngleToPointf(DegsToRads(360 - xangle), radii.X)
                _secHeight = _secCenter + AngleToPointf(DegsToRads(360 - xangle + 90), radii.Y)
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

            Dim rc As New RectangleF(pos.X + 1, pos.Y + 1, 1, 1)
            If rc.Contains(mpos.X, mpos.Y) Then
                flarge = Not flarge
                selPoint = Nothing
                RefreshMirror()
                Return
            End If
            rc = New RectangleF(pos.X + 3, pos.Y + 1, 1, 1)
            If rc.Contains(mpos.X, mpos.Y) Then
                fsweep = Not fsweep
                selPoint = Nothing
                RefreshMirror()
                Return
            End If

            selPoint = GetClosestPoint(mpos)
        End Sub

        Public Overrides Function GetClosestPoint(ByRef mpos As CPointF) As CPointF
            Return ClosestPointToPos(mpos, {Pos, _secAngleRad, _secHeight}).Value
        End Function

        Public Overrides Sub DrawSecPoints(ByRef graphs As Graphics)
            Static penIn As New Pen(Color.Salmon, 1)
            Static penOut As New Pen(Color.FromArgb(255, 40, 40, 40), 3)
            Static penHIn As New Pen(Color.Cyan, 1)
            Static penHOut As New Pen(Color.FromArgb(255, 40, 40, 40), 3)
            Static drawFont As New Font("Arial", 11, FontStyle.Bold)
            Static brushText As New SolidBrush(Color.Salmon)
            Static formatText As New StringFormat
            formatText.Alignment = StringAlignment.Center

            'Large flag
            Dim rc As New Rectangle(SVG.ApplyZoom(Pos.X + 1), SVG.ApplyZoom(Pos.Y + 1), 10, 10)
            graphs.DrawRectangle(penOut, rc)
            graphs.DrawRectangle(penIn, rc)
            graphs.DrawString("L", drawFont, brushText, New Point(rc.Left + 5, rc.Top + 10), formatText)

            If flarge Then
                graphs.DrawX(penOut, rc)
                graphs.DrawX(penIn, rc)
            End If

            'Sweep Flag
            rc = New Rectangle(SVG.ApplyZoom(Pos.X + 3), SVG.ApplyZoom(Pos.Y + 1), 10, 10)
            graphs.DrawRectangle(penOut, rc)
            graphs.DrawRectangle(penIn, rc)
            graphs.DrawString("S", drawFont, brushText, New Point(rc.Left + 5, rc.Top + 10), formatText)

            If fsweep Then
                graphs.DrawX(penOut, rc)
                graphs.DrawX(penIn, rc)
            End If

            'Width and Angle modifier
            rc = New Rectangle(SVG.ApplyZoom(_secAngleRad).X - 4, SVG.ApplyZoom(_secAngleRad).Y - 4, 8, 8)
            graphs.DrawEllipse(penOut, rc)
            graphs.DrawEllipse(penIn, rc)
            graphs.DrawLine(penIn, SVG.ApplyZoom(_secCenter), SVG.ApplyZoom(_secAngleRad))

            'Height Modifier
            rc = New Rectangle(SVG.ApplyZoom(_secHeight).X - 4, SVG.ApplyZoom(_secHeight).Y - 4, 8, 8)
            graphs.DrawEllipse(penHOut, rc)
            graphs.DrawEllipse(penHIn, rc)
            graphs.DrawLine(penHIn, SVG.ApplyZoom(_secCenter), SVG.ApplyZoom(_secHeight))

            'Dim bnd As RectangleF = GetBounds()
            'graphs.DrawRectangle(penIn, SVG.ApplyZoom(bnd).ToRectangle)
        End Sub

        Public Overrides Function HasSecondaryPoints() As Boolean
            Return True
        End Function

        Public Overrides Function GetBounds() As RectangleF
            If PrevPPoint Is Nothing Then Return New RectangleF(0, 0, 1, 1)
            Return GetEllipticalArcBounds(PrevPPoint.Pos, Pos, radii, xangle, flarge, fsweep)
            'Return LineToSemiCircle(PrevPPoint.Pos, Pos, fsweep)
        End Function

    End Class

    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    Public Class PPQuadraticBezier
        Inherits PathPoint
        Public ctrlPoint As CPointF

        Public Sub New(ByRef position As CPointF, ByRef ctrlPt As CPointF, ByRef paFig As Figure)
            MyBase.New(PointType.quadraticBezierCurve, position, paFig)
            Me.ctrlPoint = ctrlPt
        End Sub

        Public Overrides Function Clone(destIndex As Integer, Optional insertInPa As Boolean = True, Optional pa As Figure = Nothing) As PathPoint
            If pa Is Nothing Then pa = Me.parent
            Dim dup As New PPQuadraticBezier(CType(Me.Pos, PointF), CType(Me.ctrlPoint, PointF), pa)
            If insertInPa Then
                pa.Insert(destIndex, dup, False)
                dup.RefreshPrevPPoint()
            End If
            Return dup
        End Function

        Public Overrides Sub Multiply(ByRef ammount As PointF, Optional refMirror As Boolean = True)
            ctrlPoint.X *= ammount.X
            ctrlPoint.Y *= ammount.Y

            MyBase.Multiply(ammount, refMirror)
        End Sub

        Public Overrides Sub Offset(ByRef ammount As PointF, Optional refMirror As Boolean = True)
            ctrlPoint.X += ammount.X
            ctrlPoint.Y += ammount.Y

            MyBase.Offset(ammount, refMirror)
        End Sub

        Public Overrides Sub FlipSecondary(dir As Orientation)
            Select Case dir
                Case Orientation.Horizontal
                    ctrlPoint.X += (_pos.X - ctrlPoint.X) * 2
                Case Orientation.Vertical
                    ctrlPoint.Y += (_pos.Y - ctrlPoint.Y) * 2
            End Select
        End Sub

        Public Overrides Function GetStringAbsolute() As String
            Dim cutPos As PointF = CutDecimals(Pos)
            Dim cutCtrlPt As PointF = CutDecimals(ctrlPoint)

            Return Chr(pointType) & cutCtrlPt.X & "," & cutCtrlPt.Y & " " & cutPos.X & "," & cutPos.Y
        End Function

        Public Overrides Function GetStringRelative() As String
            Dim relPos As PointF = CutDecimals(Pos)
            If PrevPPoint IsNot Nothing Then relPos = CutDecimals(Pos - PrevPPoint.Pos)
            Dim relCtrlP As PointF = CutDecimals(ctrlPoint - PrevPPoint.Pos)

            Return Chr(pointType).ToString.ToLower & relCtrlP.X & "," & relCtrlP.Y & " " & relPos.X & "," & relPos.Y
        End Function

        Public Overrides Sub AddToGxPath(ByRef path As Drawing2D.GraphicsPath)
            path.AddBezier(SVG.ApplyZoom(PrevPPoint.Pos), SVG.ApplyZoom(ctrlPoint), SVG.ApplyZoom(Pos), SVG.ApplyZoom(Pos))
        End Sub

        Public Overrides Sub DrawSecPoints(ByRef graphs As Graphics)
            Dim rc As New Rectangle(SVG.ApplyZoom(ctrlPoint).X - 4, SVG.ApplyZoom(ctrlPoint).Y - 4, 8, 8)
            Static penIn As New Pen(Color.Pink, 1)
            Static penOut As New Pen(Color.FromArgb(255, 40, 40, 40), 3)

            graphs.DrawEllipse(penOut, rc)
            graphs.DrawEllipse(penIn, rc)
            graphs.DrawLine(penIn, SVG.ApplyZoom(Pos), SVG.ApplyZoom(ctrlPoint))
            graphs.DrawLine(penIn, SVG.ApplyZoom(PrevPPoint.Pos), SVG.ApplyZoom(ctrlPoint))
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
            Return ClosestPointToPos(mpos, {Pos, ctrlPoint}).Value
        End Function

        Public Overrides Sub RefreshSeccondaryData()
            If mirroredPP IsNot Nothing Then
                ctrlPoint = ReflectPoint(parent.GetMoveto().Pos, CType(mirroredPP, PPQuadraticBezier).ctrlPoint, mirrorOrient)
            End If
            MyBase.RefreshSeccondaryData()
        End Sub

        Public Overrides Function HasSecondaryPoints() As Boolean
            Return True
        End Function

        Public Overrides Function GetBounds() As RectangleF
            Return GetCurveBounds(Pos.X, Pos.Y, Pos.X, Pos.Y, ctrlPoint.X, ctrlPoint.Y, PrevPPoint.Pos.X, PrevPPoint.Pos.Y)
        End Function

        Public Overrides Sub RelativeToAbsolute(onlyPos As Boolean)
            Dim prevpp As PathPoint = SVGPath.GetPreviousPPoint(Me)
            If prevpp Is Nothing Then Return
            Pos.X = prevpp.Pos.X + Pos.X
            Pos.Y = prevpp.Pos.Y + Pos.Y
            If onlyPos = False Then
                ctrlPoint.X = prevpp.Pos.X + ctrlPoint.X
                ctrlPoint.Y = prevpp.Pos.Y + ctrlPoint.Y
            End If
        End Sub

        Public Function GetCtrlPtReflexion() As PointF
            Dim reflexion As PointF
            Dim angle As Double = DegsToRads(LineAngle(Pos, ctrlPoint) + 180)
            Dim len As Double = LineLength(Pos, ctrlPoint)
            reflexion.X = Pos.X + Math.Cos(angle) * len
            reflexion.Y = Pos.Y - Math.Sin(angle) * len
            Return reflexion
        End Function

        Public Sub ReflectPrevPP()
            If PrevPPoint Is Nothing Then Return
            If PrevPPoint.pointType = PointType.quadraticBezierCurve Then
                ctrlPoint = CType(PrevPPoint, PPQuadraticBezier).GetCtrlPtReflexion()
            Else
                ctrlPoint = PrevPPoint.Pos
            End If
            RefreshSeccondaryData()
        End Sub

    End Class

    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    Public Class PPCurveto
        Inherits PathPoint
        Public ctrlPoint1 As CPointF
        Public ctrlPoint2 As CPointF

        Public Sub New(ByRef position As CPointF, ByRef ctrlPt1 As CPointF, ByRef ctrlPt2 As CPointF, ByRef paFig As Figure)
            MyBase.New(PointType.curveto, position, paFig)
            Me.ctrlPoint1 = ctrlPt1
            Me.ctrlPoint2 = ctrlPt2
        End Sub

        Public Overrides Function Clone(destIndex As Integer, Optional insertInPa As Boolean = True, Optional pa As Figure = Nothing) As PathPoint
            If pa Is Nothing Then pa = Me.parent
            Dim dup As New PPCurveto(CType(Me.Pos, PointF), CType(Me.ctrlPoint1, PointF), CType(Me.ctrlPoint2, PointF), pa)
            If insertInPa Then
                pa.Insert(destIndex, dup, False)
                dup.RefreshPrevPPoint()
            End If
            Return dup
        End Function

        Public Overrides Sub Multiply(ByRef ammount As PointF, Optional refMirror As Boolean = True)
            ctrlPoint1.X *= ammount.X
            ctrlPoint1.Y *= ammount.Y
            ctrlPoint2.X *= ammount.X
            ctrlPoint2.Y *= ammount.Y

            MyBase.Multiply(ammount, refMirror)
        End Sub

        Public Overrides Sub Offset(ByRef ammount As PointF, Optional refMirror As Boolean = True)
            ctrlPoint1.X += ammount.X
            ctrlPoint1.Y += ammount.Y
            ctrlPoint2.X += ammount.X
            ctrlPoint2.Y += ammount.Y

            MyBase.Offset(ammount, refMirror)
        End Sub

        Public Overrides Sub FlipSecondary(dir As Orientation)
            Select Case dir
                Case Orientation.Horizontal
                    ctrlPoint1.X += (_pos.X - ctrlPoint1.X) * 2
                    ctrlPoint2.X += (_pos.X - ctrlPoint2.X) * 2
                Case Orientation.Vertical
                    ctrlPoint1.Y += (_pos.Y - ctrlPoint1.Y) * 2
                    ctrlPoint2.Y += (_pos.Y - ctrlPoint2.Y) * 2
            End Select
        End Sub

        Public Overrides Function GetStringAbsolute() As String
            Dim cutPos As PointF = CutDecimals(Pos)
            Dim cutCtrlPt1 As PointF = CutDecimals(ctrlPoint1)
            Dim cutCtrlPt2 As PointF = CutDecimals(ctrlPoint2)

            Return Chr(pointType) & cutCtrlPt1.X & "," & cutCtrlPt1.Y & " " &
                    cutCtrlPt2.X & "," & cutCtrlPt2.Y & " " &
                    cutPos.X & "," & cutPos.Y
        End Function

        Public Overrides Function GetStringRelative() As String
            Dim relPos As PointF = CutDecimals(Pos)
            If PrevPPoint IsNot Nothing Then relPos = CutDecimals(Pos - PrevPPoint.Pos)

            Dim relCtrlPt1 As PointF = CutDecimals(ctrlPoint1 - PrevPPoint.Pos)
            Dim relCtrlPt2 As PointF = CutDecimals(ctrlPoint2 - PrevPPoint.Pos)

            Return Chr(pointType).ToString.ToLower & relCtrlPt1.X & "," & relCtrlPt1.Y & " " &
                        relCtrlPt2.X & "," & relCtrlPt2.Y & " " &
                        relPos.X & "," & relPos.Y
        End Function

        Public Overrides Sub AddToGxPath(ByRef path As Drawing2D.GraphicsPath)
            path.AddBezier(SVG.ApplyZoom(PrevPPoint.Pos), SVG.ApplyZoom(ctrlPoint1), SVG.ApplyZoom(ctrlPoint2), SVG.ApplyZoom(Pos))
            'path.AddBezier(prevPoint.pos, prevPoint.pos, refPoint, pos)
        End Sub

        Public Overrides Sub DrawSecPoints(ByRef graphs As Graphics)
            Dim rc1 As New Rectangle(SVG.ApplyZoom(ctrlPoint1).X - 4, SVG.ApplyZoom(ctrlPoint1).Y - 4, 8, 8)
            Dim rc2 As New Rectangle(SVG.ApplyZoom(ctrlPoint2).X - 4, SVG.ApplyZoom(ctrlPoint2).Y - 4, 8, 8)
            Static penIn As New Pen(Color.Pink, 1)
            Static penOut As New Pen(Color.FromArgb(255, 40, 40, 40), 3)

            graphs.DrawEllipse(penOut, rc1)
            graphs.DrawEllipse(penIn, rc1)
            graphs.DrawLine(penIn, SVG.ApplyZoom(Pos), SVG.ApplyZoom(ctrlPoint1))
            graphs.DrawLine(penIn, SVG.ApplyZoom(PrevPPoint.Pos), SVG.ApplyZoom(ctrlPoint1))

            graphs.DrawEllipse(penOut, rc2)
            graphs.DrawEllipse(penIn, rc2)
            graphs.DrawLine(penIn, SVG.ApplyZoom(Pos), SVG.ApplyZoom(ctrlPoint2))
            graphs.DrawLine(penIn, SVG.ApplyZoom(PrevPPoint.Pos), SVG.ApplyZoom(ctrlPoint2))
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
            Return ClosestPointToPos(mpos, {Pos, ctrlPoint1, ctrlPoint2}).Value
        End Function

        Public Overrides Sub RefreshSeccondaryData()
            If mirroredPP IsNot Nothing Then
                ctrlPoint1 = ReflectPoint(parent.GetMoveto().Pos, CType(mirroredPP, PPCurveto).ctrlPoint2, mirrorOrient)
                ctrlPoint2 = ReflectPoint(parent.GetMoveto().Pos, CType(mirroredPP, PPCurveto).ctrlPoint1, mirrorOrient)
            End If
            MyBase.RefreshSeccondaryData()
        End Sub

        Public Overrides Function HasSecondaryPoints() As Boolean
            Return True
        End Function

        Public Overrides Function GetBounds() As RectangleF
            Return GetCurveBounds(Pos.X, Pos.Y, ctrlPoint2.X, ctrlPoint2.Y, ctrlPoint1.X, ctrlPoint1.Y, PrevPPoint.Pos.X, PrevPPoint.Pos.Y)
        End Function

        Public Overrides Sub RelativeToAbsolute(onlyPos As Boolean)
            Dim prevpp As PathPoint = SVGPath.GetPreviousPPoint(Me)
            If prevpp Is Nothing Then Return
            Pos.X = prevpp.Pos.X + Pos.X
            Pos.Y = prevpp.Pos.Y + Pos.Y
            ctrlPoint2.X = prevpp.Pos.X + ctrlPoint2.X
            ctrlPoint2.Y = prevpp.Pos.Y + ctrlPoint2.Y

            If onlyPos = False Then
                ctrlPoint1.X = prevpp.Pos.X + ctrlPoint1.X
                ctrlPoint1.Y = prevpp.Pos.Y + ctrlPoint1.Y
            End If
        End Sub

        Public Function GetCtrlPt2Reflexion() As PointF
            Dim reflexion As PointF
            'Point 1
            Dim angle As Double = DegsToRads(LineAngle(Pos, ctrlPoint2) + 180)
            Dim len As Double = LineLength(Pos, ctrlPoint2)
            reflexion.X = Pos.X + Math.Cos(angle) * len
            reflexion.Y = Pos.Y - Math.Sin(angle) * len
            Return reflexion
        End Function

        Public Sub ReflectPrevPP()
            If PrevPPoint Is Nothing Then Return
            If PrevPPoint.pointType = PointType.curveto Then
                ctrlPoint1 = CType(PrevPPoint, PPCurveto).GetCtrlPt2Reflexion()
            Else
                ctrlPoint1 = PrevPPoint.Pos
            End If
            RefreshSeccondaryData()
        End Sub

    End Class

End Module
