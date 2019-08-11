Public Class DiscardCards

    Private SelectedPictureBox As New List(Of PictureBox)
    Private PlayerHand As Game.Hand
    Private numSelected As Integer = 0
    Const selectedcap As Integer = 4
    Private scaled As Boolean = False
    Private Sub DiscardCards_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Opacity = 0
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        DoubleBuffered = True
        Me.WindowState = FormWindowState.Maximized
        Me.Owner = My.Forms.Game
        Me.Size = Me.Owner.Size
        Me.Location = New Point(0, 0)
        'img.MakeTransparent(img.GetPixel(2, 2))
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
        DynamicScale(PictureBox11)
        DynamicScale(PictureBox12)
        DynamicScale(PictureBox13)
        DynamicScale(PictureBox14)
        DynamicScale(PictureBox15)
        DynamicScale(PictureBox16)
        DynamicScale(PictureBox17)
        DynamicScale(PictureBox18)
        DynamicScale(PictureBox19)
        DynamicScale(PictureBox20)
        DynamicScale(Label1)
        DynamicScale(Label2)
        DynamicScale(Button1)
        FlowLayoutPanel2.Size = New Size(FlowLayoutPanel2.Width * (Me.Width / 1536), FlowLayoutPanel2.Height * (Me.Height / 864))
        FlowLayoutPanel2.Location = New Point((Me.Width / 2) - (FlowLayoutPanel2.Width / 2), (Me.Height / 2) - (FlowLayoutPanel2.Height / 2))
    End Sub
    Public Sub setUp(ByRef Hand As Game.Hand)
        LoadHand(Hand)
        SelectedPictureBox.Clear()
        numSelected = 0
        ClearSelection(PictureBox11)
        ClearSelection(PictureBox12)
        ClearSelection(PictureBox13)
        ClearSelection(PictureBox14)
        ClearSelection(PictureBox15)
        ClearSelection(PictureBox16)
        ClearSelection(PictureBox17)
        ClearSelection(PictureBox18)
        ClearSelection(PictureBox19)
        ClearSelection(PictureBox20)
    End Sub
    Public Sub LoadHand()
        PictureBox11.BackgroundImage = PlayerHand.GetImage(1)
        PictureBox12.BackgroundImage = PlayerHand.GetImage(2)
        PictureBox13.BackgroundImage = PlayerHand.GetImage(3)
        PictureBox14.BackgroundImage = PlayerHand.GetImage(4)
        PictureBox15.BackgroundImage = PlayerHand.GetImage(5)
        PictureBox16.BackgroundImage = PlayerHand.GetImage(6)
        PictureBox17.BackgroundImage = PlayerHand.GetImage(7)
        PictureBox18.BackgroundImage = PlayerHand.GetImage(8)
        PictureBox19.BackgroundImage = PlayerHand.GetImage(9)
        PictureBox20.BackgroundImage = PlayerHand.GetImage(10)
    End Sub
    Public Sub ClearSelection(sender As Object)
        sender.Tag = Nothing
        DisplayCross(sender, False)
    End Sub
    Public Sub onPictureBoxClick(sender As Object, e As EventArgs) Handles PictureBox20.Click, PictureBox19.Click, PictureBox18.Click, PictureBox17.Click, PictureBox16.Click, PictureBox15.Click, PictureBox14.Click, PictureBox13.Click, PictureBox12.Click, PictureBox11.Click
        If sender.tag Is Nothing Or sender.tag = False Then
            numSelected += 1
            SelectedPictureBox.Add(sender)
            DisplayCross(sender, True)
            sender.tag = True
            If numSelected >= selectedcap Then
                SelectedPictureBox.Item(0).Tag = False
                DisplayCross(SelectedPictureBox.Item(0), False)
                SelectedPictureBox.Remove(SelectedPictureBox.Item(0))
                numSelected = numSelected - 1
            End If
        ElseIf sender.tag = True Then
            numSelected = numSelected - 1
            SelectedPictureBox.Remove(sender)
            DisplayCross(sender, False)
            sender.tag = False
        End If
        My.Computer.Audio.Play(My.Resources.Play_Card, AudioPlayMode.Background)
    End Sub
    Public Sub DisplayCross(ByRef sender As PictureBox, ByVal Cross As Boolean)
        If Cross Then
            sender.Image = My.Resources.cross
        Else
            sender.Image = Nothing
        End If
    End Sub
    Public Function FinishSelection()
        Dim output() As Bitmap
        ReDim output(SelectedPictureBox.Count - 1)
        For i = 0 To SelectedPictureBox.Count - 1
            output(i) = SelectedPictureBox.Item(i).BackgroundImage
        Next
        Return output
    End Function
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Tag = FinishSelection()
        My.Computer.Audio.Stop()
        My.Forms.BlurrBackGround.Close()
        My.Computer.Audio.Play(My.Resources.cloth, AudioPlayMode.Background)
        My.Forms.Launcher.TopMost = True
        Me.Hide()
        My.Forms.Launcher.TopMost = False
    End Sub

    Private Sub Button1_MouseHover(sender As Object, e As EventArgs) Handles Button1.MouseEnter
        sender.BackgroundImage = My.Resources.buttonStock1d_hover
    End Sub

    Private Sub Button1_MouseLeave(sender As Object, e As EventArgs) Handles Button1.MouseLeave
        sender.BackgroundImage = My.Resources.buttonStock1d
    End Sub
    Public Sub LoadHand(ByRef hand As Game.Hand)
        Me.PlayerHand = hand
        LoadHand()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Stop()
        Me.Opacity = 1
    End Sub
End Class