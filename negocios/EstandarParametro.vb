
Public Class EstandarParametro

    Private _capacidad As Integer
    Private _m2in As Double
    Private _sede As Double
    Private _factorCobertura As Double
    Private _costoxm2 As Integer
    Private _tiempoSector As Double
    Public Property tiempoSector() As Double
        Get
            Return _tiempoSector
        End Get
        Set(ByVal value As Double)
            _tiempoSector = value
        End Set
    End Property

    Public Property costoxm2() As Integer
        Get
            Return _costoxm2
        End Get
        Set(ByVal value As Integer)
            _costoxm2 = value
        End Set
    End Property

    Public ReadOnly Property area() As Integer
        Get
            Return (_m2in * _capacidad)
        End Get
    End Property
    Public Property factorCobertura() As Double
        Get
            Return _factorCobertura
        End Get
        Set(ByVal value As Double)
            _factorCobertura = value
        End Set
    End Property
    Public Property capacidad() As Integer
        Get
            Return _capacidad
        End Get
        Set(ByVal value As Integer)
            _capacidad = value
        End Set
    End Property

    Public Property m2In() As Double
        Get
            Return _m2in
        End Get
        Set(ByVal value As Double)
            _m2in = value
        End Set
    End Property

    Public Property sede() As Double
        Get
            Return _sede
        End Get
        Set(ByVal value As Double)
            _sede = value
        End Set
    End Property

    Public ReadOnly Property asociacion() As Double
        Get
            Return (1.0 - _sede)
        End Get
    End Property
End Class
