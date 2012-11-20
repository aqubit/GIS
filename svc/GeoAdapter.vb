Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.DataSourcesGDB
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Editor
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.NetworkAnalyst
Imports System.Runtime.InteropServices
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.DisplayUI
Imports ESRI.ArcGIS.Catalog

Public Enum TipoAnalisis
    Ruteo = 1
    Interseccion = 2
End Enum
Public Enum TipoConsulta
    Geografica = 1
    Atributo = 2
End Enum
Public Enum TipoImpedancia
    Metros = 1
    Minutos = 2
End Enum
Public Enum TipoNodo
    Definitivo = 1
    Potencial = 2
End Enum
Public Enum TipoEstado
    Antiguo = 1
    Nuevo = 2
    Intervenido = 3
End Enum
Public Enum ClaseNodo
    Basico = 1
    Intermedio = 2
    Integrado = 3
    Ninguno = 4
End Enum
Public Enum SectorEqo
    Bienestar = 1
    Cultural = 2
    Recreacion = 4
End Enum
Public Enum CaracterEqo
    Oficial = 1
    Privado = 2
End Enum

Public Class GeoAdapter

#Region "GeoAdapter_Declaraciones"
    Private _dataProvider As DataProvider
    Private _fcColegio As IFeatureClass
    Private _fcNodo As IFeatureClass
    Private _fcAFE As IFeatureClass
    Private _fcUPZ As IFeatureClass
    Private _fcRutas As IFeatureClass
    Private _fcEquipamiento As IFeatureClass
    Private _fcTempoPuntos As IFeatureClass
    Private _fcAsociaciones As IFeatureClass
    Private _fcAsociacion As IFeatureClass
    Private Shared _seed As Integer
    Private m_pNAContext As INAContext

    Public ReadOnly Property colegioFC() As IFeatureClass
        Get
            Return _fcColegio
        End Get
    End Property
    Public ReadOnly Property nodosFC() As IFeatureClass
        Get
            Return _fcNodo
        End Get
    End Property
    Public ReadOnly Property upzFC() As IFeatureClass
        Get
            Return _fcUPZ
        End Get
    End Property
    Public Sub New(ByVal dataProvider As DataProvider)
        _dataProvider = dataProvider
        Try
            borrarFeatureClass("Asociacion")
            borrarFeatureClass("punto_temp")
            borrarFeatureClass("Asociaciones")
            borrarFeatureClass("Rutas")
            crearFeatureClass("Asociacion", 4)
            crearFeatureClass("punto_temp", 1)
            crearFeatureClass("Asociaciones", 1)
            crearFeatureClass("Rutas", 3)
            _fcColegio = _dataProvider.abrirFCxWS("Colegio")
            _fcAFE = _dataProvider.abrirFCxWS("AFE")
            _fcUPZ = _dataProvider.abrirFCxWS("UPZ")
            _fcRutas = _dataProvider.abrirFCxWS("Rutas")
            _fcEquipamiento = _dataProvider.abrirFCxWS("Equipamiento")
            _fcAsociaciones = _dataProvider.abrirFCxWS("Asociaciones")
            _fcNodo = _dataProvider.abrirFCxWS("Nodo")
            _fcAsociacion = _dataProvider.abrirFCxWS("Asociacion")
            _seed = Integer.MaxValue
        Catch ex As Exception
            Throw New ConfigException(RedNodal.My.Resources.strErrDBSoloLectura, ex)
        End Try
    End Sub
    Public ReadOnly Property dataprovider() As DataProvider
        Get
            Return _dataProvider
        End Get
    End Property

    'Función que carga información a un mapa vacío de 
    'acuerdo a unas condiciones de despliegue (consulta de definición y simbología)
    Public Function cargarMapa( _
        ByRef nombreAfe As String, _
        ByRef iAno As Integer, _
        ByRef iAFEAnterior As Integer, _
        ByRef iAnoAnterior As Integer _
    ) As Boolean
        Dim pFeatClass As IFeatureClass
        Dim strConsulta As String
        Dim arnumAFE As New ArrayList
        Dim strDefQuery As String
        Dim iNumAfe As Integer = -1

        Dim bCambio As Boolean = False

        strConsulta = "NOMBRE = " & "'" & nombreAfe & "'"
        arnumAFE = leerTabla("NUMERO_AFE", _fcAFE, strConsulta, TipoConsulta.Atributo)
        If arnumAFE.Count > 0 Then
            iNumAfe = arnumAFE.Item(0)
            If iNumAfe <> iAFEAnterior Or iAno <> iAnoAnterior Then
                'Limpiar mapa
                If global_map.LayerCount > 0 Then
                    global_map.ClearLayers()
                End If
                strDefQuery = "NUMERO_AFE = " & iNumAfe
                pFeatClass = _dataProvider.abrirFCxWS("AFE")
                addLayer(pFeatClass, strDefQuery, "AFE", True)
                addLayer(nodosFC, strDefQuery & " AND AÑO <= " & iAno, "Nodos", True)
                'Referencia
                pFeatClass = _dataProvider.abrirFCxWS("Tratamientos")
                addLayer(pFeatClass, strDefQuery, "Tratamientos", False)
                pFeatClass = _dataProvider.abrirFCxWS("Usos")
                addLayer(pFeatClass, strDefQuery, "Usos", False)
                pFeatClass = _dataProvider.abrirFCxWS("Proyecto_Urbano")
                addLayer(pFeatClass, strDefQuery, "Proyecto Urbano", False)
                'End referencia
                pFeatClass = _dataProvider.abrirFCxWS("Asociacion")
                addLayer(pFeatClass, "", "Asociación", True)
                pFeatClass = _dataProvider.abrirFCxWS("Movilidad_Vial")
                addLayer(pFeatClass, strDefQuery, "Movilidad Vial", True)
                pFeatClass = _dataProvider.abrirFCxWS("Rutas")
                addLayer(pFeatClass, "", "Rutas", True)
                pFeatClass = _dataProvider.abrirFCxWS("Equipamiento")
                addLayer(pFeatClass, strDefQuery & " AND AÑO <= " & iAno, "Equipamientos", True)
                addLayer(colegioFC, strDefQuery & " AND AÑO <= " & iAno, "Colegios", True)
                pFeatClass = _dataProvider.abrirFCxWS("Asociaciones")
                addLayer(pFeatClass, "", "Asociaciones", True)
                iAFEAnterior = iNumAfe
                iAnoAnterior = iAno
                bCambio = True
            End If
        End If
        Return bCambio
    End Function
