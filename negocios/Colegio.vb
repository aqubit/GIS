Public Class Colegio
    Inherits Equipamiento
    'Areas original
    Private _areaLudoteca As Integer
    Private _areaLote As Integer
    Private _areaConstruidaPrimerPiso As Integer
    Private _areaTotalConstruida As Integer
    Private _areaAulasPreescolar As Integer
    Private _areaAulasBPMedia As Integer
    Private _areaAulasEspecializadas As Integer
    Private _areaRecreacionPasiva As Integer
    Private _areaRecreacionActiva As Integer
    Private _areaServiciosSanitarios As Integer
    Private _areaServiciosSanitariosDiscapacitados As Integer
    Private _areaBienestarEstudiantil As Integer
    Private _areaGestionPedagogica As Integer
    Private _areaServiciosGenerales As Integer
    Private _areaTeatro As Integer
    Private _idCaracter As String
    'Jornadas
    Private _numMaxJornadas As Integer
    'Cupos
    Private _numCuposActualesPreescolar As Integer
    Private _numCuposActualesBPMedia As Integer
    'Calculados estándar
    Private _numCuposTecnicosDobleJornada As Integer
    Private _numCuposTecnicos As Integer 'Oferta técnica
    Private _numCuposTecnicosPreescolar As Integer
    Private _numCuposTecnicosBPMedia As Integer
    Private _numCuposTecnicosPreMedia As Integer
    Private _numCuposEnDeficitPreescolar As Integer
    Private _numCuposEnDeficitBPMedia As Integer
    Private _numCuposEnDeficitPreMedia As Integer
    'Si el valor de la oferta es + entonces el colegio tiene superávit, 
    'si es negativo tiene déficit
    Private _ofertaAreaLote As Integer
    Private _ofertaAreaConstruidaPrimerPiso As Integer
    Private _ofertaAreaTotalConstruida As Integer
    Private _ofertaAreaAulasPreescolar As Integer
    Private _ofertaAreaAulasBPMedia As Integer
    Private _ofertaAreaAulasEspecializadas As Integer
    Private _ofertaAreaRecreacionPasiva As Integer
    Private _ofertaAreaRecreacionActiva As Integer
    Private _ofertaAreaServiciosSanitarios As Integer
    Private _ofertaAreaServiciosSanitariosDiscapacitados As Integer
    Private _ofertaAreaBienestarEstudiantil As Integer
    Private _ofertaAreaGestionPedagogica As Integer
    Private _ofertaAreaServiciosGenerales As Integer
    Private _ofertaAreaLudoteca As Integer

    Public Sub New()

    End Sub

    Public Sub New(ByRef col As Colegio)
        _nombre = col._nombre
        _id = col._id
        _idPmee = col._idPmee
        _idUPZ = col._idUPZ
        _distancia = col._distancia
        _areaBiblioteca = col._areaBiblioteca
        _areaLaboratorio = col._areaLaboratorio
        _areaTallerArtes = col._areaTallerArtes
        _areaAulaMultimedios = col._areaAulaMultimedios
        _areaAulaMultiple = col._areaAulaMultiple
        _areaAreaLibre = col._areaAreaLibre
        _ofertaBiblioteca = col._ofertaBiblioteca
        _ofertaLaboratorios = col._ofertaLaboratorios
        _ofertaTallerArtes = col._ofertaTallerArtes
        _ofertaAulaMultimedios = col._ofertaAulaMultimedios
        _ofertaAulaMultiple = col._ofertaAulaMultiple
        _ofertaAreaLibre = col._ofertaAreaLibre
        _asociacion = col._asociacion
        _procesado = col._procesado
        _areaLudoteca = col._areaLudoteca
        _areaLote = col._areaLote
        _areaConstruidaPrimerPiso = col._areaConstruidaPrimerPiso
        _areaTotalConstruida = col._areaTotalConstruida
        _areaAulasPreescolar = col._areaAulasPreescolar
        _areaAulasBPMedia = col._areaAulasBPMedia
        _areaAulasEspecializadas = col._areaAulasEspecializadas
        _areaRecreacionPasiva = col._areaRecreacionPasiva
        _areaRecreacionActiva = col._areaRecreacionActiva
        _areaServiciosSanitarios = col._areaServiciosSanitarios
        _areaServiciosSanitariosDiscapacitados = col._areaServiciosSanitariosDiscapacitados
        _areaBienestarEstudiantil = col._areaBienestarEstudiantil
        _areaGestionPedagogica = col._areaGestionPedagogica
        _areaServiciosGenerales = col._areaServiciosGenerales
        _areaTeatro = col._areaTeatro
        _idCaracter = col._idCaracter
        _numMaxJornadas = col._numMaxJornadas
        _numCuposActualesPreescolar = col._numCuposActualesPreescolar
        _numCuposActualesBPMedia = col._numCuposActualesBPMedia
        _numCuposTecnicosDobleJornada = col._numCuposTecnicosDobleJornada
        _numCuposTecnicos = col._numCuposTecnicos
        _numCuposTecnicosPreescolar = col._numCuposTecnicosPreescolar
        _numCuposTecnicosBPMedia = col._numCuposTecnicosBPMedia
        _numCuposTecnicosPreMedia = col._numCuposTecnicosPreMedia
        _numCuposEnDeficitPreescolar = col._numCuposEnDeficitPreescolar
        _numCuposEnDeficitBPMedia = col._numCuposEnDeficitBPMedia
        _numCuposEnDeficitPreMedia = col._numCuposEnDeficitPreMedia
    End Sub

    Public Overrides ReadOnly Property deficitTotalAreaConstruida() As Integer
        Get
            Dim deficit As Integer
            If _ofertaTallerArtes < 0 Then deficit += _ofertaTallerArtes
            If _ofertaLaboratorios < 0 Then deficit += _ofertaLaboratorios
            If _ofertaBiblioteca < 0 Then deficit += _ofertaBiblioteca
            If _ofertaAulaMultimedios < 0 Then deficit += _ofertaAulaMultimedios
            If _ofertaAulaMultiple < 0 Then deficit += _ofertaAulaMultiple
            'If _ofertaAreaLudoteca < 0 Then deficit += _ofertaAreaLudoteca
            'If _ofertaAreaServiciosSanitarios < 0 Then deficit += _ofertaAreaServiciosSanitarios
            'If _ofertaAreaServiciosSanitariosDiscapacitados < 0 Then deficit += _ofertaAreaServiciosSanitariosDiscapacitados
            'If _ofertaAreaServiciosGenerales < 0 Then deficit += _ofertaAreaServiciosGenerales
            'If _ofertaAreaGestionPedagogica < 0 Then deficit += _ofertaAreaGestionPedagogica
            'If _ofertaAreaBienestarEstudiantil < 0 Then deficit += _ofertaAreaBienestarEstudiantil
            Return deficit
        End Get
    End Property
    Public Overrides ReadOnly Property deficitTotalAreaLibre() As Integer
        Get
            If _ofertaAreaLibre < 0 Then
                Return _ofertaAreaLibre
            Else
                Return 0
            End If
        End Get
    End Property
    Public ReadOnly Property deficitTotalAreaLibreEnHectareas() As Integer
        Get
            Return (deficitTotalAreaLibre / 10000)
        End Get
    End Property
    Public ReadOnly Property deficitTotalAreaConstruidaEnHectareas() As Integer
        Get
            Dim tmp As Integer
            tmp = (deficitTotalAreaConstruida / 60000) + deficitTotalAreaConstruida
            Return tmp
        End Get
    End Property

    Public ReadOnly Property deficitTotalAreaSueloEnHectareas() As Integer
        Get
            Return (deficitTotalAreaConstruidaEnHectareas + deficitTotalAreaLibreEnHectareas)
        End Get
    End Property
    Public Property numCuposTecnicos() As Integer
        Get
            Return _numCuposTecnicos
        End Get
        Set(ByVal value As Integer)
            _numCuposTecnicos = value
        End Set
    End Property

    Public Property idCaracter() As Integer
        Get
            Return _idCaracter
        End Get
        Set(ByVal value As Integer)
            _idCaracter = value
        End Set
    End Property

    Public Property areaLudoteca() As Integer
        Get
            Return _areaLudoteca
        End Get
        Set(ByVal value As Integer)
            _areaLudoteca = value
        End Set
    End Property
    Public Property areaTeatro() As Integer
        Get
            Return _areaTeatro
        End Get
        Set(ByVal value As Integer)
            _areaTeatro = value
        End Set
    End Property

    Public Property numCuposEnDeficitPreescolar() As Integer
        Get
            Return _numCuposEnDeficitPreescolar
        End Get
        Set(ByVal value As Integer)
            _numCuposEnDeficitPreescolar = value
        End Set
    End Property
    Public Property numCuposEnDeficitBPMedia() As Integer
        Get
            Return _numCuposEnDeficitBPMedia
        End Get
        Set(ByVal value As Integer)
            _numCuposEnDeficitBPMedia = value
        End Set
    End Property
    Public Property numCuposEnDeficitPreMedia() As Integer
        Get
            Return _numCuposEnDeficitPreMedia
        End Get
        Set(ByVal value As Integer)
            _numCuposEnDeficitPreMedia = value
        End Set
    End Property


    Public Property numCuposActualesPreescolar() As Integer
        Get
            Return _numCuposActualesPreescolar
        End Get
        Set(ByVal value As Integer)
            _numCuposActualesPreescolar = value
        End Set
    End Property
    Public Property numCuposActualesBPMedia() As Integer
        Get
            Return _numCuposActualesBPMedia
        End Get
        Set(ByVal value As Integer)
            _numCuposActualesBPMedia = value
        End Set
    End Property
    Public ReadOnly Property numCuposActualesPreMedia() As Integer
        Get
            Return (_numCuposActualesPreescolar + _numCuposActualesBPMedia)
        End Get
    End Property

    Public Property numCuposTecnicosDobleJornada() As Integer
        Get
            Return _numCuposTecnicosDobleJornada
        End Get
        Set(ByVal value As Integer)
            _numCuposTecnicosDobleJornada = value
        End Set
    End Property

    Public Property numCuposTecnicosPreescolar() As Integer
        Get
            Return _numCuposTecnicosPreescolar
        End Get
        Set(ByVal value As Integer)
            _numCuposTecnicosPreescolar = value
        End Set
    End Property
    Public Property numCuposTecnicosBPMedia() As Integer
        Get
            Return _numCuposTecnicosBPMedia
        End Get
        Set(ByVal value As Integer)
            _numCuposTecnicosBPMedia = value
        End Set
    End Property
    Public Property numCuposTecnicosPreMedia() As Integer
        Get
            Return _numCuposTecnicosPreMedia
        End Get
        Set(ByVal value As Integer)
            _numCuposTecnicosPreMedia = value
        End Set
    End Property


    Public Property numMaxJornadas() As Integer
        Get
            Return _numMaxJornadas
        End Get
        Set(ByVal value As Integer)
            _numMaxJornadas = value
        End Set
    End Property

    Public Property areaGestionPedagogica() As Integer
        Get
            Return _areaGestionPedagogica
        End Get
        Set(ByVal value As Integer)
            _areaGestionPedagogica = value
        End Set
    End Property
    Public Property areaServiciosGenerales() As Integer
        Get
            Return _areaServiciosGenerales
        End Get
        Set(ByVal value As Integer)
            _areaServiciosGenerales = value
        End Set
    End Property

    Public Property areaBienestarEstudiantil() As Integer
        Get
            Return _areaBienestarEstudiantil
        End Get
        Set(ByVal value As Integer)
            _areaBienestarEstudiantil = value
        End Set
    End Property

    Public Property areaServiciosSanitariosDiscapacitados() As Integer
        Get
            Return _areaServiciosSanitariosDiscapacitados
        End Get
        Set(ByVal value As Integer)
            _areaServiciosSanitariosDiscapacitados = value
        End Set
    End Property

    Public Property areaServiciosSanitarios() As Integer
        Get
            Return _areaServiciosSanitarios
        End Get
        Set(ByVal value As Integer)
            _areaServiciosSanitarios = value
        End Set
    End Property

    Public Property areaRecreacionActiva() As Integer
        Get
            Return _areaRecreacionActiva
        End Get
        Set(ByVal value As Integer)
            _areaRecreacionActiva = value
        End Set
    End Property
    Public Property areaRecreacionPasiva() As Integer
        Get
            Return _areaRecreacionPasiva
        End Get
        Set(ByVal value As Integer)
            _areaRecreacionPasiva = value
        End Set
    End Property

    Public Property areaAulasEspecializadas() As Integer
        Get
            Return _areaAulasEspecializadas
        End Get
        Set(ByVal value As Integer)
            _areaAulasEspecializadas = value
        End Set
    End Property

    Public ReadOnly Property areaTotalAulas() As Integer
        Get
            Return (_areaAulasPreescolar + _areaAulasBPMedia)
        End Get
    End Property

    Public Property areaAulasPreescolar() As Integer
        Get
            Return _areaAulasPreescolar
        End Get
        Set(ByVal value As Integer)
            _areaAulasPreescolar = value
        End Set
    End Property
    Public Property areaAulasBPMedia() As Integer
        Get
            Return _areaAulasBPMedia
        End Get
        Set(ByVal value As Integer)
            _areaAulasBPMedia = value
        End Set
    End Property
    Public Property areaTotalConstruida() As Integer
        Get
            Return _areaTotalConstruida
        End Get
        Set(ByVal value As Integer)
            _areaTotalConstruida = value
        End Set
    End Property

    Public Property areaConstruidaPrimerPiso() As Integer
        Get
            Return _areaConstruidaPrimerPiso
        End Get
        Set(ByVal value As Integer)
            _areaConstruidaPrimerPiso = value
        End Set
    End Property

    Public Property areaLote() As Integer
        Get
            Return _areaLote
        End Get
        Set(ByVal value As Integer)
            _areaLote = value
        End Set
    End Property

    Public Property ofertaAreaGestionPedagogica() As Integer
        Get
            Return _ofertaAreaGestionPedagogica
        End Get
        Set(ByVal value As Integer)
            _ofertaAreaGestionPedagogica = value
        End Set
    End Property
    Public Property ofertaAreaServiciosGenerales() As Integer
        Get
            Return _ofertaAreaServiciosGenerales
        End Get
        Set(ByVal value As Integer)
            _ofertaAreaServiciosGenerales = value
        End Set
    End Property

    Public Property ofertaAreaBienestarEstudiantil() As Integer
        Get
            Return _ofertaAreaBienestarEstudiantil
        End Get
        Set(ByVal value As Integer)
            _ofertaAreaBienestarEstudiantil = value
        End Set
    End Property

    Public Property ofertaAreaServiciosSanitariosDiscapacitados() As Integer
        Get
            Return _ofertaAreaServiciosSanitariosDiscapacitados
        End Get
        Set(ByVal value As Integer)
            _ofertaAreaServiciosSanitariosDiscapacitados = value
        End Set
    End Property

    Public Property ofertaAreaServiciosSanitarios() As Integer
        Get
            Return _ofertaAreaServiciosSanitarios
        End Get
        Set(ByVal value As Integer)
            _ofertaAreaServiciosSanitarios = value
        End Set
    End Property

    Public Property ofertaAreaRecreacionActiva() As Integer
        Get
            Return _ofertaAreaRecreacionActiva
        End Get
        Set(ByVal value As Integer)
            _ofertaAreaRecreacionActiva = value
        End Set
    End Property
    Public Property ofertaAreaRecreacionPasiva() As Integer
        Get
            Return _ofertaAreaRecreacionPasiva
        End Get
        Set(ByVal value As Integer)
            _ofertaAreaRecreacionPasiva = value
        End Set
    End Property

    Public Property ofertaAreaAulasEspecializadas() As Integer
        Get
            Return _ofertaAreaAulasEspecializadas
        End Get
        Set(ByVal value As Integer)
            _ofertaAreaAulasEspecializadas = value
        End Set
    End Property

    Public ReadOnly Property ofertaAreaTotalAulas() As Integer
        Get
            Return (_ofertaAreaAulasPreescolar + _ofertaAreaAulasBPMedia)
        End Get
    End Property

    Public Property ofertaAreaAulasPreescolar() As Integer
        Get
            Return _ofertaAreaAulasPreescolar
        End Get
        Set(ByVal value As Integer)
            _ofertaAreaAulasPreescolar = value
        End Set
    End Property
    Public Property ofertaAreaAulasBPMedia() As Integer
        Get
            Return _ofertaAreaAulasBPMedia
        End Get
        Set(ByVal value As Integer)
            _ofertaAreaAulasBPMedia = value
        End Set
    End Property
    Public Property ofertaAreaTotalConstruida() As Integer
        Get
            Return _ofertaAreaTotalConstruida
        End Get
        Set(ByVal value As Integer)
            _ofertaAreaTotalConstruida = value
        End Set
    End Property

    Public Property ofertaAreaConstruidaPrimerPiso() As Integer
        Get
            Return _ofertaAreaConstruidaPrimerPiso
        End Get
        Set(ByVal value As Integer)
            _ofertaAreaConstruidaPrimerPiso = value
        End Set
    End Property
    Public Property ofertaAreaLudoteca() As Integer
        Get
            Return _ofertaAreaLudoteca
        End Get
        Set(ByVal value As Integer)
            _ofertaAreaLudoteca = value
        End Set
    End Property

    Public Property ofertaAreaLote() As Integer
        Get
            Return _ofertaAreaLote
        End Get
        Set(ByVal value As Integer)
            _ofertaAreaLote = value
        End Set
    End Property
    Public ReadOnly Property indiceConstruccion() As Double
        Get
            If (_areaLote <> 0) Then
                Return (_areaTotalConstruida / _areaLote)
            Else
                Return 0
            End If
        End Get
    End Property
    Public ReadOnly Property indiceOcupacion() As Double
        Get
            If (_areaLote <> 0) Then
                Return (_areaConstruidaPrimerPiso / _areaLote)
            Else
                Return 0
            End If
        End Get
    End Property

    Public Overrides Function Copiar() As IAsociable
        Return New Colegio(Me)
    End Function
End Class
