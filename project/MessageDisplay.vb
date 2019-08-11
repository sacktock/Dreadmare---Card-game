Public Class MessageDisplay

    Private Sub TurnDisplay_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Opacity = 0
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        DoubleBuffered = True


        Me.Owner = My.Forms.Game
        Me.Width = Me.Owner.Width

        Me.Location = New Point(0, (Me.Owner.Height / 2) - (Me.Height / 2))


        Label1.Location = New Point((Me.Width / 2) - (Label1.Size.Width / 2), (Me.Height / 2) - (Label1.Size.Height / 2))
        PictureBox1.Location = New Point(Label1.Location.X - PictureBox1.Width - 10, (Me.Height / 2) - (PictureBox1.Height / 2))
        Timer2.Start()
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Me.Opacity = Me.Opacity - 0.05
        If Me.Opacity = 0 Then
            Timer1.Stop()
            My.Forms.Launcher.TopMost = True
            Me.Hide()
            My.Forms.Launcher.TopMost = False
            Me.Opacity = 0.9
        End If
    End Sub
    Private Sub MessageDisplay_Shown(sender As Object, e As EventArgs) Handles MyBase.VisibleChanged
        Me.Opacity = 0
        Label1.Location = New Point((Me.Width / 2) - (Label1.Size.Width / 2), (Me.Height / 2) - (Label1.Size.Height / 2))
        PictureBox1.Location = New Point(Label1.Location.X - PictureBox1.Width - 10, (Me.Height / 2) - (PictureBox1.Height / 2))
        Timer2.Start()
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Timer2.Stop()
        Me.Opacity = 0.9
    End Sub
End Class