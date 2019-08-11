<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class OptionsDisplay
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(OptionsDisplay))
        Me.ResumeCmd = New System.Windows.Forms.Button()
        Me.QuitCmd = New System.Windows.Forms.Button()
        Me.ForfeitCmd = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'ResumeCmd
        '
        Me.ResumeCmd.BackColor = System.Drawing.Color.Transparent
        Me.ResumeCmd.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.buttonStock1d
        Me.ResumeCmd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ResumeCmd.CausesValidation = False
        Me.ResumeCmd.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ResumeCmd.FlatAppearance.BorderSize = 0
        Me.ResumeCmd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.ResumeCmd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.ResumeCmd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ResumeCmd.Font = New System.Drawing.Font("Perpetua Titling MT", 22.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ResumeCmd.ForeColor = System.Drawing.Color.DarkGoldenrod
        Me.ResumeCmd.Location = New System.Drawing.Point(42, 199)
        Me.ResumeCmd.Name = "ResumeCmd"
        Me.ResumeCmd.Size = New System.Drawing.Size(300, 87)
        Me.ResumeCmd.TabIndex = 4
        Me.ResumeCmd.Text = "Resume"
        Me.ResumeCmd.UseVisualStyleBackColor = True
        '
        'QuitCmd
        '
        Me.QuitCmd.BackColor = System.Drawing.Color.Transparent
        Me.QuitCmd.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.buttonStock1d
        Me.QuitCmd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.QuitCmd.CausesValidation = False
        Me.QuitCmd.Cursor = System.Windows.Forms.Cursors.Hand
        Me.QuitCmd.FlatAppearance.BorderSize = 0
        Me.QuitCmd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.QuitCmd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.QuitCmd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.QuitCmd.Font = New System.Drawing.Font("Perpetua Titling MT", 22.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.QuitCmd.ForeColor = System.Drawing.Color.DarkGoldenrod
        Me.QuitCmd.Location = New System.Drawing.Point(42, 292)
        Me.QuitCmd.Name = "QuitCmd"
        Me.QuitCmd.Size = New System.Drawing.Size(300, 87)
        Me.QuitCmd.TabIndex = 5
        Me.QuitCmd.Text = "Quit"
        Me.QuitCmd.UseVisualStyleBackColor = True
        '
        'ForfeitCmd
        '
        Me.ForfeitCmd.BackColor = System.Drawing.Color.Transparent
        Me.ForfeitCmd.BackgroundImage = Global.WindowsApplication1.My.Resources.Resources.buttonStock1d
        Me.ForfeitCmd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ForfeitCmd.CausesValidation = False
        Me.ForfeitCmd.Cursor = System.Windows.Forms.Cursors.Hand
        Me.ForfeitCmd.FlatAppearance.BorderSize = 0
        Me.ForfeitCmd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.ForfeitCmd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.ForfeitCmd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ForfeitCmd.Font = New System.Drawing.Font("Perpetua Titling MT", 22.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForfeitCmd.ForeColor = System.Drawing.Color.DarkGoldenrod
        Me.ForfeitCmd.Location = New System.Drawing.Point(42, 106)
        Me.ForfeitCmd.Name = "ForfeitCmd"
        Me.ForfeitCmd.Size = New System.Drawing.Size(300, 87)
        Me.ForfeitCmd.TabIndex = 6
        Me.ForfeitCmd.Text = "Surrender"
        Me.ForfeitCmd.UseVisualStyleBackColor = True
        '
        'OptionsDisplay
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(384, 500)
        Me.Controls.Add(Me.ForfeitCmd)
        Me.Controls.Add(Me.QuitCmd)
        Me.Controls.Add(Me.ResumeCmd)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "OptionsDisplay"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "OptionsDisplay"
        Me.TopMost = True
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ResumeCmd As Button
    Friend WithEvents QuitCmd As Button
    Friend WithEvents ForfeitCmd As Button
End Class
