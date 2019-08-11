Public Class LoadingScreen
    Private Sub LoadingSCreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Opacity = 0
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        DoubleBuffered = True
        Me.FormBorderStyle = FormBorderStyle.None
        Me.Location = New Point(0, 0)

        Me.Owner = My.Forms.Launcher
        Me.Opacity = 1
        Me.Size = SystemInformation.PrimaryMonitorSize
        Me.AutoSize = True
        Me.AutoSizeMode = AutoSizeMode.GrowAndShrink
        Me.AutoScaleMode = AutoScaleMode.Dpi
        Me.WindowState = FormWindowState.Maximized
    End Sub
End Class