#End Region
#Region "GeoAdapter_Leer datos"

    Public Function leerTablaAFE( _
        ByRef nombreCampo As String, _
        ByRef strConsulta As String, _
        ByRef TipoC As TipoConsulta _
    ) As ArrayList
        Return leerTabla(nombreCampo, _fcAFE, strConsulta, TipoC)
    End Function
    Public Function leerTablaUPZ( _
          ByRef nombreCampo As String, _
          ByRef strConsulta As String, _
          ByRef TipoC As TipoConsulta _
      ) As ArrayList
        Return leerTabla(nombreCampo, _fcUPZ, strConsulta, TipoC)
    End Function

    Public Function leerTablaColegio( _
        ByRef nombreCampo As String, _
        ByRef strConsulta As String, _
        ByRef TipoC As TipoConsulta _
    ) As ArrayList
        Return leerTabla(nombreCampo, _fcColegio, strConsulta, TipoC)
    End Function

    'Función que lee el contenido de un campo especifico accediendo a la tabla
    Public Function leerTabla( _
        ByRef NombreCampo As String, _
        ByRef pFC As IFeatureClass, _
        ByRef strConsulta As String, _
        ByRef TipoC As TipoConsulta _
    ) As ArrayList

        Dim cursorE As IFeatureCursor = Nothing
        Dim fields As IFields
        Dim row As IFeature
        Dim inCampo As Long
        Dim strValor As String
        Dim arrayValores As New ArrayList
        Dim pQ As IQueryFilter

        pQ = New QueryFilter
        pQ.WhereClause = strConsulta
        Try
            If pFC IsNot Nothing Then
                If strConsulta.Equals("") Then
                    cursorE = pFC.Search(Nothing, False)
                Else
                    cursorE = pFC.Search(pQ, False)
                End If
                fields = cursorE.Fields
                inCampo = fields.FindField(NombreCampo)
                If Not (cursorE Is Nothing) Then
                    row = cursorE.NextFeature
                    While (Not row Is Nothing)
                        If TipoC = TipoConsulta.Atributo Then
                            If Not (row.Value(inCampo) Is System.DBNull.Value) Then
                                strValor = row.Value(inCampo)
                                arrayValores.Add(strValor)
                            End If
                        Else
                            arrayValores.Add(row)
                        End If
                        row = cursorE.NextFeature
                    End While
                End If
            End If
        Catch ex As Exception
            global_trace.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.Error)
        Finally
            If cursorE IsNot Nothing Then
                Marshal.ReleaseComObject(cursorE)
            End If
        End Try
        leerTabla = arrayValores
    End Function


    'Precondiciones:
    '1. La lista viene organizada por idPmee
    '
    'Descripción:
    'Esta función trae los eqos en un rango de iDistEnMetros metros del elemento el.
    'Los eqos vienen ordenados de menor a mayor distancia. Esta función debe garantizar que cada 
    'eqo en la lista de respuesta puede conectarse con el elemento el, es decir, que
    'existe una ruta para ir entre los dos colegios.

    Public Function getVecinos( _
        ByRef iAFE As Integer, _
        ByRef iAno As Integer, _
        ByRef el As IAsociable, _
        ByRef iDistancia As Integer, _
        ByRef eqosExistentes As List(Of IAsociable) _
    ) As List(Of IAsociable)


        Dim colsBuffer As New List(Of IAsociable)
        Dim target As IAsociable
        Dim listIdPmee As New List(Of Integer)
        Dim eqosInBuffer As New List(Of IAsociable)
        Dim pFC As IFeatureClass = Nothing

        'Elemento a buscar
        listIdPmee.Add(el.idPmee)

        If TypeOf el Is Colegio Then
            pFC = _dataProvider.abrirFCxWS("Colegio")
        ElseIf TypeOf el Is Nodo Then
            pFC = _dataProvider.abrirFCxWS("Nodo")
        ElseIf TypeOf el Is Equipamiento Then

        Else
            'Do nothing
        End If

        If pFC IsNot Nothing Then
            eqosInBuffer = encontrarFeatures(listIdPmee, pFC, Nothing, iDistancia, TipoAnalisis.Interseccion, 1, iAFE, iAno)
            'Ordenar eqos x distancia
            eqosInBuffer.Sort(AddressOf Equipamiento.CompareByDistance)
            'Buscar cada colegio que se encuentre en el área de influencia en la lista de colegios
            For Each eqo As IAsociable In eqosInBuffer
                target = IAsociable.buscarElemento(eqo, eqosExistentes)
                If target IsNot Nothing Then
                    target.distancia = eqo.distancia
                    colsBuffer.Add(target)
                End If
            Next
        End If
        Return colsBuffer
    End Function

    'Esta función trae los colegios que hacen parte del AFE seleccionada
    'y obtiene los valores que permitirán la aplicación del estándar
    Public Sub getColegios( _
        ByRef listaColegios As List(Of IAsociable), _
        ByRef iAFE As Integer, _
        ByRef iAno As Integer, _
        ByRef pGeom As IGeometry, _
        ByRef cache As List(Of IAsociable) _
    )
        Dim pCur As IFeatureCursor = Nothing
        Dim pFila As IFeature
        Dim pFClass As IFeatureClass = _dataProvider.abrirFCxWS("Colegio")
        Dim strQuery As String
        Dim pTabla As ITable = _dataProvider.abrirTabla("Colegio")


        Dim indexIdPmee As Integer = pTabla.Fields.FindField("ID_PMEE")
        Dim indexNombre As Integer = pTabla.Fields.FindField("NOMBRE_EQO")
        Dim indexLaboratorio As Integer = pTabla.Fields.FindField("O_LABORA")
        Dim indexBiblioteca As Integer = pTabla.Fields.FindField("O_BIBLIO")
        Dim indexTallerArtes As Integer = pTabla.Fields.FindField("O_T_ARTES")
        Dim indexAulaMultimedios As Integer = pTabla.Fields.FindField("O_MULTIM")
        Dim indexAreaLibre As Integer = pTabla.Fields.FindField("O_LIBRE")
        Dim indexAulaMultiple As Integer = pTabla.Fields.FindField("O_MULTIP")
        Dim indexLudoteca As Integer = pTabla.Fields.FindField("O_LUDOTE")
        Dim indexAreaLote As Integer = pTabla.Fields.FindField("O_LOTE")
        Dim indexAreaPrimerPiso As Integer = pTabla.Fields.FindField("O_1PISO")
        Dim indexAreaTotalConstruida As Integer = pTabla.Fields.FindField("O_T_CONS")
        Dim indexAreaAulasPreescolar As Integer = pTabla.Fields.FindField("O_AU_PR")
        Dim indexAreaAulasBPMedia As Integer = pTabla.Fields.FindField("O_BP_ME")
        Dim indexAreaAulasEspecializadas As Integer = pTabla.Fields.FindField("O_ESPEC")
        Dim indexAreaTeatro As Integer = pTabla.Fields.FindField("O_TEATRO")
        Dim indexAreaRecreacionActiva As Integer = pTabla.Fields.FindField("O_R_ACT")
        Dim indexAreaRecreacionPasiva As Integer = pTabla.Fields.FindField("O_R_PAS")
        Dim indexAreaServiciosSanitarios As Integer = pTabla.Fields.FindField("O_S_SAN")
        Dim indexAreaServiciosSanDiscapacitados As Integer = pTabla.Fields.FindField("O_S_SAND")
        Dim indexAreaBienestarEstudiantil As Integer = pTabla.Fields.FindField("O_BIENES")
        Dim indexAreaGestionPedagogica As Integer = pTabla.Fields.FindField("O_GE_PED")
        Dim indexAreaServiciosGenerales As Integer = pTabla.Fields.FindField("O_S_GENE")
        Dim indexCuposActualesPreescolar As Integer = pTabla.Fields.FindField("CA_PR")
        Dim indexCuposActualesBPMedia As Integer = pTabla.Fields.FindField("CA_BP_ME")
        Dim indexMaximoJornadas As Integer = pTabla.Fields.FindField("JOR_MAX")
        Dim indexIdCaracter As Integer = pTabla.Fields.FindField("ID_CRR")
        Try
            strQuery = "NUMERO_AFE = " & iAFE & " AND AÑO <= " & iAno
            If (Not pFClass Is Nothing) Then
                pCur = getFiltro(pFClass, strQuery, pGeom)

                If pCur IsNot Nothing Then
                    pFila = pCur.NextFeature
                    While Not pFila Is Nothing
                        Dim col As New Colegio
                        'Id PMEE
                        If pFila.Value(indexIdPmee) IsNot System.DBNull.Value Then
                            col.idPmee = pFila.Value(indexIdPmee)
                        End If
                        If cache IsNot Nothing Then
                            col = IAsociable.buscarElemento(col, cache)
                        Else
                            col.upz = buscarUPZ(pFila.Shape)
                            'Nombre
                            If pFila.Value(indexNombre) IsNot System.DBNull.Value Then
                                col.nombre = pFila.Value(indexNombre)
                            End If
                            'ID Caracter
                            If pFila.Value(indexIdCaracter) IsNot System.DBNull.Value Then
                                col.idCaracter = pFila.Value(indexIdCaracter)
                            End If
                            'Area biblioteca
                            If pFila.Value(indexBiblioteca) IsNot System.DBNull.Value Then
                                col.areaBiblioteca = pFila.Value(indexBiblioteca)
                            End If
                            'Area laboratorio
                            If pFila.Value(indexLaboratorio) IsNot System.DBNull.Value Then
                                col.areaLaboratorio = pFila.Value(indexLaboratorio)
                            End If
                            'Area taller artes
                            If pFila.Value(indexTallerArtes) IsNot System.DBNull.Value Then
                                col.areaTallerArtes = pFila.Value(indexTallerArtes)
                            End If
                            'Area aula multimedios
                            If pFila.Value(indexAulaMultimedios) IsNot System.DBNull.Value Then
                                col.areaAulaMultimedios = pFila.Value(indexAulaMultimedios)
                            End If
                            'Area libre
                            If pFila.Value(indexAreaLibre) IsNot System.DBNull.Value Then
                                col.areaAreaLibre = pFila.Value(indexAreaLibre)
                            End If
                            'Area aula múltiple
                            If pFila.Value(indexAulaMultiple) IsNot System.DBNull.Value Then
                                col.areaAulaMultiple = pFila.Value(indexAulaMultiple)
                            End If
                            'Area lote
                            If pFila.Value(indexAreaLote) IsNot System.DBNull.Value Then
                                col.areaLote = pFila.Value(indexAreaLote)
                            End If
                            'Area construida primer piso
                            If pFila.Value(indexAreaPrimerPiso) IsNot System.DBNull.Value Then
                                col.areaConstruidaPrimerPiso = pFila.Value(indexAreaPrimerPiso)
                            End If
                            'Area total construida
                            If pFila.Value(indexAreaTotalConstruida) IsNot System.DBNull.Value Then
                                col.areaTotalConstruida = pFila.Value(indexAreaTotalConstruida)
                            End If
                            'Area aulas preescolar
                            If pFila.Value(indexAreaAulasPreescolar) IsNot System.DBNull.Value Then
                                col.areaAulasPreescolar = pFila.Value(indexAreaAulasPreescolar)
                            End If
                            'Area aulas basica primaria media
                            If pFila.Value(indexAreaAulasBPMedia) IsNot System.DBNull.Value Then
                                col.areaAulasBPMedia = pFila.Value(indexAreaAulasBPMedia)
                            End If
                            'Area aulas especializadas 
                            If pFila.Value(indexAreaAulasEspecializadas) IsNot System.DBNull.Value Then
                                col.areaAulasEspecializadas = pFila.Value(indexAreaAulasEspecializadas)
                            End If
                            'Area ludoteca
                            If pFila.Value(indexLudoteca) IsNot System.DBNull.Value Then
                                col.areaLudoteca = pFila.Value(indexLudoteca)
                            End If
                            'Area teatro
                            If pFila.Value(indexAreaTeatro) IsNot System.DBNull.Value Then
                                col.areaTeatro = pFila.Value(indexAreaTeatro)
                            End If
                            'Area recreación activa
                            If pFila.Value(indexAreaRecreacionActiva) IsNot System.DBNull.Value Then
                                col.areaRecreacionActiva = pFila.Value(indexAreaRecreacionActiva)
                            End If
                            'Area recreación pasiva
                            If pFila.Value(indexAreaRecreacionPasiva) IsNot System.DBNull.Value Then
                                col.areaRecreacionPasiva = pFila.Value(indexAreaRecreacionPasiva)
                            End If
                            'Area servicios sanitarios
                            If pFila.Value(indexAreaServiciosSanitarios) IsNot System.DBNull.Value Then
                                col.areaServiciosSanitarios = pFila.Value(indexAreaServiciosSanitarios)
                            End If
                            'Area servicios sanitarios discapacitados
                            If pFila.Value(indexAreaServiciosSanDiscapacitados) IsNot System.DBNull.Value Then
                                col.areaServiciosSanitariosDiscapacitados = pFila.Value(indexAreaServiciosSanDiscapacitados)
                            End If
                            'Area bienestar estudiantil
                            If pFila.Value(indexAreaBienestarEstudiantil) IsNot System.DBNull.Value Then
                                col.areaBienestarEstudiantil = pFila.Value(indexAreaBienestarEstudiantil)
                            End If
                            'Area gestión pedagógica
                            If pFila.Value(indexAreaGestionPedagogica) IsNot System.DBNull.Value Then
                                col.areaGestionPedagogica = pFila.Value(indexAreaGestionPedagogica)
                            End If
                            'Area servicios generales
                            If pFila.Value(indexAreaServiciosGenerales) IsNot System.DBNull.Value Then
                                col.areaServiciosGenerales = pFila.Value(indexAreaServiciosGenerales)
                            End If
                            'Area cupos actuales preescolar
                            If pFila.Value(indexCuposActualesPreescolar) IsNot System.DBNull.Value Then
                                col.numCuposActualesPreescolar = pFila.Value(indexCuposActualesPreescolar)
                            End If
                            'Area cupos actuales básica primaria a media 
                            If pFila.Value(indexCuposActualesBPMedia) IsNot System.DBNull.Value Then
                                col.numCuposEnDeficitBPMedia = pFila.Value(indexCuposActualesBPMedia)
                            End If

                            'Número máximo de jornadas
                            If pFila.Value(indexMaximoJornadas) IsNot System.DBNull.Value Then
                                col.numMaxJornadas = pFila.Value(indexMaximoJornadas)
                            End If
                        End If
                        'Siguiente colegio
                        pFila = pCur.NextFeature
                        'Añadir colegio a la lista
                        listaColegios.Add(col)
                    End While
                End If
            End If
        Catch ex As Exception
            global_trace.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.Error)
        Finally
            If pCur IsNot Nothing Then
                Marshal.ReleaseComObject(pCur)
            End If
        End Try
    End Sub

    'Esta función trae los Nodos que hacen parte del AFE seleccionada
    'y obtiene los valores que permitirán la aplicación del estándar
    Public Sub getNodos( _
       ByRef nodos As List(Of IAsociable), _
       ByRef iAFE As Integer, _
       ByRef iAno As Integer, _
       ByRef cacheEqos As List(Of IAsociable), _
       ByRef cacheColegios As List(Of IAsociable) _
   )
        Dim strQuery As String
        Dim pFCursor As IFeatureCursor = Nothing
        Dim pFeature As IFeature
        Dim pQ As IQueryFilter
        Dim listEqos As List(Of IAsociable)
        Dim nodoNuevo As Nodo

        Try
            pQ = New QueryFilter
            strQuery = "NUMERO_AFE = " & iAFE & " AND AÑO <= " & iAno
            pQ.WhereClause = strQuery
            pFCursor = _fcNodo.Search(pQ, False)
            pFeature = pFCursor.NextFeature
            While pFeature IsNot Nothing
                'Crear nuevo nodo
                nodoNuevo = New Nodo
                nodoNuevo.upz = buscarUPZ(pFeature.Shape)
                nodoNuevo.idPmee = pFeature.Value(_fcNodo.Fields.FindField("ID_PMEE"))
                listEqos = New List(Of IAsociable)
                getColegios(listEqos, iAFE, iAno, pFeature.Shape, cacheColegios)
                getEqosxNodo(pFeature, listEqos, cacheEqos)
                'Add eqos y colegios al nodo
                nodoNuevo.AddRange(listEqos)
                pFeature = pFCursor.NextFeature
                'Agregar el nodo a la lista
                nodos.Add(nodoNuevo)
            End While

        Catch ex As Exception
            global_trace.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.Error)
        Finally
            If pFCursor IsNot Nothing Then
                Marshal.ReleaseComObject(pFCursor)
            End If
        End Try
    End Sub

    'Esta función trae todos los equipamientos que hacen parte del afe
    Public Sub getEquipamientos( _
        ByRef eqos As List(Of IAsociable), _
        ByRef iAFE As Integer, _
        ByRef iAno As Integer _
    )

        Dim pQF As IQueryFilter
        Dim pFeatureCursor As IFeatureCursor = Nothing
        Dim pFeat As IFeature
        Dim pTabla As ITable = _dataProvider.abrirTabla(_fcEquipamiento.AliasName)
        Dim indexIdPmee As Integer = pTabla.Fields.FindField("ID_PMEE")
        Dim indexNombre As Integer = pTabla.Fields.FindField("NOMBRE_EQO")
        Dim indexLaboratorio As Integer = pTabla.Fields.FindField("O_LABORA")
        Dim indexBiblioteca As Integer = pTabla.Fields.FindField("O_BIBLIO")
        Dim indexTallerArtes As Integer = pTabla.Fields.FindField("O_T_ARTES")
        Dim indexAulaMultimedios As Integer = pTabla.Fields.FindField("O_MULTIM")
        Dim indexAreaLibre As Integer = pTabla.Fields.FindField("O_LIBRE")
        Dim indexAulaMultiple As Integer = pTabla.Fields.FindField("O_MULTIP")

        Try
            pQF = New QueryFilter
            pQF.WhereClause = "NUMERO_AFE = " & iAFE & " AND AÑO <= " & iAno
            pFeatureCursor = pTabla.Search(pQF, False)
            If pFeatureCursor IsNot Nothing Then
                pFeat = pFeatureCursor.NextFeature
                While pFeat IsNot Nothing
                    Dim pEqo As IAsociable = New Equipamiento
                    If pFeat.Value(indexIdPmee) IsNot System.DBNull.Value Then
                        pEqo.idPmee = pFeat.Value(indexIdPmee)
                    End If
                    If pFeat.Value(indexNombre) IsNot System.DBNull.Value Then
                        pEqo.nombre = pFeat.Value(indexNombre)
                    End If
                    If pFeat.Value(indexLaboratorio) IsNot System.DBNull.Value Then
                        pEqo.areaLaboratorio = pFeat.Value(indexLaboratorio)
                    End If
                    If pFeat.Value(indexBiblioteca) IsNot System.DBNull.Value Then
                        pEqo.areaBiblioteca = pFeat.Value(indexBiblioteca)
                    End If
                    If pFeat.Value(indexTallerArtes) IsNot System.DBNull.Value Then
                        pEqo.areaTallerArtes = pFeat.Value(indexTallerArtes)
                    End If
                    If pFeat.Value(indexAulaMultimedios) IsNot System.DBNull.Value Then
                        pEqo.areaAulaMultimedios = pFeat.Value(indexAulaMultimedios)
                    End If
                    If pFeat.Value(indexAreaLibre) IsNot System.DBNull.Value Then
                        pEqo.areaAreaLibre = pFeat.Value(indexAreaLibre)
                    End If
                    If pFeat.Value(indexAulaMultiple) IsNot System.DBNull.Value Then
                        pEqo.areaAulaMultiple = pFeat.Value(indexAulaMultiple)
                    End If
                    eqos.Add(pEqo)
                    pFeat = pFeatureCursor.NextFeature
                End While
            End If
        Catch ex As Exception
            global_trace.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.Error)
        Finally
            If pFeatureCursor IsNot Nothing Then
                Marshal.ReleaseComObject(pFeatureCursor)
            End If
        End Try
    End Sub


    'Esta función trae los equipamientos que hacen parte de un Nodo determinado
    'y obtiene los valores que permitirán la aplicación del estándar
    Private Sub getEqosxNodo( _
        ByRef pFeature As IFeature, _
        ByRef listAso As List(Of IAsociable), _
        ByRef cacheEqos As List(Of IAsociable) _
    )
        Dim pTabla As ITable
        pTabla = _dataProvider.abrirTabla(_fcEquipamiento.AliasName)
        Dim pfcursorsel As IFeatureCursor = Nothing
        Dim pEqo As IAsociable
        Dim pFeat As IFeature
        Dim indexIdPmee As Integer = pTabla.Fields.FindField("ID_PMEE")
        Dim indexNombre As Integer = pTabla.Fields.FindField("NOMBRE_EQO")
        Dim indexLaboratorio As Integer = pTabla.Fields.FindField("O_LABORA")
        Dim indexBiblioteca As Integer = pTabla.Fields.FindField("O_BIBLIO")
        Dim indexTallerArtes As Integer = pTabla.Fields.FindField("O_T_ARTES")
        Dim indexAulaMultimedios As Integer = pTabla.Fields.FindField("O_MULTIM")
        Dim indexAreaLibre As Integer = pTabla.Fields.FindField("O_LIBRE")
        Dim indexAulaMultiple As Integer = pTabla.Fields.FindField("O_MULTIP")
        Dim pSpatialFilter As ISpatialFilter

        Try
            pSpatialFilter = New SpatialFilter
            pSpatialFilter.Geometry = pFeature.Shape
            pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects
            pfcursorsel = _fcEquipamiento.Search(pSpatialFilter, True)
            If pfcursorsel IsNot Nothing Then
                pFeat = pfcursorsel.NextFeature
                While pFeat IsNot Nothing
                    pEqo = New Equipamiento
                    If pFeat.Value(indexIdPmee) IsNot System.DBNull.Value Then
                        pEqo.idPmee = pFeat.Value(indexIdPmee)
                    End If
                    If cacheEqos IsNot Nothing Then
                        pEqo = IAsociable.buscarElemento(pEqo, cacheEqos)
                    Else
                        If pFeat.Value(indexNombre) IsNot System.DBNull.Value Then
                            pEqo.nombre = pFeat.Value(indexNombre)
                        End If
                        If pFeat.Value(indexLaboratorio) IsNot System.DBNull.Value Then
                            pEqo.areaLaboratorio = pFeat.Value(indexLaboratorio)
                        End If
                        If pFeat.Value(indexBiblioteca) IsNot System.DBNull.Value Then
                            pEqo.areaBiblioteca = pFeat.Value(indexBiblioteca)
                        End If
                        If pFeat.Value(indexTallerArtes) IsNot System.DBNull.Value Then
                            pEqo.areaTallerArtes = pFeat.Value(indexTallerArtes)
                        End If
                        If pFeat.Value(indexAulaMultimedios) IsNot System.DBNull.Value Then
                            pEqo.areaAulaMultimedios = pFeat.Value(indexAulaMultimedios)
                        End If
                        If pFeat.Value(indexAreaLibre) IsNot System.DBNull.Value Then
                            pEqo.areaAreaLibre = pFeat.Value(indexAreaLibre)
                        End If
                        If pFeat.Value(indexAulaMultiple) IsNot System.DBNull.Value Then
                            pEqo.areaAulaMultiple = pFeat.Value(indexAulaMultiple)
                        End If
                    End If
                    listAso.Add(pEqo)
                    pFeat = pfcursorsel.NextFeature
                End While
            End If
        Catch ex As Exception
            global_trace.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.Error)
        Finally
            If pfcursorsel IsNot Nothing Then
                Marshal.ReleaseComObject(pfcursorsel)
            End If
        End Try
    End Sub
    Public Function getEstandar(ByRef grupoPromedio As Integer) As Estandar
        Dim pCur As ICursor = Nothing
        Dim pFila As IRow
        Dim estandar As New Estandar(grupoPromedio)
        Dim pTabla As ITable = _dataProvider.abrirTabla("Estandar")
        Dim indexAmbiente As Integer = pTabla.Fields.FindField("AMBIENTE")
        Dim indexCapacidad As Integer = pTabla.Fields.FindField("CAP")
        Dim indexM2in As Integer = pTabla.Fields.FindField("M2_IN")
        Dim indexSede As Integer = pTabla.Fields.FindField("SEDE")
        Dim indexCosto As Integer = pTabla.Fields.FindField("COSTO")
        Dim indexFactorCobertura As Integer = pTabla.Fields.FindField("FACTOR_COBERTURA")
        Dim indexTiempoSector As Integer = pTabla.Fields.FindField("TIEMPO_SECTOR")
        Dim nombreAmbiente As String
        Dim iNumFila As Integer
        Try
            If (Not pTabla Is Nothing) Then
                pCur = pTabla.Search(Nothing, False)
                If Not (pCur Is Nothing) Then
                    pFila = pCur.NextRow
                    While Not pFila Is Nothing
                        iNumFila += 1
                        Dim parametro As New EstandarParametro
                        If pFila.Value(indexCapacidad) IsNot System.DBNull.Value Then
                            parametro.capacidad = pFila.Value(indexCapacidad)
                        End If
                        If pFila.Value(indexFactorCobertura) IsNot System.DBNull.Value Then
                            parametro.factorCobertura = pFila.Value(indexFactorCobertura)
                        End If
                        If pFila.Value(indexCosto) IsNot System.DBNull.Value Then
                            parametro.costoxm2 = pFila.Value(indexCosto)
                        End If
                        If pFila.Value(indexSede) IsNot System.DBNull.Value Then
                            parametro.sede = pFila.Value(indexSede)
                        End If
                        If pFila.Value(indexTiempoSector) IsNot System.DBNull.Value Then
                            parametro.tiempoSector = pFila.Value(indexTiempoSector)
                        End If

                        If pFila.Value(indexM2in) Is System.DBNull.Value Then
                            Throw New Exception("M2_IN")
                        Else
                            parametro.m2In = pFila.Value(indexM2in)
                        End If

                        If pFila.Value(indexAmbiente) Is System.DBNull.Value Then
                            Throw New Exception("AMBIENTE")
                        Else
                            nombreAmbiente = pFila.Value(indexAmbiente)
                            If nombreAmbiente = "Aula Preescolar" Then
                                estandar.estandarAulaPreescolar = parametro
                            End If
                            If nombreAmbiente = "Aula B. Primaria 1-3" Then
                                estandar.estandarAulaPrimaria1_3 = parametro
                            End If
                            If nombreAmbiente = "Aula B. Primaria 4-5" Then
                                estandar.estandarAulaPrimaria4_5 = parametro
                            End If
                            If nombreAmbiente = "Aula B. Secundaria" Then
                                estandar.estandarAulaSecundaria = parametro
                            End If
                            If nombreAmbiente = "Aula Media" Then
                                estandar.estandarAulaMedia = parametro
                            End If
                            If nombreAmbiente = "Ludoteca" Then
                                estandar.estandarLudoteca = parametro
                            End If
                            If nombreAmbiente = "Laboratorios" Then
                                estandar.estandarLaboratorio = parametro
                            End If
                            If nombreAmbiente = "Taller de Artes" Then
                                estandar.estandarTallerArtes = parametro
                            End If
                            If nombreAmbiente.Contains("Aula Multimedios") Then
                                estandar.estandarAulaMultimedios = parametro
                            End If
                            If nombreAmbiente = "Biblioteca" Then
                                estandar.estandarBiblioteca = parametro
                            End If
                            If nombreAmbiente = "Aula Multiple" Then
                                estandar.estandarAulaMultiple = parametro
                            End If
                            If nombreAmbiente = "R. Activa" Then
                                estandar.estandarRecActiva = parametro
                            End If
                            If nombreAmbiente = "R. Pasiva" Then
                                estandar.estandarRecPasiva = parametro
                            End If
                            If nombreAmbiente = "SERVICIOS GENERALES" Then
                                estandar.estandarServiciosGenerales = parametro
                            End If
                            If nombreAmbiente = "GESTION" Then
                                estandar.estandarGestion = parametro
                            End If
                            If nombreAmbiente = "Gestión Pedagógica" Then
                                estandar.estandarGestionPedagogica = parametro
                            End If
                            If nombreAmbiente = "BIENESTAR" Then
                                estandar.estandarBienestar = parametro
                            End If
                            If nombreAmbiente = "Baños" Then
                                estandar.estandarBanos = parametro
                            End If
                            If nombreAmbiente = "Baños para discapacitados" Then
                                estandar.estandarBanosDiscapacitados = parametro
                            End If
                            If nombreAmbiente = "AREA LIBRE" Then
                                estandar.estandarAreaLibre = parametro
                            End If
                            If nombreAmbiente = "AREA LIBRE NUCLEO" Then
                                estandar.estandarAreaLibreNucleo = parametro
                            End If
                            If nombreAmbiente = "AREA LOTE" Then
                                estandar.estandarAreaLote = parametro
                            End If
                            If nombreAmbiente = "AREA CONSTRUIDA 1 PISO" Then
                                estandar.estandarAreaConstruida1erPiso = parametro
                            End If
                            If nombreAmbiente = "AREA CONSTRUIDA TOTAL" Then
                                estandar.estandarAreaConstruidaTotal = parametro
                            End If
                        End If
                        pFila = pCur.NextRow
                    End While
                End If
            End If
        Catch ex As Exception
            Throw New DatosNoValidosException(String.Format(RedNodal.My.Resources.strErrInfoFaltante, ex.Message, iNumFila, estandar), ex)
            estandar = Nothing
        Finally
            If pCur IsNot Nothing Then
                Marshal.ReleaseComObject(pCur)
            End If
        End Try
        Return estandar
    End Function

    'Esta función obtiene un conjunto de datos (espaciales o atributos) de acuerdo
    'a una condición definida
    Public Function getFiltro(ByRef pFClass As IFeatureClass, ByRef strQuery As String, ByRef pGeomSF As IGeometry) As IFeatureCursor

        getFiltro = Nothing
        Dim pFeatCursor As IFeatureCursor = Nothing
        Try
            If pGeomSF Is Nothing Then
                Dim pQF As IQueryFilter

                pQF = New QueryFilter
                pQF.WhereClause = strQuery
                If strQuery.Equals("") Then
                    pFeatCursor = pFClass.Search(Nothing, True)
                Else
                    pFeatCursor = pFClass.Search(pQF, True)
                End If

            Else
                Dim pSF As ISpatialFilter
                pSF = New SpatialFilter
                pSF.Geometry = pGeomSF
                pSF.WhereClause = strQuery
                pSF.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects
                pFeatCursor = pFClass.Search(pSF, True)
            End If
            If pFeatCursor IsNot Nothing Then
                getFiltro = pFeatCursor
            Else
                getFiltro = Nothing
            End If

        Catch ex As Exception
            global_trace.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.Error)
        End Try
    End Function

    Public Function getListIDsNodos(ByRef pIntGeo As IGeometry, ByRef strCampo As String) As List(Of Integer)
        Return getListIDsGeo(_fcNodo, pIntGeo, strCampo)
    End Function

    Public Function getListIDsColegio(ByRef pIntGeo As IGeometry, ByRef strCampo As String) As List(Of Integer)
        Return getListIDsGeo(_fcColegio, pIntGeo, strCampo)
    End Function

    Public Function getListIDsAsociacion(ByRef pIntGeo As IGeometry, ByRef strCampo As String) As List(Of Integer)
        Return getListIDsGeo(_fcAsociacion, pIntGeo, strCampo)
    End Function

    'Esta función trae los identificadores de los elementos que caen dentro de
    'un área específica
    Private Function getListIDsGeo(ByRef pFClass As IFeatureClass, ByRef pIntGeo As IGeometry, ByRef strCampo As String) As List(Of Integer)

        getListIDsGeo = Nothing
        Dim pSF As ISpatialFilter
        pSF = New SpatialFilter
        Dim pFcursor As IFeatureCursor = Nothing
        Dim pFeature As IFeature
        Dim listIDS As New List(Of Integer)

        Try

            If pFClass IsNot Nothing Then
                pSF.Geometry = pIntGeo
                pSF.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects
                pFcursor = pFClass.Search(pSF, True)
                pFeature = pFcursor.NextFeature
                While pFeature IsNot Nothing
                    listIDS.Add(pFeature.Value(pFClass.Fields.FindField(strCampo)))
                    pFeature = pFcursor.NextFeature
                End While
                getListIDsGeo = listIDS
            End If
        Catch ex As Exception
            global_trace.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.Error)
        Finally
            If pFcursor IsNot Nothing Then
                Marshal.ReleaseComObject(pFcursor)
            End If
        End Try

    End Function

    ' Encuentra todos los features dentro de un radio de 20 pixeles de la posición del cursor
    ' Adiciona estos dentro de una colección
    Public Sub getClosestFeature(ByRef pMap As IMap, ByRef pSearchPt As IPoint, ByRef pselected As List(Of Feature), ByRef pFeatureClosest As IFeature)
        Dim pUID As New UID
        Dim pLayers As IEnumLayer
        Dim pSrchEnv As IEnvelope
        Dim searchDist As Double
        Dim pGeoLayer As IGeoFeatureLayer
        Dim pID As IIdentify2
        Dim pIDArray As IArray
        Dim ipFeatIdObj As IFeatureIdentifyObj
        Dim pRowObj As IRowIdentifyObject
        Dim pFeature As IFeature
        Dim j As Integer
        Dim pArLayers As New List(Of IFeatureLayer)
        Dim player As IFeatureLayer
        ' Initializa las variables que retorna
        pselected = New List(Of Feature)
        pFeatureClosest = Nothing
        ' Asignar radio de busqueda
        pSrchEnv = pSearchPt.Envelope
        searchDist = 20 '20 pixeles 
        searchDist = convertPixelsToMapUnits(pMap, searchDist)
        pSrchEnv.Width = searchDist
        pSrchEnv.Height = searchDist
        pSrchEnv.CenterAt(pSearchPt)

        ' Obtiene las capas de tipo Feature del mapa
        pUID.Value = "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}" ' CLSID for GeoFeatureLayer
        pLayers = pMap.Layers(pUID, True)

        If findLayer(global_map, "Colegios") IsNot Nothing Then
            pArLayers.Add(findLayer(global_map, "Colegios"))
        End If
        If findLayer(global_map, "Equipamientos") IsNot Nothing Then
            pArLayers.Add(findLayer(global_map, "Equipamientos"))
        End If
        'If findLayer(global_map, "Asociación") IsNot Nothing Then
        '    pArLayers.Add(findLayer(global_map, "Asociación"))
        'End If
        'If findLayer(global_map, "Nodos") IsNot Nothing Then
        '    pArLayers.Add(findLayer(global_map, "Nodos"))
        'End If
        For Each player In pArLayers
            pGeoLayer = player
            If (pGeoLayer.Selectable And pGeoLayer.Visible And (Not pGeoLayer.FeatureClass Is Nothing)) Then
                pID = pGeoLayer
                pIDArray = pID.Identify(pSrchEnv, Nothing)

                ' Adiciona los features a la colección y limpia el arreglo de identificadores
                If Not pIDArray Is Nothing Then
                    For j = 0 To pIDArray.Count - 1
                        If TypeOf pIDArray.Element(j) Is IFeatureIdentifyObj Then
                            ipFeatIdObj = pIDArray.Element(j)
                            pRowObj = ipFeatIdObj
                            pFeature = pRowObj.Row
                            pselected.Add(pFeature)
                        End If
                    Next j
                    pIDArray.RemoveAll()

                End If
            End If
        Next

        ' Encuentra el feature más cercano de la colección
        getClosestFeatureInCollection(searchDist, pselected, pSearchPt, pFeatureClosest)

    End Sub

    Private Sub getClosestFeatureInCollection(ByRef searchDist As Double, ByRef searchCollection As List(Of Feature), ByRef pPoint As ESRI.ArcGIS.Geometry.IPoint, ByRef pFeature As ESRI.ArcGIS.Geodatabase.IFeature)
        Dim pProximity As IProximityOperator
        Dim pTestFeature As IFeature
        Dim pPointFeature As IFeature = Nothing
        Dim pLineFeature As IFeature = Nothing
        Dim pAreaFeature As IFeature = Nothing
        Dim pGeom As IGeometry
        Dim pointTestDistance As Double
        Dim lineTestDistance As Double
        Dim areaTestDistance As Double
        Dim testDistance As Double
        Dim tempDist As Double
        Dim i As Integer

        ' Busca a través de una colección de features y obtiene el más cercano

        If (searchCollection.Count() < 1) Then Exit Sub

        pointTestDistance = -1
        lineTestDistance = -1
        areaTestDistance = -1
        testDistance = -1

        ' encuentra el feature cercano de acuerdo a la posición del cursor
        pProximity = pPoint
        For i = 0 To searchCollection.Count - 1
            pTestFeature = searchCollection.Item(i)
            pGeom = pTestFeature.Shape
            tempDist = pProximity.ReturnDistance(pGeom)

            If (tempDist < searchDist) Then
                Select Case pGeom.GeometryType
                    Case esriGeometryType.esriGeometryPoint
                        If (pointTestDistance < 0) Then pointTestDistance = tempDist + 1
                        If (tempDist < pointTestDistance) Then
                            pointTestDistance = tempDist
                            pPointFeature = pTestFeature
                        End If
                    Case esriGeometryType.esriGeometryPolyline
                        If (lineTestDistance < 0) Then lineTestDistance = tempDist + 1
                        If (tempDist < lineTestDistance) Then
                            lineTestDistance = tempDist
                            pLineFeature = pTestFeature
                        End If
                    Case esriGeometryType.esriGeometryPolygon
                        If (areaTestDistance < 0) Then areaTestDistance = tempDist + 1
                        If (tempDist < areaTestDistance) Then
                            areaTestDistance = tempDist
                            pAreaFeature = pTestFeature
                        End If
                End Select
            Else
                'inicializa la variable de distancia
                If (testDistance < 0) Then testDistance = tempDist + 1
                If (tempDist < testDistance) Then
                    testDistance = tempDist
                    pFeature = pTestFeature
                End If
            End If
        Next i
        ' Si el feature fue encontrado dentro de la tolerancia de busqueda retorna este
        'de acuerdo a la geometría
        If (Not pPointFeature Is Nothing) Then
            pFeature = pPointFeature
        ElseIf (Not pLineFeature Is Nothing) Then
            pFeature = pLineFeature
        ElseIf (Not pAreaFeature Is Nothing) Then
            pFeature = pAreaFeature
        End If
    End Sub

