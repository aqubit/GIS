Imports ESRI.ArcGIS.Carto
Imports ESRI.ArcGIS.Geodatabase
Imports System.Drawing
Imports System.Windows.Forms.Keys

Public Class frmIdentificador
    Private _featuresInTree As New Dictionary(Of String, IFeature)
    Private _geoAdapter As GeoAdapter
    Private _estandar As Estandar
    Private _iIndexColumnaTotales As Integer = 1
    Private _iIndexColumnaAso As Integer = 2
    Private _iIndexColumnaSolved As Integer = 3
    Private _iIndexFilaBBA As Integer = 2
    Private _iIndexFilaLBO As Integer = 3
    Private _iIndexFilaTAS As Integer = 4
    Private _iIndexFilaAMS As Integer = 5
    Private _iIndexFilaAME As Integer = 6
    Private _iIndexFilaALE As Integer = 7
    Private _iIdAnterior As Integer

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _geoAdapter = global_redNodal.geoadapter
        _estandar = global_redNodal.estandar
    End Sub

    Public Sub populateFeatureTree( _
        ByRef pFeatures As List(Of Feature), _
        ByRef pClosestFeature As IFeature _
    )
        Dim dictAdded As IDictionary = New Dictionary(Of String, String)
        tvFeatures.Nodes.Clear()
        Try
            Dim i As Integer
            Dim strAliasName As String
            Dim pNode As System.Windows.Forms.TreeNode
            Dim strKey, strField As String

            For Each pFeature As IFeature In pFeatures
                strAliasName = pFeature.Class.AliasName
                If Not dictAdded.Contains(strAliasName) Then
                    dictAdded.Add(strAliasName, strAliasName)
                    tvFeatures.Nodes.Add(strAliasName, strAliasName)
                End If
                strKey = _geoAdapter.getKey(pFeature, i)
                strField = _geoAdapter.getString(pFeature)
                pNode = tvFeatures.Nodes.Find(strAliasName, True)(0).Nodes.Add(strKey, strField)
                If pNode IsNot Nothing And pNode.Parent IsNot Nothing Then
                    pNode.EnsureVisible()
                    If Not _featuresInTree.ContainsKey(strKey) Then
                        _featuresInTree.Add(strKey, pFeature)
                        If (pClosestFeature Is pFeature) Then
                            pNode.Checked = True
                        End If
                    End If
                End If
                i += 1
            Next
            llenarInformacion(pClosestFeature)
            _geoAdapter.flashFeature(pClosestFeature)
        Catch ex As Exception
            global_trace.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.Warning)
        End Try
    End Sub

    Private Sub llenarInformacion(ByRef pFeature As IFeature)

        Dim dictEstandar As New Dictionary(Of Windows.Forms.Label, EstandarParametro)
        Dim dictTotal As New Dictionary(Of Windows.Forms.Label, Long)
        Dim dictFinal As New Dictionary(Of Windows.Forms.Label, Long)
        Dim dictAso As New Dictionary(Of Windows.Forms.Label, Long)
        Dim eqoAntes As IAsociable = Nothing
        Dim eqoDespues As IAsociable = Nothing
        Dim eqoDefAso As IAsociable = Nothing
        Dim target As IAsociable
        Dim idPmee As Integer
        Dim iDeficitTotal, iDeficitSolved, iDeficitAso, iDeficitFinal As Long
        Dim parametro As EstandarParametro
        Dim pFC As IFeatureClass
        Dim lbl, lblSolved, lblAsociacion As Windows.Forms.Label

        pFC = pFeature.Class
        If pFC.AliasName.Equals("Colegio") Then
            idPmee = pFeature.Value(pFC.Fields.FindField("ID_PMEE"))
            target = New Colegio
            target.idPmee = idPmee
            eqoDespues = global_redNodal.buscarColegio(target)
            eqoAntes = eqoDespues.getSavedState()
            eqoDefAso = eqoAntes.Copiar()
            _estandar.doObtenerDeficitAsociacion(eqoDefAso)
        ElseIf pFC.AliasName.Equals("Equipamiento") Then
            idPmee = pFeature.Value(pFC.Fields.FindField("ID_PMEE"))
            target = New Equipamiento
            target.idPmee = idPmee
            eqoDespues = global_redNodal.buscarEquipamiento(target)
            eqoAntes = eqoDespues.getSavedState()
            eqoDefAso = eqoAntes.Copiar()
            _estandar.doObtenerDeficitAsociacion(eqoDefAso)
            'ElseIf pFC.AliasName.Equals("Asociacion") Then
            '    idPmee = pFeature.Value(pFC.Fields.FindField("ID_PMEE"))
            '    target = New Asociacion
            '    target.idPmee = idPmee
            '    eqoDespues = global_redNodal.buscarAsociacion(target)
            '    eqoAntes = eqoDespues.getSavedState()
            '    eqoDefAso = eqoAntes.Copiar()
            'ElseIf pFC.AliasName.Equals("Nodo") Then
            '    idPmee = pFeature.Value(pFC.Fields.FindField("ID_PMEE"))
            '    target = New Nodo
            '    target.idPmee = idPmee
            '    eqoDespues = global_redNodal.buscarNodo(target)
            '    eqoAntes = eqoDespues.getSavedState()
            '    If eqoAntes IsNot Nothing Then eqoDefAso = eqoAntes.Copiar()
        End If
        If eqoAntes IsNot Nothing Then
            If eqoAntes.idPmee <> _iIdAnterior Then
                tblEstandar.SuspendLayout()
                'Texto en el dialog
                Text = eqoAntes.nombre & " - ID_PMEE=" & eqoAntes.idPmee
                'Biblioteca
                lbl = tblEstandar.GetControlFromPosition(_iIndexColumnaTotales, _iIndexFilaBBA)
                dictEstandar.Add(lbl, _estandar.estandarBiblioteca)
                If eqoAntes.ofertaBiblioteca >= 0 Then
                    dictTotal.Add(lbl, eqoDefAso.ofertaBiblioteca)
                Else
                    dictTotal.Add(lbl, eqoAntes.ofertaBiblioteca)
                End If
                dictAso.Add(lbl, eqoDefAso.ofertaBiblioteca)
                dictFinal.Add(lbl, eqoDespues.ofertaBiblioteca)
                'Laboratorio
                lbl = tblEstandar.GetControlFromPosition(_iIndexColumnaTotales, _iIndexFilaLBO)
                dictEstandar.Add(lbl, _estandar.estandarLaboratorio)
                If eqoAntes.ofertaLaboratorio >= 0 Then
                    dictTotal.Add(lbl, eqoDefAso.ofertaLaboratorio)
                Else
                    dictTotal.Add(lbl, eqoAntes.ofertaLaboratorio)
                End If
                dictAso.Add(lbl, eqoDefAso.ofertaLaboratorio)
                dictFinal.Add(lbl, eqoDespues.ofertaLaboratorio)
                'Taller artes
                lbl = tblEstandar.GetControlFromPosition(_iIndexColumnaTotales, _iIndexFilaTAS)
                dictEstandar.Add(lbl, _estandar.estandarTallerArtes)
                If eqoAntes.ofertaTallerArtes >= 0 Then
                    dictTotal.Add(lbl, eqoDefAso.ofertaTallerArtes)
                Else
                    dictTotal.Add(lbl, eqoAntes.ofertaTallerArtes)
                End If
                dictAso.Add(lbl, eqoDefAso.ofertaTallerArtes)
                dictFinal.Add(lbl, eqoDespues.ofertaTallerArtes)
                'Multimedios
                lbl = tblEstandar.GetControlFromPosition(_iIndexColumnaTotales, _iIndexFilaAMS)
                dictEstandar.Add(lbl, _estandar.estandarAulaMultimedios)
                If eqoAntes.ofertaAulaMultimedios >= 0 Then
                    dictTotal.Add(lbl, eqoDefAso.ofertaAulaMultimedios)
                Else
                    dictTotal.Add(lbl, eqoAntes.ofertaAulaMultimedios)
                End If
                dictAso.Add(lbl, eqoDefAso.ofertaAulaMultimedios)
                dictFinal.Add(lbl, eqoDespues.ofertaAulaMultimedios)
                'Aula múltiple
                lbl = tblEstandar.GetControlFromPosition(_iIndexColumnaTotales, _iIndexFilaAME)
                dictEstandar.Add(lbl, _estandar.estandarAulaMultiple)
                If eqoAntes.ofertaAulaMultiple >= 0 Then
                    dictTotal.Add(lbl, eqoDefAso.ofertaAulaMultiple)
                Else
                    dictTotal.Add(lbl, eqoAntes.ofertaAulaMultiple)
                End If
                dictAso.Add(lbl, eqoDefAso.ofertaAulaMultiple)
                dictFinal.Add(lbl, eqoDespues.ofertaAulaMultiple)
                'Area libre
                lbl = tblEstandar.GetControlFromPosition(_iIndexColumnaTotales, _iIndexFilaALE)
                dictEstandar.Add(lbl, _estandar.estandarAreaLibre)
                If eqoAntes.ofertaAreaLibre >= 0 Then
                    dictTotal.Add(lbl, eqoDefAso.ofertaAreaLibre)
                Else
                    dictTotal.Add(lbl, eqoAntes.ofertaAreaLibre)
                End If
                dictAso.Add(lbl, eqoDefAso.ofertaAreaLibre)
                dictFinal.Add(lbl, eqoDespues.ofertaAreaLibre)
                'Crear labels para valores en asociación y solved
                Dim iPrimeraFila As Integer = 2
                For Each lbl In dictEstandar.Keys
                    parametro = dictEstandar.Item(lbl)
                    iDeficitTotal = dictTotal.Item(lbl)
                    iDeficitFinal = dictFinal.Item(lbl)
                    lblSolved = tblEstandar.GetControlFromPosition(_iIndexColumnaSolved, iPrimeraFila)
                    lblAsociacion = tblEstandar.GetControlFromPosition(_iIndexColumnaAso, iPrimeraFila)
                    If iDeficitTotal < 0 Then
                        iDeficitAso = dictAso.Item(lbl)
                        iDeficitSolved = Math.Abs(iDeficitAso - iDeficitFinal)
                        dictTotal.Add(lblAsociacion, iDeficitAso)
                        dictTotal.Add(lblSolved, iDeficitSolved)
                    Else
                        lbl.Text = iDeficitTotal & " | " & iDeficitFinal
                        lbl.ForeColor = Color.Black
                        dictTotal.Remove(lbl)
                        dictTotal.Add(lblAsociacion, 0)
                        dictTotal.Add(lblSolved, 0)
                    End If
                    iPrimeraFila += 1
                Next
                'Aplicar formato a todos los labels dependiendo de su valor 
                Dim iLblValue As Integer
                For Each lbl In dictTotal.Keys
                    iLblValue = dictTotal.Item(lbl)
                    If iLblValue < 0 Then
                        lbl.ForeColor = Color.Red
                    Else
                        lbl.ForeColor = Color.Black
                    End If
                    lbl.Text = iLblValue.ToString()
                Next
                _iIdAnterior = eqoAntes.idPmee
                tblEstandar.ResumeLayout(False)
                tblEstandar.PerformLayout()
            End If
        End If
    End Sub

    Private Sub selectFeature()

        Dim pFeature As IFeature
        Try
            If _featuresInTree IsNot Nothing And _
                tvFeatures IsNot Nothing _
            Then
                If (tvFeatures.SelectedNode IsNot Nothing) Then
                    Dim bContains As Boolean
                    bContains = _featuresInTree.ContainsKey(tvFeatures.SelectedNode.Name)
                    If bContains Then
                        pFeature = _featuresInTree.Item(tvFeatures.SelectedNode.Name)
                        If pFeature IsNot Nothing Then
                            llenarInformacion(pFeature)
                            _geoAdapter.flashFeature(pFeature)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            global_trace.WriteEntry(ex.Message, System.Diagnostics.EventLogEntryType.Warning)
        End Try
    End Sub

    Private Sub tvFeatures_AfterSelect(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvFeatures.AfterSelect
        selectFeature()
    End Sub

    Private Sub trvFeatures_KeyUp(ByVal eventSender As System.Object, ByVal eventArgs As System.Windows.Forms.KeyEventArgs) Handles tvFeatures.KeyUp
        Dim keyCode As Short = eventArgs.KeyCode
        Dim Shift As Short = eventArgs.KeyData \ &H10000
        If (keyCode = Windows.Forms.Keys.Escape) Then
            Me.Close()
        Else
            selectFeature()
        End If

    End Sub

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub

    Private Sub tvFeatures_NodeMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles tvFeatures.NodeMouseClick
        selectFeature()
    End Sub
End Class