Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.ADF.BaseClasses


Module variables
    Public Declare Function SetWindowLong Lib "user32" Alias "SetWindowLongA" (ByVal hwnd As Int32, ByVal nIndex As Int32, ByVal dwNewLong As Int32) As Int32
    Public Const GWL_HWNDPARENT = (-8)

    'Usadas para construir la barra de herramientas
    Public global_IMxApplication As IMxApplication
    Public global_IApplication As IApplication
    Public global_IMxDocument As IMxDocument
    Public global_map As Map
    'Red nodal business
    Public global_redNodal As RedNodalAlg
    'Toolbar buttons
    Public global_btnCalcularEstandar As btnCalcularEstandar
    Public global_btnIdentificar As btnIdentificar
    Public global_btnRedNodal As btnRedNodal
    Public global_btnTejido As btnTejidoEdu
    Public global_btnAFE As btnAFE
    Public global_btnReporte As btnReporte
    Public global_btnGenerarNodoF As btnGenerarNodoF
    Public global_btnConfig As btnConfig
    'Common
    Public global_trace As New EventLog("Application", System.Environment.MachineName, "Red Nodal")
    Public global_bInitializationError = False
    Public global_bInitialized = False
End Module
