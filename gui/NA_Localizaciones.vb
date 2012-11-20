Imports ESRI.ArcGIS.NetworkAnalyst
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Carto

Module NA_Localizaciones
    Public Sub LoadNANetworkLocations(ByRef pContext As INAContext, ByVal strNAClassName As String, ByVal pInputFC As IFeatureClass, ByVal SnapTolerance As Double, ByVal strExpresion As String)

        Dim pNAClass As INAClass
        Dim pClasses As INamedSet
        Dim pQF As IQueryFilter

        Try
            pClasses = pContext.NAClasses
            pNAClass = pClasses.ItemByName(strNAClassName)
            pQF = New QueryFilter
            pQF.WhereClause = strExpresion
            ' Borra las paradas existentes excepto si son barreras
            pNAClass.DeleteAllRows()

            ' Crea un NAClassLoader y asigna la tolerancia (unidades: metros)
            Dim pLoader As INAClassLoader
            pLoader = New NAClassLoader
            pLoader.Locator = pContext.Locator
            If SnapTolerance > 0 Then pLoader.Locator.SnapTolerance = SnapTolerance
            pLoader.NAClass = pNAClass
            'Crea un mapeador de campos para asignar automáticamente los campos del fc de entrada al NAclass
            Dim pFieldMap As INAClassFieldMap
            pFieldMap = New NAClassFieldMap

            pFieldMap.CreateMapping(pNAClass.ClassDefinition, pInputFC.Fields)
            pLoader.FieldMap = pFieldMap

            'Carga las paradas de la red
            Dim rowsIn As Integer
            Dim rowsLocated As Integer
            '+ modificar para que lea unos puntos en específico.
            pLoader.Load(pInputFC.Search(pQF, True), Nothing, rowsIn, rowsLocated)
        Catch ex As Exception
            global_trace.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.Error)
        End Try
    End Sub
End Module