#End Region
#Region "GeoAdapter_Encontrar elementos"
    Public Function encontrarFeaturesCapaNodos( _
        ByRef listIDFeatures As List(Of Integer), _
        ByRef distancia As Integer, _
        ByRef tipo As TipoAnalisis, _
        ByRef intID As Integer, _
        ByRef iAFE As Integer, _
        ByRef iAno As Integer _
    ) As List(Of IAsociable)
        _fcTempoPuntos = _dataProvider.abrirFCxWS("punto_temp")
        Return encontrarFeatures(listIDFeatures, _fcNodo, _fcTempoPuntos, distancia, tipo, intID, iAFE, iAno)
    End Function
    Public Function encontrarFeaturesCapaColegio( _
        ByRef listIDFeatures As List(Of Integer), _
        ByRef distancia As Integer, _
        ByRef tipo As TipoAnalisis, _
        ByRef intID As Integer, _
        ByRef iAFE As Integer, _
        ByRef iAno As Integer _
    ) As List(Of IAsociable)
        _fcTempoPuntos = _dataProvider.abrirFCxWS("punto_temp")
        Return encontrarFeatures(listIDFeatures, _fcColegio, _fcTempoPuntos, distancia, tipo, intID, iAFE, iAno)
    End Function
    Public Function encontrarFeaturesCapaEqos( _
        ByRef listIDFeatures As List(Of Integer), _
        ByRef distancia As Integer, _
        ByRef tipo As TipoAnalisis, _
        ByRef intID As Integer, _
        ByRef iAFE As Integer, _
        ByRef iAno As Integer _
    ) As List(Of IAsociable)
        _fcTempoPuntos = _dataProvider.abrirFCxWS("punto_temp")
        Return encontrarFeatures(listIDFeatures, _fcEquipamiento, _fcTempoPuntos, distancia, tipo, intID, iAFE, iAno)
    End Function

    'Esta función trae la geometría de los elementos basada en una condición o consulta inicial
    'y la almacena en una lista
    Public Function encontrarFeatures( _
        ByRef listIDFeatures As List(Of Integer), _
        ByRef pFCEntrada As IFeatureClass, _
        ByRef pFCSalida As IFeatureClass, _
        ByRef distancia As Integer, _
        ByRef tipo As TipoAnalisis, _
        ByRef intID As Integer, _
        ByRef iAFE As Integer, _
        ByRef iAno As Integer _
    ) As List(Of IAsociable)

        Dim pFeatCursor As IFeatureCursor = Nothing
        Dim pFeat As IFeature
        Dim pPoly As IPolygon
        Dim pQF As IQueryFilter
        Dim strExpresion As String
        Dim arEquipamiento As New List(Of IAsociable)
        Dim enumID As List(Of Integer).Enumerator
        Dim pPunto As IPoint
        Dim pArea As IArea
        encontrarFeatures = Nothing

        Try
            pPunto = New Point
            strExpresion = ""
            pQF = New QueryFilter
            If listIDFeatures IsNot Nothing Then
                enumID = listIDFeatures.GetEnumerator()
                If enumID.MoveNext() Then
                    strExpresion = "ID_PMEE = " & enumID.Current
                End If
                While enumID.MoveNext()
                    strExpresion = strExpresion & " OR ID_PMEE = " & enumID.Current
                End While
            End If
            pQF.WhereClause = strExpresion
            pFeatCursor = pFCEntrada.Search(pQF, False)
            If pFeatCursor IsNot Nothing Then
                pFeat = pFeatCursor.NextFeature
                While (Not pFeat Is Nothing)
                    If pFeat.Shape.GeometryType = esriGeometryType.esriGeometryPolygon Then
                        pArea = pFeat.Shape
                        pPunto = pArea.Centroid
                    Else
                        pPunto = pFeat.Shape
                    End If
                    If tipo = TipoAnalisis.Ruteo Then
                        insertarFeatures(pFCSalida, pPunto, intID, "ID_PMEE")
                        arEquipamiento = Nothing
                    Else
                        pPoly = crearBuffer(pFCEntrada, pFeat, distancia)
                        arEquipamiento = realizarIntersect(pPoly, pFCEntrada, pPunto, iAFE, iAno)
                    End If
                    pFeat = pFeatCursor.NextFeature()
                End While
                encontrarFeatures = arEquipamiento
            End If
        Catch ex As Exception
            global_trace.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.Error)
        Finally
            If pFeatCursor IsNot Nothing Then
                Marshal.ReleaseComObject(pFeatCursor)
            End If
        End Try
    End Function

    'Esta función crea un área de influencia basada en una geometría inicial y una distancia
    Private Function crearBuffer( _
        ByRef pFC As IFeatureClass, _
        ByRef pFeat As IFeature, _
        ByRef intDistancia As Integer _
    ) As IPolygon
        Dim pTopoOp As ITopologicalOperator
        Dim pPoly As IPolygon
        pPoly = New Polygon
        pTopoOp = pFeat.Shape
        pPoly = pTopoOp.Buffer(intDistancia)
        crearBuffer = pPoly
    End Function

    'Esta función realiza una operación espacial que devuelve los elementos intersectados en un área 
    'especifica, adicionalmente calcula la distancia existentre entre los elementos analizados
    Public Function realizarIntersect( _
        ByRef pGeomIntersect As IGeometry, _
        ByRef pFCAnalisisCorte As IFeatureClass, _
        ByRef ptoComp As IPoint, _
        ByRef iAFE As Integer, _
        ByRef iAno As Integer _
    ) As List(Of IAsociable)

        Dim pSpatialFilter As ISpatialFilter
        Dim pFeatLayer As IFeatureLayer
        Dim pFeatSel As IFeatureSelection
        Dim pFCursorSel As IFeatureCursor = Nothing
        Dim pFeatSelInt As IFeature
        Dim arOIDColInt As New ArrayList
        Dim distPtos As Double
        Dim listEquos As New List(Of IAsociable)
        Dim eqo As Equipamiento
        Dim indexIdPmee As Integer
        Dim pLine As ILine = New Line()
        Dim pPunto As IPoint
        Dim pArea As IArea

        realizarIntersect = Nothing
        Try
            pPunto = New Point
            pFeatLayer = New FeatureLayer
            pFeatLayer.FeatureClass = pFCAnalisisCorte
            indexIdPmee = pFCAnalisisCorte.Fields.FindField("ID_PMEE")

            If Not (pGeomIntersect Is Nothing) Then
                pFeatSel = pFeatLayer
                pSpatialFilter = New SpatialFilter
                pSpatialFilter.Geometry = pGeomIntersect
                pSpatialFilter.WhereClause = "NUMERO_AFE = " & iAFE & " AND AÑO <= " & iAno
                pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects
                pFCursorSel = pFCAnalisisCorte.Search(pSpatialFilter, True)

                If pFCursorSel IsNot Nothing Then
                    pFeatSelInt = pFCursorSel.NextFeature
                    While Not pFeatSelInt Is Nothing
                        If pFeatSelInt.Shape.GeometryType = esriGeometryType.esriGeometryPolygon Then
                            pArea = pFeatSelInt.Shape
                            pPunto = pArea.Centroid
                        Else
                            pPunto = pFeatSelInt.Shape
                        End If
                        pLine.PutCoords(ptoComp, pPunto)
                        distPtos = pLine.Length
                        If distPtos <> 0 Then
                            eqo = New Equipamiento()
                            eqo.distancia = distPtos
                            eqo.idPmee = pFeatSelInt.Value(indexIdPmee)
                            listEquos.Add(eqo)
                        End If
                        pFeatSelInt = pFCursorSel.NextFeature()
                    End While
                End If
                Return listEquos
            End If
        Catch ex As Exception
            global_trace.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.Error)
        Finally
            If pFCursorSel IsNot Nothing Then
                Marshal.ReleaseComObject(pFCursorSel)
            End If
        End Try
    End Function

    'Esta función estima si en un área definida existe información de una capa
    'especifica
    Public Function contieneCapa(ByRef intAFESel As Integer, ByVal listCampos As List(Of String)) As Boolean

        contieneCapa = True
        Dim pFC1 As IFeatureClass = _dataProvider.abrirFCxWS("AFE")
        Dim pFC2 As IFeatureClass = _dataProvider.abrirFCxWS("Proyecto_Urbano")
        Dim pQF As IQueryFilter
        Dim pFeatCursor1 As IFeatureCursor = Nothing
        Dim pFeature1 As IFeature
        Dim pFeature2 As IFeature
        Dim pFeatCursor2 As IFeatureCursor = Nothing
        Dim strExpresion As String = "NUMERO_AFE = " & intAFESel
        Dim intNombreCampo As Integer
        Dim arFeatureVacios As New List(Of Integer)

        Try
            pQF = New QueryFilter
            pQF.WhereClause = strExpresion
            If pFC1 IsNot Nothing Then
                pFeatCursor1 = pFC1.Search(pQF, False)
                If pFeatCursor1 IsNot Nothing Then
                    pFeature1 = pFeatCursor1.NextFeature
                    While pFeature1 IsNot Nothing
                        If pFC2 IsNot Nothing Then
                            pFeatCursor2 = getFiltro(pFC2, "", pFeature1.Shape)
                            If pFeatCursor2 IsNot Nothing Then
                                pFeature2 = pFeatCursor2.NextFeature
                                If pFeature2 IsNot Nothing Then
                                    While pFeature2 IsNot Nothing
                                        For Each strNombreCampo As String In listCampos
                                            intNombreCampo = pFC2.Fields.FindField(strNombreCampo)
                                            If (pFeature2.Value(intNombreCampo) Is System.DBNull.Value Or (pFeature2.Value(intNombreCampo)).Equals("")) Then
                                                arFeatureVacios.Add(pFeature2.OID)
                                            End If
                                        Next
                                        pFeature2 = pFeatCursor2.NextFeature
                                    End While
                                Else
                                    contieneCapa = False
                                End If
                            End If
                        Else
                            contieneCapa = False
                        End If
                        If arFeatureVacios.Count > 0 Then
                            contieneCapa = False
                        End If
                        pFeature1 = pFeatCursor1.NextFeature
                    End While
                End If
            End If
        Catch ex As Exception
            global_trace.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.Error)
        Finally
            If pFeatCursor1 IsNot Nothing Then
                Marshal.ReleaseComObject(pFeatCursor1)
            End If
            If pFeatCursor2 IsNot Nothing Then
                Marshal.ReleaseComObject(pFeatCursor2)
            End If
        End Try
    End Function
    Private Function buscarUPZ(ByRef pGeom As IGeometry) As Integer
        Dim pfeatCursor As IFeatureCursor = Nothing
        Dim pFeat As IFeature
        Dim indexUPZ As Integer
        Try
            indexUPZ = _fcUPZ.Fields.FindField("NUMERO")
            pfeatCursor = getFiltro(_fcUPZ, "", pGeom)
            If pfeatCursor IsNot Nothing Then
                pFeat = pfeatCursor.NextFeature
                If pFeat IsNot Nothing Then
                    buscarUPZ = pFeat.Value(indexUPZ)
                Else
                    buscarUPZ = 0
                End If
            End If
        Catch ex As Exception
            global_trace.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.Warning)
        Finally
            If pfeatCursor IsNot Nothing Then
                Marshal.ReleaseComObject(pfeatCursor)
            End If
        End Try
    End Function

    Private Function detClaseNodo( _
        ByVal pGeometry As IGeometry _
    ) As ClaseNodo
        detClaseNodo = ClaseNodo.Ninguno
        Dim pFeatCursor As IFeatureCursor = Nothing
        Dim pFeat As IFeature
        Dim indexID_SCR As Integer = _fcEquipamiento.Fields.FindField("ID_SCR")
        Dim sumaBienestar As Integer
        Dim sumaCultural As Integer
        Dim sumaRecreacion As Integer

        pFeatCursor = getFiltro(_fcEquipamiento, "", pGeometry)
        If pFeatCursor IsNot Nothing Then
            pFeat = pFeatCursor.NextFeature
            If pFeat IsNot Nothing Then
                sumaBienestar = 0
                sumaCultural = 0
                sumaRecreacion = 0
                While pFeat IsNot Nothing
                    If pFeat.Value(indexID_SCR) = SectorEqo.Bienestar Then
                        sumaBienestar += 1
                    ElseIf pFeat.Value(indexID_SCR) = SectorEqo.Cultural Then
                        sumaCultural += 1
                    ElseIf pFeat.Value(indexID_SCR) = SectorEqo.Recreacion Then
                        sumaRecreacion += 1
                    End If
                    pFeat = pFeatCursor.NextFeature
                End While
                If (sumaRecreacion = 1) And (sumaCultural = 0) And (sumaBienestar = 0) Then
                    detClaseNodo = ClaseNodo.Basico
                ElseIf sumaRecreacion >= 1 And (sumaCultural <= 1 Or sumaBienestar <= 1 Or sumaRecreacion >= 2) Then
                    detClaseNodo = ClaseNodo.Integrado
                End If
            End If
        End If

    End Function

