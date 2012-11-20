Imports System.Runtime.InteropServices
Imports System.Drawing
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI
Imports System.Windows.Forms
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Geometry
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.CartoUI
<ComClass(btnIdentificar.ClassId, btnIdentificar.InterfaceId, btnIdentificar.EventsId), _
 ProgId("RedNodal.IdentificarPMEE")> _
Public NotInheritable Class btnIdentificar
    Inherits BaseTool

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "325daed5-b542-4e23-bd5e-037e9c555002"
    Public Const InterfaceId As String = "9da23461-6608-4eb5-a80c-961e37ed2846"
    Public Const EventsId As String = "0166ae83-c846-476a-9a2b-01935bd31ba3"
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
    Private _geoAdapter As GeoAdapter
    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()

        ' TODO: Define values for the public properties
        ' TODO: Define values for the public properties
        MyBase.m_category = "PMEE"  'localizable text 
        MyBase.m_caption = "Identificar equipamiento"   'localizable text 
        MyBase.m_message = "Identificar equipamiento"   'localizable text 
        MyBase.m_toolTip = "Identificar equipamiento" 'localizable text 
        MyBase.m_name = "Identificar_PMEE"  'unique id, non-localizable (e.g. "MyCategory_ArcMapTool")

        Try
            MyBase.m_bitmap = RedNodal.My.Resources.bmpIdentificar
        Catch ex As Exception
            global_trace.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.Warning)
        End Try
        global_btnIdentificar = Me
        _geoAdapter = global_redNodal.geoadapter
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
    Public Overrides Sub OnMouseDown(ByVal Button As Integer, ByVal Shift As Integer, ByVal X As Integer, ByVal Y As Integer)
        Dim pPoint As IPoint
        Dim pActView As IActiveView
        Dim pFeatures As New List(Of Feature)
        Dim pFeature As IFeature = Nothing
        Dim frmIdent As frmIdentificador
        pActView = global_IMxDocument.FocusMap
        pPoint = global_IMxDocument.ActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(X, Y)
        Try
            _geoAdapter.getClosestFeature((global_IMxDocument.FocusMap), pPoint, pFeatures, pFeature)
            If (pFeature IsNot Nothing) Then
                frmIdent = New frmIdentificador
                frmIdent.populateFeatureTree(pFeatures, pFeature)
                SetWindowLong(frmIdent.Handle.ToInt32(), GWL_HWNDPARENT, global_IApplication.hWnd)
                frmIdent.Show()
            End If
        Catch ex As Exception
            global_trace.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.Warning)
        End Try
    End Sub
End Class

