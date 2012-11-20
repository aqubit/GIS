Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports System.Runtime.InteropServices

<ComClass(tbPMEE.ClassId, tbPMEE.InterfaceId, tbPMEE.EventsId), _
 ProgId("RedNodal.PMEE")> _
Public NotInheritable Class tbPMEE
    Inherits BaseToolbar

#Region "COM Registration Function(s)"
    <ComRegisterFunction(), ComVisibleAttribute(False)> _
    Public Shared Sub RegisterFunction(ByVal registerType As Type)
        ' Required for ArcGIS Component Category Registrar support

        ArcGISCategoryRegistration(registerType)

        'Add any COM registration code after the ArcGISCategoryRegistration() call

    End Sub

    <ComUnregisterFunction(), ComVisibleAttribute(False)> _
    Public Shared Sub UnregisterFunction(ByVal registerType As Type)
        ' Required for ArcGIS Component Category Registrar support
        ArcGISCategoryUnregistration(registerType)

        'Add any COM unregistration code after the ArcGISCategoryUnregistration() call

    End Sub

#Region "ArcGIS Component Category Registrar generated code"
    ''' <summary>
    ''' Required method for ArcGIS Component Category registration -
    ''' Do not modify the contents of this method with the code editor.
    ''' </summary>
    Private Shared Sub ArcGISCategoryRegistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        MxCommandBars.Register(regKey)

    End Sub
    ''' <summary>
    ''' Required method for ArcGIS Component Category unregistration -
    ''' Do not modify the contents of this method with the code editor.
    ''' </summary>
    Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        MxCommandBars.Unregister(regKey)

    End Sub

#End Region
#End Region

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "91786ad0-de3d-478f-8754-0e74257e3520"
    Public Const InterfaceId As String = "42a01359-b71c-47b0-9ec2-0fbf26b7ade1"
    Public Const EventsId As String = "92334e1d-364f-40f8-a747-00d14987d5ac"
#End Region

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        'Red Nodal
        Dim dataProvider As FileDataProvider
        Dim config As New ConfigRedNodal
        Dim bErrorOccurred As Boolean = False
        If (Not global_bInitializationError) And (Not global_bInitialized) Then
            Try
                Dim wndInicio As New frmSplashScreen
                wndInicio.ShowDialog()
                config.cargar()
                dataProvider = New FileDataProvider(config.geodatabase, config.simbologia)
                global_redNodal = New RedNodalAlg(dataProvider, config)
                global_bInitialized = True
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical, "Red Nodal")
                global_trace.WriteEntry(ex.ToString, System.Diagnostics.EventLogEntryType.Error)
                global_bInitializationError = True
            End Try
        End If
        AddItem("RedNodal.AFE")
        AddItem("RedNodal.CalcularEstandar")
        AddItem("RedNodal.IdentificarPMEE")
        AddItem("RedNodal.btnGenerarNodoF")
        AddItem("RedNodal.TejidoEdu")
        AddItem("RedNodal.btnRedNodal")
        AddItem("RedNodal.btnReporte")
        AddItem("RedNodal.btnConfig")
    End Sub

    Public Overrides ReadOnly Property Caption() As String
        Get
            'TODO: Replace bar caption
            Return "PMEE"
        End Get
    End Property

    Public Overrides ReadOnly Property Name() As String
        Get
            'TODO: Replace bar ID
            Return "PMEE"
        End Get
    End Property
End Class