#End Region
#Region "GeoAdapter_Modificar datos"

    'Función que obtiene el ambiente de edición
    Public Function getEditor() As IEditor
        Dim pID As New UID 'UID
        Dim pApp As IApplication 'Interface a la aplicacion
        Dim pEditor As IEditor 'Interface al editor

        getEditor = Nothing
        Try
            pID.Value = "EsriCore.Editor"
            pApp = global_IApplication
            pEditor = pApp.FindExtensionByCLSID(pID)
            getEditor = pEditor
            Exit Function
        Catch ex As Exception
            global_trace.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.Error)
        End Try
    End Function

    'Función que inserta elementos (geometría y atributos) dentro de una capa específica
    Public Sub insertarFeatures( _
        ByRef pFeatClass As IFeatureClass, _
        ByRef pGeometry As IGeometry, _
        ByRef intValorCampo As Integer, _
        ByRef strNombreCampo As String _
    )

        Dim pFeatCur As IFeatureCursor = Nothing
        Dim pFeatBuf As IFeatureBuffer
        Dim pFlds As IFields
        Dim pFld As IField
        Dim i As Long
        Dim pGeo As IGeometry

        Try
            If pFeatClass IsNot Nothing Then
                pFeatCur = pFeatClass.Insert(True)
                pFeatBuf = pFeatClass.CreateFeatureBuffer
                If (TypeOf pGeometry Is IPoint) Then
                    If pGeometry Is Nothing Then
                        pGeo = New Point
                    End If
                ElseIf (TypeOf pGeometry Is IPolygon) Then
                    If pGeometry Is Nothing Then
                        pGeo = New Polygon
                    End If
                ElseIf (TypeOf pGeometry Is IPolyline) Then
                    If pGeometry Is Nothing Then
                        pGeo = New Polyline
                    End If
                End If
                pGeo = pGeometry
                pFlds = pFeatClass.Fields

                For i = 0 To pFlds.FieldCount - 1
                    pFld = pFlds.Field(i)
                    If (pFld.Type = esriFieldType.esriFieldTypeGeometry) Then
                        pFeatBuf.Value(i) = pGeo
                    End If
                    If pFld.AliasName.Equals(strNombreCampo) Then
                        If Not (strNombreCampo.Equals("ID_PMEE")) Then
                            _seed -= 1
                            pFeatBuf.Value(pFeatClass.Fields.FindField("ID_PMEE")) = _seed
                        End If
                        pFeatBuf.Value(i) = intValorCampo
                    End If
                Next i
                pFeatCur.InsertFeature(pFeatBuf)

            End If
        Catch ex As Exception
            global_trace.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.Error)
        Finally
            If pFeatCur IsNot Nothing Then
                Marshal.ReleaseComObject(pFeatCur)
            End If
        End Try
    End Sub
    Private Sub insertarAsociaciones( _
        ByRef listAso As List(Of IAsociable), _
        ByRef featureEntrada As IFeatureClass, _
        ByRef idAso As Integer _
    )
        Dim asociable As IAsociable
        Dim arElementos As New List(Of Integer)
        Dim strQuery As String
        Dim pQueryFilter As IQueryFilter
        Dim pFCursor As IFeatureCursor = Nothing
        Dim pFeature As IFeature
        Dim pFCTempo As IFeatureClass
        Dim enumID As List(Of Integer).Enumerator
        Dim pPunto As IPoint
        Dim pArea As IArea
        pPunto = New Point

        strQuery = ""
        pFCTempo = _dataProvider.abrirFCxWS("Asociaciones")
        Try
            If pFCTempo IsNot Nothing Then
                If listAso IsNot Nothing Then
                    For Each asociable In listAso
                        arElementos.Add(asociable.idPmee)
                    Next
                    If arElementos IsNot Nothing Then
                        enumID = arElementos.GetEnumerator
                        If enumID.MoveNext() Then
                            strQuery = "ID_PMEE = " & enumID.Current
                        End If
                        While enumID.MoveNext()
                            strQuery = strQuery & " OR ID_PMEE = " & enumID.Current
                        End While
                    End If
                    pQueryFilter = New QueryFilter
                    pQueryFilter.WhereClause = strQuery
                    pFCursor = featureEntrada.Search(pQueryFilter, False)
                    If pFCursor IsNot Nothing Then
                        pFeature = pFCursor.NextFeature
                        While pFeature IsNot Nothing
                            If pFeature.Shape.GeometryType = esriGeometryType.esriGeometryPolygon Then
                                pArea = pFeature.Shape
                                pPunto = pArea.Centroid
                            Else
                                pPunto = pFeature.Shape
                            End If
                            insertarFeatures(pFCTempo, pPunto, idAso, "ID_PMEE")
                            pFeature = pFCursor.NextFeature
                        End While
                    End If
                End If
            End If
        Catch ex As Exception
            global_trace.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.Error)
        Finally
            If pFCursor IsNot Nothing Then
                Marshal.ReleaseComObject(pFCursor)
            End If
        End Try
    End Sub
    Public Sub generarNodosFuncionales( _
        ByRef iAfe As Integer, _
        ByRef iAno As Integer, _
        ByRef radio As Integer, _
        ByRef tipo As TipoEstado, _
        ByRef pStepPro As IStepProgressor _
    )
        'Nodo feature class
        Dim arNodos As New ArrayList
        Dim arFeatNodos As New List(Of IFeature)
        Dim pTopoOp As ITopologicalOperator
        Dim pPoly As IPolygon
        Dim pLayer As IFeatureLayer
        Dim strExpresion As String = ""
        Dim claseN As ClaseNodo
        pLayer = New FeatureLayer
        pPoly = New Polygon
        pLayer.FeatureClass = _fcNodo
        If _fcNodo IsNot Nothing Then
            borrarFeatures(_fcNodo, "TIPO = 2")
        End If
        If _fcColegio IsNot Nothing Then
            strExpresion = "NUMERO_AFE = " & iAfe & " AND AÑO <= " & iAno
            arNodos = leerTablaColegio("ESTADO", strExpresion & " AND (ESTADO = " & tipo & ")", TipoConsulta.Geografica)
            arFeatNodos = New List(Of IFeature)(arNodos.ToArray(GetType(IFeature)))
            If arFeatNodos IsNot Nothing Then
                pStepPro.MaxRange = arFeatNodos.Count
                For Each pFeat As IFeature In arFeatNodos
                    pTopoOp = pFeat.Shape
                    pPoly = pTopoOp.Buffer(radio)
                    If global_redNodal.config.tipoestado = TipoEstado.Nuevo Then
                        insertarNodo(pPoly, ClaseNodo.Integrado, iAfe, iAno)
                    ElseIf tipo = TipoEstado.Intervenido Then
                        claseN = detClaseNodo(pPoly)
                        If claseN = ClaseNodo.Integrado Then
                            insertarNodo(pPoly, ClaseNodo.Integrado, iAfe, iAno)
                        ElseIf claseN = ClaseNodo.Basico Then
                            insertarNodo(pPoly, ClaseNodo.Basico, iAfe, iAno)
                        Else 'No hacer nada
                        End If
                    End If
                    pStepPro.Step()
                Next
            End If
        End If
    End Sub

    Private Sub insertarNodo( _
      ByRef pGeometry As IGeometry, _
      ByRef intClaseNodo As Integer, _
      ByRef iAfe As Integer, _
      ByRef iAno As Integer _
  )

        Dim pFeatCur As IFeatureCursor = Nothing
        Dim pFeatBuf As IFeatureBuffer
        Dim pGeo As IGeometry
        Dim indexAFE As Integer = nodosFC.Fields.FindField("NUMERO_AFE")
        Dim indexAÑO As Integer = nodosFC.Fields.FindField("AÑO")
        Dim indexIDPMEE As Integer = nodosFC.Fields.FindField("ID_PMEE")
        Dim indexTipo As Integer = nodosFC.Fields.FindField("TIPO")
        Dim indexClase As Integer = nodosFC.Fields.FindField("CLASE")

        Try
            If nodosFC IsNot Nothing Then
                pFeatCur = nodosFC.Insert(True)
                pFeatBuf = nodosFC.CreateFeatureBuffer

                If (TypeOf pGeometry Is IPolygon) Then
                    If pGeometry Is Nothing Then
                        pGeo = New Polygon
                    End If
                    pGeo = pGeometry
                    pFeatBuf.Shape = pGeo
                    _seed -= 1
                    pFeatBuf.Value(indexIDPMEE) = _seed
                    pFeatBuf.Value(indexAFE) = iAfe
                    pFeatBuf.Value(indexAÑO) = iAno
                    pFeatBuf.Value(indexTipo) = TipoNodo.Potencial
                    pFeatBuf.Value(indexClase) = intClaseNodo
                    pFeatCur.InsertFeature(pFeatBuf)
                End If
            End If
        Catch ex As Exception
            global_trace.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.Error)
        Finally
            If pFeatCur IsNot Nothing Then
                Marshal.ReleaseComObject(pFeatCur)
            End If
        End Try
    End Sub
    Public Sub crearPoligonoAso( _
        ByRef listAsociables As List(Of IAsociable), _
        ByRef idAso As Integer, _
        ByRef idUPZ As Integer _
    )

        Dim pFC As IFeatureCursor = Nothing
        Dim pFeat As IFeature
        Dim pTopo As ITopologicalOperator
        Dim pPoligono As IPolygon
        Dim pPoint As IPoint
        Dim pFCAso As IFeatureClass
        Dim pFCPol As IFeatureClass
        Dim pQFilter As IQueryFilter
        Dim strIDAso As String
        Dim pPointCol As IPointCollection
        Dim pFeature As IFeatureClass = Nothing

        Try
            pPoint = New Point
            pPointCol = New Polyline
            pQFilter = New QueryFilter
            pPoligono = New Polygon

            If listAsociables.Count > 0 Then
                If TypeOf listAsociables.Item(0) Is Colegio Then
                    pFeature = _fcColegio
                ElseIf TypeOf listAsociables.Item(0) Is Nodo Then
                    pFeature = _fcNodo
                Else
                    'No reachable
                End If
                insertarAsociaciones(listAsociables, pFeature, idAso)
                strIDAso = "ID_PMEE = " & idAso
                pQFilter.WhereClause = strIDAso
                pFCAso = _dataProvider.abrirFCxWS("Asociaciones")
                pFCPol = _dataProvider.abrirFCxWS("Asociacion")

                If pFCAso IsNot Nothing Then
                    pFC = pFCAso.Search(pQFilter, True)
                    If pFC IsNot Nothing Then
                        pFeat = pFC.NextFeature
                        While Not pFeat Is Nothing
                            pPoint = New Point
                            pPoint = pFeat.Shape
                            pPointCol.AddPoint(pPoint)
                            pFeat = pFC.NextFeature
                        End While
                        pTopo = pPointCol
                        pPoligono = pTopo.Buffer(100)
                        insertarFeatures(pFCPol, pPoligono, idAso, "ID_PMEE")
                        idUPZ = buscarUPZ(pPoligono)
                    End If
                End If
            End If
        Catch ex As Exception
            global_trace.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.Warning)
        Finally
            If pFC IsNot Nothing Then
                Marshal.ReleaseComObject(pFC)
            End If
        End Try
    End Sub

    'Función que crea un feature con una geometría específica
    Private Function crearFeatureClass( _
        ByRef strNameFC As String, _
        ByRef intGeometria As Integer _
    ) As IFeatureClass

        Dim pFields As IFields
        Dim pFeatWS As IFeatureWorkspace
        Dim m_pCLSID As UID
        Dim pFC As IFeatureClass = Nothing
        pFields = crearCampos(intGeometria)
        m_pCLSID = New UID
        m_pCLSID.Value = "esricore.Feature"
        Try
            pFeatWS = _dataProvider.getWorkspace()
            pFC = pFeatWS.CreateFeatureClass(strNameFC, pFields, m_pCLSID, Nothing, esriFeatureType.esriFTSimple, "Shape", "")
        Catch ex As FeatureNoAccesibleException
            Throw New CreacionFeatureException(String.Format(RedNodal.My.Resources.strErrCreacionFeature, strNameFC), ex)
        End Try
        Return pFC
    End Function

    'Función que le adiciona determinados campos a un feature class nuevo
    Public Function crearCampos( _
        ByRef geom As Integer _
    ) As Fields

        Dim pfields As IFields
        Dim pFieldsEdit As IFieldsEdit
        pFieldsEdit = New Fields

        ' Crea el campo que almacena la geometría

        Dim pGeomDef As IGeometryDef
        pGeomDef = New GeometryDef
        Dim pGeomDefEdit As IGeometryDefEdit
        pGeomDefEdit = pGeomDef

        ' Asigna la referencia espacial
        Dim pSpatRefFact As ISpatialReferenceFactory
        pSpatRefFact = New SpatialReferenceEnvironment
        Dim pProjCoordSys As IProjectedCoordinateSystem
        pProjCoordSys = pSpatRefFact.CreateProjectedCoordinateSystem(21897)

        Dim pSR As ISpatialReference
        pSR = pProjCoordSys
        pSR.SetDomain(0, 214748.3645, 0, 214748.3645)
        pSR.SetFalseOriginAndUnits(0, 0, 1000)

        ' Asigna las propiedades del campo de geometría.
        With pGeomDefEdit
            .GeometryType_2 = geom
            .GridCount_2 = 1
            .GridSize_2(0) = 1000
            .AvgNumPoints_2 = 2
            .HasM_2 = False
            .HasZ_2 = False
            .SpatialReference_2 = pSR
        End With

        Dim pField As IField
        Dim pFieldEdit As IFieldEdit
        pField = New Field
        pFieldEdit = pField

        pFieldEdit.Name_2 = "shape"
        pFieldEdit.AliasName_2 = "shape"
        pFieldEdit.Type_2 = esriFieldType.esriFieldTypeGeometry
        pFieldEdit.GeometryDef_2 = pGeomDef
        pFieldsEdit.AddField(pField)

        ' crea el campo OBJECTID
        pField = New Field
        pFieldEdit = pField
        pFieldEdit.Name_2 = "OBJECTID"
        pFieldEdit.AliasName_2 = "OBJECTID"
        pFieldEdit.Type_2 = esriFieldType.esriFieldTypeOID
        pFieldsEdit.AddField(pField)

        ' crea el campo identificador
        pField = New Field
        pFieldEdit = pField
        pFieldEdit.Name_2 = "ID_PMEE"
        pFieldEdit.AliasName_2 = "ID_PMEE"
        pFieldEdit.Type_2 = esriFieldType.esriFieldTypeInteger
        pFieldsEdit.AddField(pField)

        pfields = pFieldsEdit
        crearCampos = pfields
    End Function

    'Función que borra un feature class específico
    Private Sub borrarFeatureClass( _
        ByRef strNombreFC As String)
        Dim pDS As IDataset = Nothing
        Try
            pDS = _dataProvider.abrirFCxWS(strNombreFC)
        Catch ex As FeatureNoAccesibleException
            'Do nothing 
        End Try
        Try
            If pDS IsNot Nothing Then
                pDS.Delete()
            End If
        Catch ex As Exception
            Throw New BorrarFeatureException(String.Format(RedNodal.My.Resources.strErrCreacionFeature, strNombreFC), ex)
        End Try

    End Sub

    Public Sub borrarFeaturesTemporales()
        borrarFeatures(_fcRutas, "")
        borrarFeatures(_fcAsociaciones, "")
        borrarFeatures(_fcAsociacion, "")
    End Sub

    'Función que borra los elementos o features de un feature class específico
    Public Sub borrarFeatures( _
        ByRef pFeatcls As IFeatureClass, _
        ByRef strExpresion As String _
    )
        Dim pFeatCursor As IFeatureCursor = Nothing
        Dim pFeat As IFeature
        Dim pQF As IQueryFilter
        Try
            pQF = New QueryFilter
            pQF.WhereClause = strExpresion
            If pFeatcls IsNot Nothing Then
                If strExpresion.Equals("") Then
                    pFeatCursor = pFeatcls.Search(Nothing, False)
                Else
                    pFeatCursor = pFeatcls.Search(pQF, False)
                End If

                If pFeatCursor IsNot Nothing Then
                    pFeat = pFeatCursor.NextFeature
                    If pFeat IsNot Nothing Then
                        While pFeat IsNot Nothing
                            pFeat.Delete()
                            pFeat = pFeatCursor.NextFeature
                        End While
                    End If
                End If
            End If
        Catch ex As Exception
            global_trace.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.Warning)
        Finally
            If pFeatCursor IsNot Nothing Then
                Marshal.ReleaseComObject(pFeatCursor)
            End If
        End Try
    End Sub

