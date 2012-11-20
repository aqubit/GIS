Imports System.Runtime.InteropServices
Imports System.Drawing
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.Carto
'Progress dialog
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Framework.esriProgressAnimationTypes

<ComClass(btnTejidoEdu.ClassId, btnTejidoEdu.InterfaceId, btnTejidoEdu.EventsId), _
 ProgId("RedNodal.TejidoEdu")> _
Public NotInheritable Class btnTejidoEdu
    Inherits BaseCommand

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "7811d280-515f-4293-839c-37111931ba3d"
    Public Const InterfaceId As String = "1f5c2249-7d29-4a69-a491-5cfa2b21fb37"
    Public Const EventsId As String = "d49e22ca-ec24-4ef0-b03b-af2119a8292f"
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
        MyBase.m_caption = "Construir Tejido Educativo"   'localizable text 
        MyBase.m_message = "Construir Tejido Educativo"   'localizable text 
        MyBase.m_toolTip = "Construir Tejido Educativo" 'localizable text 
        MyBase.m_name = "TejidoEdu"  'unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")

        Try
            'TODO: change bitmap name if necessary
            MyBase.m_bitmap = RedNodal.My.Resources.bmpTejido

        Catch ex As Exception
            global_trace.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.Warning)
        End Try
        global_btnTejido = Me
    End Sub


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
    Public Property enable() As Boolean
        Get
            Return MyBase.m_enabled
        End Get
        Set(ByVal value As Boolean)
            MyBase.m_enabled = value
        End Set
    End Property

    Public Overrides Sub OnClick()
        Dim pProDlgFact As IProgressDialogFactory
        Dim pProDlg As IProgressDialog2
        Dim pTrkCan As ITrackCancel
        ' Create a CancelTracker
        pTrkCan = New CancelTracker
        ' Create the ProgressDialog. This automatically displays the dialog
        pProDlgFact = New ProgressDialogFactory
        pProDlg = pProDlgFact.Create(pTrkCan, global_IMxDocument.ActiveView.ScreenDisplay.hWnd)
        ' Set the properties of the ProgressDialog
        pProDlg.CancelEnabled = True
        pProDlg.Title = "Construyendo tejido educativo..."
        pProDlg.Animation = esriProgressGlobe
        global_redNodal.geoadapter.borrarFeaturesTemporales()

        global_redNodal.construirTejidoEducativo( _
            pProDlg, _
            pTrkCan)

        global_redNodal.crearRutas( _
            pProDlg, _
            pTrkCan)

        pProDlg.HideDialog()
        global_redNodal.geoadapter.refreshMap()
    End Sub
End Class


