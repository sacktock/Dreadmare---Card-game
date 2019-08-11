<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VersusLoadingScreen
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
        Me.components = New System.ComponentModel.Container()
        Me.PictureBox47 = New System.Windows.Forms.PictureBox()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ResumeCmd = New System.Windows.Forms.Button()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox47, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox47
        '
        Me.PictureBox47.BackColor = System.Drawing.Color.White
        Me.PictureBox47.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox47.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox47.Location = New System.Drawing.Point(767, 66)
        Me.PictureBox47.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PictureBox47.Name = "PictureBox47"
        Me.PictureBox47.Size = New System.Drawing.Size(215, 278)
        Me.PictureBox47.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox47.TabIndex = 19
        Me.PictureBox47.TabStop = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.White
        Me.PictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PictureBox1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.PictureBox1.Location = New System.Drawing.Point(1021, 682)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(215, 278)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 20
        Me.PictureBox1.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Black
        Me.Label1.Font = New System.Drawing.Font("Perpetua Titling MT", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(800, 682)
        Me.Label1.MaximumSize = New System.Drawing.Size(215, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(90, 23)
        Me.Label1.TabIndex = 21
        Me.Label1.Text = "hhhhh"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Black
        Me.Label2.Font = New System.Drawing.Font("Perpetua Titling MT", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(988, 66)
        Me.Label2.MaximumSize = New System.Drawing.Size(215, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 23)
        Me.Label2.TabIndex = 22
        Me.Label2.Text = "gfhgf"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
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
        Me.ResumeCmd.Location = New System.Drawing.Point(1561, 500)
        Me.ResumeCmd.Name = "ResumeCmd"
        Me.ResumeCmd.Size = New System.Drawing.Size(300, 87)
        Me.ResumeCmd.TabIndex = 23
        Me.ResumeCmd.Text = "Continue"
        Me.ResumeCmd.UseVisualStyleBackColor = True
        '
        'Timer1
        '
        Me.Timer1.Interval = 20
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = Global.WindowsApplication1.My.Resources.Resources.Vs_logo
        Me.PictureBox2.Location = New System.Drawing.Point(870, 364)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(225, 300)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 24
        Me.PictureBox2.TabStop = False
        '
        'VersusLoadingScreen
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Black
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1920, 1080)
        Me.Controls.Add(Me.PictureBox2)
        Me.Controls.Add(Me.ResumeCmd)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.PictureBox47)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "VersusLoadingScreen"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "VersusLoadingScreen"
        Me.TopMost = True
        Me.TransparencyKey = System.Drawing.Color.Black
        CType(Me.PictureBox47, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PictureBox47 As PictureBox
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents ResumeCmd As Button
    Friend WithEvents Timer1 As Timer
    Friend WithEvents PictureBox2 As PictureBox
End Class
