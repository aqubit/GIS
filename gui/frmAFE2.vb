Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.DataSourcesGDB
Imports ESRI.ArcGIS.Carto
'Progress dialog
Imports ESRI.ArcGIS.esriSystem
Imports ESRI.ArcGIS.Display
Imports ESRI.ArcGIS.Framework.esriProgressAnimationTypes
Imports ESRI.ArcGIS.Framework


Public Class frmAFE2
    Private _dataProvider As DataProvider
    Private _geoAdapter As GeoAdapter
    Private Shared _singleton As frmAFE2

    Public Shared ReadOnly Property singleton() As frmAFE2
        Get
            If _singleton Is Nothing Then
                _singleton = New frmAFE2
            End If
            Return _singleton
        End Get
    End Property

    Private Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        _dataProvider = global_redNodal.geoadapter.dataprovider
        _geoAdapter = global_redNodal.geoadapter
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub frmAFE_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True
        Me.Hide()
    End Sub
    Private Sub frmAFE_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim listaAfe As ArrayList
        Dim listaAños As ArrayList
        Dim valorAño As Integer
        Dim en As IEnumerator

        'Poblar dropdown afes
        listaAfe = _geoAdapter.leerTablaAFE("NOMBRE", "", TipoConsulta.Atributo)
        en = listaAfe.GetEnumerator()
        While en.MoveNext()
            cbxAFE.Items.Add(en.Current)
        End While
        'Poblar dropdown colegios
        listaAños = _geoAdapter.LeerTablaColegio("AÑO", "", TipoConsulta.Atributo)
        If listaAños IsNot Nothing And listaAños.Count > 0 Then
            en = listaAños.GetEnumerator()
            en.MoveNext()
            valorAño = en.Current
            cbxAño.Items.Add(valorAño)
            While en.MoveNext()
                valorAño = en.Current
                If Not cbxAño.Items.Contains(valorAño) Then
                    cbxAño.Items.Add(valorAño)
                End If
            End While
        End If
    End Sub
    Private Sub cbxAño_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbxAño.SelectedIndexChanged
        Dim anoSelected As Integer
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
        pProDlg.Title = "Cargando datos..."
        pProDlg.Animation = esriProgressGlobe

        If Not cbxAño.Text.Equals("") Then
            anoSelected = CInt(cbxAño.Text)
            If global_redNodal.cargarDatos( _
                    cbxAFE.Text, _
                    anoSelected, _
                    pProDlg _
            ) Then
                global_btnTejido.enable = False
                global_btnRedNodal.enable = False
                global_btnReporte.enable = False
                global_btnIdentificar.enable = False
                global_btnCalcularEstandar.enable = True
                global_btnGenerarNodoF.enable = True
            End If
        End If
        pProDlg.HideDialog()
    End Sub

    Private Sub cbxAFE_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbxAFE.SelectedIndexChanged
        cbxAño.SelectedIndex = -1
    End Sub
End Class