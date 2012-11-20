Imports System
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Reflection
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Framework


Public Enum TipoAsociacion
    Peatonal = 0
    Vehicular = 1
End Enum

Public Class RedNodalAlg

    Private _iAno As Integer
    Private _iAfe As Integer
    Private _nodos As List(Of IAsociable)
    Private _colegios As List(Of IAsociable)
    Private _eqos As List(Of IAsociable)
    Private _noasociados As List(Of IAsociable)
    Private _redPeatonal As Asociacion
    Private _aies As Asociacion

    Private _bEstandarAplicado As Boolean
    Private _bRedNodalConstruida As Boolean
    Private _bTejidoConstruido As Boolean
    Private _estandar As Estandar
    Private _config As ConfigRedNodal
    Private _geoAdapter As GeoAdapter
    Private _ambientesEnRangoPeatonal() As String = {"ofertaBiblioteca", "ofertaAreaLibre", "ofertaAulaMultiple", _
                                                "ofertaAulaMultimedios", "ofertaTallerArtes", "ofertaLaboratorio"}
    Private _ambientesEnRangoVehicular() As String = {"ofertaBiblioteca", "ofertaAreaLibre", "ofertaAulaMultiple"}
    Private _label As New Dictionary(Of String, String)
    Private _ambienteEnAnalisis As String

    Sub New(ByRef dataProvider As DataProvider, ByRef config As ConfigRedNodal)
        _config = config
        _geoAdapter = New GeoAdapter(dataProvider)
        _estandar = _geoAdapter.getEstandar(_config.grupoPromedio)
        _label.Add(_ambientesEnRangoPeatonal(0), "Biblioteca")
        _label.Add(_ambientesEnRangoPeatonal(1), "Área libre")
        _label.Add(_ambientesEnRangoPeatonal(2), "Aula múltiple")
        _label.Add(_ambientesEnRangoPeatonal(3), "Aula multimedios")
        _label.Add(_ambientesEnRangoPeatonal(4), "Taller de artes")
        _label.Add(_ambientesEnRangoPeatonal(5), "Laboratorio")
        _ambienteEnAnalisis = ""
    End Sub
    Public ReadOnly Property geoadapter() As GeoAdapter
        Get
            Return _geoAdapter
        End Get
    End Property
    Public ReadOnly Property estandar() As Estandar
        Get
            Return _estandar
        End Get
    End Property
    Public ReadOnly Property config() As ConfigRedNodal
        Get
            Return _config
        End Get
    End Property
    Public ReadOnly Property ano() As Integer
        Get
            Return _iAno
        End Get
    End Property
    Public ReadOnly Property afe() As Integer
        Get
            Return _iAfe
        End Get
    End Property
    Public ReadOnly Property isEstandarAplicado() As Boolean
        Get
            Return _bEstandarAplicado
        End Get
    End Property
    Public ReadOnly Property isRedNodalConstruida() As Boolean
        Get
            Return _bRedNodalConstruida
        End Get
    End Property
    Public ReadOnly Property idTejidoConstruido() As Boolean
        Get
            Return _bTejidoConstruido
        End Get
    End Property

    Public ReadOnly Property aies() As Asociacion
        Get
            Return _aies
        End Get
    End Property
    Public ReadOnly Property asociaciones() As Asociacion
        Get
            Return _redPeatonal
        End Get
    End Property
    Public ReadOnly Property noasociados() As List(Of IAsociable)
        Get
            Return _noasociados
        End Get
    End Property

    '*********************************************************************************************
    ' Precondiciones:
    ' 1. Se ha aplicado el estándar a cada uno de los colegios de la lista
    ' 2. Lista de colegios ordenada por ID
    '
    '
    '
    '*********************************************************************************************

    Public Sub evaluarDemandaOferta()
        '1. Aplicar el estándar a los colegios
        For Each col As IAsociable In _colegios
            _estandar.aplicarEstandar(col)
            col.saveState()
            _estandar.doObtenerDeficitAsociacion(col)
        Next col
        '2. Calcular cuánto pueden compartir los eqos?
        For Each eqo As IAsociable In _eqos
            _estandar.aplicarEstandar(eqo)
            eqo.saveState()
            _estandar.doObtenerDeficitAsociacion(eqo)
        Next eqo
        '3. Aplicar el estándar a los nodos
        For Each nodo As IAsociable In _nodos
            _estandar.aplicarEstandar(nodo)
            _estandar.doObtenerDeficitAsociacion(nodo)
        Next nodo
        _bEstandarAplicado = True
        _bRedNodalConstruida = False
        _bTejidoConstruido = False
    End Sub

    Public Sub construirTejidoEducativo( _
        ByRef pProDlg As IProgressDialog2, _
        ByRef pTrkCan As ITrackCancel _
   )
        '1. Aplicar estándar sino se había calculado
        If Not _bEstandarAplicado Then
            evaluarDemandaOferta()
        End If
        '2. Construir el tejido
        _redPeatonal = New Asociacion
        _aies = New Asociacion
        _bRedNodalConstruida = False
        _bTejidoConstruido = False
        construirRed(_colegios, "colegios", pProDlg, pTrkCan)
        _bTejidoConstruido = True
    End Sub
    Public Sub construirRedNodal( _
        ByRef pProDlg As IProgressDialog2, _
        ByRef pTrkCan As ITrackCancel _
        )
        '1. Aplicar estándar sino se había calculado
        If Not _bEstandarAplicado Then
            evaluarDemandaOferta()
        End If
        _redPeatonal = New Asociacion
        _aies = New Asociacion
        _bRedNodalConstruida = False
        _bTejidoConstruido = False
        '2. Balancear nodos
        global_redNodal.balancearNodos( _
            pProDlg, _
            pTrkCan)
        '4. Construir red nodal
        construirRed(_nodos, "nodos", pProDlg, pTrkCan)
        _bRedNodalConstruida = True
    End Sub

    Public Sub generarNodosFuncionales( _
        ByRef pProDlg As IProgressDialog2 _
    )
        'Progress dialog
        Dim pStepPro As IStepProgressor
        pStepPro = pProDlg
        pStepPro.MinRange = 0
        pStepPro.StepValue = 1
        _nodos = New List(Of IAsociable)
        _geoAdapter.generarNodosFuncionales(_iAfe, _iAno, _config.radioNodo, _config.tipoestado, pProDlg)
        _geoAdapter.getNodos(_nodos, afe, ano, _eqos, _colegios)
        _nodos.Sort()
        _bEstandarAplicado = False
    End Sub


    Public Sub balancearNodos( _
        ByRef pProDlg As IProgressDialog2, _
        ByRef pTrkCan As ITrackCancel _
    )
        'Distancia peatonal
        Dim distPeatonal As Integer = _config.distanciaPeatonal
        'Progress dialog
        Dim pStepPro As IStepProgressor
        Dim bCancel As Boolean
        'Objeto para ordenar listas
        Dim sorter As New GenericSorter(Of IAsociable)("")

        pStepPro = pProDlg
        pStepPro.MinRange = 0
        pStepPro.StepValue = 1
        Try
            '1. Crear circuitos peatonales entre los elementos del nodo
            pProDlg.Description = "Creando circuitos peatonales en el nodo"
            sorter.tipoOrdenamiento = TipoOrdenamiento.Descendente
            pStepPro.MaxRange = _nodos.Count
            For Each nodo As IAsociable In _nodos
                If nodo.elementos.Count > 1 Then
                    Dim copia As New List(Of IAsociable)(nodo.elementos)
                    For Each ambiente As String In _ambientesEnRangoPeatonal
                        'Buscar los equipamientos con superávit en el nodo
                        sorter.criterio = ambiente
                        nodo.elementos.Sort(sorter)
                        'Reiniciar el progress dialog
                        pStepPro.Message = String.Format("Ambiente : {0}", _label.Item(ambiente))
                        For Each eqo As IAsociable In nodo.elementos
                            'Se usa TipoAsociacion.Vehicular debido a que no se necesita
                            'la creación de asociaciones
                            crearAsociacion(eqo, _
                                                ambiente, _
                                                copia, _
                                                Nothing, _
                                                TipoAsociacion.Vehicular)
                        Next eqo
                        bCancel = pTrkCan.Continue
                        If Not bCancel Then
                            Throw New CancelException()
                        End If
                    Next ambiente
                End If
                nodo.saveState()
            Next nodo
        Catch ex As CancelException
            _bEstandarAplicado = False
        Catch ex As Exception
            global_trace.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.Warning)
        End Try
    End Sub

    Public Sub crearRutas( _
        ByRef pProDlg As IProgressDialog2, _
        ByRef pTrkCan As ITrackCancel _
    )
        'Progress dialog
        Dim pStepPro As IStepProgressor
        Dim bCancel As Boolean
        Dim frmRutaAIE As New frmRuta
        Dim errorRuta As List(Of String)

        pStepPro = pProDlg
        pStepPro.MinRange = 0
        pStepPro.StepValue = 1

        '2) Crear rutas
        pProDlg.Description = "Creando rutas"
        pStepPro.MaxRange = _aies.elementos.Count
        pTrkCan.Reset()
        Try
            For Each rutaAIE As AIE In _aies.elementos
                Dim arIDPMEE As New List(Of Integer)
                pStepPro.Message = "id_pmee " & rutaAIE.eqo1.idPmee & " ->" & "id_pmee " & rutaAIE.eqo2.idPmee
                arIDPMEE.Add(rutaAIE.eqo1.idPmee)
                arIDPMEE.Add(rutaAIE.eqo2.idPmee)
                'Rutas entre dos nodos
                If TypeOf rutaAIE.eqo1 Is Nodo Then
                    _geoAdapter.encontrarFeaturesCapaNodos(arIDPMEE, _config.distanciaPeatonal, RedNodal.TipoAnalisis.Ruteo, rutaAIE.idPmee, _iAfe, _iAno)
                ElseIf (TypeOf rutaAIE.eqo1 Is Colegio) And _
                      (TypeOf rutaAIE.eqo2 Is Colegio) Then
                    _geoAdapter.encontrarFeaturesCapaColegio(arIDPMEE, _config.distanciaPeatonal, RedNodal.TipoAnalisis.Ruteo, rutaAIE.idPmee, _iAfe, _iAno)
                ElseIf (TypeOf rutaAIE.eqo1 Is Colegio) And _
                      (TypeOf rutaAIE.eqo2 Is Equipamiento) Then
                    arIDPMEE.Remove(rutaAIE.eqo2.idPmee)
                    _geoAdapter.encontrarFeaturesCapaColegio(arIDPMEE, _config.distanciaPeatonal, RedNodal.TipoAnalisis.Ruteo, rutaAIE.idPmee, _iAfe, _iAno)
                    arIDPMEE.Remove(rutaAIE.eqo1.idPmee)
                    arIDPMEE.Add(rutaAIE.eqo2.idPmee)
                    _geoAdapter.encontrarFeaturesCapaEqos(arIDPMEE, _config.distanciaPeatonal, RedNodal.TipoAnalisis.Ruteo, rutaAIE.idPmee, _iAfe, _iAno)
                End If
                _geoAdapter.initializeRutas(rutaAIE.idPmee)
                errorRuta = _geoAdapter.ejecutarCrearRuta(_config.impedancia, _config.hierarchy, rutaAIE.idPmee, rutaAIE.distancia)
                bCancel = pTrkCan.Continue
                If Not bCancel Then
                    Throw New CancelException()
                End If
            Next
        Catch ex As CancelException
            'Do nothing
        End Try
    End Sub
    '*********************************************************************************************
    ' Precondiciones:
    ' 1. Lista ordenada por ID
    ' 
    '
    '
    '
    '*********************************************************************************************
    Private Sub construirRed( _
        ByRef eqos As List(Of IAsociable), _
        ByRef label As String, _
        ByRef pProDlg As IProgressDialog2, _
        ByRef pTrkCan As ITrackCancel _
    )
        'Variables para iterar
        Dim asn As Asociacion
        Dim vecinos As List(Of IAsociable)
        Dim filtro As List(Of IAsociable)
        Dim filtroAsn As List(Of IAsociable)
        Dim ambiente As String
        'Distancia vehicular
        Dim distVehicular As Integer
        'Distancia peatonal
        Dim distPeatonal As Integer = _config.distanciaPeatonal
        'Progress dialog
        Dim pStepPro As IStepProgressor
        Dim bCancel As Boolean
        'Cache para evitar consultas redundantes al geoadapter
        Dim _cacheVecinos As Dictionary(Of IAsociable, List(Of IAsociable))
        'Objeto para ordenar listas
        Dim sorter As New GenericSorter(Of IAsociable)("")

        pStepPro = pProDlg
        pStepPro.MinRange = 0
        pStepPro.StepValue = 1
        Try
            '___________________________________________________________________
            '1. Estimar distancia vehicular con base en las características del AFE
            distVehicular = _config.tiempoVehicular * _config.velocidad_vehiculo
            'Crear una copia de los colegios. 
            'Los colegios se retirarán de esta lista a medida que el colegio forme 
            'alguna relación. Después de crear los circuitos peatonales esta lista 
            'contendrá únicamente los colegios que no se asociaron con ningún colegio.
            _noasociados = New List(Of IAsociable)(eqos)
            '___________________________________________________________________
            '2. Crear circuitos peatonales
            '2a) Dar prioridad a los colegios con mayor superávit x ambiente
            _cacheVecinos = New Dictionary(Of IAsociable, List(Of IAsociable))
            pProDlg.Description = "Creando circuitos peatonales"
            Dim copiaEqos As New List(Of IAsociable)(eqos)
            sorter.tipoOrdenamiento = TipoOrdenamiento.Descendente

            For Each ambiente In _ambientesEnRangoPeatonal
                'Buscar los equipamientos con superávit x cada ambiente
                _ambienteEnAnalisis = ambiente
                sorter.criterio = ambiente
                filtro = copiaEqos.FindAll(AddressOf tieneSuperavit)
                filtro.Sort(sorter)
                'Reiniciar el progress dialog
                pStepPro.Message = String.Format("Ambiente : {0}", _label.Item(ambiente))
                pStepPro.MaxRange = filtro.Count
                pTrkCan.Reset()
                For Each eqo As IAsociable In filtro
                    vecinos = getVecinos(eqo, distPeatonal, eqos, _cacheVecinos)
                    crearAsociacion(eqo, _
                                        ambiente, _
                                        vecinos, _
                                        _noasociados, _
                                        TipoAsociacion.Peatonal)
                    bCancel = pTrkCan.Continue
                    If Not bCancel Then
                        Throw New CancelException()
                    End If
                Next eqo
            Next ambiente
            'Guardar el estado de las asociaciones peatonales para futuras comparaciones
            For Each aso As IAsociable In _redPeatonal.elementos
                aso.saveState()
                aso.TraceConsole()
            Next
            '___________________________________________________________________
            '3. Crear circuitos vehiculares
            '3a) Dar prioridad a los colegios no asociados con mayor superávit x ambiente
            _cacheVecinos.Clear() 'Borrar el cache de vecinos
            pProDlg.Description = String.Format("Creando circuitos vehiculares : {0} no asociados", label)

            For Each ambiente In _ambientesEnRangoVehicular
                'Buscar los equipamientos con superávit x cada ambiente
                _ambienteEnAnalisis = ambiente
                sorter.criterio = ambiente
                filtro = _noasociados.FindAll(AddressOf tieneSuperavit)
                filtro.Sort(sorter)
                'Reiniciar el progress dialog
                pStepPro.Message = String.Format("Ambiente : {0}", _label.Item(ambiente))
                pStepPro.MaxRange = filtro.Count
                pTrkCan.Reset()
                For Each eqo As IAsociable In filtro
                    vecinos = getVecinos(eqo, distVehicular, eqos, _cacheVecinos)
                    crearAsociacion(eqo, _
                                        ambiente, _
                                        vecinos, _
                                        _noasociados, _
                                        TipoAsociacion.Vehicular)
                    bCancel = pTrkCan.Continue
                    If Not bCancel Then
                        Throw New CancelException()
                    End If
                Next eqo
            Next ambiente
            '___________________________________________________________________
            '3b) Dar prioridad a las asociaciones peatonales con mayor superávit x ambiente

            pProDlg.Description = String.Format("Creando circuitos vehiculares entre asociaciones peatonales de {0}", label)

            For Each ambiente In _ambientesEnRangoVehicular
                'Buscar los equipamientos con superávit x cada ambiente
                _ambienteEnAnalisis = ambiente
                sorter.criterio = ambiente
                filtroAsn = _redPeatonal.FindAll(AddressOf tieneSuperavit)
                filtroAsn.Sort(sorter)
                'Reiniciar el progress dialog
                pStepPro.Message = String.Format("Ambiente : {0}", _label.Item(ambiente))
                pStepPro.MaxRange = filtroAsn.Count
                pTrkCan.Reset()
                For Each asn In filtroAsn
                    'Ordenar los elementos en la asociacion por superavit según el ambiente
                    asn.Sort(sorter)
                    For Each eqo As IAsociable In asn.elementos
                        vecinos = getVecinos(eqo, distVehicular, eqos, _cacheVecinos)
                        crearAsociacion(eqo, _
                                            ambiente, _
                                            vecinos, _
                                            _noasociados, _
                                            TipoAsociacion.Vehicular)
                    Next eqo
                    bCancel = pTrkCan.Continue
                    If Not bCancel Then
                        Throw New CancelException()
                    End If
                Next asn
            Next ambiente

            '4) Visualizar las asociaciones
            pProDlg.Description = "Creando asociaciones"
            pStepPro.MaxRange = _redPeatonal.elementos.Count
            pTrkCan.Reset()
            For Each aso As IAsociable In _redPeatonal.elementos
                pStepPro.Message = aso.nombre & " " & aso.idPmee.ToString
                _geoAdapter.crearPoligonoAso(CType(aso, Asociacion).elementos, aso.idPmee, aso.upz)
                bCancel = pTrkCan.Continue
                If Not bCancel Then
                    Throw New CancelException()
                End If
            Next
        Catch ex As CancelException
            _bEstandarAplicado = False
        End Try

    End Sub
