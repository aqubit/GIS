<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAFE2
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAFE2))
        Me.cbxAFE = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblAño = New System.Windows.Forms.Label
        Me.cbxAño = New System.Windows.Forms.ComboBox
        Me.SuspendLayout()
        '
        'cbxAFE
        '
        Me.cbxAFE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbxAFE.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbxAFE.FormattingEnabled = True
        Me.cbxAFE.Location = New System.Drawing.Point(87, 11)
        Me.cbxAFE.Name = "cbxAFE"
        Me.cbxAFE.Size = New System.Drawing.Size(163, 21)
        Me.cbxAFE.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(-1, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Seleccione AFE:"
        '
        'lblAño
        '
        Me.lblAño.AutoSize = True
        Me.lblAño.Location = New System.Drawing.Point(256, 13)
        Me.lblAño.Name = "lblAño"
        Me.lblAño.Size = New System.Drawing.Size(84, 13)
        Me.lblAño.TabIndex = 2
        Me.lblAño.Text = "Seleccione año:"
        '
        'cbxAño
        '
        Me.cbxAño.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cbxAño.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cbxAño.FormattingEnabled = True
        Me.cbxAño.Location = New System.Drawing.Point(346, 11)
        Me.cbxAño.Name = "cbxAño"
        Me.cbxAño.Size = New System.Drawing.Size(79, 21)
        Me.cbxAño.TabIndex = 3
        '
        'frmAFE2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.InactiveCaptionText
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(433, 35)
        Me.Controls.Add(Me.cbxAño)
        Me.Controls.Add(Me.lblAño)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cbxAFE)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAFE2"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "AFE"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cbxAFE As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblAño As System.Windows.Forms.Label
    Friend WithEvents cbxAño As System.Windows.Forms.ComboBox
End Class