#End Region
#Region "GeoAdapter_Mapa"

    Public Sub refreshMap()
        Dim pDoc As IMxDocument
        pDoc = global_IApplication.Document
        'Refresca el mapa
        pDoc.ActiveView.Refresh()
    End Sub


    Public Function getMapa() As IMap
        '++ Función que permite obtener el mapa activo del documento
        Dim documento As IMxDocument
        Dim mapa As Map
        documento = global_IApplication.Document
        mapa = documento.FocusMap
        getMapa = mapa
    End Function

    Public Function findLayer(ByRef pMap As IMap, _
        ByRef slayername As String _
    ) As IFeatureLayer
        '-->  Busca un layer y devuelve iFeatureLayer
        Dim Count As Integer
        findLayer = Nothing
        For Count = 0 To pMap.LayerCount - 1
            If UCase(pMap.Layer(Count).Name) = UCase(slayername) Then
                findLayer = pMap.Layer(Count)
                Exit Function
            End If
        Next Count
    End Function

    Public Sub addLayerMapa(ByRef NombreCapa As String, ByRef strExp As String, ByRef bVisible As Boolean)
        Dim filePath As String
        filePath = _dataProvider.getPathSimbologia & NombreCapa & ".lyr"
        Dim pLayerFile As ILayerFile
        Dim pFLDef As IFeatureLayerDefinition
        pLayerFile = New LayerFile
        pLayerFile.Open(filePath)
        pLayerFile.Layer.Visible = bVisible
        pFLDef = pLayerFile.Layer
        pFLDef.DefinitionExpression = strExp
        global_IMxDocument.FocusMap.AddLayer(pLayerFile.Layer)
    End Sub


    Public Sub addLayer( _
        ByRef pFC As IFeatureClass, _
        ByRef strExpresion As String, _
        ByRef strAlias As String, _
        ByRef bVisible As Boolean)
        '++ Permite adicionar una capa al mapa y filtrar el contenido de acuerdo a una condición

        Dim pFeatureLayer As IFeatureLayer
        Dim pMxDoc As IMxDocument
        Dim pEnvelope As IEnvelope
        Dim pAV As IActiveView
        Dim pC As IFeatureCursor
        Dim pFeat As IFeature
        Dim strNombreOri As String
        Dim pQ As IQueryFilter

        pMxDoc = global_IApplication.Document
        pQ = New QueryFilter
        If strExpresion.Equals("") Then
            pQ.WhereClause = Nothing
        Else
            pQ.WhereClause = strExpresion
        End If
        strNombreOri = pFC.AliasName
        pFeatureLayer = New FeatureLayer
        pFeatureLayer.FeatureClass = pFC
        pFeatureLayer.Name = strAlias
        pFeatureLayer.Visible = bVisible

        If strNombreOri.Equals("AFE") Then '++ Condiciona si el FC es AFE para filtrar el contenido por este valor
            pFC = pFeatureLayer.FeatureClass
            pC = pFC.Search(pQ, False)
            pFeat = pC.NextFeature
            addLayerMapa(strAlias, strExpresion, bVisible)
            pAV = pMxDoc.ActiveView
            If Not pFeat Is Nothing Then
                pEnvelope = pFeat.Extent
                If Not pEnvelope Is Nothing Then
                    pAV.Extent = pEnvelope
                Else
                    pAV.Extent = pAV.FullExtent
                End If
            End If
            pAV.Refresh()
            Marshal.ReleaseComObject(pC)
        Else
            addLayerMapa(strAlias, strExpresion, bVisible)
        End If
    End Sub

