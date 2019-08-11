Public Class SpellDisplay
    Private Sub SpellDisplay_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        DoubleBuffered = True
        Opacity = 1
        Me.Owner = My.Forms.Game
        Me.Location = New Point((Me.Owner.Width / 2) - Me.Width / 2, (Me.Owner.Height / 2) - Me.Height / 2)
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Me.Opacity = Me.Opacity - 0.05
        If Opacity = 0 Then
            Timer1.Stop()
            My.Forms.Launcher.TopMost = True
            Me.Hide()
            Me.Opacity = 1

            My.Forms.Launcher.TopMost = False

        End If
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub
End Class