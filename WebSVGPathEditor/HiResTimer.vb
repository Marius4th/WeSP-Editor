Public Class HiResTimer
    Private isPerfCounterSupported As Boolean = False
    Private timerFrequency As Int64 = 0
    Private startTime As Int64 = 0

    ' Windows CE native library with QueryPerformanceCounter().
    Private Const [lib] As String = "coredll.dll"

    Declare Function QueryPerformanceCounter Lib "Kernel32" (ByRef X As Long) As Short
    Declare Function QueryPerformanceFrequency Lib "Kernel32" (ByRef X As Long) As Short

    Public Sub New()
        ' Query the high-resolution timer only if it is supported.
        ' A returned frequency of 1000 typically indicates that it is not
        ' supported and is emulated by the OS using the same value that is
        ' returned by Environment.TickCount.
        ' A return value of 0 indicates that the performance counter is
        ' not supported.
        Dim returnVal As Integer = QueryPerformanceFrequency(timerFrequency)

        If returnVal <> 0 AndAlso timerFrequency <> 1000 Then
            ' The performance counter is supported.
            isPerfCounterSupported = True
        Else
            ' The performance counter is not supported. Use
            ' Environment.TickCount instead.
            timerFrequency = 1000
        End If

    End Sub

    Public ReadOnly Property Frequency() As Int64
        Get
            Return timerFrequency
        End Get
    End Property

    Public ReadOnly Property Value() As Int64
        Get
            Dim tickCount As Int64 = 0

            If isPerfCounterSupported Then
                ' Get the value here if the counter is supported.
                QueryPerformanceCounter(tickCount)
                Return tickCount
            Else
                ' Otherwise, use Environment.TickCount
                Return CType(Environment.TickCount, Int64)
            End If
        End Get
    End Property

    Public ReadOnly Property ElapsedTime() As Int64
        Get
            Return Value - startTime
        End Get
    End Property

    Public Sub Start()
        startTime = Value
    End Sub

End Class