#End Region
#Region "GeoAdapter_Rutas"

    'Función que despues de obtener la configuración del generador de rutas (solver), lo ejecuta
    Public Function ejecutarCrearRuta( _
        ByRef strImpedancia As String, _
        ByRef bHierarchy As Boolean, _
        ByVal idRuta As Integer, _
        ByRef iLongitud As Integer _
    ) As List(Of String)
        Dim repRuta As New List(Of String)
        Dim strMsg As String
        Dim pGPMessages As IGPMessages
        Dim i As Short

        Try
            setSolverSettings(m_pNAContext, strImpedancia, False, bHierarchy, False)
            ' Calcula Ruta
            pGPMessages = New GPMessages
            strMsg = doSolve(m_pNAContext, pGPMessages)
            If Not (strMsg = "OK") Then
                repRuta.Add("Ruta " & idRuta & " - " & strMsg)
            End If
            ' Despliega errores, advertencias o mensajes de información 
            If Not pGPMessages Is Nothing Then
                For i = 0 To pGPMessages.Count - 1
                    Select Case pGPMessages.GetMessage(i).Type
                        Case esriGPMessageType.esriGPMessageTypeError
                            repRuta.Add("Ruta " & idRuta & " - Error " & Str(pGPMessages.GetMessage(i).ErrorCode) & " " & pGPMessages.GetMessage(i).Description)
                        Case esriGPMessageType.esriGPMessageTypeWarning
                            repRuta.Add("Ruta " & idRuta & " - Advertencia " & Str(pGPMessages.GetMessage(i).ErrorCode) & pGPMessages.GetMessage(i).Description)
                        Case Else
                            repRuta.Add("Ruta " & idRuta & " - Información " & pGPMessages.GetMessage(i).Description)
                    End Select
                Next i
            End If
            'Guarda la ruta en un feature class
            iLongitud = guardarRuta(m_pNAContext, "Routes", idRuta, strImpedancia)
        Catch ex As Exception
            global_trace.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.Warning)
        End Try
        Return repRuta
    End Function

    'Rutina que inicializa las variables necesarias para la ejecución del generador de rutas
    Public Sub initializeRutas(ByRef idAIE As Integer)

        Dim pNetworkDataset As INetworkDataset
        Dim pFeatWS As IFeatureWorkspace
        Dim strExpresion As String
        Dim nombreLNA As String = "Network Dataset"


        Try
            'Abrir Network Dataset
            strExpresion = "ID_PMEE = " & idAIE
            pFeatWS = global_redNodal.geoadapter.dataprovider.getWorkspace
            pNetworkDataset = AbrirNetworkDataset(pFeatWS, "Red_Nodal_ND")

            ' Crear NAContext y NASolver
            m_pNAContext = crearSolverContext(pNetworkDataset)
            global_map = global_redNodal.geoadapter.getMapa()
            'global_Impedancia = "Meters"

            ' carga las paradas desde un FC
            Dim pInputFClass As IFeatureClass
            pInputFClass = pFeatWS.OpenFeatureClass("punto_temp")
            LoadNANetworkLocations(m_pNAContext, "Stops", pInputFClass, 500, strExpresion)

        Catch ex As Exception
            global_trace.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.Error)
        End Try

    End Sub

    'Función que que almacena la ruta generada dentro de un feature class específico
    Private Function guardarRuta( _
        ByRef pContext As INAContext, _
        ByRef strNAClass As String, _
        ByRef idAIE As Integer, _
        ByRef strImpedancia As String _
    ) As Integer

        Dim pFC As IFeatureClass
        guardarRuta = 0
        Try
            pFC = pContext.NAClasses.ItemByName(strNAClass)
            If pFC Is Nothing Then
                Exit Function
            End If

            Dim pFCursor As IFeatureCursor
            Dim pFeat As IFeature
            pFCursor = pFC.Search(Nothing, False)

            pFeat = pFCursor.NextFeature
            If Not pFeat Is Nothing Then
                guardarRuta = CInt(pFeat.Value(pFC.Fields.FindField("Total_" & strImpedancia)))
                insertarFeatures(_fcRutas, pFeat.Shape, idAIE, "ID_PMEE")
            End If
        Catch ex As Exception
            guardarRuta = 0
            global_trace.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.Warning)
        End Try

    End Function


