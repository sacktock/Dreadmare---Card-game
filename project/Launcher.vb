Public Class Launcher
    Private championTickBox1 As ChampionTickBox
    Private championTickBox2 As ChampionTickBox
    Private championTickBox3 As ChampionTickBox
    Private championTickBox4 As ChampionTickBox
    Private championTickBox5 As ChampionTickBox
    Private championTickBox6 As ChampionTickBox
    Private championTickBox7 As ChampionTickBox
    Private championTickBox8 As ChampionTickBox
    Private YourChampion As Game.Champion.Name
    Private OpponentChampion As Game.Champion.Name
    Private Deck As New Collection
    Private List As New Game.CardList
    Private RandomOpponentChampion As Boolean = False
    Private PresetDeck As Boolean = False
    Private AudioThread As Threading.Thread
    Public Sub DynamicScale(ByRef sender As Object)
        sender.Size = New Size(sender.Size.Width * (Me.Width / 1536), sender.Size.Height * (Me.Height / 864))
        sender.Location = New Point(sender.Location.X * (Me.Width / 1536), sender.Location.Y * (Me.Height / 864))
    End Sub

    Private Sub PerformDynamicScale()
        DynamicScale(FlowLayoutPanel1)
        DynamicScale(FlowLayoutPanel3)
        DynamicScale(FlowLayoutPanel4)
        DynamicScale(Label4)
        DynamicScale(Label1)
        DynamicScale(Label3)
        DynamicScale(PresetDeckLabel)
        DynamicScale(RandomChampionLabel)
        DynamicScale(PresetDeckBox)
        DynamicScale(RandomChampionBox)
        DynamicScale(Label2)
        DynamicScale(Button1)
        DynamicScale(FlowLayoutPanel2)

        DynamicScale(PictureBox1)
        DynamicScale(PictureBox2)
        DynamicScale(PictureBox3)
        DynamicScale(DataGridView1)
        DynamicScale(PlayCmd)

        DynamicScale(Button65)
        DynamicScale(Button66)
        DynamicScale(Button67)
        DynamicScale(Button68)

    End Sub
    Private Sub Launcher_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Opacity = 0
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        DoubleBuffered = True
        Me.Location = New Point(0, 0)
        Me.WindowState = FormWindowState.Maximized
        Me.CreateGraphics()
        SetUpChampionPicture()
        setupDeck()
        setUpButtons()
        My.Forms.PopupMessage.Owner = Me
        My.Forms.PopupMessage.Show()
        My.Forms.PopupMessage.Hide()
        My.Forms.Document.Show()
        My.Forms.Document.Hide()
        My.Forms.Background_Music_Player.Show()
        My.Forms.Background_Music_Player.Hide()
        My.Forms.Launcher.TopMost = True
        My.Forms.Launcher.TopMost = False
        My.Forms.LoadingScreen.Owner = Me
        My.Forms.LoadingScreen.Show()

        PerformDynamicScale()


    End Sub
    Private Sub Launcher_visible(sender As Object, e As EventArgs) Handles MyBase.VisibleChanged
        Timer1.Start()
    End Sub

    Public Sub setupDeck()
        DataGridView1.ColumnCount = 1
        DataGridView1.Columns(0).Name = "Cards"
    End Sub
    Private Sub setUpButton(ByRef ButtonRef As Button)
        AddHandler ButtonRef.MouseClick, AddressOf Me.ButtonCardClick
        AddHandler ButtonRef.MouseEnter, AddressOf Me.ButtonCardMouseEnter
        AddHandler ButtonRef.MouseLeave, AddressOf Me.ButtonCardMouseLeave_1
        DynamicScale(ButtonRef)
    End Sub
    Private Sub setUpButtons()
        setUpButton(Button1)
        setUpButton(Button2)
        setUpButton(Button3)
        setUpButton(Button4)
        setUpButton(Button5)
        setUpButton(Button6)
        setUpButton(Button7)
        setUpButton(Button8)
        setUpButton(Button9)
        setUpButton(Button10)
        setUpButton(Button11)
        setUpButton(Button12)
        setUpButton(Button13)
        setUpButton(Button14)
        setUpButton(Button15)
        setUpButton(Button16)
        setUpButton(Button17)
        setUpButton(Button18)
        setUpButton(Button19)
        setUpButton(Button20)
        setUpButton(Button21)
        setUpButton(Button22)
        setUpButton(Button23)
        setUpButton(Button24)
        setUpButton(Button25)
        setUpButton(Button26)
        setUpButton(Button27)
        setUpButton(Button28)
        setUpButton(Button29)
        setUpButton(Button30)
        setUpButton(Button31)
        setUpButton(Button32)
        setUpButton(Button33)
        setUpButton(Button34)
        setUpButton(Button35)
        setUpButton(Button36)
        setUpButton(Button37)
        setUpButton(Button38)
        setUpButton(Button39)
        setUpButton(Button40)
        setUpButton(Button41)
        setUpButton(Button42)
        setUpButton(Button43)
        setUpButton(Button44)
        setUpButton(Button45)
        setUpButton(Button46)
        setUpButton(Button47)
        setUpButton(Button48)
        setUpButton(Button49)
        setUpButton(Button50)
        setUpButton(Button51)
        setUpButton(Button52)
        setUpButton(Button53)
        setUpButton(Button54)
        setUpButton(Button55)
        setUpButton(Button56)
        setUpButton(Button57)
        setUpButton(Button58)
        setUpButton(Button59)
        setUpButton(Button60)
        setUpButton(Button61)
        setUpButton(Button62)
        setUpButton(Button63)
        setUpButton(Button64)

    End Sub

    Public Sub SetUpChampionPicture()
        championTickBox1 = New ChampionTickBox(Champion1, ChampionBox1, My.Resources.desature_knight, My.Resources.praetorian_tercius_by_peterprime_d5ijpbl, Game.Champion.Name.Knight)
        championTickBox2 = New ChampionTickBox(Champion2, ChampionBox2, My.Resources.desature_lord, My.Resources.dragon_lord_image, Game.Champion.Name.Lord)
        championTickBox3 = New ChampionTickBox(Champion3, ChampionBox3, My.Resources.desature_druid, My.Resources.druidimage, Game.Champion.Name.Sage)
        championTickBox4 = New ChampionTickBox(Champion4, ChampionBox4, My.Resources.desature_witch, My.Resources.witch_character, Game.Champion.Name.Witch)
        championTickBox5 = New ChampionTickBox(Champion5, ChampionBox5, My.Resources.desature_knight, My.Resources.praetorian_tercius_by_peterprime_d5ijpbl, Game.Champion.Name.Knight)
        championTickBox6 = New ChampionTickBox(Champion6, ChampionBox6, My.Resources.desature_lord, My.Resources.dragon_lord_image, Game.Champion.Name.Lord)
        championTickBox7 = New ChampionTickBox(Champion7, ChampionBox7, My.Resources.desature_druid, My.Resources.druidimage, Game.Champion.Name.Sage)
        championTickBox8 = New ChampionTickBox(Champion8, ChampionBox8, My.Resources.desature_witch, My.Resources.witch_character, Game.Champion.Name.Witch)
    End Sub
    Private Class ChampionTickBox
        Private PictureBoxRef As PictureBox
        Private CheckBoxRef As CheckBox
        Private SelectedImage As Bitmap
        Private Champion As Game.Champion.Name
        Public Sub New(ByRef PictureBoxRef As PictureBox, ByRef CheckBoxRef As CheckBox, ByVal desatureImage As Bitmap, ByVal selectedImage As Bitmap, ByVal Champion As Game.Champion.Name)
            Me.PictureBoxRef = PictureBoxRef
            Me.CheckBoxRef = CheckBoxRef
            Me.SelectedImage = selectedImage
            PictureBoxRef.BackgroundImage = desatureImage
            AddHandler CheckBoxRef.CheckedChanged, AddressOf Me.HandleBoxClick
            AddHandler PictureBoxRef.MouseEnter, AddressOf Me.HandleBoxHover
            AddHandler PictureBoxRef.MouseLeave, AddressOf Me.HandleBoxHoverLeave
            My.Forms.Launcher.DynamicScale(PictureBoxRef)
            My.Forms.Launcher.DynamicScale(CheckBoxRef)
            Me.Champion = Champion
        End Sub
        Public Sub HandleBoxClick(sender As Object, e As EventArgs)
            If PictureBoxRef.Image Is Nothing Then
                PictureBoxRef.Image = SelectedImage
            Else
                PictureBoxRef.Image = Nothing
            End If
        End Sub
        Public Sub HandleBoxHover(sender As Object, e As EventArgs)
            Dim temp As New Game.Champion(Champion, Nothing)
            My.Forms.Launcher.DisplayHoveringMessage("------ ABILITY ------ " + temp.ReturnAbilityDescription)
        End Sub
        Public Sub HandleBoxHoverLeave(sender As Object, e As EventArgs)
            My.Forms.Launcher.TopMost = True
            My.Forms.HoverMessage.Close()
            My.Forms.Launcher.TopMost = False
            My.Forms.Launcher.Timer2.Stop()
        End Sub
    End Class

    Private Sub PresetDeckBox_CheckedChanged(sender As Object, e As EventArgs) Handles PresetDeckBox.CheckedChanged
        If PresetDeck = True Then
            PresetDeck = False
        ElseIf PresetDeck = False Then
            PresetDeck = True
        End If
        DataGridView1.Rows.Clear()
        Deck.Clear()
        Label4.Text = "Your Deck (" + Convert.ToString(DataGridView1.Rows.Count) + " / 30)"
    End Sub

    Private Sub RandomChampionBox_CheckedChanged(sender As Object, e As EventArgs) Handles RandomChampionBox.CheckedChanged
        If RandomOpponentChampion = True Then
            RandomOpponentChampion = False
        ElseIf RandomOpponentChampion = False Then
            RandomOpponentChampion = True
        End If
        If sender.checked = True Then
            ChampionBox5.Checked = False
            ChampionBox6.Checked = False
            ChampionBox7.Checked = False
            ChampionBox8.Checked = False
        End If
    End Sub

    Private Sub ChampionBox1_CheckedChanged(sender As Object, e As EventArgs) Handles ChampionBox1.CheckedChanged
        If sender.checked = True Then
            ChampionBox2.Checked = False
            ChampionBox3.Checked = False
            ChampionBox4.Checked = False
        End If
        DataGridView1.Rows.Clear()
        Deck.Clear()
        Label4.Text = "Your Deck (" + Convert.ToString(DataGridView1.Rows.Count) + " / 30)"
    End Sub

    Private Sub ChampionBox2_CheckedChanged(sender As Object, e As EventArgs) Handles ChampionBox2.CheckedChanged
        If sender.checked = True Then
            ChampionBox1.Checked = False
            ChampionBox3.Checked = False
            ChampionBox4.Checked = False
        End If
        DataGridView1.Rows.Clear()
        Deck.Clear()
        Label4.Text = "Your Deck (" + Convert.ToString(DataGridView1.Rows.Count) + " / 30)"
    End Sub

    Private Sub ChampionBox3_CheckedChanged(sender As Object, e As EventArgs) Handles ChampionBox3.CheckedChanged
        If sender.checked = True Then
            ChampionBox2.Checked = False
            ChampionBox1.Checked = False
            ChampionBox4.Checked = False
        End If
        DataGridView1.Rows.Clear()
        Deck.Clear()
        Label4.Text = "Your Deck (" + Convert.ToString(DataGridView1.Rows.Count) + " / 30)"
    End Sub

    Private Sub ChampionBox4_CheckedChanged(sender As Object, e As EventArgs) Handles ChampionBox4.CheckedChanged
        If sender.checked = True Then
            ChampionBox2.Checked = False
            ChampionBox3.Checked = False
            ChampionBox1.Checked = False
        End If
        DataGridView1.Rows.Clear()
        Deck.Clear()
        Label4.Text = "Your Deck (" + Convert.ToString(DataGridView1.Rows.Count) + " / 30)"
    End Sub

    Private Sub ChampionBox5_CheckedChanged(sender As Object, e As EventArgs) Handles ChampionBox5.CheckedChanged
        If sender.checked = True Then
            ChampionBox6.Checked = False
            ChampionBox7.Checked = False
            ChampionBox8.Checked = False
            RandomChampionBox.Checked = False
        End If
    End Sub

    Private Sub ChampionBox6_CheckedChanged(sender As Object, e As EventArgs) Handles ChampionBox6.CheckedChanged
        If sender.checked = True Then
            ChampionBox5.Checked = False
            ChampionBox7.Checked = False
            ChampionBox8.Checked = False
            RandomChampionBox.Checked = False
        End If
    End Sub

    Private Sub ChampionBox7_CheckedChanged(sender As Object, e As EventArgs) Handles ChampionBox7.CheckedChanged
        If sender.checked = True Then
            ChampionBox6.Checked = False
            ChampionBox5.Checked = False
            ChampionBox8.Checked = False
            RandomChampionBox.Checked = False
        End If
    End Sub

    Private Sub ChampionBox8_CheckedChanged(sender As Object, e As EventArgs) Handles ChampionBox8.CheckedChanged
        If sender.checked = True Then
            ChampionBox6.Checked = False
            ChampionBox7.Checked = False
            ChampionBox5.Checked = False
            RandomChampionBox.Checked = False
        End If
    End Sub
    Private Sub setUpData()
        If ChampionBox1.Checked = True Then YourChampion = Game.Champion.Name.Knight
        If ChampionBox2.Checked = True Then YourChampion = Game.Champion.Name.Lord
        If ChampionBox3.Checked = True Then YourChampion = Game.Champion.Name.Sage
        If ChampionBox4.Checked = True Then YourChampion = Game.Champion.Name.Witch
        If ChampionBox5.Checked = True Then OpponentChampion = Game.Champion.Name.Knight
        If ChampionBox6.Checked = True Then OpponentChampion = Game.Champion.Name.Lord
        If ChampionBox7.Checked = True Then OpponentChampion = Game.Champion.Name.Sage
        If ChampionBox8.Checked = True Then OpponentChampion = Game.Champion.Name.Witch
    End Sub
    Private Sub sendData()
        Dim sendDeck As New Collection
        For i = 1 To Deck.Count
            sendDeck.Add(Deck.Item(i))
        Next
        My.Forms.Game.YourDeck = sendDeck
        My.Forms.Game.YourCHampionName = Me.YourChampion
        My.Forms.Game.AIChampionName = Me.OpponentChampion
        My.Forms.Game.RandomAIChampion = Me.RandomOpponentChampion
        My.Forms.Game.PresetDeck = Me.PresetDeck
    End Sub
    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles PlayCmd.Click
        Dim message As String
        Dim DeckFull As Boolean = False
        If PresetDeck = True Or Deck.Count = 30 Then
            DeckFull = True
        End If
        If DeckFull = True And ChampionSelected() = True And (OpponentChampionSelected() Or RandomOpponentChampion) Then
            setUpData()
            sendData()
            My.Forms.Background_Music_Player.AxWindowsMediaPlayer1.Ctlcontrols.pause()
            My.Computer.Audio.Play(My.Resources.cloth, AudioPlayMode.Background)
            My.Forms.Game.Show()
        ElseIf ChampionSelected() = False Then
            message = ("You Have No Champion Selected")
        ElseIf OpponentChampionSelected() = False And RandomOpponentChampion = False Then
            message = ("You Have No Champion Selected For Your Opponent")
        ElseIf DeckFull = False Then
            message = ("Your deck Is Not Complete")
        End If
        If message IsNot Nothing Then
            PopUpMessage(message)
        End If
    End Sub
    Private Sub PopUpMessage(ByVal message As String)
        My.Forms.PopupMessage.Owner = Me
        My.Forms.PopupMessage.Hide()
        My.Forms.PopupMessage.StopTimers()
        My.Forms.PopupMessage.Message = message
        My.Computer.Audio.Play(My.Resources.cloth, AudioPlayMode.Background)
        My.Forms.PopupMessage.ShowDialog(Me)
    End Sub

    Private Sub Button1_MouseHover(sender As Object, e As EventArgs) Handles PlayCmd.MouseEnter
        sender.BackgroundImage = My.Resources.buttonStock1d_hover
    End Sub

    Private Sub Button1_MouseLeave(sender As Object, e As EventArgs) Handles PlayCmd.MouseLeave
        sender.BackgroundImage = My.Resources.buttonStock1d
    End Sub

    Private Sub ButtonCardClick(sender As Object, e As EventArgs)
        Dim cardobj As Object
        Dim message As String
        If DataGridView1.Rows.Count < 30 And ChampionSelected() And PresetDeckBox.Checked = False Then
            cardobj = List.GetCard(sender.TabIndex + 1)
            If CheckDeck(cardobj) And CheckCardAvailable(cardobj) Then
                Deck.Add(cardobj)
                Dim row As String() = {sender.text}
                DataGridView1.Rows.Add(row)
                Label4.Text = "Your Deck (" + Convert.ToString(DataGridView1.Rows.Count) + " / 30)"
                My.Computer.Audio.Play(My.Resources.Play_Card, AudioPlayMode.Background)
            ElseIf CheckDeck(cardobj) = False Then
                message = ("You Can't Have Any More Of This Card")
            ElseIf CheckCardAvailable(cardobj) = False Then
                message = ("YouR Champion Can't use This Card")
            End If
        ElseIf ChampionSelected() = False Then
            message = "You Have No Champion Selected"
        ElseIf DataGridView1.Rows.Count >= 30 Then
            message = "Your Deck is Full"
        End If
        If message IsNot Nothing Then
            PopUpMessage(message)
        End If

    End Sub
    Private Function CheckDeck(ByRef Card As Object)
        Dim int As Integer = 0
        Dim count As Integer = 0
        If Card.IsSpecial = True Then int = 1 Else int = 2
        For i = 1 To Deck.Count
            If Card Is Deck.Item(i) Then
                count += 1
            End If
        Next
        If count >= int Then Return False
        Return True
    End Function
    Private Function CheckCardAvailable(ByRef CardObj As Object)
        If ChampionBox1.Checked = True Then
            If CardObj.GetCardID = 2 Then Return False
            If CardObj.GetCardID = 10 Then Return False
            If CardObj.GetCardID = 11 Then Return False
            If CardObj.GetCardID = 9 Then Return False
            If CardObj.GetCardID = 15 Then Return False
            If CardObj.GetCardID = 16 Then Return False
            If CardObj.GetCardID = 26 Then Return False
            If CardObj.GetCardID = 30 Then Return False
            If CardObj.GetCardID = 34 Then Return False
            If CardObj.GetCardID = 39 Then Return False
        End If
        If ChampionBox2.Checked = True Then
            If CardObj.GetCardID = 2 Then Return False
            If CardObj.GetCardID = 10 Then Return False
            If CardObj.GetCardID = 11 Then Return False
            If CardObj.GetCardID = 9 Then Return False
            If CardObj.GetCardID = 15 Then Return False
            If CardObj.GetCardID = 3 Then Return False
            If CardObj.GetCardID = 35 Then Return False
            If CardObj.GetCardID = 30 Then Return False
            If CardObj.GetCardID = 34 Then Return False
            If CardObj.GetCardID = 23 Then Return False
        End If
        If ChampionBox3.Checked = True Then
            If CardObj.GetCardID = 2 Then Return False
            If CardObj.GetCardID = 39 Then Return False
            If CardObj.GetCardID = 26 Then Return False
            If CardObj.GetCardID = 9 Then Return False
            If CardObj.GetCardID = 15 Then Return False
            If CardObj.GetCardID = 3 Then Return False
            If CardObj.GetCardID = 35 Then Return False
            If CardObj.GetCardID = 30 Then Return False
            If CardObj.GetCardID = 16 Then Return False
            If CardObj.GetCardID = 23 Then Return False
        End If
        If ChampionBox4.Checked = True Then
            If CardObj.GetCardID = 10 Then Return False
            If CardObj.GetCardID = 39 Then Return False
            If CardObj.GetCardID = 26 Then Return False
            If CardObj.GetCardID = 34 Then Return False
            If CardObj.GetCardID = 11 Then Return False
            If CardObj.GetCardID = 3 Then Return False
            If CardObj.GetCardID = 35 Then Return False
            If CardObj.GetCardID = 16 Then Return False
            If CardObj.GetCardID = 23 Then Return False
        End If

        Return True
    End Function
    Private Function ChampionSelected() As Boolean
        If ChampionBox1.Checked = True Then Return True
        If ChampionBox2.Checked = True Then Return True
        If ChampionBox3.Checked = True Then Return True
        If ChampionBox4.Checked = True Then Return True
        Return False
    End Function
    Private Function OpponentChampionSelected() As Boolean
        If ChampionBox5.Checked = True Then Return True
        If ChampionBox6.Checked = True Then Return True
        If ChampionBox7.Checked = True Then Return True
        If ChampionBox8.Checked = True Then Return True
        Return False
    End Function
    Private Sub DataGridView1_SelectionChanged(sender As Object, e As EventArgs) Handles DataGridView1.SelectionChanged
        sender.ClearSelection
    End Sub

    Private Sub ButtonCardMouseEnter(sender As Object, e As EventArgs)
        PictureBox1.Image = List.GetCard(sender.TabIndex + 1).ReturnImage
    End Sub
    Private Sub ButtonCardMouseLeave_1(sender As Object, e As EventArgs)
        PictureBox1.Image = Nothing
    End Sub
    Private Sub DataGridView1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        Dim int As Integer = e.RowIndex
        Dim Cell As DataGridViewRow = DataGridView1.Rows(int)
        DataGridView1.Rows.Remove(Cell)
        Deck.Remove(int + 1)
        Label4.Text = "Your Deck (" + Convert.ToString(DataGridView1.Rows.Count) + " / 30)"
        My.Computer.Audio.Play(My.Resources.Draw_Cards, AudioPlayMode.Background)
        PictureBox1.Image = Nothing
    End Sub
    Private Sub DataGridView1_CellMouseEnter(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellMouseEnter
        Dim int As Integer = e.RowIndex
        Dim Cell As DataGridViewRow = DataGridView1.Rows(int)
        PictureBox1.Image = Deck.Item(int + 1).ReturnImage
    End Sub
    Private Sub DataGridView1_CellMouseLeave(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellMouseLeave
        PictureBox1.Image = Nothing
    End Sub
    Private Sub PlayBackgroundMusic()
        My.Computer.Audio.Play(My.Resources.background_music, AudioPlayMode.BackgroundLoop)
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Stop()
        Me.Opacity = 1
        My.Forms.LoadingScreen.Hide()
        My.Forms.Background_Music_Player.PlayMusic()
    End Sub
    Private Sub Button66_Click(sender As Object, e As EventArgs) Handles Button66.Click

        My.Forms.BlurrBackGround.Owner = Me
        My.Forms.BlurrBackGround.Show()
        My.Forms.Document.LoadFile(My.Resources.Dreadmare_Instructions)
        My.Computer.Audio.Play(My.Resources.cloth, AudioPlayMode.Background)
        My.Forms.Document.ShowDialog(Me)
    End Sub

    Private Sub Button65_Click(sender As Object, e As EventArgs) Handles Button65.Click
        Me.Close()
    End Sub
    Public Sub DisplayHoveringMessage(ByVal message As String)
        My.Forms.HoverMessage.Owner = Me
        Timer2.Stop()
        Timer2.Tag = message
        Timer2.Start()
    End Sub
    Private Sub Button65_MouseHover(sender As Object, e As EventArgs) Handles Button65.MouseEnter
        DisplayHoveringMessage("Exit")
    End Sub

    Private Sub Button66_MouseHover(sender As Object, e As EventArgs) Handles Button66.MouseEnter
        DisplayHoveringMessage("Instructions")
    End Sub

    Private Sub Button67_MouseHover(sender As Object, e As EventArgs) Handles Button67.MouseEnter
        DisplayHoveringMessage("Credits")
    End Sub
    Private Sub Button68_MouseHover(sender As Object, e As EventArgs) Handles Button68.MouseEnter
        DisplayHoveringMessage("Mute/UnMute Music")
    End Sub
    Private Sub closeHoveringMessage(sender As Object, e As EventArgs) Handles Button65.MouseLeave, Button66.MouseLeave, Button67.MouseLeave, Button68.MouseLeave
        My.Forms.Launcher.TopMost = True
        My.Forms.HoverMessage.Hide()
        My.Forms.Launcher.TopMost = False
        Timer2.Stop()
    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        My.Forms.HoverMessage.Owner = Me
        My.Forms.HoverMessage.Message = Timer2.Tag
        My.Forms.HoverMessage.Show()
    End Sub

    Private Sub Button67_Click(sender As Object, e As EventArgs) Handles Button67.Click
        My.Forms.BlurrBackGround.Owner = Me
        My.Forms.BlurrBackGround.Show()
        My.Forms.Document.LoadFile(My.Resources.Epq_References)
        My.Computer.Audio.Play(My.Resources.cloth, AudioPlayMode.Background)
        My.Forms.Document.ShowDialog(Me)
    End Sub

    Private Sub Button68_Click(sender As Object, e As EventArgs) Handles Button68.Click
        Dim mute As Boolean = My.Forms.Background_Music_Player.AxWindowsMediaPlayer1.settings.mute
        If mute Then My.Forms.Background_Music_Player.AxWindowsMediaPlayer1.settings.mute = False Else My.Forms.Background_Music_Player.AxWindowsMediaPlayer1.settings.mute = True
    End Sub

    Private Sub Timer3_Tick(sender As Object, e As EventArgs) Handles Timer3.Tick
        Timer3.Stop()
        My.Forms.LoadingScreen.Close()
        My.Forms.Background_Music_Player.PlayMusic()
    End Sub


End Class