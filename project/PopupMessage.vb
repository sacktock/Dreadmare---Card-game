Public Class PopupMessage
    Public Message As String
    Private Sub PopupMessage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DoubleBuffered = True
    End Sub
    Private Sub SizeMessage(sender As Object, e As EventArgs) Handles MyBase.VisibleChanged

        Me.Opacity = 0
        Label1.Location = New Point(10, 10)
        Label1.Text = Message
        Me.Width = Label1.Width + 20
        Me.Height = Label1.Height + 20



        Label1.Location = New Point(10, 10)

        Me.Location = New Point((Me.Owner.Width / 2) - Me.Width / 2, (Me.Owner.Height / 2) - Me.Height / 2)
        Timer1.Start()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Stop()
        Me.Opacity = 1
        Timer2.Start()
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        Timer2.Stop()
        Timer3.Start()
    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        Me.Opacity = Me.Opacity - 0.05
        If Me.Opacity <= 0 Then
            Timer3.Stop()
            My.Forms.Launcher.TopMost = True
            Me.Hide()

            My.Forms.Launcher.TopMost = False
        End If
    End Sub

    Public Sub StopTimers()
        Timer1.Stop()
        Timer2.Stop()
        Timer3.Stop()
    End Sub
End Class