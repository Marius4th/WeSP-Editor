Public Module Async

    Public Delegate Sub OneParams(p1 As Object)
    Public Delegate Sub TwoParams(p1 As Object, p2 As Object)
    Public Delegate Sub ThreeParams(p1 As Object, p2 As Object, p3 As Object)

    Public Class CAsync
        Private Structure AsyncData
            Public f1 As OneParams
            Public f2 As TwoParams
            Public f3 As ThreeParams
            Public params() As Object

            Public Sub New(f As OneParams, ps As Object())
                f1 = f
                params = ps
            End Sub
            Public Sub New(f As TwoParams, ps As Object())
                f2 = f
                params = ps
            End Sub
            Public Sub New(f As ThreeParams, ps As Object())
                f3 = f
                params = ps
            End Sub
            Public Sub CallFunc()
                Select Case params.Count
                    Case 1
                        f1(params(0))
                    Case 2
                        f2(params(0), params(1))
                    Case 3
                        f3(params(0), params(1), params(2))
                End Select
            End Sub
        End Structure


        Private timer As New Timer
        Private tasksQueue As New Queue(Of AsyncData)
        Private _callers As New Queue(Of Object)

        Public Sub New()
            timer.Interval = 1000
            timer.Enabled = True
            AddHandler timer.Tick, AddressOf TimerTick
        End Sub

        Public Sub AddTask(func As OneParams, params As Object(), ByRef caller As Object)
            If _callers.Contains(caller) Then Return
            tasksQueue.Enqueue(New AsyncData(func, params))
            _callers.Enqueue(caller)
        End Sub
        Public Sub AddTask(func As TwoParams, params As Object(), ByRef caller As Object)
            tasksQueue.Enqueue(New AsyncData(func, params))
            _callers.Enqueue(caller)
        End Sub
        Public Sub AddTask(func As ThreeParams, params As Object(), ByRef caller As Object)
            tasksQueue.Enqueue(New AsyncData(func, params))
            _callers.Enqueue(caller)
        End Sub

        Public Sub AddTaskReseter(func As OneParams, params As Object(), ByRef caller As Object)
            If _callers.Contains(caller) Then
                'Reset the timer
                timer.Enabled = False
                timer.Enabled = True
                Return
            End If
            tasksQueue.Enqueue(New AsyncData(func, params))
            _callers.Enqueue(caller)
        End Sub

        Private Sub TimerTick(sender As Object, e As EventArgs)
            While tasksQueue.Count > 0
                Dim t As AsyncData = tasksQueue.Dequeue()
                t.CallFunc()
                _callers.Dequeue()
            End While
        End Sub

    End Class
End Module
