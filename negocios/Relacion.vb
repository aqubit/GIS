Public Class Relacion

    Private _cantidad As Integer
    Private _ambiente As String
    Private _tipo As TipoAsociacion

    Public Property cantidad() As Integer
        Get
            Return _cantidad
        End Get
        Set(ByVal value As Integer)
            _cantidad = value
        End Set
    End Property

    Public Property ambiente() As String
        Get
            Return _ambiente
        End Get
        Set(ByVal value As String)
            _ambiente = value
        End Set
    End Property

    Public Property tipo() As TipoAsociacion
        Get
            Return _tipo
        End Get
        Set(ByVal value As TipoAsociacion)
            _tipo = value
        End Set
    End Property
End Class
