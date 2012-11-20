Imports System.Runtime.InteropServices
Imports System.Drawing
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
'Progress dialog
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Framework.esriProgressAnimationTypes

<ComClass(btnGenerarNodoF.ClassId, btnGenerarNodoF.InterfaceId, btnGenerarNodoF.EventsId), _
 ProgId("RedNodal.btnGenerarNodoF")> _
Public NotInheritable Class btnGenerarNodoF
    Inherits BaseCommand
    Private _geoadapter As GeoAdapter
    Private _config As ConfigRedNodal

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "11701cf1-356e-485f-b88e-861407d65804"
    Public Const InterfaceId As String = "ee08ef1d-4c27-4e0d-ac5a-7734cbd83a60"
    Public Const EventsId As String = "5ee38c32-3bc9-4232-b089-e5f98fb043cf"
#End Region

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
    Private Shared Sub ArcGISCategoryRegistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        MxCommands.Register(regKey)

    End Sub
    Private Shared Sub ArcGISCategoryUnregistration(ByVal registerType As Type)
        Dim regKey As String = String.Format("HKEY_CLASSES_ROOT\CLSID\{{{0}}}", registerType.GUID)
        MxCommands.Unregister(regKey)

    End Sub

#End Region
#End Region


    Private m_application As IApplication

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()

        ' TODO: Define values for the public properties
        MyBase.m_category = "PMEE"  'localizable text 
        MyBase.m_caption = "Generar Nodos Funcionales"   'localizable text 
        MyBase.m_message = "Generar Nodos Funcionales"   'localizable text 
        MyBase.m_toolTip = "Generar Nodos Funcionales" 'localizable text 
        MyBase.m_name = "GenerarNodoF"  'unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")

        Try
            MyBase.m_bitmap = RedNodal.My.Resources.bmpNodos
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap")
        End Try
        global_btnGenerarNodoF = Me
        _geoadapter = global_redNodal.geoadapter
    End Sub
    Public Property enable() As Boolean
        Get
            Return MyBase.m_enabled
        End Get
        Set(ByVal value As Boolean)
            MyBase.m_enabled = value
        End Set
    End Property

    Public Overrides Sub OnCreate(ByVal hook As Object)
        If Not hook Is Nothing Then
            m_application = CType(hook, IApplication)
            'Disable if it is not ArcMap
            If TypeOf hook Is IMxApplication Then
                If (global_IMxApplication Is Nothing) Then
                    global_IMxApplication = hook
                    global_IApplication = CType(hook, IApplication)
                    global_IMxDocument = CType(global_IApplication.Document, IMxDocument)
                    global_map = CType(global_IMxDocument.FocusMap, Map)
                End If
            End If
            MyBase.m_enabled = False
        End If
    End Sub

    Public Overrides Sub OnClick()
        'Progress dialog
        Dim pProDlgFact As IProgressDialogFactory
        Dim pProDlg As IProgressDialog2
        Dim pTrkCan As ITrackCancel
        ' Create a CancelTracker
        pTrkCan = New CancelTracker
        ' Create the ProgressDialog. This automatically displays the dialog
        pProDlgFact = New ProgressDialogFactory
        pProDlg = pProDlgFact.Create(pTrkCan, global_IMxDocument.ActiveView.ScreenDisplay.hWnd)
        ' Set the properties of the ProgressDialog
        pProDlg.CancelEnabled = False
        pProDlg.Title = "Buscando nodos funcionales..."
        pProDlg.Animation = esriProgressGlobe
        global_redNodal.generarNodosFuncionales(pProDlg)
        pProDlg.HideDialog()
        global_redNodal.geoadapter.refreshMap()
    End Sub
End Class



