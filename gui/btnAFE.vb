Imports System.Runtime.InteropServices
Imports System.Drawing
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.esriSystem

<ComClass(btnAFE.ClassId, btnAFE.InterfaceId, btnAFE.EventsId), _
 ProgId("RedNodal.AFE")> _
Public NotInheritable Class btnAFE
    Inherits BaseCommand

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "37e2d3be-50cf-443c-bd27-a190eab33613"
    Public Const InterfaceId As String = "7d80dd70-aaa6-4a56-a730-b00a26ff0c67"
    Public Const EventsId As String = "bc1f3d3b-80da-4698-9eaa-d56645e1454d"
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
    'Private m_dockableWindow As IDockableWindow
    'Private Const DockableWindowGuid As String = "{2fcbe2b8-6b1d-4943-a77c-bd997db7b9bc}"

    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()

        ' TODO: Define values for the public properties
        MyBase.m_category = "PMEE"  'localizable text 
        MyBase.m_caption = "AFE"   'localizable text 
        MyBase.m_message = "Definir AFE"   'localizable text 
        MyBase.m_toolTip = "Definir AFE" 'localizable text 
        MyBase.m_name = "AFE"  'unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")

        Try
            MyBase.m_bitmap = RedNodal.My.Resources.bmpAfe
        Catch ex As Exception
            global_trace.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.Warning)
        End Try
        global_btnAFE = Me
    End Sub
    Public Property enable() As Boolean
        Get
            Return MyBase.m_enabled
        End Get
        Set(ByVal value As Boolean)
            MyBase.m_enabled = value
        End Set
    End Property
    ''' <summary>
    ''' Occurs when this command is created
    ''' </summary>
    ''' <param name="hook">Instance of the application</param>
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

    ''' <summary>
    ''' Toggle visiblity of dockable window and show the visible state by its checked property
    ''' </summary>
    Public Overrides Sub OnClick()
        Dim wnd As frmAFE2 = frmAFE2.singleton()
        SetWindowLong(wnd.Handle.ToInt32(), GWL_HWNDPARENT, global_IApplication.hWnd)
        wnd.Show()
    End Sub
End Class



