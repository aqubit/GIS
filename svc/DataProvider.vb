Imports ESRI.ArcGIS.Geodatabase

Public Interface DataProvider
    Function getPathSimbologia() As String
    Function getWorkspace() As IWorkspace
    Function abrirTabla(ByVal NombreTabla As String) As ITable
    Function abrirFCxWS(ByVal NombreFC As String) As IFeatureClass
End Interface
