<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReporte
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
        Me.btnGenerar = New System.Windows.Forms.Button
        Me.rbtAFE = New System.Windows.Forms.RadioButton
        Me.rbtUPZ = New System.Windows.Forms.RadioButton
        Me.clUPZxAFE = New System.Windows.Forms.CheckedListBox
        Me.rbtTejido = New System.Windows.Forms.RadioButton
        Me.rbtRedNodal = New System.Windows.Forms.RadioButton
        Me.gbxUnidad = New System.Windows.Forms.GroupBox
        Me.gbxFuncion = New System.Windows.Forms.GroupBox
        Me.gbxUnidad.SuspendLayout()
        Me.gbxFuncion.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnGenerar
        '
        Me.btnGenerar.Enabled = False
        Me.btnGenerar.Location = New System.Drawing.Point(185, 217)
        Me.btnGenerar.Name = "btnGenerar"
        Me.btnGenerar.Size = New System.Drawing.Size(75, 22)
        Me.btnGenerar.TabIndex = 4
        Me.btnGenerar.Text = "Generar"
        Me.btnGenerar.UseVisualStyleBackColor = True
        '
        'rbtAFE
        '
        Me.rbtAFE.AutoSize = True
        Me.rbtAFE.Checked = True
        Me.rbtAFE.Location = New System.Drawing.Point(6, 16)
        Me.rbtAFE.Name = "rbtAFE"
        Me.rbtAFE.Size = New System.Drawing.Size(46, 17)
        Me.rbtAFE.TabIndex = 5
        Me.rbtAFE.TabStop = True
        Me.rbtAFE.Text = "AFE"
        Me.rbtAFE.UseVisualStyleBackColor = True
        '
        'rbtUPZ
        '
        Me.rbtUPZ.AutoSize = True
        Me.rbtUPZ.Location = New System.Drawing.Point(59, 16)
        Me.rbtUPZ.Name = "rbtUPZ"
        Me.rbtUPZ.Size = New System.Drawing.Size(48, 17)
        Me.rbtUPZ.TabIndex = 6
        Me.rbtUPZ.TabStop = True
        Me.rbtUPZ.Text = "UPZ"
        Me.rbtUPZ.UseVisualStyleBackColor = True
        '
        'clUPZxAFE
        '
        Me.clUPZxAFE.FormattingEnabled = True
        Me.clUPZxAFE.Location = New System.Drawing.Point(12, 66)
        Me.clUPZxAFE.Name = "clUPZxAFE"
        Me.clUPZxAFE.Size = New System.Drawing.Size(238, 94)
        Me.clUPZxAFE.TabIndex = 7
        Me.clUPZxAFE.Visible = False
        '
        'rbtTejido
        '
        Me.rbtTejido.AutoSize = True
        Me.rbtTejido.Location = New System.Drawing.Point(6, 19)
        Me.rbtTejido.Name = "rbtTejido"
        Me.rbtTejido.Size = New System.Drawing.Size(106, 17)
        Me.rbtTejido.TabIndex = 9
        Me.rbtTejido.TabStop = True
        Me.rbtTejido.Text = "Tejido Educativo"
        Me.rbtTejido.UseVisualStyleBackColor = True
        '
        'rbtRedNodal
        '
        Me.rbtRedNodal.AutoSize = True
        Me.rbtRedNodal.Location = New System.Drawing.Point(124, 19)
        Me.rbtRedNodal.Name = "rbtRedNodal"
        Me.rbtRedNodal.Size = New System.Drawing.Size(77, 17)
        Me.rbtRedNodal.TabIndex = 10
        Me.rbtRedNodal.TabStop = True
        Me.rbtRedNodal.Text = "Red Nodal"
        Me.rbtRedNodal.UseVisualStyleBackColor = True
        '
        'gbxUnidad
        '
        Me.gbxUnidad.Controls.Add(Me.rbtAFE)
        Me.gbxUnidad.Controls.Add(Me.rbtUPZ)
        Me.gbxUnidad.ForeColor = System.Drawing.SystemColors.Desktop
        Me.gbxUnidad.Location = New System.Drawing.Point(12, 8)
        Me.gbxUnidad.Name = "gbxUnidad"
        Me.gbxUnidad.Size = New System.Drawing.Size(238, 55)
        Me.gbxUnidad.TabIndex = 11
        Me.gbxUnidad.TabStop = False
        Me.gbxUnidad.Text = "Unidad de Consulta"
        '
        'gbxFuncion
        '
        Me.gbxFuncion.Controls.Add(Me.rbtTejido)
        Me.gbxFuncion.Controls.Add(Me.rbtRedNodal)
        Me.gbxFuncion.ForeColor = System.Drawing.SystemColors.Desktop
        Me.gbxFuncion.Location = New System.Drawing.Point(12, 164)
        Me.gbxFuncion.Name = "gbxFuncion"
        Me.gbxFuncion.Size = New System.Drawing.Size(238, 44)
        Me.gbxFuncion.TabIndex = 12
        Me.gbxFuncion.TabStop = False
        Me.gbxFuncion.Text = "Función"
        '
        'frmReporte
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(262, 243)
        Me.Controls.Add(Me.gbxFuncion)
        Me.Controls.Add(Me.gbxUnidad)
        Me.Controls.Add(Me.clUPZxAFE)
        Me.Controls.Add(Me.btnGenerar)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmReporte"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Reportes"
        Me.gbxUnidad.ResumeLayout(False)
        Me.gbxUnidad.PerformLayout()
        Me.gbxFuncion.ResumeLayout(False)
        Me.gbxFuncion.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnGenerar As System.Windows.Forms.Button
    Friend WithEvents rbtAFE As System.Windows.Forms.RadioButton
    Friend WithEvents rbtUPZ As System.Windows.Forms.RadioButton
    Friend WithEvents clUPZxAFE As System.Windows.Forms.CheckedListBox
    Friend WithEvents rbtTejido As System.Windows.Forms.RadioButton
    Friend WithEvents rbtRedNodal As System.Windows.Forms.RadioButton
    Friend WithEvents gbxUnidad As System.Windows.Forms.GroupBox
    Friend WithEvents gbxFuncion As System.Windows.Forms.GroupBox
End Class
