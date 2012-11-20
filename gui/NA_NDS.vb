Imports ESRI.ArcGIS.Geodatabase


Module NA_NDS

    '*********************************************************************************
    ' Abrir Network Dataset
    ' ********************************************************************************
    Public Function AbrirNetworkDataset(ByVal pWorkspace As IWorkspace, ByVal sNDSNombre As String) As INetworkDataset

        Dim pFeatWS As IFeatureWorkspace
        pFeatWS = global_redNodal.geoadapter.dataprovider.getWorkspace
        Dim fds As IFeatureDataset
        fds = pFeatWS.OpenFeatureDataset("Red_Nodal")
        Dim fdsExtCont As IFeatureDatasetExtensionContainer = CType(fds, IFeatureDatasetExtensionContainer)
        Dim fdsExt As IFeatureDatasetExtension = fdsExtCont.FindExtension(esriDatasetType.esriDTNetworkDataset)
        Dim dsCont As IDatasetContainer2 = CType(fdsExt, IDatasetContainer2)

        AbrirNetworkDataset = Nothing
        Try
            AbrirNetworkDataset = dsCont.DatasetByName(esriDatasetType.esriDTNetworkDataset, sNDSNombre)
        Catch ex As Exception
            global_trace.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.Error)
        End Try
    End Function

    Public Function ObtenerDENetworkDataset(ByRef pNetDataset As INetworkDataset) As IDENetworkDataset
        'QI desde ND a DatasetComponent
        Dim pDSComponent As IDatasetComponent
        ObtenerDENetworkDataset = Nothing
        Try
            pDSComponent = pNetDataset

            'Obtener elemento (RED)
            ObtenerDENetworkDataset = pDSComponent.DataElement
        Catch ex As Exception
            global_trace.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.Error)
        End Try
    End Function
End Module