#Region "Crea asociaciones"

    '*********************************************************************************************
    ' Precondiciones:
    ' 1. Los vecinos están ordenados por distancia
    ' 2. Existe una ruta para ir desde el equipamiento a cada uno de los equipamientos en la lista
    '
    '
    '
    '*********************************************************************************************
    Private Sub crearAsociacion( _
            ByRef eqo As IAsociable, _
            ByRef ambiente As String, _
            ByRef vecinos As List(Of IAsociable), _
            ByRef eqosNoAsociados As List(Of IAsociable), _
            ByRef tipo As TipoAsociacion _
    )
        Dim peorVecino As IAsociable
        Dim propertyInfo As PropertyInfo
        Dim type As Type = GetType(IAsociable)
        Dim superavit As Integer
        Dim deficit As Integer
        Try
            '¿Cuál es el superávit del eqo?
            propertyInfo = type.GetProperty(ambiente)
            superavit = propertyInfo.GetValue(eqo, Nothing)
            'Si hay superávit
            If superavit > 0 Then
                '¿Cuál es el vecino al que debería suplirle el déficit y se encuentra a menor distancia?
                peorVecino = encontrarPeorVecino(eqo, superavit, ambiente, vecinos)
                'Crear la asociación entre los colegios si existe un vecino adecuado
                If peorVecino IsNot Nothing Then
                    Dim aie As New AIE
                    Dim oldAie As AIE
                    Dim nuevaRelacion As New Relacion
                    'Crear AIE
                    deficit = propertyInfo.GetValue(peorVecino, Nothing)
                    aie.eqo1 = peorVecino
                    aie.eqo2 = eqo
                    oldAie = _aies.Find(aie)
                    If oldAie IsNot Nothing Then
                        aie = oldAie
                    Else
                        _aies.Add(aie)
                    End If
                    nuevaRelacion.ambiente = _label.Item(ambiente)
                    nuevaRelacion.tipo = tipo
                    nuevaRelacion.cantidad = -deficit
                    aie.relaciones.Add(nuevaRelacion)
                    'Actualizar oferta de los eqos
                    propertyInfo.SetValue(aie, -deficit, Nothing)
                    'Crear asociación peatonal
                    If tipo = TipoAsociacion.Peatonal Then
                        'Conectar colegios
                        'Los dos ya estaban en una asociación
                        If eqo.asociacion IsNot Nothing And peorVecino.asociacion IsNot Nothing Then
                            Dim aso1, aso2 As Asociacion
                            aso1 = eqo.asociacion
                            aso2 = peorVecino.asociacion
                            'Si no están en la misma asociación
                            If aso1.elementos IsNot aso2.elementos Then
                                'Unir la asociación vecina a la asociación del eqo
                                aso1.addAsociacion(peorVecino.asociacion)
                                'Cambiar en cada eqo perteneciente a la asociación vecina, la
                                'asociación x la asociación del eqo
                                For Each col As IAsociable In aso2.elementos
                                    col.asociacion = aso1
                                Next
                                'Quitar la asociación vecina de la lista de asociaciones
                                _redPeatonal.elementos.Remove(aso2)
                            End If

                        ElseIf eqo.asociacion Is Nothing And peorVecino.asociacion Is Nothing Then
                            'Ninguno estaba en una asociación
                            Dim nuevaAsociacion As New Asociacion
                            nuevaAsociacion.Add(eqo)
                            nuevaAsociacion.Add(peorVecino)
                            eqo.asociacion = nuevaAsociacion
                            peorVecino.asociacion = nuevaAsociacion
                            _redPeatonal.Add(nuevaAsociacion)
                        ElseIf eqo.asociacion Is Nothing And peorVecino.asociacion IsNot Nothing Then
                            'Vecino estaba en una asociación y eqo no
                            peorVecino.asociacion.Add(eqo)
                            eqo.asociacion = peorVecino.asociacion
                        Else
                            'eqo estaba en una asociación y vecino no
                            eqo.asociacion.Add(peorVecino)
                            peorVecino.asociacion = eqo.asociacion
                        End If
                    End If
                    'Los eqos tienen al menos una relación 
                    eqosNoAsociados.Remove(eqo)
                    eqosNoAsociados.Remove(peorVecino)
                End If
            End If
        Catch ex As Exception
            global_trace.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.Warning)
        End Try
    End Sub
    '*********************************************************************************************
    ' Precondiciones:
    ' 1. Los vecinos están ordenados por distancia
    ' 2. Existe una ruta para ir desde el equipamiento a cada uno de los equipamientos en la lista
    '
    '
    '
    '*********************************************************************************************
    Private Function encontrarPeorVecino( _
            ByRef eqo As IAsociable, _
            ByRef superavit As Integer, _
            ByRef ambiente As String, _
            ByRef vecinos As List(Of IAsociable) _
    ) As IAsociable
        Dim vecino, peorVecino As IAsociable
        Dim propertyInfo As PropertyInfo
        Dim type As Type = GetType(IAsociable)
        Dim deficit As Integer
        Dim it As List(Of IAsociable).Enumerator
        Dim diferencia As Integer
        Dim ruta As New AIE

        propertyInfo = type.GetProperty(ambiente)
        'Ordenar los vecinos por deficit (de menor a mayor)
        vecinos.Sort(New GenericSorter(Of IAsociable)(ambiente))
        'Encontrar el vecino al que se le cubre el déficit en su totalidad
        peorVecino = Nothing
        it = vecinos.GetEnumerator()
        deficit = -1
        ruta.eqo2 = eqo
        While it.MoveNext And deficit < 0
            vecino = it.Current
            ruta.eqo1 = vecino
            If eqo IsNot vecino Then
                If AIE.existeRelacion(_aies.elementos, ruta, _label.Item(ambiente)) Then
                    Continue While
                End If
                deficit = propertyInfo.GetValue(vecino, Nothing)
                diferencia = superavit + deficit
                If diferencia >= 0 And deficit < 0 Then
                    peorVecino = vecino
                    Exit While
                End If
            End If
        End While
        Return peorVecino
    End Function
