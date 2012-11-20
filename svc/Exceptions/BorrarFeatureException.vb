Public Class BorrarFeatureException
    Inherits Exception

    Public Sub New(ByVal Message As String)
        MyBase.New(Message)
    End Sub

    Public Sub New(ByVal Message As String, _
     ByVal Inner As Exception)
        MyBase.New(Message, Inner)
    End Sub
End Class
