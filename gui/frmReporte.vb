Imports ESRI.ArcGIS.Geodatabase
Imports ESRI.ArcGIS.Geometry
Imports System.Runtime.InteropServices


Public Enum TipoFuncion
    Tejido = 1
    Red = 2
End Enum
Public Class frmReporte
    Private strEx As String = "NUMERO_AFE = " & global_redNodal.afe
    Private _geoAdapter As GeoAdapter
    Private Shared _singleton As frmReporte

    Public Shared ReadOnly Property singleton() As frmReporte
        Get
            If _singleton Is Nothing Then
                _singleton = New frmReporte
            End If
            Return _singleton
        End Get
    End Property
    Private Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        _geoAdapter = global_redNodal.geoadapter
    End Sub
    Private Sub rbtAFE_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtAFE.CheckedChanged
        If rbtAFE.Checked Then
            clUPZxAFE.Visible = False
        End If
    End Sub

    Private Sub rbtUPZ_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtUPZ.CheckedChanged
        Dim en As IEnumerator
        Dim listUPZxAFE As New ArrayList
        If rbtUPZ.Checked Then
            clUPZxAFE.Visible = True
            listUPZxAFE = global_redNodal.geoadapter.leerTablaUPZ("NOMBRE", strEx, TipoConsulta.Atributo)
            en = listUPZxAFE.GetEnumerator()
            While en.MoveNext()
                clUPZxAFE.Items.Add(en.Current)
            End While
        End If
    End Sub

    Private Sub btnGenerar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerar.Click
        Dim listFeat As ArrayList = Nothing
        Dim enuUPZs As IEnumerator
        Dim strUPZ As String = ""
        'Dim pFeat As IFeature
        Dim pSF As ISpatialFilter
        Dim indexIdNombre As Integer
        'Dim strNombreUPZ As String
        Dim strNombreAfe As String
        Dim iFila As Integer
        Dim templatePath As String
        Dim excelApp As Excel.Application = Nothing
        Dim excelWorkbook As Excel.Workbook
        Dim excelWorkSheet As Excel.Worksheet
        Dim rpt As Reporte

        Try
            btnGenerar.Enabled = False
            Me.Text = "Generando....."
            pSF = New SpatialFilter
            templatePath = global_redNodal.config.template
            excelApp = New Excel.Application()
            If excelApp IsNot Nothing Then
                excelWorkbook = excelApp.Workbooks.Open(templatePath)
                excelWorkSheet = excelWorkbook.Worksheets("Red Nodal")
                excelWorkSheet.Activate()
                rpt = New Reporte(global_redNodal.geoadapter, global_redNodal.estandar, excelWorkSheet)
                indexIdNombre = _geoAdapter.upzFC.Fields.FindField("NOMBRE")
                strNombreAfe = _geoAdapter.leerTablaAFE("NOMBRE", "NUMERO_AFE = " & global_redNodal.afe, TipoConsulta.Atributo).Item(0)
                iFila += 3
                excelWorkSheet.Cells(iFila, 1) = "AFE :"
                excelWorkSheet.Cells(iFila, 2) = strNombreAfe
                iFila += 1
                excelWorkSheet.Cells(iFila, 1) = "AÑO :"
                excelWorkSheet.Cells(iFila, 2) = global_redNodal.ano
                iFila += 1

                'Reporte x AFE
                If rbtAFE.Checked Then
                    If rbtTejido.Checked Then
                        rpt.doReportexAFE( _
                                iFila, _
                                "Colegios" _
                        )
                    Else
                        rpt.doReportexAFE( _
                                iFila, _
                                "Nodos" _
                        )
                    End If
                Else
                    'Reporte x UPZ
                    enuUPZs = clUPZxAFE.CheckedItems.GetEnumerator()
                    If rbtTejido.Checked Then
                        rpt.doReportexUPZ( _
                                enuUPZs, _
                                iFila, _
                                "Colegios" _
                        )
                    Else
                        rpt.doReportexUPZ( _
                                enuUPZs, _
                                iFila, _
                                "Nodos" _
                        )
                    End If
                End If
                excelApp.Visible = True
            End If
        Catch ex As Exception
            global_trace.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.Warning)
        Finally
            If excelApp IsNot Nothing Then
                Marshal.ReleaseComObject(excelApp)
            End If
            Me.Hide()
        End Try

    End Sub

    Private Sub rbtTejido_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtTejido.CheckedChanged
        If rbtTejido.Checked Then
            rbtRedNodal.Checked = False
        End If
    End Sub

    Private Sub rbtRedNodal_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtRedNodal.CheckedChanged
        If rbtRedNodal.Checked Then
            rbtTejido.Checked = False
        End If
    End Sub

    Private Sub frmReporte_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True
        Me.Hide()
    End Sub

    Private Sub frmReporte_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        ' Se cálculo la red nodal?
        btnGenerar.Enabled = False
        If global_redNodal.isRedNodalConstruida Then
            rbtRedNodal.Enabled = True
            btnGenerar.Enabled = True
        Else
            rbtRedNodal.Enabled = False
            rbtRedNodal.Checked = False
        End If
        ' Se cálculo el tejido educativo?
        If global_redNodal.idTejidoConstruido Then
            rbtTejido.Enabled = True
            btnGenerar.Enabled = True
        Else
            rbtTejido.Enabled = False
            rbtTejido.Checked = False
        End If
        Me.Text = "Reportes"
    End Sub
End Class