Public Class HoverMessage
    Public Message As String = ""
    Private Sub HoverMessage_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DoubleBuffered = True
        Me.Opacity = 0
    End Sub
    Private Sub MessageSize(sender As Object, e As EventArgs) Handles MyBase.VisibleChanged
        Me.Opacity = 0
        Label1.Location = New Point(2, 2)
        Label1.Text = Message
        Me.Width = Label1.Width + 4
        Me.Height = Label1.Height + 4
        Label1.Location = New Point(2, 2)
        Me.Location = New Point(Cursor.Position.X + 5, Cursor.Position.Y + 5)
        Timer1.Start()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Stop()
        Me.Opacity = 0.9
    End Sub
End Class