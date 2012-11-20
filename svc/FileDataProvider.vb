Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.DataSourcesGDB
Public Class FileDataProvider
    Implements DataProvider
    Private pWS As IWorkspace
    Private _cache As New Dictionary(Of String, IFeatureClass)
    Private _simbologiaPath As String
    Sub New(ByVal rutaPGDB As String, ByRef rutaSimbologia As String)
        Dim pWorkspaceFactory As IWorkspaceFactory
        Try
            _simbologiaPath = rutaSimbologia
            pWorkspaceFactory = New AccessWorkspaceFactory
            pWS = pWorkspaceFactory.OpenFromFile(rutaPGDB, 0)
        Catch ex As Exception
            Throw New ConfigException(RedNodal.My.Resources.strErrDBNotFound, ex)
        End Try
    End Sub
    Public Function getWorkspace() As IWorkspace Implements DataProvider.getWorkspace
        Return pWS
    End Function
    Public Function abrirTabla(ByVal NombreTabla As String) As ITable Implements DataProvider.abrirTabla
        '++ Función que abre una tabla a partir de la definición del Workspace donde se encuentra
        Dim pFeatureWorkSpace As IFeatureWorkspace
        abrirTabla = Nothing
        Try
            pFeatureWorkSpace = pWS
            If (Not pFeatureWorkSpace Is Nothing) Then
                abrirTabla = pFeatureWorkSpace.OpenTable(NombreTabla)
            End If
        Catch ex As Exception
            global_trace.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.Warning)
        End Try
    End Function

    Public Function AbrirFCxWS(ByVal strNombreFC As String) As IFeatureClass Implements DataProvider.abrirFCxWS
        '++ Función que abre un Feature Class a partir de la definición del Workspace donde se encuentra
        Dim result As IFeatureClass
        Dim pFeatureWorkSpace As IFeatureWorkspace

        result = Nothing
        pFeatureWorkSpace = pWS
        If (Not pFeatureWorkSpace Is Nothing) Then
            Try
                If _cache.ContainsKey(strNombreFC) Then
                    result = _cache.Item(strNombreFC)
                Else
                    result = pFeatureWorkSpace.OpenFeatureClass(strNombreFC)
                    _cache.Add(strNombreFC, result)
                End If
            Catch ex As Exception
                Throw New FeatureNoAccesibleException(String.Format(RedNodal.My.Resources.strErrFeatureNoAccesible, strNombreFC), ex)
            End Try
        End If
        Return result
    End Function

    Public Function getPathSimbologia() As String Implements DataProvider.getPathSimbologia
        Return _simbologiaPath
    End Function
End Class
