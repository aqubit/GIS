Public Class Estandar
    Private _estandarAulaPreescolar As EstandarParametro
    Private _estandarAulaPrimaria1_3 As EstandarParametro
    Private _estandarAulaPrimaria4_5 As EstandarParametro
    Private _estandarAulaSecundaria As EstandarParametro
    Private _estandarAulaMedia As EstandarParametro
    Private _estandarLudoteca As EstandarParametro
    Private _estandarLaboratorio As EstandarParametro
    Private _estandarTallerArtes As EstandarParametro
    Private _estandarAulaMultimedios As EstandarParametro
    Private _estandarBiblioteca As EstandarParametro
    Private _estandarAulaMultiple As EstandarParametro
    Private _estandarRecActiva As EstandarParametro
    Private _estandarRecPasiva As EstandarParametro
    Private _estandarServiciosGenerales As EstandarParametro
    Private _estandarGestion As EstandarParametro
    Private _estandarGestionPedagogica As EstandarParametro
    Private _estandarBienestar As EstandarParametro
    Private _estandarBanos As EstandarParametro
    Private _estandarBanosDiscapacitados As EstandarParametro
    Private _estandarAreaLibre As EstandarParametro
    Private _estandarAreaLibreNucleo As EstandarParametro
    Private _estandarAreaLote As EstandarParametro
    Private _estandarAreaConstruida1erPiso As EstandarParametro
    Private _estandarAreaConstruidaTotal As EstandarParametro
    Private _grupoPromedio As Integer

    Public Property estandarGestionPedagogica() As EstandarParametro
        Get
            Return _estandarGestionPedagogica
        End Get
        Set(ByVal value As EstandarParametro)
            _estandarGestionPedagogica = value
        End Set
    End Property
    Public Property estandarAulaPreescolar() As EstandarParametro
        Get
            Return _estandarAulaPreescolar
        End Get
        Set(ByVal value As EstandarParametro)
            _estandarAulaPreescolar = value
        End Set
    End Property
    Public Property estandarAulaPrimaria1_3() As EstandarParametro
        Get
            Return _estandarAulaPrimaria1_3
        End Get
        Set(ByVal value As EstandarParametro)
            _estandarAulaPrimaria1_3 = value
        End Set
    End Property
    Public Property estandarAulaPrimaria4_5() As EstandarParametro
        Get
            Return _estandarAulaPrimaria4_5
        End Get
        Set(ByVal value As EstandarParametro)
            _estandarAulaPrimaria4_5 = value
        End Set
    End Property
    Public Property estandarAulaSecundaria() As EstandarParametro
        Get
            Return _estandarAulaSecundaria
        End Get
        Set(ByVal value As EstandarParametro)
            _estandarAulaSecundaria = value
        End Set
    End Property
    Public Property estandarAulaMedia() As EstandarParametro
        Get
            Return _estandarAulaMedia
        End Get
        Set(ByVal value As EstandarParametro)
            _estandarAulaMedia = value
        End Set
    End Property
    Public Property estandarLudoteca() As EstandarParametro
        Get
            Return _estandarLudoteca
        End Get
        Set(ByVal value As EstandarParametro)
            _estandarLudoteca = value
        End Set
    End Property
    Public Property estandarLaboratorio() As EstandarParametro
        Get
            Return _estandarLaboratorio
        End Get
        Set(ByVal value As EstandarParametro)
            _estandarLaboratorio = value
        End Set
    End Property
    Public Property estandarTallerArtes() As EstandarParametro
        Get
            Return _estandarTallerArtes
        End Get
        Set(ByVal value As EstandarParametro)
            _estandarTallerArtes = value
        End Set
    End Property
    Public Property estandarAulaMultimedios() As EstandarParametro
        Get
            Return _estandarAulaMultimedios
        End Get
        Set(ByVal value As EstandarParametro)
            _estandarAulaMultimedios = value
        End Set
    End Property
    Public Property estandarBiblioteca() As EstandarParametro
        Get
            Return _estandarBiblioteca
        End Get
        Set(ByVal value As EstandarParametro)
            _estandarBiblioteca = value
        End Set
    End Property
    Public Property estandarAulaMultiple() As EstandarParametro
        Get
            Return _estandarAulaMultiple
        End Get
        Set(ByVal value As EstandarParametro)
            _estandarAulaMultiple = value
        End Set
    End Property
    Public Property estandarRecActiva() As EstandarParametro
        Get
            Return _estandarRecActiva
        End Get
        Set(ByVal value As EstandarParametro)
            _estandarRecActiva = value
        End Set
    End Property
    Public Property estandarRecPasiva() As EstandarParametro
        Get
            Return _estandarRecPasiva
        End Get
        Set(ByVal value As EstandarParametro)
            _estandarRecPasiva = value
        End Set
    End Property
    Public Property estandarServiciosGenerales() As EstandarParametro
        Get
            Return _estandarServiciosGenerales
        End Get
        Set(ByVal value As EstandarParametro)
            _estandarServiciosGenerales = value
        End Set
    End Property
    Public Property estandarGestion() As EstandarParametro
        Get
            Return _estandarGestion
        End Get
        Set(ByVal value As EstandarParametro)
            _estandarGestion = value
        End Set
    End Property
    Public Property estandarBienestar() As EstandarParametro
        Get
            Return _estandarBienestar
        End Get
        Set(ByVal value As EstandarParametro)
            _estandarBienestar = value
        End Set
    End Property
    Public Property estandarBanos() As EstandarParametro
        Get
            Return _estandarBanos
        End Get
        Set(ByVal value As EstandarParametro)
            _estandarBanos = value
        End Set
    End Property
    Public Property estandarBanosDiscapacitados() As EstandarParametro
        Get
            Return _estandarBanosDiscapacitados
        End Get
        Set(ByVal value As EstandarParametro)
            _estandarBanosDiscapacitados = value
        End Set
    End Property
    Public Property estandarAreaLibre() As EstandarParametro
        Get
            Return _estandarAreaLibre
        End Get
        Set(ByVal value As EstandarParametro)
            _estandarAreaLibre = value
        End Set
    End Property
    Public Property estandarAreaLibreNucleo() As EstandarParametro
        Get
            Return _estandarAreaLibreNucleo
        End Get
        Set(ByVal value As EstandarParametro)
            _estandarAreaLibreNucleo = value
        End Set
    End Property
    Public Property estandarAreaLote() As EstandarParametro
        Get
            Return _estandarAreaLote
        End Get
        Set(ByVal value As EstandarParametro)
            _estandarAreaLote = value
        End Set
    End Property
    Public Property estandarAreaConstruida1erPiso() As EstandarParametro
        Get
            Return _estandarAreaConstruida1erPiso
        End Get
        Set(ByVal value As EstandarParametro)
            _estandarAreaConstruida1erPiso = value
        End Set
    End Property
    Public Property estandarAreaConstruidaTotal() As EstandarParametro
        Get
            Return _estandarAreaConstruidaTotal
        End Get
        Set(ByVal value As EstandarParametro)
            _estandarAreaConstruidaTotal = value
        End Set
    End Property

    Public Sub aplicarEstandar(ByRef el As IAsociable)
        Dim demanda As Integer

        If TypeOf (el) Is Colegio Then
            Dim col As Colegio = el
            With col
                'Cálculo de cupos
                .numCuposTecnicosPreescolar = CType(.areaAulasPreescolar / _estandarAulaPreescolar.m2In, Integer)
                .numCuposTecnicosBPMedia = CType(.areaAulasBPMedia / _estandarAulaSecundaria.m2In, Integer)
                .numCuposTecnicosPreMedia = .numCuposTecnicosPreescolar + .numCuposTecnicosBPMedia
                .numCuposTecnicosDobleJornada = .numCuposTecnicosPreMedia * .numMaxJornadas
                ' Colegio público
                If .idCaracter = 0 Then
                    .numCuposTecnicos = .numCuposTecnicosDobleJornada
                Else 'Colegio privado
                    .numCuposTecnicos = .numCuposTecnicosPreMedia + .numCuposActualesPreMedia
                End If
                .numCuposEnDeficitPreescolar = .numCuposTecnicosPreescolar - .numCuposActualesPreescolar
                .numCuposEnDeficitBPMedia = .numCuposTecnicosBPMedia - .numCuposActualesBPMedia
                .numCuposEnDeficitPreMedia = .numCuposEnDeficitPreescolar + .numCuposEnDeficitBPMedia
                'Ludoteca
                demanda = CType((.numCuposTecnicosPreescolar / _estandarLudoteca.factorCobertura) * _estandarLudoteca.area, Integer)
                .ofertaAreaLudoteca = .areaLudoteca - demanda
                'Laboratorios
                demanda = CType((.numCuposTecnicosBPMedia / _estandarLaboratorio.factorCobertura) * _estandarLaboratorio.area, Integer)
                .ofertaLaboratorio = .areaLaboratorio + .areaAulasEspecializadas - demanda
                'Taller de artes
                demanda = CType((.numCuposTecnicosPreMedia / _estandarTallerArtes.factorCobertura) * _estandarTallerArtes.area, Integer)
                .ofertaTallerArtes = .areaTallerArtes - demanda
                'Aula multimedios
                demanda = CType((.numCuposTecnicosPreMedia / _estandarAulaMultimedios.factorCobertura) * _estandarAulaMultimedios.area, Integer)
                .ofertaAulaMultimedios = .areaAulaMultimedios - demanda
                'Aula multiple
                demanda = CType(.numCuposTecnicosPreMedia * _estandarAulaMultiple.factorCobertura * _estandarAulaMultiple.m2In, Integer)
                .ofertaAulaMultiple = .areaAulaMultiple + .areaTeatro - demanda
                'Biblioteca
                demanda = CType(.numCuposTecnicosPreMedia * _estandarBiblioteca.factorCobertura * _estandarBiblioteca.m2In, Integer)
                .ofertaBiblioteca = .areaBiblioteca - demanda
                'Recreacion activa
                demanda = CType(.numCuposTecnicosPreMedia * _estandarRecActiva.m2In, Integer)
                .ofertaAreaRecreacionActiva = .areaRecreacionActiva - demanda
                'Recreacion pasiva
                demanda = CType(.numCuposTecnicosPreMedia * _estandarRecPasiva.m2In, Integer)
                .ofertaAreaRecreacionPasiva = .areaRecreacionPasiva - demanda
                'Servicios generales
                demanda = CType(.numCuposTecnicosPreMedia * _estandarServiciosGenerales.m2In, Integer)
                .ofertaAreaServiciosGenerales = .areaServiciosGenerales - demanda
                'Gestión pedagógica
                demanda = CType(.numCuposTecnicosPreMedia * _estandarGestionPedagogica.m2In, Integer)
                .ofertaAreaGestionPedagogica = .areaGestionPedagogica - demanda
                'Bienestar
                demanda = CType(.numCuposTecnicosPreMedia * _estandarBienestar.m2In, Integer)
                .ofertaAreaBienestarEstudiantil = .areaBienestarEstudiantil - demanda
                'Baños
                demanda = CType(.numCuposTecnicosPreMedia * _estandarBanos.m2In, Integer)
                .ofertaAreaServiciosSanitarios = .areaServiciosSanitarios - demanda
                'Baños discapacitados
                demanda = CType(.numCuposTecnicosPreMedia * _estandarBanosDiscapacitados.factorCobertura * _estandarBanosDiscapacitados.m2In, Integer)
                .ofertaAreaServiciosSanitariosDiscapacitados = .areaServiciosSanitariosDiscapacitados - demanda
                'Area libre
                demanda = CType(.numCuposTecnicosPreMedia * _estandarAreaLibre.m2In, Integer)
                .ofertaAreaLibre = .areaAreaLibre - demanda
                'Area lote
                demanda = CType(.numCuposTecnicosPreMedia * _estandarAreaLote.m2In, Integer)
                .ofertaAreaLote = .areaLote - demanda
                'Area primer piso
                demanda = CType(.numCuposTecnicosPreMedia * _estandarAreaConstruida1erPiso.m2In, Integer)
                .ofertaAreaConstruidaPrimerPiso = .areaConstruidaPrimerPiso - demanda
                'Area total construida
                demanda = CType(.numCuposTecnicosPreMedia * _estandarAreaConstruidaTotal.m2In, Integer)
                .ofertaAreaTotalConstruida = .areaTotalConstruida - demanda
            End With
        ElseIf TypeOf (el) Is Equipamiento Then
            Dim eqo As Equipamiento = el
            With eqo
                'Tiempo que los eqos de otros sectores comparten con el sector educativo
                .ofertaAreaLibre = .areaAreaLibre * 0.5
                .ofertaLaboratorio = .areaLaboratorio
                .ofertaTallerArtes = .areaTallerArtes * 0.1
                .ofertaAulaMultimedios = .areaAulaMultimedios * 0.1
                .ofertaAulaMultiple = .areaAulaMultiple * 0.25
                .ofertaBiblioteca = .areaBiblioteca * 0.25
            End With
        ElseIf TypeOf (el) Is Asociacion Then
            For Each subEl As IAsociable In el.elementos
                aplicarEstandar(subEl)
            Next
        Else
            'Do nothing
        End If
    End Sub
    Public Sub doObtenerDeficitAsociacion(ByRef el As IAsociable)
        If TypeOf (el) Is Colegio Then
            Dim col As Colegio = el
            With col
                If .ofertaLaboratorio < 0 Then
                    .ofertaLaboratorio *= _estandarLaboratorio.asociacion
                Else
                    .ofertaLaboratorio *= _estandarLaboratorio.tiempoSector
                End If
                If .ofertaTallerArtes < 0 Then
                    .ofertaTallerArtes *= _estandarTallerArtes.asociacion
                Else
                    .ofertaTallerArtes *= _estandarTallerArtes.tiempoSector
                End If
                If .ofertaAulaMultimedios < 0 Then
                    .ofertaAulaMultimedios *= _estandarAulaMultimedios.asociacion
                Else
                    .ofertaAulaMultimedios *= _estandarAulaMultimedios.tiempoSector
                End If
                If .ofertaAulaMultiple < 0 Then
                    .ofertaAulaMultiple *= _estandarAulaMultiple.asociacion
                Else
                    .ofertaAulaMultiple *= _estandarAulaMultiple.tiempoSector
                End If
                If .ofertaBiblioteca < 0 Then
                    .ofertaBiblioteca *= _estandarBiblioteca.asociacion
                Else
                    .ofertaBiblioteca *= _estandarBiblioteca.tiempoSector
                End If
                If .ofertaAreaLibre < 0 Then
                    .ofertaAreaLibre *= _estandarAreaLibre.asociacion
                Else
                    .ofertaAreaLibre *= _estandarAreaLibre.tiempoSector
                End If
            End With
        ElseIf TypeOf (el) Is Nodo Then
            For Each subEl As IAsociable In el.elementos
                doObtenerDeficitAsociacion(subEl)
            Next
        Else
            'Do nothing
        End If
    End Sub

    Public Sub New(ByRef grupoPromedio As Integer)
        _grupoPromedio = grupoPromedio
    End Sub
End Class