#End Region
#Region "GeoAdapter_Flash"
    Public Sub flashFeature(ByRef pFeature As IFeature)

        ' Dibuja el feature en la pantalla
        Dim pActiveView As IActiveView
        pActiveView = global_IMxDocument.ActiveView
        pActiveView.ScreenDisplay.StartDrawing(0, esriScreenCache.esriNoScreenCache)

        ' Cambia la función de acuerdo a la geometría
        Select Case pFeature.Shape.GeometryType
            Case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolyline
                flashLine((pActiveView.ScreenDisplay), (pFeature.Shape))
            Case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolygon
                flashPolygon((pActiveView.ScreenDisplay), (pFeature.Shape))
            Case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPoint
                flashPoint((pActiveView.ScreenDisplay), (pFeature.Shape))
        End Select
        pActiveView.ScreenDisplay.FinishDrawing()

    End Sub

    Private Sub flashLine(ByRef pDisplay As IScreenDisplay, ByRef pGeometry As IGeometry)

        Dim pLineSymbol As ISimpleLineSymbol
        Dim pSymbol As ISymbol
        Dim pRGBColor As IRgbColor

        pLineSymbol = New SimpleLineSymbol
        pLineSymbol.Width = 4

        pRGBColor = New RgbColor
        pRGBColor.Green = 128

        pSymbol = pLineSymbol
        pSymbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen

        pDisplay.SetSymbol(pLineSymbol)
        pDisplay.DrawPolyline(pGeometry)
        System.Threading.Thread.Sleep(300)
        pDisplay.DrawPolyline(pGeometry)

    End Sub

    Private Sub flashPolygon(ByRef pDisplay As IScreenDisplay, ByRef pGeometry As IGeometry)

        Dim pFillSymbol As ISimpleFillSymbol
        Dim pSymbol As ISymbol
        Dim pRGBColor As IRgbColor

        pFillSymbol = New SimpleFillSymbol
        pFillSymbol.Outline = Nothing

        pRGBColor = New RgbColor
        pRGBColor.Green = 128

        pSymbol = pFillSymbol
        pSymbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen

        pDisplay.SetSymbol(pFillSymbol)
        pDisplay.DrawPolygon(pGeometry)
        System.Threading.Thread.Sleep(300)
        pDisplay.DrawPolygon(pGeometry)

    End Sub

    Private Sub flashPoint(ByRef pDisplay As IScreenDisplay, ByRef pGeometry As IGeometry)

        Dim pMarkerSymbol As ISimpleMarkerSymbol
        Dim pSymbol As ISymbol
        Dim pRGBColor As IRgbColor

        pMarkerSymbol = New SimpleMarkerSymbol
        pMarkerSymbol.Style = esriSimpleMarkerStyle.esriSMSCircle

        pRGBColor = New ESRI.ArcGIS.Display.RgbColor
        pRGBColor.Green = 128

        pSymbol = pMarkerSymbol
        pSymbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen

        pDisplay.SetSymbol(pMarkerSymbol)
        pDisplay.DrawPoint(pGeometry)
        System.Threading.Thread.Sleep(300)
        pDisplay.DrawPoint(pGeometry)

    End Sub