#End Region
#Region "Helpers"

    '*********************************************************************************************
    ' Funciones utilizadas para realizar filtros
    '*********************************************************************************************
    Private Function tieneSuperavit(Of IAsociable)(ByVal obj As IAsociable) As Boolean
        Dim propertyInfo As PropertyInfo
        Dim type As Type = GetType(IAsociable)
        propertyInfo = type.GetProperty(_ambienteEnAnalisis)
        If propertyInfo.GetValue(obj, Nothing) > 0 Then
            Return True
        End If
        Return False
    End Function
    Private Function getVecinos( _
        ByRef el As IAsociable, _
        ByRef iDistancia As Integer, _
        ByRef eqosExistentes As List(Of IAsociable), _
        ByRef _cache As Dictionary(Of IAsociable, List(Of IAsociable)) _
    ) As List(Of IAsociable)

        Dim listVecinos As List(Of IAsociable)

        If _cache.ContainsKey(el) Then
            listVecinos = _cache.Item(el)
        Else
            listVecinos = _geoAdapter.getVecinos(_iAfe, _iAno, el, iDistancia, eqosExistentes)
            _cache.Add(el, listVecinos)
        End If
        Return listVecinos
    End Function
    Public Function cargarDatos( _
        ByRef nombreAfe As String, _
        ByRef iAno As Integer, _
        ByRef pProDlg As IProgressDialog2 _
    ) As Boolean
        'Progress dialog
        Dim pStepPro As IStepProgressor
        Dim bCambio As Boolean
        pStepPro = pProDlg
        pStepPro.MinRange = 0
        pStepPro.StepValue = 1
        pStepPro.MaxRange = 4
        _colegios = New List(Of IAsociable)
        _nodos = New List(Of IAsociable)
        _eqos = New List(Of IAsociable)
        _geoAdapter.borrarFeaturesTemporales()
        Try
            pProDlg.Description = "Cargando capas..."
            bCambio = _geoAdapter.cargarMapa(nombreAfe, iAno, _iAfe, _iAno)
            pStepPro.Step()
            If bCambio Then
                pProDlg.Description = "Cargando colegios..."
                _geoAdapter.getColegios(_colegios, _iAfe, _iAno, Nothing, Nothing)
                _colegios.Sort()
                pStepPro.Step()
                pProDlg.Description = "Cargando equipamientos..."
                _geoAdapter.getEquipamientos(_eqos, _iAfe, _iAno)
                _eqos.Sort()
                pStepPro.Step()
                pProDlg.Description = "Cargando nodos..."
                _geoAdapter.getNodos(_nodos, afe, ano, _eqos, _colegios)
                _nodos.Sort()
                pStepPro.Step()
                _bEstandarAplicado = False
            End If
        Catch ex As Exception
            'Throw New DatosNoValidosException(String.Format(RedNodal.My.Resources.strErrInfoFaltante, ex.Message, iNumFila, estandar), ex)
        End Try
        Return bCambio
    End Function

    Private Function Frac(ByVal x As Double) As Integer
        Dim absolut As Double = Math.Abs(x)
        Dim fraction As Double = Math.Abs(absolut - Int(absolut))
        If fraction >= 0.7 Then
            Frac = CInt(Math.Round(x))
        Else
            Frac = CInt(x)
        End If
    End Function
#End Region
#Region "Finders"

    Public Function buscarColegio( _
        ByRef target As IAsociable _
    ) As IAsociable
        Return IAsociable.buscarElemento(target, _colegios)
    End Function

    Public Function buscarNodo( _
        ByRef target As IAsociable _
    ) As IAsociable
        Return IAsociable.buscarElemento(target, _nodos)
    End Function

    Public Function buscarAsociacion( _
        ByRef target As IAsociable _
    ) As IAsociable
        Return IAsociable.buscarElemento(target, _redPeatonal.elementos)
    End Function

    Public Function buscarEquipamiento( _
        ByRef target As IAsociable _
    ) As IAsociable
        Return IAsociable.buscarElemento(target, _eqos)
    End Function

#End Region
End Class