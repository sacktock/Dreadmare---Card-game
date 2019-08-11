Public Class OptionsDisplay
    Private Sub ForfeitCmd_Click(sender As Object, e As EventArgs) Handles ForfeitCmd.Click

        My.Forms.Game.Surrender()
        My.Forms.Launcher.TopMost = True
        Me.Hide()
        My.Forms.BlurrBackGround.Hide()
        My.Forms.Launcher.TopMost = False
    End Sub

    Private Sub ResumeCmd_Click(sender As Object, e As EventArgs) Handles ResumeCmd.Click
        My.Computer.Audio.Play(My.Resources.cloth, AudioPlayMode.Background)
        My.Forms.Launcher.TopMost = True
        Me.Hide()
        My.Forms.BlurrBackGround.Hide()

        My.Forms.Launcher.TopMost = False
    End Sub
    Private Sub QuitCmd_Click(sender As Object, e As EventArgs) Handles QuitCmd.Click
        Me.Hide()
        My.Forms.BlurrBackGround.Hide()
        My.Forms.Game.Close()
        My.Forms.Launcher.Close()
    End Sub

    Private Sub OptionsDisplay_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Owner = My.Forms.Game
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        DoubleBuffered = True

        Me.Location = New Point((Me.Owner.Width / 2) - (Me.Width / 2), (Me.Owner.Height / 2) - (Me.Height / 2))
    End Sub

    Private Sub ForfeitCmd_MouseHover(sender As Object, e As EventArgs) Handles ForfeitCmd.MouseHover
        ForfeitCmd.BackgroundImage = My.Resources.buttonStock1d_hover
    End Sub

    Private Sub ResumeCmd_MouseHover(sender As Object, e As EventArgs) Handles ResumeCmd.MouseHover
        ResumeCmd.BackgroundImage = My.Resources.buttonStock1d_hover
    End Sub
    Private Sub QuitCmd_MouseHover(sender As Object, e As EventArgs) Handles QuitCmd.MouseHover
        QuitCmd.BackgroundImage = My.Resources.buttonStock1d_hover
    End Sub

    Private Sub ForfeitCmd_MouseLeave(sender As Object, e As EventArgs) Handles ForfeitCmd.MouseLeave
        ForfeitCmd.BackgroundImage = My.Resources.buttonStock1d
    End Sub

    Private Sub ResumeCmd_MouseLeave(sender As Object, e As EventArgs) Handles ResumeCmd.MouseLeave
        ResumeCmd.BackgroundImage = My.Resources.buttonStock1d
    End Sub
    Private Sub QuitCmd_MouseLeave(sender As Object, e As EventArgs) Handles QuitCmd.MouseLeave
        QuitCmd.BackgroundImage = My.Resources.buttonStock1d
    End Sub
End Class