#End Region
#Region "GeoAdapter_Helpers"
    Public Function getKey(ByRef pFeature As IFeature, ByRef iFCount As Integer) As String
        Dim strPrefix, result As String

        If pFeature.Class.ObjectClassID < 0 Then
            strPrefix = pFeature.Class.AliasName
        Else
            strPrefix = CStr(pFeature.Class.ObjectClassID)
        End If

        If pFeature.HasOID Then
            result = strPrefix & "_" & CStr(pFeature.OID)
        Else
            result = strPrefix & "_" & CStr(iFCount)
        End If
        Return result
    End Function

    Public Function getString(ByRef pFeature As IFeature) As String
        Dim pFeatureClass As IFeatureClass
        Dim i As Integer

        ' Busca el primer campo de tipo string si no existe utiliza el OID

        For i = 0 To pFeature.Fields.FieldCount - 1
            pFeatureClass = pFeature.Class
            If (UCase(pFeature.Fields.Field(i).AliasName) <> UCase(pFeatureClass.ShapeFieldName)) Then
                If pFeature.Fields.Field(i).Type = esriFieldType.esriFieldTypeString Then
                    If Not IsDBNull(pFeature.Value(i)) Then
                        getString = pFeature.Value(i)
                        Exit Function
                    End If
                End If
            End If
        Next i

        Return CStr(pFeature.OID)
    End Function

    Public Function convertPixelsToMapUnits(ByRef pActiveView As IActiveView, ByRef pixelUnits As Double) As Double
        Dim realWorldDisplayExtent As Double
        Dim pixelExtent As Short
        Dim sizeOfOnePixel As Double
        Dim deviceRect As tagRECT
        deviceRect = pActiveView.ScreenDisplay.DisplayTransformation.DeviceFrame
        pixelExtent = deviceRect.right - deviceRect.left
        realWorldDisplayExtent = pActiveView.ScreenDisplay.DisplayTransformation.VisibleBounds.Width
        sizeOfOnePixel = realWorldDisplayExtent / pixelExtent
        convertPixelsToMapUnits = pixelUnits * sizeOfOnePixel
    End Function

#End Region


End Class
