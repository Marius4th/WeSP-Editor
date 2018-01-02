Imports System.Runtime.CompilerServices

Public Module SomeClasses

    Public Class TriValue(Of T1, T2, T3)
        Public val1 As T1
        Public val2 As T2
        Public val3 As T3

        Public Sub New(v1 As T1, v2 As T2, v3 As T3)
            val1 = v1
            val2 = v2
            val3 = v3
        End Sub
    End Class

    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    Public Class BiValue(Of T1, T2)
        Public val1 As T1
        Public val2 As T2

        Public Sub New(v1 As T1, v2 As T2)
            val1 = v1
            val2 = v2
        End Sub
    End Class

    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    Public Class CPoint
        Inherits Object

        Private _x, _y As Integer

        Public Property X() As Integer
            Get
                Return _x
            End Get
            Set(ByVal value As Integer)
                _x = value
            End Set
        End Property

        Public Property Y() As Integer
            Get
                Return _y
            End Get
            Set(ByVal value As Integer)
                _y = value
            End Set
        End Property

        Public Sub New()
            _x = 0
            _y = 0
        End Sub
        Public Sub New(dw As Integer)
            _x = IntLoWord(dw)
            _y = IntHiWord(dw)
        End Sub
        Public Sub New(sz As Size)
            _x = sz.Width
            _y = sz.Height
        End Sub
        Public Sub New(x As Integer, y As Integer)
            _x = x
            _y = y
        End Sub

        Public Function ToPointF() As PointF
            Return New PointF(X, Y)
        End Function

        Public Shared Widening Operator CType(ByVal p As Point) As CPoint
            Return New CPoint(p.X, p.Y)
        End Operator
        Public Shared Narrowing Operator CType(ByVal cp As CPoint) As Point
            Return New Point(cp.X, cp.Y)
        End Operator

        Public Shared Operator =(ByVal cp1 As CPoint, ByVal cp2 As CPoint) As Boolean
            'If CType(cp2, Object) = Nothing Then
            '    If CType(cp1, Object) = Nothing Then
            '        Return True
            '    Else
            '        Return False
            '    End If
            'End If
            Return CType(cp1, Point) = CType(cp2, Point)
        End Operator
        Public Shared Operator <>(ByVal cp1 As CPoint, ByVal cp2 As CPoint) As Boolean
            'If CType(cp2, Object) = Nothing AndAlso Not CType(cp1, Object) = Nothing Then Return True
            Return CType(cp1, Point) <> CType(cp2, Point)
        End Operator

        Public Shared Operator -(ByVal cp1 As CPoint, ByVal cp2 As CPoint) As CPoint
            'If CType(cp2, Object) = Nothing OrElse CType(cp1, Object) = Nothing Then Return False
            Return New CPoint(cp1.X - cp2.X, cp1.Y - cp2.Y)
        End Operator

        Public Shared Operator -(ByVal cp1 As CPoint, ByVal cp2 As Point) As CPoint
            'If CType(cp2, Object) = Nothing OrElse CType(cp1, Object) = Nothing Then Return False
            Return New CPoint(cp1.X - cp2.X, cp1.Y - cp2.Y)
        End Operator

        Public Shared Operator *(ByVal cp1 As CPoint, ByVal val As Integer) As CPoint
            'If CType(cp2, Object) = Nothing OrElse CType(cp1, Object) = Nothing Then Return False
            Return New CPoint(cp1.X * val, cp1.Y * val)
        End Operator
        Public Shared Operator *(ByVal cp1 As CPoint, ByVal val As Double) As CPoint
            'If CType(cp2, Object) = Nothing OrElse CType(cp1, Object) = Nothing Then Return False
            Return New CPoint(cp1.X * val, cp1.Y * val)
        End Operator

        Public Shared Operator /(ByVal cp1 As CPoint, ByVal val As Integer) As CPoint
            'If CType(cp2, Object) = Nothing OrElse CType(cp1, Object) = Nothing Then Return False
            Return New CPoint(cp1.X / val, cp1.Y / val)
        End Operator
        Public Shared Operator /(ByVal cp1 As CPoint, ByVal val As Double) As CPoint
            'If CType(cp2, Object) = Nothing OrElse CType(cp1, Object) = Nothing Then Return False
            Return New CPoint(cp1.X / val, cp1.Y / val)
        End Operator

    End Class

    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    Public Class CPointF
        Inherits Object

        Private _x, _y As Single

        Public Property X() As Single
            Get
                Return _x
            End Get
            Set(ByVal value As Single)
                _x = value
            End Set
        End Property

        Public Property Y() As Single
            Get
                Return _y
            End Get
            Set(ByVal value As Single)
                _y = value
            End Set
        End Property

        Public Sub New()
            _x = 0
            _y = 0
        End Sub
        Public Sub New(x As Single, y As Single)
            _x = x
            _y = y
        End Sub
        Public Sub New(ByVal p As PointF)
            _x = p.X
            _y = p.Y
        End Sub

        Public Function ToPoint() As Point
            Return New Point(X, Y)
        End Function

        Public Shared Widening Operator CType(ByVal p As PointF) As CPointF
            Return New CPointF(p.X, p.Y)
        End Operator
        Public Shared Narrowing Operator CType(ByVal cp As CPointF) As PointF
            Return New PointF(cp.X, cp.Y)
        End Operator

        Public Shared Operator =(ByVal cp1 As CPointF, ByVal cp2 As CPointF) As Boolean
            'If CType(cp2, Object) = Nothing OrElse CType(cp1, Object) = Nothing Then Return False
            Return CType(cp1, PointF) = CType(cp2, PointF)
        End Operator

        Public Shared Operator <>(ByVal cp1 As CPointF, ByVal cp2 As CPointF) As Boolean
            'If CType(cp2, Object) = Nothing AndAlso Not CType(cp1, Object) = Nothing Then Return True
            Return CType(cp1, PointF) <> CType(cp2, PointF)
        End Operator

        Public Shared Operator -(ByVal cp1 As CPointF, ByVal cp2 As CPointF) As CPointF
            'If CType(cp2, Object) = Nothing OrElse CType(cp1, Object) = Nothing Then Return False
            Return New CPointF(cp1.X - cp2.X, cp1.Y - cp2.Y)
        End Operator

        Public Shared Operator -(ByVal cp1 As CPointF, ByVal cp2 As PointF) As CPointF
            'If CType(cp2, Object) = Nothing OrElse CType(cp1, Object) = Nothing Then Return False
            Return New CPointF(cp1.X - cp2.X, cp1.Y - cp2.Y)
        End Operator

        Public Shared Operator *(ByVal cp1 As CPointF, ByVal val As Integer) As CPointF
            'If CType(cp2, Object) = Nothing OrElse CType(cp1, Object) = Nothing Then Return False
            Return New CPointF(cp1.X * val, cp1.Y * val)
        End Operator
        Public Shared Operator *(ByVal cp1 As CPointF, ByVal val As Double) As CPointF
            'If CType(cp2, Object) = Nothing OrElse CType(cp1, Object) = Nothing Then Return False
            Return New CPointF(cp1.X * val, cp1.Y * val)
        End Operator

        Public Shared Operator /(ByVal cp1 As CPointF, ByVal val As Integer) As CPointF
            'If CType(cp2, Object) = Nothing OrElse CType(cp1, Object) = Nothing Then Return False
            Return New CPointF(cp1.X / val, cp1.Y / val)
        End Operator
        Public Shared Operator /(ByVal cp1 As CPointF, ByVal val As Double) As CPointF
            'If CType(cp2, Object) = Nothing OrElse CType(cp1, Object) = Nothing Then Return False
            Return New CPointF(cp1.X / val, cp1.Y / val)
        End Operator

    End Class

    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    Public Class ListWithEvents(Of T)
        Implements IEnumerable(Of T)

        '----------------------------------------------------------------------------------------------------------------------
        Private data As New List(Of T)

        Public Sub New()

        End Sub

        '----------------------------------------------------------------------------------------------------------------------
        Public Iterator Function GetEnumerator() As IEnumerator(Of T) Implements IEnumerable(Of T).GetEnumerator
            For Each p As T In data
                Yield p
            Next
        End Function

        Private Iterator Function GetEnumerator1() As IEnumerator _
        Implements IEnumerable.GetEnumerator
            Yield GetEnumerator()
        End Function

        Public Function Point(index As Integer)
            Return data(index)
        End Function

        Public Function FirstPPoint() As T
            If data.Count <= 0 Then Return Nothing
            Return data(0)
        End Function

        Public Function LastPPoint() As T
            If data.Count <= 0 Then Return Nothing
            Return data(data.Count - 1)
        End Function

        Public Function IndexOf(ByRef pp As T) As Integer
            Return data.IndexOf(pp)
        End Function

        Public Sub Add(ByRef item As T)
            data.Add(item)
            RaiseEvent OnAdd(Me, item)
        End Sub

        Public Sub Insert(index As Integer, ByRef item As T)
            data.Insert(index, item)
            RaiseEvent OnAdd(Me, item)
        End Sub

        Public Sub RemoveAt(index As Integer)
            data.RemoveAt(index)
            RaiseEvent OnRemoveAt(Me, index, 1)
        End Sub

        Public Sub Remove(ByRef pp As T)
            data.Remove(pp)
            RaiseEvent OnRemove(Me, pp)
        End Sub

        Public Sub RemoveRange(index As Integer, count As Integer)
            data.RemoveRange(index, count)
            RaiseEvent OnRemoveAt(Me, index, count)
        End Sub

        Public Function Count() As Integer
            Return data.Count
        End Function

        Public Sub Clear()
            data.Clear()
            RaiseEvent OnClear(Me)
        End Sub

        '----------------------------------------------------------------------------------------------------------------------
        Public Event OnAdd(sender As ListWithEvents(Of T), d As T)
        Public Event OnRemove(sender As ListWithEvents(Of T), d As T)
        Public Event OnRemoveAt(sender As ListWithEvents(Of T), start As Integer, count As Integer)
        Public Event OnClear(sender As ListWithEvents(Of T))
    End Class

    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++


    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++



End Module
