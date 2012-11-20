Imports System.Runtime.InteropServices
Imports System.Drawing
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Carto

<ComClass(btnConfig.ClassId, btnConfig.InterfaceId, btnConfig.EventsId), _
 ProgId("RedNodal.btnConfig")> _
Public NotInheritable Class btnConfig
    Inherits BaseCommand

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "6d40175f-d445-440f-b44e-ba2020427642"
    Public Const InterfaceId As String = "e0b0985d-6ad8-486d-be9b-612ee94798ee"
    Public Const EventsId As String = "30013de1-94f2-4eb1-87dd-734c39e702b3"
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
        MyBase.m_caption = "Configuración"   'localizable text 
        MyBase.m_message = "Configuración"   'localizable text 
        MyBase.m_toolTip = "Configuración" 'localizable text 
        MyBase.m_name = "Configuración"  'unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")

        Try
            MyBase.m_bitmap = RedNodal.My.Resources.bmpConfig
        Catch ex As Exception
            System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap")
        End Try
        global_btnConfig = Me
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
            MyBase.m_enabled = global_bInitialized
        End If
    End Sub

    Public Overrides Sub OnClick()
        Dim wnd As frmConfig = frmConfig.singleton()
        SetWindowLong(wnd.Handle.ToInt32(), GWL_HWNDPARENT, global_IApplication.hWnd)
        wnd.ShowDialog()
    End Sub
End Class



