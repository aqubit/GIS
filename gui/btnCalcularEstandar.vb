Imports System.Runtime.InteropServices
Imports System.Drawing
Imports ESRI.ArcGIS.ADF.BaseClasses
Imports ESRI.ArcGIS.ADF.CATIDs
Imports ESRI.ArcGIS.Framework
Imports ESRI.ArcGIS.ArcMapUI
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.esriSystem.esriExtensionState

<ComClass(btnCalcularEstandar.ClassId, btnCalcularEstandar.InterfaceId, btnCalcularEstandar.EventsId), _
 ProgId("RedNodal.CalcularEstandar")> _
Public NotInheritable Class btnCalcularEstandar
    Inherits BaseCommand

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "a32377d2-ba77-4495-a47a-b6c1c7a20c81"
    Public Const InterfaceId As String = "9db0ed0b-f470-4b44-b26d-6d6d2f9b580b"
    Public Const EventsId As String = "0f957fe9-458e-47e1-93dc-ec25dbfc01cf"
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
        MyBase.m_caption = "Estándar"   'localizable text 
        MyBase.m_message = "Estándar"   'localizable text 
        MyBase.m_toolTip = "Estándar" 'localizable text 
        MyBase.m_name = "Estándar"  'unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")

        'Try
        '    Dim bitmapResourceName As String = Me.GetType().Name + "nada.bmp"
        '    MyBase.m_bitmap = New Bitmap(Me.GetType(), bitmapResourceName)
        'Catch ex As Exception
        '    MsgBox("")  global_trace.WriteEntry(ex.Message, "Invalid Bitmap")
        'End Try
        global_btnCalcularEstandar = Me

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
        global_redNodal.evaluarDemandaOferta()
        global_btnIdentificar.enable = True
        global_btnReporte.enable = True
        If checkExtension() Then
            global_btnRedNodal.enable = True
            global_btnTejido.enable = True
        End If
    End Sub

    Private Function checkExtension() As Boolean
        Dim pExten As IExtension
        Dim pExtenconfig As IExtensionConfig
        Dim uid As New UID
        Dim result As Boolean

        result = False
        uid.Value = "{C967BD39-1118-42EE-AAAB-B31642C89C3E}"
        Try
            pExten = m_application.FindExtensionByCLSID(uid)
            If pExten IsNot Nothing Then
                pExtenconfig = pExten
                If pExtenconfig.State = esriESUnavailable Then
                    MsgBox(RedNodal.My.Resources.strErrNoExtensionAvalaible, MsgBoxStyle.Critical, "Red Nodal")
                    pExtenconfig.State = esriESDisabled
                ElseIf pExtenconfig.State = esriESDisabled Then
                    'Habilitar la extensión
                    pExtenconfig.State = esriESEnabled
                    result = True
                Else
                    'Ya estaba habilitada la extensión
                    result = True
                End If
            Else
                MsgBox(RedNodal.My.Resources.strErrNoExtensionAvalaible, MsgBoxStyle.Critical, "Red Nodal")
                result = False
            End If
        Catch ex As Exception
            MsgBox(RedNodal.My.Resources.strErrNoExtensionAvalaible, MsgBoxStyle.Critical, "Red Nodal")
            result = False
        End Try
        Return result
    End Function
End Class



