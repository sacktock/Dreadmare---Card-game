Public Class CardDisplay
    Private Sub CardDisplay_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Opacity = 0
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        DoubleBuffered = True
        Me.Owner = My.Forms.Game
        Me.Location = New Point(Cursor.Position.X + 5, Cursor.Position.Y - Me.Height - 5)
        Timer2.Start()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Me.Opacity = Opacity - 0.05
        If Opacity = 0 Then
            Timer1.Stop()
            My.Forms.Launcher.TopMost = True
            Me.Hide()
            Me.Opacity = 1
            My.Forms.Launcher.TopMost = False
        End If
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Timer2.Stop()
        Me.Opacity = 1
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub
End Class