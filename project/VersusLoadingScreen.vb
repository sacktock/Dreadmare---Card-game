Public Class VersusLoadingScreen
    Private clicked As Boolean
    Private scaled As Boolean = False
    Private Sub VersusLoadingScreen_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Opacity = 0
        Me.Owner = My.Forms.Game
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        DoubleBuffered = True
        Me.FormBorderStyle = FormBorderStyle.None
        Me.Location = New Point(0, 0)
        Me.AutoSize = True
        Me.AutoSizeMode = AutoSizeMode.GrowAndShrink
        Me.AutoScaleMode = AutoScaleMode.Dpi
        Me.WindowState = FormWindowState.Maximized
        My.Computer.Audio.Play(My.Resources.sword_unsheathe, AudioPlayMode.Background)
        My.Forms.LoadingScreen.Close()
        My.Forms.BlurrBackGround.Show()
        If scaled = False Then
            PerformDynamicScale()
            scaled = True
        End If

        Timer1.Start()
    End Sub

    Public Sub DynamicScale(ByRef sender As Object)
        sender.Size = New Size(sender.Size.Width * (Me.Width / 1536), sender.Size.Height * (Me.Height / 864))
        sender.Location = New Point(sender.Location.X * (Me.Width / 1536), sender.Location.Y * (Me.Height / 864))
    End Sub

    Private Sub PerformDynamicScale()
        DynamicScale(PictureBox1)
        DynamicScale(PictureBox2)
        DynamicScale(PictureBox47)
        DynamicScale(Label2)
        DynamicScale(Label1)
        DynamicScale(ResumeCmd)


    End Sub
    Public Sub Setup(ByRef Champion1 As Game.Champion, ByRef Champion2 As Game.Champion)
        PictureBox47.Image = Champion1.GetPictureBoxReference.Image
        PictureBox1.Image = Champion2.GetPictureBoxReference.Image
        Label1.Text = "------ ABILITY ------ " + Champion2.ReturnAbilityDescription
        Label2.Text = "------ ABILITY ------ " + Champion1.ReturnAbilityDescription
    End Sub
    Private Sub ResumeCmd_Click(sender As Object, e As EventArgs) Handles ResumeCmd.Click
        My.Computer.Audio.Stop()
        My.Computer.Audio.Play(My.Resources.cloth, AudioPlayMode.Background)
        My.Forms.Launcher.TopMost = True
        Me.Hide()
        My.Forms.Launcher.TopMost = False
    End Sub

    Private Sub ResumeCmd_MouseEnter(sender As Object, e As EventArgs) Handles ResumeCmd.MouseEnter
        sender.BackgroundImage = My.Resources.buttonStock1d_hover
    End Sub

    Private Sub ResumeCmd_MouseLeave(sender As Object, e As EventArgs) Handles ResumeCmd.MouseLeave
        sender.BackgroundImage = My.Resources.buttonStock1d
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Stop()
        Me.Opacity = 0.9
    End Sub
End Class