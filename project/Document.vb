Public Class Document
    Public Sub LoadFile(ByVal filepath As Object)
        RichTextBox1.Rtf = filepath
        RichTextBox1.Refresh()
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Stop()
        Me.Opacity = 1
    End Sub
    Public Sub onClose(sender As Object, e As EventArgs) Handles MyBase.Closed
        My.Forms.BlurrBackGround.Hide()
    End Sub
    Private Sub Instructions_Display_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Owner = My.Forms.Launcher
        Me.Opacity = 0
        Me.Location = New Point((Me.Owner.Width / 2) - (Me.Width / 2), (Me.Owner.Height / 2) - (Me.Height / 2))
        Timer1.Start()
        RichTextBox1.BackColor = System.Drawing.SystemColors.Window
    End Sub
    Private Sub Button65_Click(sender As Object, e As EventArgs) Handles Button65.Click
        My.Computer.Audio.Play(My.Resources.cloth, AudioPlayMode.Background)
        My.Forms.Launcher.TopMost = True
        Me.Close()
        My.Forms.Launcher.TopMost = False
    End Sub
    Private Sub RichTextBox1_Enter(sender As Object, e As EventArgs) Handles RichTextBox1.GotFocus
        Label1.Focus()
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click

    End Sub
End Class