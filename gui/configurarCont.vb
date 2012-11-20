Imports ESRI.ArcGIS.NetworkAnalyst
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Carto

Module configurarCont

    'Private pNAContext As INAContext

    'Public Sub CrearContexto()

    '    Dim pMapa As IMap = Herramientas.ObtenerMapa
    '    Dim pNetworkDataset As INetworkDataset
    '    pNAContext = CreateSolverContext(pNetworkDataset)

    '    Dim pNetworkAttribute As ESRI.ArcGIS.Geodatabase.INetworkAttribute
    '    Dim i As Integer
    '    For i = 0 To pNetworkDataset.AttributeCount - 1
    '        pNetworkAttribute = pNetworkDataset.Attribute(i)
    '        If pNetworkAttribute.UsageType = ESRI.ArcGIS.Geodatabase.esriNetworkAttributeUsageType.esriNAUTCost Then
    '            'CmdCostAttribute.Items.Add(pNetworkAttribute.Name)
    '        End If
    '    Next i
    '    'CmdCostAttribute.SelectedIndex = 0

    '    ' Load locations from FC
    '    Dim pInputFClass As ESRI.ArcGIS.Geodatabase.IFeatureClass
    '    pInputFClass = pWS.OpenFeatureClass("Colegio")
    '    LoadNANetworkLocations(pNAContext, "Paradas", pInputFClass, 100)

    '    Dim pLayer As ILayer
    '    Dim pNetworkLayer As INetworkLayer
    '    pNetworkLayer = New NetworkLayer
    '    pNetworkLayer.NetworkDataset = pNetworkDataset
    '    pLayer = pNetworkLayer
    '    pLayer.Name = "Network Dataset"
    '    pMapa.AddLayer(pLayer)

    '    'Create a Network Analysis Layer and add to ArcMap
    '    Dim pNALayer As INALayer
    '    pNALayer = pNAContext.Solver.CreateLayer(m_pNAContext)
    '    pLayer = pNALayer
    '    pLayer.Name = m_pNAContext.Solver.DisplayName
    '    pMapa.AddLayer(pLayer)

    '    CmdSolve.Visible = True
    '    lstOutput.Items.Clear()

    'End Sub



End Module
