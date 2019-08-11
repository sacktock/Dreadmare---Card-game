Public Class WinnerDisplay
    Private Sub WinnerDisplay_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Opacity = 0
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        DoubleBuffered = True
        Me.Owner = My.Forms.Game
        Me.Location = New Point(0, (Me.Owner.Height / 2) - (Me.Height / 2))
        Me.Height = sender.Size.Height * (SystemInformation.PrimaryMonitorSize.Height / 864)
        Me.Width = Me.Owner.Width
        DynamicScale(Label1)
        DynamicScale(Label2)
        DynamicScale(PictureBox1) '
        DynamicScale(PictureBox2)
        DynamicScale(Button1)
        Label2.Location = New Point((Me.Width / 2) - (Label2.Width / 2), (Me.Height / 2) - (Label2.Height / 2))
        Label1.Location = New Point(Label2.Location.X, Label2.Location.Y - Label1.Height - 10)
        PictureBox1.Location = New Point(Label2.Location.X - PictureBox1.Width - 10, (Me.Height / 2) - (PictureBox1.Height / 2))
        Button1.Location = New Point(Label2.Location.X + 10, Label2.Location.Y + Label2.Height + 10)
        PictureBox2.Location = New Point(Label1.Location.X + Label1.Width + 10, Label1.Location.Y)
        Timer1.Start()
    End Sub
    Public Sub DynamicScale(ByRef sender As Object)
        sender.Size = New Size(sender.Size.Width * (SystemInformation.PrimaryMonitorSize.Width / 1536), sender.Size.Height * (SystemInformation.PrimaryMonitorSize.Height / 864))

    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        My.Forms.Launcher.TopMost = True
        My.Forms.LoadingScreen.Show()
        Me.Close()
        My.Forms.BlurrBackGround.Close()
        My.Forms.Game.Close()

        My.Forms.Launcher.Timer3.Start()
        My.Forms.Launcher.TopMost = False

    End Sub

    Private Sub Button1_MouseHover(sender As Object, e As EventArgs) Handles Button1.MouseHover
        Button1.BackgroundImage = My.Resources.buttonStock1d_hover
    End Sub

    Private Sub Button1_MouseLeave(sender As Object, e As EventArgs) Handles Button1.MouseLeave
        Button1.BackgroundImage = My.Resources.buttonStock1d
    End Sub

    Private Sub Label3_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Stop()
        Me.Opacity = 0.9
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub
End Class