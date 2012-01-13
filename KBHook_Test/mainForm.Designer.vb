<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class mainForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(mainForm))
        Me.btnCtrlAltDel = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'btnCtrlAltDel
        '
        Me.btnCtrlAltDel.Location = New System.Drawing.Point(12, 12)
        Me.btnCtrlAltDel.Name = "btnCtrlAltDel"
        Me.btnCtrlAltDel.Size = New System.Drawing.Size(261, 109)
        Me.btnCtrlAltDel.TabIndex = 0
        Me.btnCtrlAltDel.Text = "Hook Keyboard"
        Me.btnCtrlAltDel.UseVisualStyleBackColor = True
        '
        'mainForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 133)
        Me.Controls.Add(Me.btnCtrlAltDel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "mainForm"
        Me.Text = "Disable Hot Keys"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnCtrlAltDel As System.Windows.Forms.Button

End Class
