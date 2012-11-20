Public Class CancelException
    Inherits Exception

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByVal Message As String, _
     ByVal Inner As Exception)
        MyBase.New(Message, Inner)
    End Sub
End Class
