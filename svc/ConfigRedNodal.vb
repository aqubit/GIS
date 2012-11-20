Imports Microsoft.Win32

Public Class ConfigRedNodal

    Private _geodatabasePath As String
    Private _distanciaPeatonal As Integer
    Private _distanciaBicicleta As Integer
    Private _tiempoVehicular As Integer
    Private _grupoPromedio As Integer
    Private _radioNodo As Integer
    Private _bHierarchy As Boolean
    Private _strImpedancia As String
    Private _templatePath As String
    Private _simbologiaPath As String
    Private _tipoestado As TipoEstado

    Public Property grupoPromedio() As Integer
        Get
            Return _grupoPromedio
        End Get
        Set(ByVal value As Integer)
            _grupoPromedio = value
        End Set
    End Property

    Public Property tipoestado() As TipoEstado
        Get
            Return _tipoestado
        End Get
        Set(ByVal value As TipoEstado)
            _tipoestado = value
        End Set
    End Property
    Public ReadOnly Property geodatabase() As String
        Get
            Return _geodatabasePath
        End Get
    End Property
    Public ReadOnly Property simbologia() As String
        Get
            Return _simbologiaPath
        End Get
    End Property
    Public ReadOnly Property template() As String
        Get
            Return _templatePath
        End Get
    End Property
    Public Property distanciaBicicleta() As Integer
        Get
            Return _distanciaBicicleta
        End Get
        Set(ByVal value As Integer)
            _distanciaBicicleta = value
        End Set
    End Property

    Public Property radioNodo() As Integer
        Get
            Return _radioNodo
        End Get
        Set(ByVal value As Integer)
            _radioNodo = value
        End Set
    End Property

    Public Property distanciaPeatonal() As Integer
        Get
            Return _distanciaPeatonal
        End Get
        Set(ByVal value As Integer)
            _distanciaPeatonal = value
        End Set
    End Property

    Public Property tiempoVehicular() As Integer
        Get
            Return _tiempoVehicular
        End Get
        Set(ByVal value As Integer)
            _tiempoVehicular = value
        End Set
    End Property
    Public Property impedancia() As String
        Get
            Return _strImpedancia
        End Get
        Set(ByVal value As String)
            _strImpedancia = value
        End Set
    End Property
    Public Property hierarchy() As Boolean
        Get
            Return _bHierarchy
        End Get
        Set(ByVal value As Boolean)
            _bHierarchy = value
        End Set
    End Property
    Public ReadOnly Property velocidad_vehiculo() As Integer
        Get
            Return 300 'metros x minuto approx. 18 km/h
        End Get
    End Property

    Public Sub cargar()
        Dim regKey As RegistryKey = Nothing
        Try
            regKey = Registry.LocalMachine.OpenSubKey("Software\RedNodal", False)
            If regKey Is Nothing Then
                Throw New Exception("Software\RedNodal")
            End If
            If regKey.GetValue("Ruta GeoDatabase") Is Nothing Then
                Throw New Exception("Software\RedNodal\Ruta GeoDatabase")
            End If
            If regKey.GetValue("Distancia peatonal") Is Nothing Then
                Throw New Exception("Software\RedNodal\Distancia peatonal")
            End If
            If regKey.GetValue("Tiempo vehicular") Is Nothing Then
                Throw New Exception("Software\RedNodal\Tiempo vehicular")
            End If
            If regKey.GetValue("Distancia bicicleta") Is Nothing Then
                Throw New Exception("Software\RedNodal\Distancia bicicleta")
            End If
            If regKey.GetValue("Radio nodo") Is Nothing Then
                Throw New Exception("Software\RedNodal\Radio nodo")
            End If
            If regKey.GetValue("Template") Is Nothing Then
                Throw New Exception("Software\RedNodal\Template")
            End If
            If regKey.GetValue("Simbologia") Is Nothing Then
                Throw New Exception("Software\RedNodal\Simbologia")
            End If
            If regKey.GetValue("Grupo promedio") Is Nothing Then
                Throw New Exception("Software\RedNodal\Grupo promedio")
            End If

            _geodatabasePath = regKey.GetValue("Ruta GeoDatabase")
            _distanciaPeatonal = regKey.GetValue("Distancia peatonal")
            _tiempoVehicular = regKey.GetValue("Tiempo vehicular")
            _distanciaBicicleta = regKey.GetValue("Distancia bicicleta")
            _radioNodo = regKey.GetValue("Radio nodo")
            _templatePath = regKey.GetValue("Template")
            _simbologiaPath = regKey.GetValue("Simbologia")
            _grupoPromedio = regKey.GetValue("Grupo promedio")
        Catch ex As Exception
            Throw New ConfigException(String.Format(RedNodal.My.Resources.strErrRegistryKeyNotFound, ex.Message), ex)
        Finally
            If regKey IsNot Nothing Then
                regKey.Close()
            End If
        End Try
    End Sub
    Public Sub New()
        _strImpedancia = "Meters"
        _bHierarchy = True
        _tipoestado = RedNodal.TipoEstado.Nuevo
    End Sub
End Class
