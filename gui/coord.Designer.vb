<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class coord
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
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtX = New System.Windows.Forms.TextBox
        Me.btnOK = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(18, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "ID"
        '
        'txtX
        '
        Me.txtX.Location = New System.Drawing.Point(40, 12)
        Me.txtX.Name = "txtX"
        Me.txtX.Size = New System.Drawing.Size(86, 20)
        Me.txtX.TabIndex = 2
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(52, 38)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(63, 29)
        Me.btnOK.TabIndex = 4
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'coord
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(145, 78)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.txtX)
        Me.Controls.Add(Me.Label1)
        Me.Name = "coord"
        Me.Text = "coord"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtX As System.Windows.Forms.TextBox
    Friend WithEvents btnOK As System.Windows.Forms.Button
End Class
