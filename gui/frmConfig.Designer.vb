<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfig
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.chbJerarquia = New System.Windows.Forms.CheckBox
        Me.rbtDistancia = New System.Windows.Forms.RadioButton
        Me.rbtTiempo = New System.Windows.Forms.RadioButton
        Me.btnAceptar = New System.Windows.Forms.Button
        Me.btnCancelar = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtFldTiempoVehicular = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtFldDistPeatonal = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtFldRadioNodo = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.rbtColNuevos = New System.Windows.Forms.RadioButton
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.rbtColIntervenidos = New System.Windows.Forms.RadioButton
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'chbJerarquia
        '
        Me.chbJerarquia.AutoSize = True
        Me.chbJerarquia.Checked = True
        Me.chbJerarquia.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chbJerarquia.Location = New System.Drawing.Point(196, 35)
        Me.chbJerarquia.Name = "chbJerarquia"
        Me.chbJerarquia.Size = New System.Drawing.Size(141, 17)
        Me.chbJerarquia.TabIndex = 0
        Me.chbJerarquia.Text = "Utilizar jerarquía de vías"
        Me.chbJerarquia.UseVisualStyleBackColor = True
        '
        'rbtDistancia
        '
        Me.rbtDistancia.AutoSize = True
        Me.rbtDistancia.Checked = True
        Me.rbtDistancia.Location = New System.Drawing.Point(6, 34)
        Me.rbtDistancia.Name = "rbtDistancia"
        Me.rbtDistancia.Size = New System.Drawing.Size(69, 17)
        Me.rbtDistancia.TabIndex = 1
        Me.rbtDistancia.TabStop = True
        Me.rbtDistancia.Text = "Distancia"
        Me.rbtDistancia.UseVisualStyleBackColor = True
        '
        'rbtTiempo
        '
        Me.rbtTiempo.AutoSize = True
        Me.rbtTiempo.Location = New System.Drawing.Point(100, 35)
        Me.rbtTiempo.Name = "rbtTiempo"
        Me.rbtTiempo.Size = New System.Drawing.Size(60, 17)
        Me.rbtTiempo.TabIndex = 2
        Me.rbtTiempo.Text = "Tiempo"
        Me.rbtTiempo.UseVisualStyleBackColor = True
        '
        'btnAceptar
        '
        Me.btnAceptar.Location = New System.Drawing.Point(97, 286)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(75, 23)
        Me.btnAceptar.TabIndex = 5
        Me.btnAceptar.Text = "Aceptar"
        Me.btnAceptar.UseVisualStyleBackColor = True
        '
        'btnCancelar
        '
        Me.btnCancelar.Location = New System.Drawing.Point(224, 286)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(75, 23)
        Me.btnCancelar.TabIndex = 6
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbtDistancia)
        Me.GroupBox1.Controls.Add(Me.rbtTiempo)
        Me.GroupBox1.Controls.Add(Me.chbJerarquia)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(366, 63)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Cálculo de ruta"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.txtFldTiempoVehicular)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.txtFldDistPeatonal)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 91)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(366, 88)
        Me.GroupBox2.TabIndex = 8
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Red nodal / Tejido educativo"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(209, 61)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(43, 13)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "minutos"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(209, 32)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(38, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "metros"
        '
        'txtFldTiempoVehicular
        '
        Me.txtFldTiempoVehicular.Location = New System.Drawing.Point(143, 58)
        Me.txtFldTiempoVehicular.Name = "txtFldTiempoVehicular"
        Me.txtFldTiempoVehicular.Size = New System.Drawing.Size(60, 20)
        Me.txtFldTiempoVehicular.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(11, 58)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Tiempo vehicular"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(11, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(95, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Distancia peatonal"
        '
        'txtFldDistPeatonal
        '
        Me.txtFldDistPeatonal.Location = New System.Drawing.Point(143, 29)
        Me.txtFldDistPeatonal.Name = "txtFldDistPeatonal"
        Me.txtFldDistPeatonal.Size = New System.Drawing.Size(60, 20)
        Me.txtFldDistPeatonal.TabIndex = 0
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(127, 26)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(38, 13)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "metros"
        '
        'txtFldRadioNodo
        '
        Me.txtFldRadioNodo.Location = New System.Drawing.Point(61, 23)
        Me.txtFldRadioNodo.Name = "txtFldRadioNodo"
        Me.txtFldRadioNodo.Size = New System.Drawing.Size(60, 20)
        Me.txtFldRadioNodo.TabIndex = 7
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(11, 26)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(35, 13)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Radio"
        '
        'rbtColNuevos
        '
        Me.rbtColNuevos.AutoSize = True
        Me.rbtColNuevos.Checked = True
        Me.rbtColNuevos.Location = New System.Drawing.Point(14, 54)
        Me.rbtColNuevos.Name = "rbtColNuevos"
        Me.rbtColNuevos.Size = New System.Drawing.Size(103, 17)
        Me.rbtColNuevos.TabIndex = 10
        Me.rbtColNuevos.TabStop = True
        Me.rbtColNuevos.Text = "Colegios nuevos"
        Me.rbtColNuevos.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.rbtColIntervenidos)
        Me.GroupBox3.Controls.Add(Me.rbtColNuevos)
        Me.GroupBox3.Controls.Add(Me.txtFldRadioNodo)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 193)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(366, 87)
        Me.GroupBox3.TabIndex = 11
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Nodo funcional"
        '
        'rbtColIntervenidos
        '
        Me.rbtColIntervenidos.AutoSize = True
        Me.rbtColIntervenidos.Location = New System.Drawing.Point(143, 54)
        Me.rbtColIntervenidos.Name = "rbtColIntervenidos"
        Me.rbtColIntervenidos.Size = New System.Drawing.Size(125, 17)
        Me.rbtColIntervenidos.TabIndex = 9
        Me.rbtColIntervenidos.Text = "Colegios intervenidos"
        Me.rbtColIntervenidos.UseVisualStyleBackColor = True
        '
        'frmConfig
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(390, 315)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnAceptar)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmConfig"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Configuración"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents chbJerarquia As System.Windows.Forms.CheckBox
    Friend WithEvents rbtDistancia As System.Windows.Forms.RadioButton
    Friend WithEvents rbtTiempo As System.Windows.Forms.RadioButton
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtFldTiempoVehicular As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtFldDistPeatonal As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtFldRadioNodo As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents rbtColNuevos As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents rbtColIntervenidos As System.Windows.Forms.RadioButton
End Class
