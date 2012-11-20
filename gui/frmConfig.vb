Public Class frmConfig
    Private Shared _singleton As frmConfig

    Public Shared ReadOnly Property singleton() As frmConfig
        Get
            If _singleton Is Nothing Then
                _singleton = New frmConfig
            End If
            Return _singleton
        End Get
    End Property
    Private Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
    End Sub

    Private Sub rbtDistancia_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtDistancia.CheckedChanged
        If rbtDistancia.Checked Then
            rbtTiempo.Checked = False
        End If
    End Sub

    Private Sub rbtTiempo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtTiempo.CheckedChanged
        If rbtTiempo.Checked Then
            rbtDistancia.Checked = False
        End If
    End Sub

    Private Sub frmConfig_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True
        Me.Hide()
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        If rbtDistancia.Checked Then
            global_redNodal.config.impedancia = "Meters"
        Else
            global_redNodal.config.impedancia = "Minutes"
        End If
        If chbJerarquia.Checked Then
            global_redNodal.config.hierarchy = True
        Else
            global_redNodal.config.hierarchy = False
        End If
        If rbtColNuevos.Checked Then
            global_redNodal.config.tipoestado = TipoEstado.Nuevo
        Else
            global_redNodal.config.tipoestado = TipoEstado.Intervenido
        End If

        global_redNodal.config.distanciaPeatonal = CInt(txtFldDistPeatonal.Text)
        global_redNodal.config.tiempoVehicular = CInt(txtFldTiempoVehicular.Text)
        global_redNodal.config.radioNodo = CInt(txtFldRadioNodo.Text)
        Me.Hide()
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.Hide()
    End Sub

    Private Sub frmConfig_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        If global_redNodal.config.impedancia.Equals("Meters") Then
            rbtDistancia.Checked = True
        Else
            rbtTiempo.Checked = True
        End If
        If global_redNodal.config.hierarchy Then
            chbJerarquia.Checked = True
        Else
            chbJerarquia.Checked = False
        End If
        If global_redNodal.config.tipoestado = TipoEstado.Nuevo Then
            rbtColNuevos.Checked = True
        Else
            rbtColNuevos.Checked = False
        End If

        txtFldDistPeatonal.Text = global_redNodal.config.distanciaPeatonal.ToString
        txtFldTiempoVehicular.Text = global_redNodal.config.tiempoVehicular.ToString
        txtFldRadioNodo.Text = global_redNodal.config.radioNodo.ToString
    End Sub

    Private Sub txtFldDistPeatonal_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtFldDistPeatonal.Validating
        Dim value As Integer
        Try
            value = CInt(txtFldDistPeatonal.Text)
        Catch ex As Exception
            e.Cancel = True
            MsgBox(String.Format(RedNodal.My.Resources.strErrIntegerRequired, "Distancia peatonal"), MsgBoxStyle.Critical, "Red Nodal")
        End Try
    End Sub

    Private Sub txtFldRadioNodo_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtFldRadioNodo.Validating
        Dim value As Integer
        Try
            value = CInt(txtFldRadioNodo.Text)
        Catch ex As Exception
            e.Cancel = True
            MsgBox(String.Format(RedNodal.My.Resources.strErrIntegerRequired, "Radio nodo"), MsgBoxStyle.Critical, "Red Nodal")
        End Try
    End Sub

    Private Sub txtFldTiempoVehicular_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtFldTiempoVehicular.Validating
        Dim value As Integer
        Try
            value = CInt(txtFldTiempoVehicular.Text)
        Catch ex As Exception
            e.Cancel = True
            MsgBox(String.Format(RedNodal.My.Resources.strErrIntegerRequired, "Tiempo vehicular"), MsgBoxStyle.Critical, "Red Nodal")
        End Try
    End Sub
End Class