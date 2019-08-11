
Public Class Game
    Const PlayerHandSize As Integer = 10
    Dim Player1Deck As DeckPile
    Dim Player1Hand As Hand
    Dim Player1Buff As BuffDisplay
    Dim Player1Board As PlayerBoard
    Dim Player1DiscardPile As DiscardPile
    Dim Player1Champion As Champion
    Dim Player1BattlePower As BattlePower
    Dim Player1Life As Lifes
    Dim Player2Deck As DeckPile
    Dim Player2Hand As Hand
    Dim Player2Buff As BuffDisplay
    Dim Player2Board As PlayerBoard
    Dim Player2DiscardPile As DiscardPile
    Dim Player2Champion As Champion
    Dim Player2BattlePower As BattlePower
    Dim Player2Life As Lifes
    Dim AURA As AuraDisplay
    Dim PictureCards As CardList
    Dim AIPlayer As AI
    '------all other variables------'
    Dim PlayerTurn As Integer
    Dim tempPic As Bitmap
    Public PlayableState As Boolean = False
    Dim Player1Passed As Boolean
    Dim Player2Passed As Boolean
    '------input variables------'
    Public PresetDeck As Boolean
    Public RandomAIChampion As Boolean
    Public YourCHampionName As Champion.Name
    Public AIChampionName As Champion.Name
    Public YourDeck As Collection



    '-------animation variables-----'
    Public Sub DynamicScale(ByRef sender As Object)
        sender.Size = New Size(sender.Size.Width * (Me.Width / 1536), sender.Size.Height * (Me.Height / 864))

        sender.Location = New Point(sender.Location.X * (Me.Width / 1536), sender.Location.Y * (Me.Height / 864))
    End Sub

    Private Sub PerformDynamicScale()
        DynamicScale(FlowLayoutPanel1)
        DynamicScale(FlowLayoutPanel2)
        DynamicScale(FlowLayoutPanel3)
        DynamicScale(FlowLayoutPanel4)
        DynamicScale(PictureBox46)
        DynamicScale(PictureBox47)
        DynamicScale(Passed1)
        DynamicScale(Passed2)
        DynamicScale(PictureBox50)
        DynamicScale(Label3)
        DynamicScale(Label5)
        DynamicScale(Label8)
        DynamicScale(Label7)
        DynamicScale(Label6)
        DynamicScale(Label4)
        DynamicScale(PictureBox49)
        DynamicScale(Player1DiscardNumber)
        DynamicScale(Player2DiscardNumber)
        DynamicScale(Button1)
        DynamicScale(Button2)
        DynamicScale(Button66)
        DynamicScale(EndTurn)

    End Sub

    Public Sub SetWInnerBold()
        If Player1BattlePower.getpower > Player2BattlePower.getpower Then
            Player1BattlePower.boldtext(True)
            Player2BattlePower.boldtext(False)
        ElseIf Player1BattlePower.getpower < Player2BattlePower.getpower Then
            Player2BattlePower.boldtext(True)
            Player1BattlePower.boldtext(False)
        Else
            Player1BattlePower.boldtext(False)
            Player2BattlePower.boldtext(False)
        End If
    End Sub
    Public Sub NewRound()
        PlayableState = False
        Dim playerwin As Integer = 0
        If Player1BattlePower.GetPower > Player2BattlePower.GetPower Then
            Player2Life.LooseLife()
            playerwin = 1
        End If
        If Player1BattlePower.GetPower < Player2BattlePower.GetPower Then
            Player1Life.LooseLife()
            playerwin = 2
        End If
        If Player1BattlePower.GetPower = Player2BattlePower.GetPower Then
            Player1Life.LooseLife()
            Player2Life.LooseLife()
            playerwin = 0
        End If

        WInRoundTimer.Tag = playerwin
        WInRoundTimer.Start()

    End Sub
    Public Sub EndGame(ByVal Winner As Integer)
        If Winner = 1 Then
            WinnerDisplayTimer.Tag = 1
            Player1Champion.Refresh()
        End If
        If Winner = 2 Then
            WinnerDisplayTimer.Tag = 2
            Player2Champion.Refresh()
        End If
        If Winner = 0 Then
            WinnerDisplayTimer.Tag = 0
        End If
        WinnerDisplayTimer.Start()
    End Sub
    Public Sub Surrender()
        PlayableState = False
        AITimer.Stop()
        DisplayMessage("You Surrender", My.Resources.flying_flag__1_, My.Resources.cloth)
        If AIPlayer.GetPlayer = 1 Then
            Player2Life.LooseLife()
            Player2Life.LooseLife()
        Else
            Player1Life.LooseLife()
            Player1Life.LooseLife()
        End If
        WinnerDisplayTimer.Interval = 4000
        EndGame(AIPlayer.GetPlayer)
    End Sub
    Public Sub NewTurn()
        PlayableState = True
        If Player1Passed = True And Player2Passed = True Then
            NewRound()
            GoTo SkipReferesh
        End If
        If PlayerTurn = 1 And Player2Passed = False Then
            PictureBox50.Image = Nothing
            PlayerTurn = 2
            PictureBox49.Image = My.Resources.pointy_sword
        ElseIf PlayerTurn = 2 And Player1Passed = False Then
            PictureBox49.Image = Nothing
            PlayerTurn = 1
            PictureBox50.Image = My.Resources.pointy_sword
        End If
        If PlayerTurn = AIPlayer.GetPlayer Then
            AITimer.Start()
        End If

        RefreshBuffEffect()
SkipReferesh:
        SetWInnerBold()
    End Sub
    Public Function RandomChampion() As Champion.Name
        Dim int As Integer = randomnumber(4, 1)
        If int = 1 Then Return Champion.Name.Knight
        If int = 2 Then Return Champion.Name.Lord
        If int = 3 Then Return Champion.Name.Sage
        If int = 4 Then Return Champion.Name.Witch
    End Function
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Owner = My.Forms.Launcher
        Me.Opacity = 0

        LoadingScreen.Show()
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        DoubleBuffered = True
        Me.FormBorderStyle = FormBorderStyle.None
        Me.WindowState = FormWindowState.Maximized

        If RandomAIChampion Then
            AIChampionName = RandomChampion()
        End If
        Dim Player1ChampionName As Champion.Name = AIChampionName
        Dim PLayer2ChampionName As Champion.Name = YourCHampionName

        Dim AIPlayerInt As Integer = 1 ' set the AIplayer to 1
        PlayerTurn = MathFunc.randomnumber(2, 1)
        AIPlayer = New AI(AIPlayerInt, Player1ChampionName, PLayer2ChampionName, PlayerTurn)


        Player1DiscardPile = New DiscardPile(PictureBox44, Player1DiscardNumber)
        Player1Hand = New Hand(PictureBox1, PictureBox2, PictureBox3, PictureBox4, PictureBox5, PictureBox6, PictureBox7, PictureBox8, PictureBox9, PictureBox10, MathFunc.isEqualTo(AIPlayerInt, 1))
        Player1Board = New PlayerBoard(PictureBox11, PictureBox12, PictureBox13, PictureBox14, PictureBox15, PictureBox16, PictureBox17, PictureBox18, PictureBox19, PictureBox20)
        Player1Buff = New BuffDisplay(PictureBox41)
        Player1Champion = New Champion(Player1ChampionName, PictureBox47)
        Player1BattlePower = New BattlePower(Button3)
        Player1Life = New Lifes(PictureBox51, characterLife1)

        Player2DiscardPile = New DiscardPile(PictureBox43, Player2DiscardNumber)
        Player2Hand = New Hand(PictureBox21, PictureBox22, PictureBox23, PictureBox24, PictureBox25, PictureBox26, PictureBox27, PictureBox28, PictureBox29, PictureBox30, MathFunc.isEqualTo(AIPlayerInt, 2))
        Player2Board = New PlayerBoard(PictureBox31, PictureBox32, PictureBox33, PictureBox34, PictureBox35, PictureBox36, PictureBox37, PictureBox38, PictureBox39, PictureBox40)
        Player2Buff = New BuffDisplay(PictureBox42)
        Player2Champion = New Champion(PLayer2ChampionName, PictureBox46)
        Player2BattlePower = New BattlePower(Button4)
        Player2Life = New Lifes(PictureBox52, PictureBox53)

        AURA = New AuraDisplay(PictureBox45)

        Dim builder As New DeckBuilder

        PictureCards = New CardList

        Player1Deck = builder.BuildDeck(Player1Champion.ReturnName, 1)
        Player1Deck.shuffle(100)

        If PresetDeck = True Then
            Player2Deck = builder.BuildDeck(Player2Champion.ReturnName, 2)
        Else
            Player2Deck = New DeckPile(YourDeck, Label3, PictureBox54)
        End If
        Player2Deck.shuffle(100)

        For i = 1 To 10
            DrawCard(1)
            DrawCard(2)
        Next


        My.Forms.SpellDisplay.Show()
        My.Forms.CardDisplay.Show()
        My.Forms.MessageDisplay.Show()
        My.Forms.SpellDisplay.Hide()
        My.Forms.CardDisplay.Hide()
        My.Forms.MessageDisplay.Hide()
        My.Forms.BlurrBackGround.Owner = Me
        PerformDynamicScale()
        LoadingScreenTimer.Start()

    End Sub
    Private Sub CentrePictureBox(ByRef Reference As PictureBox)
        Dim w As Integer = My.Forms.Game.Width / 2
        Dim h As Integer = My.Forms.Game.Height / 2
        Dim refw As Integer = Reference.Height / 2
        Dim refh As Integer = Reference.Width / 2
        Dim punt As New Point
        punt.X = w - refw
        punt.Y = h - refh
        Reference.Location = punt
    End Sub
    Public Sub ChampionBoxDoubleClick(sender As Object, e As EventArgs)
        If PlayableState = True Then
            If sender Is PictureBox47 Then
                If PlayerTurn = 1 Then
                    If Player1Champion.IsUsed = False Then
                        PlayChampionAbility(Player1Champion.ReturnAbility)
                        Player1Champion.UseAbility()
                        ChampionAbilityDisplay(1)
                        NewTurn()
                    Else
                        PopUpMessage("Champion Ability Already Used")
                    End If

                End If
            ElseIf sender Is PictureBox46 Then
                If PlayerTurn = 2 Then
                    If Player2Champion.IsUsed = False Then
                        PlayChampionAbility(Player2Champion.ReturnAbility)
                        Player2Champion.UseAbility()
                        ChampionAbilityDisplay(2)
                        NewTurn()
                    Else
                        PopUpMessage("Champion Ability Already Used")
                    End If
                End If
            End If
        End If
    End Sub
    Public Sub GiveAIInfo(ByRef selectAi As AI)
        If selectAi.GetPlayer = 1 Then
            selectAi.getInfo(Player1BattlePower.GetPower, Player2BattlePower.GetPower, Player1Hand.GetCardCollection, Player1Board.GetCardCollection,
                             Player2Board.GetCardCollection, Player2Passed, Player2Hand.GetCardCollection.Count, Player1Life.GetLifes, Player2Life.GetLifes,
                             Player2Board.IsBuffPlayed, Player1Board.IsBuffPlayed, Player1DiscardPile.Item.Count)
        End If
        If selectAi.GetPlayer = 2 Then
            selectAi.getInfo(Player2BattlePower.GetPower, Player1BattlePower.GetPower, Player2Hand.GetCardCollection, Player2Board.GetCardCollection,
                             Player1Board.GetCardCollection, Player1Passed, Player1Hand.GetCardCollection.Count, Player2Life.GetLifes,
                             Player1Life.GetLifes, Player1Board.IsBuffPlayed, Player2Board.IsBuffPlayed, Player2DiscardPile.Item.Count)
        End If
    End Sub
    Public Sub PlayChampionAbility(ByVal ability As Champion.Ability)
        If ability = Champion.Ability.Onslaught Then
            PlaySpell(PictureCards.getCard(45))
        End If
        If ability = Champion.Ability.Destiny Then
            Dim tempObj As Object = PictureCards.GetCard(65)
            If PlayerTurn = 1 Then
                If Player1Board.IsNotfull = True Then
                    Player1Board.add(tempObj)
                    Player1BattlePower.add(tempObj.getAttack_Value)
                    RefreshBuffEffect()
                End If
            ElseIf PlayerTurn = 2 Then
                If Player2Board.IsNotfull = True Then
                    Player2Board.add(tempObj)
                    Player2BattlePower.add(tempObj.getAttack_Value)
                    RefreshBuffEffect()
                End If
            End If
        End If
        If ability = Champion.Ability.Endarken Then
            CastSpell(Spell.CardEffect.DestroyRandomMinion)
            CastSpell(Spell.CardEffect.DestroyRandomMinion)
        End If
        If ability = Champion.Ability.Purify Then
            If PlayerTurn = 1 Then PlayBuff(Nothing, 2)
            If PlayerTurn = 2 Then PlayBuff(Nothing, 1)
            PlayAURA(Nothing)
        End If
    End Sub
    Public Sub PlaySpell(ByRef Spellobj As Spell)
        If Spellobj.GetSPellType = Spell.SpellType.Instant Then CastSpell(Spellobj.GetSpellEffect)
        If Spellobj.GetSPellType = Spell.SpellType.Buff Then PlayBuff(Spellobj, PlayerTurn)
        If Spellobj.GetSPellType = Spell.SpellType.Aura Then PlayAURA(Spellobj)
        DisplayLargeSpell(Spellobj.ReturnImage)

    End Sub
    Public Sub PlayAURA(ByRef Spellobj As Spell)
        Dim int As Integer
        Dim AURAInt As Integer
        If Spellobj Is Nothing Then
            AURA.NullCardRef()
            Player1BattlePower.add((-1) * Player1Board.AURAPower)
            Player1Board.AURAPower = 0

            Player2BattlePower.add((-1) * Player2Board.AURAPower)
            Player2Board.AURAPower = 0
            GoTo finishAURA
        End If
        AURA.AddCardRef(Spellobj)
        If Spellobj.GetSpellEffect = Spell.CardEffect.Dragon10Aura Then
            AURAInt = Player1Board.AURAPower
            int = (Player1Board.ReturnSpeciesBuffNumber(CharacterCard.CardSpecies.Dragon) * 20) - AURAInt
            Player1BattlePower.Add(int)
            Player1Board.AURAPower = int + AURAInt

            AURAInt = Player2Board.AURAPower
            int = (Player2Board.ReturnSpeciesBuffNumber(CharacterCard.CardSpecies.Dragon) * 20) - AURAInt
            Player2BattlePower.Add(int)
            Player2Board.AURAPower = int + AURAInt
        End If

        If Spellobj.GetSpellEffect = Spell.CardEffect.Spirit10Aura Then
            AURAInt = Player1Board.AURAPower
            int = (Player1Board.ReturnSpeciesBuffNumber(CharacterCard.CardSpecies.Spirit) * 10) - AURAInt
            Player1BattlePower.Add(int)
            Player1Board.AURAPower = int + AURAInt

            AURAInt = Player2Board.AURAPower
            int = (Player2Board.ReturnSpeciesBuffNumber(CharacterCard.CardSpecies.Spirit) * 10) - AURAInt
            Player2BattlePower.Add(int)
            Player2Board.AURAPower = int + AURAInt
        End If

        If Spellobj.GetSpellEffect = Spell.CardEffect.Law15Aura Then
            AURAInt = Player1Board.AURAPower
            int = (Player1Board.ReturnTypeBuffNumber(CharacterCard.CardTypes.Law) * 35) - AURAInt
            Player1BattlePower.Add(int)
            Player1Board.AURAPower = int + AURAInt

            AURAInt = Player2Board.AURAPower
            int = (Player2Board.ReturnTypeBuffNumber(CharacterCard.CardTypes.Law) * 35) - AURAInt
            Player2BattlePower.Add(int)
            Player2Board.AURAPower = int + AURAInt
        End If

        If Spellobj.GetSpellEffect = Spell.CardEffect.NullifyAura Then
            Player1BattlePower.add((-1) * Player1Board.AURAPower)
            Player1Board.AURAPower = 0

            Player2BattlePower.add((-1) * Player2Board.AURAPower)
            Player2Board.AURAPower = 0
        End If
finishAURA:
    End Sub
    Public Sub PlayBuff(ByRef Spellobj As Spell, ByVal PlayerTurn As Integer)
        Dim int As Integer
        Dim BuffInt As Integer
        If Spellobj Is Nothing Then
            If PlayerTurn = 1 Then
                Player1Buff.NullBuff()
                Player1Board.BuffPlayed(Nothing)
                BuffInt = Player1Board.BuffPower
                Player1BattlePower.Add((-1) * BuffInt)
                Player1Board.BuffPower = 0
            End If
            If PlayerTurn = 2 Then
                Player2Buff.NullBuff()
                Player2Board.BuffPlayed(Nothing)
                BuffInt = Player2Board.BuffPower
                Player2BattlePower.Add((-1) * BuffInt)
                Player2Board.BuffPower = 0
            End If
            GoTo finish
        End If
        If PlayerTurn = 1 Then
            Player1Buff.AddCardRef(Spellobj)
            Player1Board.BuffPlayed(Spellobj)
        End If
        If PlayerTurn = 2 Then
            Player2Buff.addcardref(Spellobj)
            Player2Board.BuffPlayed(Spellobj)
        End If
        If Spellobj.GetSpellEffect() = Spell.CardEffect.Nature70Buff Then
            If PlayerTurn = 1 Then
                BuffInt = Player1Board.BuffPower
                int = (Player1Board.ReturnTypeBuffNumber(CharacterCard.CardTypes.Nature) * 50) - BuffInt
                Player1BattlePower.Add(int)
                Player1Board.BuffPower = int + BuffInt
            End If
            If PlayerTurn = 2 Then
                BuffInt = Player2Board.BuffPower
                int = (Player2Board.ReturnTypeBuffNumber(CharacterCard.CardTypes.Nature) * 50) - BuffInt
                Player2BattlePower.Add(int)
                Player2Board.BuffPower = int + BuffInt
            End If
        End If
        If Spellobj.GetSpellEffect() = Spell.CardEffect.Fairy80Buff Then
            If PlayerTurn = 1 Then
                BuffInt = Player1Board.BuffPower
                int = (Player1Board.ReturnSpeciesBuffNumber(CharacterCard.CardSpecies.Fairy) * 40) - BuffInt
                Player1BattlePower.Add(int)
                Player1Board.BuffPower = int + BuffInt
            End If
            If PlayerTurn = 2 Then
                BuffInt = Player2Board.BuffPower
                int = (Player2Board.ReturnSpeciesBuffNumber(CharacterCard.CardSpecies.Fairy) * 40) - BuffInt
                Player2BattlePower.Add(int)
                Player2Board.BuffPower = int + BuffInt
            End If
        End If
        If Spellobj.GetSpellEffect() = Spell.CardEffect.Dragon50Buff Then
            If PlayerTurn = 1 Then
                BuffInt = Player1Board.BuffPower
                int = (Player1Board.ReturnSpeciesBuffNumber(CharacterCard.CardSpecies.Dragon) * 80) - BuffInt
                Player1BattlePower.Add(int)
                Player1Board.BuffPower = int + BuffInt
            End If
            If PlayerTurn = 2 Then
                BuffInt = Player2Board.BuffPower
                int = (Player2Board.ReturnSpeciesBuffNumber(CharacterCard.CardSpecies.Dragon) * 80) - BuffInt
                Player2BattlePower.Add(int)
                Player2Board.BuffPower = int + BuffInt
            End If
        End If
        If Spellobj.GetSpellEffect() = Spell.CardEffect.Dark50Buff Then
            If PlayerTurn = 1 Then
                BuffInt = Player1Board.BuffPower
                int = (Player1Board.ReturnTypeBuffNumber(CharacterCard.CardTypes.Dark) * 50) - BuffInt
                Player1BattlePower.Add(int)
                Player1Board.BuffPower = int + BuffInt
            End If
            If PlayerTurn = 2 Then
                BuffInt = Player2Board.BuffPower
                int = (Player2Board.ReturnTypeBuffNumber(CharacterCard.CardTypes.Dark) * 50) - BuffInt
                Player2BattlePower.Add(int)
                Player2Board.BuffPower = int + BuffInt
            End If
        End If
finish:
    End Sub
    Public Sub RefreshBuffEffect()
        Player1Board.RefreshBuff(1)
        Player2Board.RefreshBuff(2)
        PlayAURA(AURA.CardRef)
    End Sub
    Public Sub DiscardCard(ByVal player As Integer)
        If player = 1 Then Player1Hand.RemoveCardByIndex(MathFunc.randomnumber(Player1Hand.Cards.Count, 1))
        If player = 2 Then Player2Hand.RemoveCardByIndex(MathFunc.randomnumber(Player2Hand.Cards.Count, 1))
    End Sub
    Public Sub CastSpell(ByVal effect As Spell.CardEffect)
        If effect = Spell.CardEffect.DrawCard Then
            DrawCard(PlayerTurn)
        End If

        If effect = Spell.CardEffect.Draw2Card Then
            DrawCard(PlayerTurn)
            DrawCard(PlayerTurn)
        End If

        If effect = Spell.CardEffect.Draw3Card Then
            DrawCard(PlayerTurn)
            DrawCard(PlayerTurn)
            DrawCard(PlayerTurn)
        End If

        If effect = Spell.CardEffect.Draw5Card Then
            If PlayerTurn = AIPlayer.GetPlayer Then AIPlayer.AbilityUsed = False
            If PlayerTurn = 1 Then Player1Champion.ReplenishAbility()
            If PlayerTurn = 2 Then Player2Champion.ReplenishAbility()
        End If

        If effect = Spell.CardEffect.DestroyRandomMinion Then
            If PlayerTurn = 1 Then DestroyRandomMinion(2)
            If PlayerTurn = 2 Then DestroyRandomMinion(1)
        End If

        If effect = Spell.CardEffect.Destroy2MinionFor1 Then
            If PlayerTurn = 1 Then
                DestroyRandomMinion(2)
                DestroyRandomMinion(2)
                DestroyRandomMinion(1)
            ElseIf PlayerTurn = 2 Then
                DestroyRandomMinion(2)
                DestroyRandomMinion(1)
                DestroyRandomMinion(1)
            End If
        End If

        If effect = Spell.CardEffect.IncreaseBattlePower10 Then
            If PlayerTurn = 1 Then Player1BattlePower.Add(Player1BattlePower.GetPower * 0.2)
            If PlayerTurn = 2 Then Player2BattlePower.Add(Player2BattlePower.GetPower * 0.2)
        End If

        If effect = Spell.CardEffect.Destory1To5Minions Then
            Dim int As Integer = MathFunc.randomnumber(5, 1)
            For i = 1 To int
                DestroyRandomMinion()
            Next
        End If

        If effect = Spell.CardEffect.DecreaseOpponentPower5 Then
            If PlayerTurn = 1 Then Player1BattlePower.Add((Player1DiscardPile.Item.Count * 20))
            If PlayerTurn = 2 Then Player2BattlePower.Add((Player2DiscardPile.Item.Count * 20))
        End If

        If effect = Spell.CardEffect.Add50BattlePower Then
            If PlayerTurn = 1 Then
                Dim int As Integer
                int = (Player1Board.ReturnSpeciesBuffNumber(CharacterCard.CardSpecies.Human) * 20)
                Player1BattlePower.Add(Int)
            End If
            If PlayerTurn = 2 Then
                Dim int As Integer
                int = (Player2Board.ReturnSpeciesBuffNumber(CharacterCard.CardSpecies.Human) * 20)
                Player2BattlePower.Add(Int)
            End If
        End If

        If effect = Spell.CardEffect.ReviveCard Then
            Dim tempobj As Object
            If PlayerTurn = 1 Then
                tempobj = Player1DiscardPile.ReviveTopCard
                If tempobj IsNot Nothing Then
                    Player1Board.add(tempobj)
                    Player1BattlePower.add(tempobj.GetAttack_Value)
                End If
            End If
            If PlayerTurn = 2 Then
                tempobj = Player2DiscardPile.ReviveTopCard
                If tempobj IsNot Nothing Then
                    Player2Board.add(tempobj)
                    Player2BattlePower.add(tempobj.GetAttack_Value)
                End If
            End If
        End If

        If effect = Spell.CardEffect.NullifyBuff Then
            If PlayerTurn = 1 Then
                Player2Buff.NullBuff
                Player2Board.BuffPlayed(Nothing)
            End If
            If PlayerTurn = 2 Then
                Player1Buff.NullBuff
                Player1Board.BuffPlayed(Nothing)
            End If
        End If

        If effect = Spell.CardEffect.DestroyRandomMinionIfEnnrith Then
            Dim tempobj As Object
            If PlayerTurn = 1 Then
                If CheckCardPlayed(1, 16) Then
                    DestroyRandomMinion(2)
                    tempobj = Player2DiscardPile.ReviveTopCard
                    If tempobj IsNot Nothing Then
                        Player1Board.Add(tempobj)
                        Player1BattlePower.Add(tempobj.GetAttack_Value)
                    End If
                End If
            ElseIf PlayerTurn = 2 Then
                If CheckCardPlayed(2, 16) Then
                    DestroyRandomMinion(1)
                    tempobj = Player1DiscardPile.ReviveTopCard
                    If tempobj IsNot Nothing Then
                        Player2Board.Add(tempobj)
                        Player2BattlePower.Add(tempobj.GetAttack_Value)
                    End If
                End If
            End If
        End If

        If effect = Spell.CardEffect.LizardRiderNightRiderint Then
            If PlayerTurn = 1 Then
                If CheckCardPlayed(1, 28) And CheckCardPlayed(1, 31) Then Player1BattlePower.Add(130)
            ElseIf PlayerTurn = 2 Then
                If CheckCardPlayed(2, 28) And CheckCardPlayed(2, 31) Then Player2BattlePower.Add(130)
            End If
        End If

        If effect = Spell.CardEffect.Add30IfEnnrith Then
            If PlayerTurn = 1 Then
                If CheckCardPlayed(1, 16) Then
                    Player1Board.SwapCard(PictureCards.GetCard(16), PictureCards.GetCard(67))
                    Player1BattlePower.Add(PictureCards.GetCard(67).getAttack_Value)
                    Player1BattlePower.Add((-1) * PictureCards.GetCard(16).GetAttack_Value)
                    RefreshBuffEffect()
                End If
            ElseIf PlayerTurn = 2 Then
                If CheckCardPlayed(2, 16) Then
                    Player2Board.SwapCard(PictureCards.GetCard(16), PictureCards.GetCard(67))
                    Player2BattlePower.Add(PictureCards.GetCard(67).getAttack_Value)
                    Player2BattlePower.Add((-1) * PictureCards.GetCard(16).GetAttack_Value)
                    RefreshBuffEffect()
                End If
            End If
        End If

        If effect = Spell.CardEffect.Add60IfNergal Then
            If PlayerTurn = 1 Then
                If CheckCardPlayed(1, 30) Then
                    For i = 1 To 2
                        Player1Board.Add(PictureCards.GetCard(66))
                        Player1BattlePower.Add(PictureCards.GetCard(66).getAttack_Value)
                        RefreshBuffEffect()
                    Next
                End If
            ElseIf PlayerTurn = 2 Then
                If CheckCardPlayed(2, 30) Then
                    For i = 1 To 2
                        Player2Board.Add(PictureCards.GetCard(66))
                        Player2BattlePower.Add(PictureCards.GetCard(66).getAttack_Value)
                        RefreshBuffEffect()
                    Next
                End If
            End If
        End If

        If effect = Spell.CardEffect.ClearIfNergal Then
            If PlayerTurn = 1 Then
                If CheckCardPlayed(1, 30) Then ClearPlayerBoards(True)
            ElseIf PlayerTurn = 2 Then
                If CheckCardPlayed(2, 30) Then ClearPlayerBoards(True)
            End If
        End If
    End Sub
    Public Function CheckCardPlayed(ByVal player As Integer, ByVal CardID As Integer)
        If player = 1 Then Return Player1Board.CheckCardPlayed(CardID)
        If player = 2 Then Return Player2Board.CheckCardPlayed(CardID)
    End Function
    Public Sub ClearPlayerBoards(ByVal nergal As Boolean)
        Dim tempobj As Object
        For i = Player1Board.cards.count To 1 Step -1
            If Player1Board.Cards.Item(i).IsSpecial = False Or nergal = False Then
                tempobj = (Player1Board.RemoveCardByIndexReference(i))
                Player1BattlePower.add((-1) * (tempobj.getAttack_Value))
                Player1DiscardPile.add(tempobj)
            End If
        Next
        For i = Player2Board.cards.count To 1 Step -1
            If Player2Board.Cards.Item(i).IsSpecial = False Or nergal = False Then
                tempobj = (Player2Board.RemoveCardByIndexReference(i))
                Player2BattlePower.add((-1) * (tempobj.getAttack_Value))
                Player2DiscardPile.add(tempobj)
            End If
        Next
    End Sub
    Public Sub DestroyRandomMinion(ByVal Player As Integer)
        Dim tempobj As Object
        Dim indexArray() As Integer

        If Player = 1 And Player1Board.IsNotEmpty Then
            indexArray = CreateDiscreteArray(Player1Board.GetCardCollection())
            tempobj = (Player1Board.RemoveCardByIndexReference(MathFunc.DiscreteRandomNumber(indexArray)))
            If tempobj IsNot Nothing Then
                Player1BattlePower.add((-1) * (tempobj.getAttack_Value))
                Player1DiscardPile.add(tempobj)
            End If
        End If
        If Player = 2 And Player2Board.IsNotEmpty Then
            indexArray = CreateDiscreteArray(Player2Board.GetCardCollection())
            tempobj = (Player2Board.RemoveCardByIndexReference(MathFunc.DiscreteRandomNumber(indexArray)))
            If tempobj IsNot Nothing Then
                Player2BattlePower.add((-1) * (tempobj.getAttack_Value))
                Player2DiscardPile.add(tempobj)
            End If
        End If
    End Sub
    Private Function CreateDiscreteArray(CardArray As Collection) As Integer()
        Dim Array(0) As Integer
        Array(0) = 0
        Dim x As Integer = 0
        For i = 1 To CardArray.Count
            If CardArray.Item(i).isSpecial = False Then
                ReDim Preserve Array(x)
                Array(x) = i
                x = x + 1
            End If
        Next
        Return Array
    End Function
    Public Sub DestroyRandomMinion()
        Dim tempobj As Object
        Dim player As Integer = MathFunc.randomnumber(2, 1)
        Dim indexArray() As Integer
        If player = 1 Then
            If Player1Board.IsNotEmpty Then
                indexArray = CreateDiscreteArray(Player1Board.GetCardCollection())
                tempobj = (Player1Board.RemoveCardByIndexReference(MathFunc.DiscreteRandomNumber(indexArray)))
                If tempobj IsNot Nothing Then
                    Player1BattlePower.Add((-1) * (tempobj.getAttack_Value))
                    Player1DiscardPile.Add(tempobj)
                End If
            ElseIf Player2Board.IsNotEmpty Then
                indexArray = CreateDiscreteArray(Player2Board.GetCardCollection())
                tempobj = (Player2Board.RemoveCardByIndexReference(MathFunc.DiscreteRandomNumber(indexArray)))
                If tempobj IsNot Nothing Then
                    Player2BattlePower.Add((-1) * (tempobj.getAttack_Value))
                    Player2DiscardPile.Add(tempobj)
                End If
            End If
        End If

        If player = 2 Then
            If Player2Board.IsNotEmpty Then
                indexArray = CreateDiscreteArray(Player2Board.GetCardCollection())
                tempobj = (Player2Board.RemoveCardByIndexReference(MathFunc.DiscreteRandomNumber(indexArray)))
                If tempobj IsNot Nothing Then
                    Player2BattlePower.Add((-1) * (tempobj.getAttack_Value))
                    Player2DiscardPile.Add(tempobj)
                End If
            ElseIf Player1Board.IsNotEmpty Then
                indexArray = CreateDiscreteArray(Player1Board.GetCardCollection())
                tempobj = (Player1Board.RemoveCardByIndexReference(MathFunc.DiscreteRandomNumber(indexArray)))
                If tempobj IsNot Nothing Then
                    Player1BattlePower.Add((-1) * (tempobj.getAttack_Value))
                    Player1DiscardPile.Add(tempobj)
                End If
            End If
        End If
    End Sub

    Public Sub PictureBoxClick(sender As Object, e As EventArgs)
        Dim tempObj As Object
        If PlayableState = True Then
            If sender IsNot Nothing Then
                If PlayerTurn = 1 And Player1Hand.CardExists(sender) Then
                    tempObj = Player1Hand.GetCard(sender, e)
                    If TypeOf tempObj Is CharacterCard Then
                        If Player1Board.IsNotFull = True Then
                            My.Computer.Audio.Play(My.Resources.Play_Card, AudioPlayMode.Background)
                            Player1Board.Add(Player1Hand.PlayCard(sender, e))
                            Player1BattlePower.Add(tempObj.getAttack_Value)
                            RefreshBuffEffect()
                        End If
                    Else
                        My.Computer.Audio.Play(My.Resources.spell, AudioPlayMode.Background)
                        PlaySpell(Player1Hand.PlayCard(sender, e))
                        RefreshBuffEffect()
                    End If
                End If
                If PlayerTurn = 2 And Player2Hand.CardExists(sender) Then
                    tempObj = Player2Hand.GetCard(sender, e)
                    If TypeOf tempObj Is CharacterCard Then
                        If Player2Board.IsNotFull = True Then
                            My.Computer.Audio.Play(My.Resources.Play_Card, AudioPlayMode.Background)
                            Player2Board.Add(Player2Hand.PlayCard(sender, e))
                            Player2BattlePower.Add(tempObj.getAttack_Value)
                            RefreshBuffEffect()
                        End If
                    Else
                        My.Computer.Audio.Play(My.Resources.spell, AudioPlayMode.Background)
                        PlaySpell(Player2Hand.PlayCard(sender, e))
                        RefreshBuffEffect()
                    End If
                End If
                If PlayerTurn = 1 And Player1Hand.Cards.Count = 0 And Player1Passed = False Then
                    PlayerNoCardsLeft(1)
                End If
                If PlayerTurn = 2 And Player2Hand.Cards.Count = 0 And Player2Passed = False Then
                    PlayerNoCardsLeft(2)
                End If
                NewTurn()
            End If
        End If
            PictureBoxStopHover(sender, e)
    End Sub
    Public Sub PictureBoxHover(sender As Object, e As EventArgs)
        If sender.image IsNot Nothing Then
            tempPic = sender.image
            CardHoverTimer.Start()
        End If
    End Sub
    Public Sub PictureBoxStopHover(sender As Object, e As EventArgs)
        CardHoverTimer.Stop()
        My.Forms.CardDisplay.Timer1.Start()
    End Sub
    Public Sub DrawCard(ByVal Player As Integer)
        If Player = 1 Then
            If Player1Hand.cards.count < 10 Then
                Player1Hand.add(Player1Deck.dealcard)
            Else
                Player1DiscardPile.add(Player1Deck.DealCard)
            End If
        End If
        If Player = 2 Then
            If Player2Hand.cards.count < 10 Then
                Player2Hand.add(Player2Deck.dealcard)
            Else
                Player2DiscardPile.add(Player1Deck.dealcard)
            End If
        End If
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles CardHoverTimer.Tick
        Dim point As Point = Cursor.Position
        point.X = point.X + 5
        point.Y = point.Y - 5 - My.Forms.CardDisplay.Height
        My.Forms.CardDisplay.Location = point
        DisplayLargeCard(tempPic, point)
        CardHoverTimer.Stop()
    End Sub
    Private Sub EndTurn_Click(sender As Object, e As EventArgs) Handles EndTurn.Click
        If PlayerTurn <> AIPlayer.GetPlayer Then
            PlayerPassed(PlayerTurn)
            NewTurn()
        End If
    End Sub
    Private Class DeckBuilder
        Dim ChampionName As Champion.Name
        Dim GameCards = New CardList()

        Public Function BuildDeck(ByVal ChampionIN As Champion.Name, ByVal player As Integer)
            Dim picturebox As Object
            Dim label As Object
            If player = 1 Then
                picturebox = Game.PictureBox48
                label = Game.Label4
            Else
                picturebox = Game.PictureBox54
                label = Game.Label3
            End If
            ChampionName = ChampionIN
            Dim Deck As New DeckPile(30, label, picturebox)
            If ChampionName = Champion.Name.Lord Then
                Deck.Add(GameCards.GetCard(27))
                Deck.Add(GameCards.getcard(27))
                Deck.Add(GameCards.getcard(5))
                Deck.Add(GameCards.getcard(5))
                Deck.Add(GameCards.GetCard(7))
                Deck.Add(GameCards.getcard(7))
                Deck.Add(GameCards.getcard(25))
                Deck.Add(GameCards.getcard(25))
                Deck.Add(GameCards.GetCard(26))
                Deck.Add(GameCards.getcard(32))
                Deck.Add(GameCards.getcard(32))
                Deck.Add(GameCards.getcard(39))
                Deck.Add(GameCards.GetCard(40))
                Deck.Add(GameCards.getcard(40))
                Deck.Add(GameCards.getcard(42))
                Deck.Add(GameCards.getcard(45))
                Deck.Add(GameCards.GetCard(42))
                Deck.Add(GameCards.getcard(45))
                Deck.Add(GameCards.getcard(62))
                Deck.Add(GameCards.getcard(48))
                Deck.Add(GameCards.GetCard(48))
                Deck.Add(GameCards.getcard(51))
                Deck.Add(GameCards.getcard(55))
                Deck.Add(GameCards.getcard(55))
                Deck.Add(GameCards.getcard(58))
                Deck.Add(GameCards.getcard(58))
                Deck.Add(GameCards.GetCard(60))
                Deck.Add(GameCards.getcard(60))
                Deck.Add(GameCards.getcard(59))
                Deck.Add(GameCards.getcard(16))
            End If
            If ChampionName = Champion.Name.Sage Then
                Deck.Add(GameCards.GetCard(8))
                Deck.Add(GameCards.getcard(8))
                Deck.Add(GameCards.getcard(10))
                Deck.Add(GameCards.getcard(11))
                Deck.Add(GameCards.GetCard(17))
                Deck.Add(GameCards.getcard(17))
                Deck.Add(GameCards.getcard(18))
                Deck.Add(GameCards.getcard(18))
                Deck.Add(GameCards.GetCard(21))
                Deck.Add(GameCards.getcard(21))
                Deck.Add(GameCards.getcard(22))
                Deck.Add(GameCards.getcard(22))
                Deck.Add(GameCards.GetCard(34))
                Deck.Add(GameCards.getcard(38))
                Deck.Add(GameCards.getcard(38))
                Deck.Add(GameCards.getcard(48))
                Deck.Add(GameCards.GetCard(41))
                Deck.Add(GameCards.getcard(41))
                Deck.Add(GameCards.getcard(48))
                Deck.Add(GameCards.getcard(47))
                Deck.Add(GameCards.GetCard(47))
                Deck.Add(GameCards.getcard(40))
                Deck.Add(GameCards.getcard(40))
                Deck.Add(GameCards.getcard(55))
                Deck.Add(GameCards.getcard(55))
                Deck.Add(GameCards.getcard(56))
                Deck.Add(GameCards.GetCard(56))
                Deck.Add(GameCards.getcard(58))
                Deck.Add(GameCards.getcard(58))
                Deck.Add(GameCards.getcard(53))
            End If
            If ChampionName = Champion.Name.Knight Then
                Deck.Add(GameCards.GetCard(3))
                Deck.Add(GameCards.getcard(4))
                Deck.Add(GameCards.getcard(4))
                Deck.Add(GameCards.getcard(6))
                Deck.Add(GameCards.GetCard(6))
                Deck.Add(GameCards.getcard(14))
                Deck.Add(GameCards.getcard(14))
                Deck.Add(GameCards.getcard(23))
                Deck.Add(GameCards.GetCard(24))
                Deck.Add(GameCards.getcard(24))
                Deck.Add(GameCards.getcard(35))
                Deck.Add(GameCards.getcard(27))
                Deck.Add(GameCards.GetCard(27))
                Deck.Add(GameCards.getcard(46))
                Deck.Add(GameCards.getcard(46))
                Deck.Add(GameCards.getcard(48))
                Deck.Add(GameCards.GetCard(48))
                Deck.Add(GameCards.getcard(53))
                Deck.Add(GameCards.getcard(47))
                Deck.Add(GameCards.getcard(47))
                Deck.Add(GameCards.GetCard(57))
                Deck.Add(GameCards.getcard(57))
                Deck.Add(GameCards.getcard(51))
                Deck.Add(GameCards.getcard(59))
                Deck.Add(GameCards.getcard(19))
                Deck.Add(GameCards.getcard(19))
                Deck.Add(GameCards.GetCard(58))
                Deck.Add(GameCards.getcard(58))
                Deck.Add(GameCards.getcard(55))
                Deck.Add(GameCards.getcard(55))
            End If
            If ChampionName = Champion.Name.Witch Then
                Deck.Add(GameCards.GetCard(2))
                Deck.Add(GameCards.getcard(8))
                Deck.Add(GameCards.getcard(8))
                Deck.Add(GameCards.getcard(9))
                Deck.Add(GameCards.GetCard(15))
                Deck.Add(GameCards.getcard(24))
                Deck.Add(GameCards.getcard(24))
                Deck.Add(GameCards.getcard(27))
                Deck.Add(GameCards.GetCard(27))
                Deck.Add(GameCards.getcard(30))
                Deck.Add(GameCards.getcard(32))
                Deck.Add(GameCards.getcard(32))
                Deck.Add(GameCards.GetCard(38))
                Deck.Add(GameCards.getcard(38))
                Deck.Add(GameCards.getcard(44))
                Deck.Add(GameCards.getcard(44))
                Deck.Add(GameCards.GetCard(47))
                Deck.Add(GameCards.getcard(47))
                Deck.Add(GameCards.getcard(49))
                Deck.Add(GameCards.getcard(50))
                Deck.Add(GameCards.GetCard(51))
                Deck.Add(GameCards.getcard(53))
                Deck.Add(GameCards.getcard(53))
                Deck.Add(GameCards.getcard(55))
                Deck.Add(GameCards.getcard(55))
                Deck.Add(GameCards.getcard(58))
                Deck.Add(GameCards.GetCard(58))
                Deck.Add(GameCards.getcard(59))
                Deck.Add(GameCards.getcard(50))
                Deck.Add(GameCards.getcard(63))
            End If
            Return Deck
        End Function

    End Class
    Public Class CardList ' list of all card instances existent and their values 
        Private Cards As New List(Of Object)
        Public Sub New() ' add and dim all the new instaces of each card manually
            Cards.Add(New CharacterCard(My.Resources.Aether_Dragon, False, 1, CharacterCard.CardTypes.Dark, CharacterCard.CardSpecies.Dragon, 50))
            Cards.Add(New CharacterCard(My.Resources.Akrambuela, True, 2, CharacterCard.CardTypes.Law, CharacterCard.CardSpecies.Spirit, 250))
            Cards.Add(New CharacterCard(My.Resources.Alleyn, True, 3, CharacterCard.CardTypes.Balance, CharacterCard.CardSpecies.Human, 200))
            Cards.Add(New CharacterCard(My.Resources.Aris, False, 4, CharacterCard.CardTypes.Law, CharacterCard.CardSpecies.Human, 100))
            Cards.Add(New CharacterCard(My.Resources.Autumn_Dragon, False, 5, CharacterCard.CardTypes.Balance, CharacterCard.CardSpecies.Dragon, 90))
            Cards.Add(New CharacterCard(My.Resources.blood_knight, False, 6, CharacterCard.CardTypes.Balance, CharacterCard.CardSpecies.Human, 120))
            Cards.Add(New CharacterCard(My.Resources.Dark_Dragon, False, 7, CharacterCard.CardTypes.Dark, CharacterCard.CardSpecies.Dragon, 90))
            Cards.Add(New CharacterCard(My.Resources.dark_warden, False, 8, CharacterCard.CardTypes.Dark, CharacterCard.CardSpecies.Fairy, 100))
            Cards.Add(New CharacterCard(My.Resources.Darklurker, True, 9, CharacterCard.CardTypes.Dark, CharacterCard.CardSpecies.Spirit, 150))
            Cards.Add(New CharacterCard(My.Resources.dead_king_s_ivy, True, 10, CharacterCard.CardTypes.Nature, CharacterCard.CardSpecies.Fairy, 180))
            Cards.Add(New CharacterCard(My.Resources.dr__deadlift, True, 11, CharacterCard.CardTypes.Nature, CharacterCard.CardSpecies.Fairy, 200))
            Cards.Add(New CharacterCard(My.Resources.Dragon_Commander, False, 12, CharacterCard.CardTypes.Dark, CharacterCard.CardSpecies.Human, 80))
            Cards.Add(New CharacterCard(My.Resources.Dragon_Paladin, False, 13, CharacterCard.CardTypes.Balance, CharacterCard.CardSpecies.Human, 70))
            Cards.Add(New CharacterCard(My.Resources.Dragon_Warlord, False, 14, CharacterCard.CardTypes.Law, CharacterCard.CardSpecies.Human, 120))
            Cards.Add(New CharacterCard(My.Resources.Drakthar, True, 15, CharacterCard.CardTypes.Law, CharacterCard.CardSpecies.Spirit, 250))
            Cards.Add(New CharacterCard(My.Resources.Ennrith, True, 16, CharacterCard.CardTypes.Law, CharacterCard.CardSpecies.Dragon, 100))
            Cards.Add(New CharacterCard(My.Resources.ent_archer, False, 17, CharacterCard.CardTypes.Nature, CharacterCard.CardSpecies.Fairy, 90))
            Cards.Add(New CharacterCard(My.Resources.ent_shaman, False, 18, CharacterCard.CardTypes.Nature, CharacterCard.CardSpecies.Fairy, 100))
            Cards.Add(New CharacterCard(My.Resources.executioner, False, 19, CharacterCard.CardTypes.Dark, CharacterCard.CardSpecies.Human, 100))
            Cards.Add(New CharacterCard(My.Resources.Fairy_Dragon, False, 20, CharacterCard.CardTypes.Nature, CharacterCard.CardSpecies.Fairy, 70))
            Cards.Add(New CharacterCard(My.Resources.fox_archer, False, 21, CharacterCard.CardTypes.Nature, CharacterCard.CardSpecies.Fairy, 100))
            Cards.Add(New CharacterCard(My.Resources.great_ent, False, 22, CharacterCard.CardTypes.Nature, CharacterCard.CardSpecies.Fairy, 130))
            Cards.Add(New CharacterCard(My.Resources.Guatzelin, True, 23, CharacterCard.CardTypes.Balance, CharacterCard.CardSpecies.Human, 150))
            Cards.Add(New CharacterCard(My.Resources.Hallowed_Warlord, False, 24, CharacterCard.CardTypes.Dark, CharacterCard.CardSpecies.Human, 150))
            Cards.Add(New CharacterCard(My.Resources.Hell_Drake, False, 25, CharacterCard.CardTypes.Law, CharacterCard.CardSpecies.Dragon, 110))
            Cards.Add(New CharacterCard(My.Resources.Jadirrayis, True, 26, CharacterCard.CardTypes.Balance, CharacterCard.CardSpecies.Dragon, 170))
            Cards.Add(New CharacterCard(My.Resources.last_sun_soldier, False, 27, CharacterCard.CardTypes.Law, CharacterCard.CardSpecies.Spirit, 150))
            Cards.Add(New CharacterCard(My.Resources.lizard_rider, False, 28, CharacterCard.CardTypes.Balance, CharacterCard.CardSpecies.Human, 100))
            Cards.Add(New CharacterCard(My.Resources.mouse_warrior, False, 29, CharacterCard.CardTypes.Nature, CharacterCard.CardSpecies.Fairy, 70))
            Cards.Add(New CharacterCard(My.Resources.nergal, True, 30, CharacterCard.CardTypes.Nature, CharacterCard.CardSpecies.Spirit, 30))
            Cards.Add(New CharacterCard(My.Resources.night_rider, False, 31, CharacterCard.CardTypes.Balance, CharacterCard.CardSpecies.Human, 100))
            Cards.Add(New CharacterCard(My.Resources.Shadow_Drake, False, 32, CharacterCard.CardTypes.Dark, CharacterCard.CardSpecies.Dragon, 110))
            Cards.Add(New CharacterCard(My.Resources.spirit_warrior, False, 33, CharacterCard.CardTypes.Dark, CharacterCard.CardSpecies.Spirit, 80))
            Cards.Add(New CharacterCard(My.Resources.the_faceless_druid, True, 34, CharacterCard.CardTypes.Dark, CharacterCard.CardSpecies.Fairy, 170))
            Cards.Add(New CharacterCard(My.Resources.Thorkel_Asvard, True, 35, CharacterCard.CardTypes.Law, CharacterCard.CardSpecies.Human, 180))
            Cards.Add(New CharacterCard(My.Resources.tormented_dregs, False, 36, CharacterCard.CardTypes.Dark, CharacterCard.CardSpecies.Spirit, 80))
            Cards.Add(New CharacterCard(My.Resources.Twin_Knights, False, 37, CharacterCard.CardTypes.Balance, CharacterCard.CardSpecies.Human, 80))
            Cards.Add(New CharacterCard(My.Resources.watcher, False, 38, CharacterCard.CardTypes.Dark, CharacterCard.CardSpecies.Fairy, 120))
            Cards.Add(New CharacterCard(My.Resources.Zyrgym, True, 39, CharacterCard.CardTypes.Balance, CharacterCard.CardSpecies.Dragon, 150))
            Cards.Add(New Spell(My.Resources.Curse, False, 40, Spell.CardEffect.DestroyRandomMinion, Spell.SpellType.Instant))
            Cards.Add(New Spell(My.Resources.deeproot, False, 41, Spell.CardEffect.Nature70Buff, Spell.SpellType.Buff))
            Cards.Add(New Spell(My.Resources.Dragon_Dominion, False, 42, Spell.CardEffect.Dragon10Aura, Spell.SpellType.Aura))
            Cards.Add(New Spell(My.Resources.embody, False, 43, Spell.CardEffect.LizardRiderNightRiderint, Spell.SpellType.Instant))
            Cards.Add(New Spell(My.Resources.endarken, False, 44, Spell.CardEffect.Dark50Buff, Spell.SpellType.Buff))
            Cards.Add(New Spell(My.Resources.enforcement, False, 45, Spell.CardEffect.Dragon50Buff, Spell.SpellType.Buff))
            Cards.Add(New Spell(My.Resources.equilibrium, False, 46, Spell.CardEffect.Law15Aura, Spell.SpellType.Aura))
            Cards.Add(New Spell(My.Resources.etheral_sacrifice, False, 47, Spell.CardEffect.Destroy2MinionFor1, Spell.SpellType.Instant))
            Cards.Add(New Spell(My.Resources.kings_blessing, False, 48, Spell.CardEffect.IncreaseBattlePower10, Spell.SpellType.Instant))
            Cards.Add(New Spell(My.Resources.last_stand, False, 49, Spell.CardEffect.Destory1To5Minions, Spell.SpellType.Instant))
            Cards.Add(New Spell(My.Resources.nergals_secret, False, 50, Spell.CardEffect.Add60IfNergal, Spell.SpellType.Instant))
            Cards.Add(New Spell(My.Resources.nullify, False, 51, Spell.CardEffect.NullifyBuff, Spell.SpellType.Instant))
            Cards.Add(New Spell(My.Resources.nullify_aura, False, 52, Spell.CardEffect.NullifyAura, Spell.SpellType.Aura))
            Cards.Add(New Spell(My.Resources.overgrowth, False, 53, Spell.CardEffect.DecreaseOpponentPower5, Spell.SpellType.Instant))
            Cards.Add(New Spell(My.Resources.pilgrimage, False, 54, Spell.CardEffect.DrawCard, Spell.SpellType.Instant))
            Cards.Add(New Spell(My.Resources.recycle, False, 55, Spell.CardEffect.Draw2Card, Spell.SpellType.Instant))
            Cards.Add(New Spell(My.Resources.Red_Tree_Blossom, False, 56, Spell.CardEffect.Fairy80Buff, Spell.SpellType.Buff))
            Cards.Add(New Spell(My.Resources.reinforce, False, 57, Spell.CardEffect.Add50BattlePower, Spell.SpellType.Instant))
            Cards.Add(New Spell(My.Resources.rejuvinate, False, 58, Spell.CardEffect.Draw3Card, Spell.SpellType.Instant))
            Cards.Add(New Spell(My.Resources.replenish, True, 59, Spell.CardEffect.Draw5Card, Spell.SpellType.Instant))
            Cards.Add(New Spell(My.Resources.revive, False, 60, Spell.CardEffect.ReviveCard, Spell.SpellType.Instant))
            Cards.Add(New Spell(My.Resources.tainted_growth, False, 61, Spell.CardEffect.Spirit10Aura, Spell.SpellType.Aura))
            Cards.Add(New Spell(My.Resources.Transform, False, 62, Spell.CardEffect.Add30IfEnnrith, Spell.SpellType.Instant))
            Cards.Add(New Spell(My.Resources.unleash_nergal, False, 63, Spell.CardEffect.ClearIfNergal, Spell.SpellType.Instant))
            Cards.Add(New Spell(My.Resources.unleash_wyvern, False, 64, Spell.CardEffect.DestroyRandomMinionIfEnnrith, Spell.SpellType.Instant))
            Cards.Add(New CharacterCard(My.Resources.desmos, True, 65, CharacterCard.CardTypes.Balance, CharacterCard.CardSpecies.Spirit, 180))
            Cards.Add(New CharacterCard(My.Resources.Skeleton_Mage, False, 66, CharacterCard.CardTypes.Law, CharacterCard.CardSpecies.Spirit, 70))
            Cards.Add(New CharacterCard(My.Resources.Ennrith_Unleashed, False, 67, CharacterCard.CardTypes.Law, CharacterCard.CardSpecies.Dragon, 280))
        End Sub
        Public Function GetCard(ByVal index As Integer)
            Return Cards.Item(index - 1)
        End Function

    End Class
    Public Class DeckPile
        Public Item As New Collection ' array of object that store data for the cards
        Private Length As Integer
        Private LabelReference As Label
        Private PictureBoxReference As Object
        Public Sub New(ByVal Size As Integer, ByRef label As Object, ByRef picturebox As Object)
            Length = Size
            Me.LabelReference = label
            Me.PictureBoxReference = picturebox
            My.Forms.Game.DynamicScale(picturebox)
            label.text = "0"
        End Sub
        Public Sub New(ByVal Collection As Collection, ByRef label As Object, ByRef picturebox As Object)
            Length = Collection.Count
            Item = Collection
            Me.LabelReference = label
            Me.PictureBoxReference = picturebox
            My.Forms.Game.DynamicScale(picturebox)
            label.text = "0"
        End Sub
        Public Sub ShuffleCardBackIn(ByRef card As Object)
            Add(card)
            shuffle(20)
        End Sub
        Public Sub Add(ByRef Card As Object)
            Item.Add(Card)
        End Sub
        Public Sub Remove(ByVal Value As Integer) ' procedure nulls object at specific location
            Item.Remove(Value)
        End Sub
        Public Sub shuffle(ByVal times As Integer) ' shuffles items in the deck to random places
            Dim temp As Object
            Dim rand As Integer
            For x = 1 To times
                Randomize()
                rand = CInt(Math.Ceiling(Rnd() * (Item.Count - 1)))
                temp = Item.Item(rand)
                Me.Remove(rand)
                Me.Add(temp)
            Next
        End Sub
        Public Function DealCard()
            If Item.Count = 0 Then
                Return Nothing
            End If
            Dim temp As Object = Item.Item(1)
            Item.Remove(1)
            If Item.Count = 0 Then
                PictureBoxReference.Image = Nothing
            End If
            LabelReference.Text = Convert.ToString(Item.Count)
            Return temp
        End Function
    End Class
    Public Class Card
        Protected Image As Bitmap
        Protected special As Boolean
        Protected CardID As Integer
        Public Overridable Function ReturnImage() As Bitmap
            Return Image
        End Function
        Public Overridable Function GetCardID() As Integer
            Return CardID
        End Function
        Public Overridable Function IsSpecial() As Boolean
            Return special
        End Function
    End Class
    Public Class CharacterCard
        Inherits Card
        Private Type As String
        Private Species As String
        Private Attack_Value As Integer = 0
        Public Enum CardTypes
            Balance
            Dark
            Nature
            Law
        End Enum
        Public Enum CardSpecies
            Dragon
            Human
            Spirit
            Fairy
        End Enum
        Public Function GetCardType() As CardTypes
            Return Type
        End Function
        Public Function GetSpecies() As CardSpecies
            Return Species
        End Function
        Public Function GetAttack_Value() As Integer
            Return Attack_Value
        End Function
        Public Sub ChangeAttack_Value(ByVal value As Integer)
            Attack_Value += value
        End Sub
        Public Sub New(ByRef Image As Bitmap, ByVal InitalSpecial As Boolean, ByVal InitialCardID As Integer, ByVal InitialType As CardTypes, ByVal InitialSpecies As CardSpecies, ByVal InitalAttack_Value As Integer)
            Me.Image = Image
            special = InitalSpecial
            CardID = InitialCardID
            Type = InitialType
            Species = InitialSpecies
            Attack_Value = InitalAttack_Value
        End Sub
    End Class
    Public Class Spell
        Inherits Card
        Private Effect As CardEffect = Nothing
        Private Type As SpellType = Nothing
        Public Enum SpellType
            Aura
            Buff
            Instant
        End Enum
        Public Enum CardEffect
            Draw2Card
            Draw3Card
            DrawCard
            Draw5Card
            Destroy2MinionFor1
            DestroyRandomMinion
            IncreaseBattlePower10%
            Destory1To5Minions
            DecreaseOpponentPower5
            Add50BattlePower
            ReviveCard
            NullifyBuff
            LizardRiderNightRiderint
            Add60IfNergal
            Add30IfEnnrith
            ClearIfNergal
            DestroyRandomMinionIfEnnrith
            Nature70Buff
            Dragon10Aura
            Dark50Buff
            Dragon50Buff
            Law15Aura
            NullifyAura
            Fairy80Buff
            Spirit10Aura
        End Enum
        Public Function GetSpellEffect() As CardEffect
            Return Effect
        End Function
        Public Function GetSPellType() As SpellType
            Return Type
        End Function
        Public Sub New(ByVal Image As Bitmap, ByVal InitalSpecial As Boolean, ByVal InitialCardID As Integer, ByVal InitalEffect As CardEffect, ByVal InitialType As SpellType)
            Me.Image = Image
            special = InitalSpecial
            CardID = InitialCardID
            Type = InitialType
            Effect = InitalEffect
        End Sub
    End Class

    Public MustInherit Class CardGroup
        Public CardHolders As New Collection ' make new procedure to remove an item from card holder
        Public Cards As New Collection
        Public Function GetCardIndexByBitmapValue(ByVal img As Bitmap)
            For i = 1 To Cards.Count
                If Cards.Item(i).ReturnImage Is img Then
                    Return i
                End If
            Next
            Return 0

        End Function
        Public Function CardExists(ByRef input As Object)
            For i = 1 To CardHolders.Count
                If CardHolders.Item(i).ReturnPictureBoxRef(input) = True Then
                    If CardHolders.Item(i).cardref IsNot Nothing Then
                        Return True
                    Else
                        Return False
                    End If
                End If
            Next
            Return False
        End Function
        Public Sub ShiftCards()
            Dim CheckBoo As Boolean
            For i = 1 To 10
                If CardHolders.Item(i).CardRef IsNot Nothing And CheckBoo = True Then
                    CardHolders.Item(i - 1).AddCardRef(CardHolders.Item(i).CardRef)
                    CardHolders.Item(i).NullCardRef()
                End If
                If CardHolders.Item(i).CardRef Is Nothing Then
                    CheckBoo = True
                Else
                    CheckBoo = False
                End If
            Next
        End Sub
        Public Function GetCardIndexByPictureBoxReference(ByRef input As Object)
            For i = 1 To CardHolders.Count
                If CardHolders.Item(i).ReturnPictureBoxRef(input) = True Then
                    Return i
                End If
            Next
        End Function
        Public Sub Add(ByRef Input As Object)
            If Cards.Count < 10 Then
                Cards.Add(Input)
                CardHolders.Item(Cards.Count).AddCardRef(Input)
            End If
        End Sub
        Public Function RemoveCardByIndexReference(ByVal index As Integer)
            Dim tempobj As Object
            If index = 0 Then
                Return Nothing
            End If
            CardHolders.Item(index).NullCardRef
            tempobj = Cards.Item(index)
            Cards.Remove(index)
            ShiftCards()
            Return tempobj
        End Function
        Public Sub RemoveCardByCardReference(ByRef Input As Object)
            Dim index As Integer
            For i = 1 To CardHolders.Count
                If CardHolders.Item(i).CardRef Is Input Then
                    index = i
                End If
            Next
            If index <> 0 Then
                CardHolders.Item(index).NullCardRef
                Cards.Remove(index)
                ShiftCards()
            End If

        End Sub
        Public Sub RemoveCardByPictureBoxReference(ByRef Input As Object)
            For i = 1 To CardHolders.Count
                If CardHolders.Item(i).ReturnPictureBoxRef(Input) Then
                    CardHolders.Item(i).NullCardRef
                    Cards.Remove(i)
                End If
            Next
            ShiftCards()
        End Sub
        Public Function GetCardCollection()
            Return Cards
        End Function
        Public Function GetPictureBoxRefByIndex(ByVal index As Integer)
            If index <= 0 Or index > 10 Then Return Nothing
            Return CardHolders.Item(index).GetPictureBoxReference
        End Function
    End Class

    Public Class Hand
        Inherits CardGroup
        Public Sub New(ByRef Reference1 As PictureBox, ByRef Reference2 As PictureBox, ByRef Reference3 As PictureBox, ByRef Reference4 As PictureBox, ByRef Reference5 As PictureBox, ByRef Reference6 As PictureBox, ByRef Reference7 As PictureBox, ByRef Reference8 As PictureBox, ByRef Reference9 As PictureBox, ByRef Reference10 As PictureBox, ByVal AIControlled As Boolean)
            CardHolders.Add(New HandHolder(Reference1, AIControlled))
            CardHolders.Add(New HandHolder(Reference2, AIControlled))
            CardHolders.Add(New HandHolder(Reference3, AIControlled))
            CardHolders.Add(New HandHolder(Reference4, AIControlled))
            CardHolders.Add(New HandHolder(Reference5, AIControlled))
            CardHolders.Add(New HandHolder(Reference6, AIControlled))
            CardHolders.Add(New HandHolder(Reference7, AIControlled))
            CardHolders.Add(New HandHolder(Reference8, AIControlled))
            CardHolders.Add(New HandHolder(Reference9, AIControlled))
            CardHolders.Add(New HandHolder(Reference10, AIControlled))
        End Sub

        Public Function GetImage(ByVal Index As Integer) As Bitmap
            Return Cards.Item(Index).ReturnImage
        End Function

        Public Function PlayCard(sender As Object, e As EventArgs)
            Dim int As Integer = GetCardIndexByPictureBoxReference(sender)
            Dim output As Object = Cards.Item(int)
            RemoveCardByPictureBoxReference(sender)
            ShiftCards()
            Return output
        End Function
        Public Sub RemoveCardByIndex(ByVal index As Integer)
            RemoveCardByIndexReference(index)
        End Sub
        Public Function GetCard(sender As Object, e As EventArgs)
            Dim int As Integer = GetCardIndexByPictureBoxReference(sender)
            Return Cards.Item(int)
        End Function
    End Class
    Public Class CardHolder
        Protected PictureBoxRef As PictureBox
        Public CardRef As Object
        Public Function ReturnCardRef(ByRef Reference As Object) As Boolean
            If CardRef Is Reference Then Return True
            Return False
        End Function
        Public Function ReturnPictureBoxRef(ByRef Reference As Object) As Boolean
            If PictureBoxRef Is Reference Then Return True
            Return False
        End Function
        Public Function PeekCardID()
            If CardRef IsNot Nothing Then
                Return CardRef.GetCardID
            End If
            Return 0
        End Function
        Public Sub New(ByRef Reference As PictureBox)
            PictureBoxRef = Reference
            AddHandler PictureBoxRef.MouseHover, AddressOf My.Forms.Game.PictureBoxHover
            AddHandler PictureBoxRef.MouseLeave, AddressOf My.Forms.Game.PictureBoxStopHover
            My.Forms.Game.DynamicScale(Reference)
        End Sub
        Public Overridable Sub NullCardRef()
            CardRef = Nothing
            PictureBoxRef.Image = Nothing
        End Sub
        Public Overridable Sub AddCardRef(ByRef Reference As Object)
            CardRef = Reference
            If CardRef IsNot Nothing Then
                PictureBoxRef.Image = CardRef.ReturnImage
            End If
        End Sub
        Public Sub DisplayImage(ByRef input As Bitmap)
            PictureBoxRef.Image = input
        End Sub
        Public Function GetPictureBoxReference()
            Return PictureBoxRef
        End Function
    End Class
    Public Class BoardHolder
        Inherits CardHolder
        Public Sub New(ByRef reference As PictureBox)
            MyBase.New(reference)
        End Sub
    End Class
    Public Class HandHolder
        Inherits CardHolder
        Private AIControlled
        Public Sub New(ByRef reference As PictureBox, ByVal isAIControlled As Boolean)
            MyBase.New(reference)
            AIControlled = isAIControlled
            If AIControlled = False Then
                AddHandler PictureBoxRef.DoubleClick, AddressOf My.Forms.Game.PictureBoxClick
            Else
                RemoveHandler PictureBoxRef.MouseHover, AddressOf My.Forms.Game.PictureBoxHover
                RemoveHandler PictureBoxRef.MouseLeave, AddressOf My.Forms.Game.PictureBoxStopHover
            End If
        End Sub
        Public Overrides Sub AddCardRef(ByRef Reference As Object)
            CardRef = Reference
            If CardRef IsNot Nothing Then
                If AIControlled Then
                    PictureBoxRef.Image = My.Resources.card_back
                    ' code to hide opponents cards
                    'PictureBoxRef.Image = CardRef.ReturnImage
                Else
                    MyBase.AddCardRef(Reference)
                End If
            End If
        End Sub
    End Class
    Public Class BuffDisplay
        Inherits CardHolder
        Public Sub New(ByRef Reference As PictureBox)
            MyBase.New(Reference)
        End Sub
        Public Function GetBuffEffect() As Spell.CardEffect
            Return CardRef.GetSpellEffect()
        End Function
        Public Sub NullBuff()
            MyBase.AddCardRef(Nothing)
            PictureBoxRef.Image = Nothing
        End Sub
    End Class
    Public Class AuraDisplay
        Inherits CardHolder
        Public Sub New(ByRef Reference As PictureBox)
            MyBase.New(Reference)
        End Sub
        Public Sub RefreshAuraEffect()
            My.Forms.Game.PlayAURA(CardRef)
        End Sub
    End Class
    Public Class DiscardPile
        Public Item As New Stack(Of Object)
        Private DiscardPileObject As Object
        Private Label As Label

        Public Sub New(ByRef Reference As Object, ByRef label As Object)
            DiscardPileObject = New CardHolder(Reference)

            Me.Label = label
            Me.Label.Text = "0"
        End Sub
        Public Sub Add(ByRef Input As Object)
            Item.Push(Input)
            DiscardPileObject.AddCardRef(Input)
            Label.Text = Convert.ToString(Item.Count)
        End Sub
        Public Function ReturnTopPicture() As Bitmap
            Return Item.Peek().Image
        End Function
        Public Function ReviveTopCard() As Object ' returns top card regardless if it is special or not
            Dim skip As New Stack(Of Object)
            Dim output As Object = Nothing
            Dim x As Integer = 0
            For i = 1 To Item.Count
                If Item.Peek().IsSpecial = False And TypeOf Item.Peek() Is CharacterCard Then
                    output = Item.Pop()
                    GoTo Found
                Else
                    skip.Push(Item.Pop)
                    x += 1
                End If
            Next
Found:
            DiscardPileObject.NullCardRef()
            For i = 0 To x - 1
                Add(skip.Pop())
            Next
            If Item.Count > 0 Then
                If Item.Peek() IsNot Nothing Then
                    DiscardPileObject.DisplayImage(Item.Peek().ReturnImage)
                End If
            Else
                DiscardPileObject.DisplayImage(Nothing)
            End If
            If output IsNot Nothing Then
                Label.Text = Convert.ToString(Convert.ToInt16(Label.Text.ToString) - 1)
            End If
            Return output
        End Function
    End Class

    Public Class PlayerBoard
        Inherits CardGroup
        Private BuffReference As Object
        Private AuraReference As Object
        Public Buff As Object
        Public BuffsStack As Boolean
        Public BuffPower As Integer = 0
        Public AURAPower As Integer = 0
        Public Sub New(ByRef Reference1 As Object, ByRef Reference2 As Object, ByRef Reference3 As Object, ByRef Reference4 As Object, ByRef Reference5 As Object, ByRef Reference6 As Object, ByRef Reference7 As Object, ByRef Reference8 As Object, ByRef Reference9 As Object, ByRef Reference10 As Object)
            CardHolders.Add(New BoardHolder(Reference1))
            CardHolders.Add(New BoardHolder(Reference2))
            CardHolders.Add(New BoardHolder(Reference3))
            CardHolders.Add(New BoardHolder(Reference4))
            CardHolders.Add(New BoardHolder(Reference5))
            CardHolders.Add(New BoardHolder(Reference6))
            CardHolders.Add(New BoardHolder(Reference7))
            CardHolders.Add(New BoardHolder(Reference8))
            CardHolders.Add(New BoardHolder(Reference9))
            CardHolders.Add(New BoardHolder(Reference10))
        End Sub
        Public Sub SwapCard(ByRef CardOut As CharacterCard, ByRef CardIn As CharacterCard)
            Dim index As Integer
            For i = 1 To Cards.Count
                If CardOut.GetCardID = (Cards.Item(i)).GetCardID Then index = i
            Next
            Me.CardHolders.Item(index).AddCardRef(CardIn)
        End Sub
        Public Sub BuffPlayed(ByRef Input As Spell)
            Buff = Input
        End Sub
        Public Function IsNotFull()
            For i = 1 To 10
                If CardHolders.Item(i).CardRef() Is Nothing Then
                    Return True
                End If
            Next
            Return False
        End Function
        Public Sub RefreshBuff(ByVal player As Integer)
            My.Forms.Game.PlayBuff(Buff, player)
        End Sub
        Public Sub Clear()
            Cards.Clear()
        End Sub
        Public Function CheckCardPlayed(ByRef CheckcardID As Integer)
            For i = 1 To 10
                If CardHolders.Item(i).PeekCardID = CheckcardID Then
                    Return True
                End If
            Next
            Return False
        End Function
        Public Function ReturnSpeciesBuffNumber(ByVal species As CharacterCard.CardSpecies)
            Dim int As Integer
            For i = 1 To Cards.Count
                If Cards.Item(i).getSpecies = species Then int += 1
            Next
            Return int
        End Function
        Public Function ReturnTypeBuffNumber(ByVal species As CharacterCard.CardTypes)
            Dim int As Integer
            For i = 1 To Cards.Count
                If Cards.Item(i).GetCardType = species Then int += 1
            Next
            Return int
        End Function
        Public Function IsNotEmpty()
            For i = 1 To 10
                If CardHolders.Item(i).CardRef() IsNot Nothing Then
                    Return True
                End If
            Next
            Return False
        End Function
        Public Function IsBuffPlayed()
            If Buff Is Nothing Then Return False Else Return True

        End Function
    End Class
    Public Class Champion
        Private PictureBoxReference As PictureBox
        Private AbilityUsed As Boolean
        Public Enum Name
            Knight
            Sage
            Lord
            Witch
        End Enum
        Public Enum Ability
            Destiny
            Purify
            Onslaught
            Endarken
        End Enum
        Private ChampionInstance As Name
        Public Sub New(ByVal ChosenChampion As Name, ByRef Reference As Object)
            ChampionInstance = ChosenChampion
            PictureBoxReference = Reference

            AbilityUsed = False
            If PictureBoxReference IsNot Nothing Then
                PictureBoxReference.Image = ReturnChampionImage()
                AddHandler PictureBoxReference.DoubleClick, AddressOf My.Forms.Game.ChampionBoxDoubleClick
            End If
        End Sub
        Public Function ReturnAbilityDescription()
            If ChampionInstance = Name.Knight Then Return "Destiny: Summon a Unique Character Card to Your Battlfeild"
            If ChampionInstance = Name.Sage Then Return "Purify: Nullify Your Opponents Buff and any Aura played in the Game "
            If ChampionInstance = Name.Lord Then Return "Onslaught: Play an Enforcement Buff to Your Battlefeild"
            If ChampionInstance = Name.Witch Then Return "Endarken: Destroy 2 Random Enemy Characters"
        End Function
        Public Function ReturnAbility()
            If ChampionInstance = Name.Knight Then Return Ability.Destiny
            If ChampionInstance = Name.Sage Then Return Ability.Purify
            If ChampionInstance = Name.Lord Then Return Ability.Onslaught
            If ChampionInstance = Name.Witch Then Return Ability.Endarken
            Return Nothing
        End Function
        Public Function ReturnStringAbility()
            If ChampionInstance = Name.Knight Then Return Ability.Destiny.ToString
            If ChampionInstance = Name.Sage Then Return Ability.Purify.ToString
            If ChampionInstance = Name.Lord Then Return Ability.Onslaught.ToString
            If ChampionInstance = Name.Witch Then Return Ability.Endarken.ToString
            Return Nothing
        End Function
        Public Function ReturnName()
            Return ChampionInstance
        End Function
        Public Sub Refresh()
            PictureBoxReference.Image = ReturnChampionImage()
        End Sub
        Public Function ReturnStringName() As String
            If ChampionInstance = Name.Knight Then Return "Knight"
            If ChampionInstance = Name.Sage Then Return "Sage"
            If ChampionInstance = Name.Lord Then Return "Lord"
            If ChampionInstance = Name.Witch Then Return "Witch"
        End Function
        Public Function ReturnChampionImage()
            If ChampionInstance = Name.Knight Then Return My.Resources.praetorian_tercius_by_peterprime_d5ijpbl
            If ChampionInstance = Name.Sage Then Return My.Resources.druidimage
            If ChampionInstance = Name.Lord Then Return My.Resources.dragon_lord_image
            If ChampionInstance = Name.Witch Then Return My.Resources.witch_character
        End Function
        Private Function ReturnDesatureChampionImage()
            If ChampionInstance = Name.Knight Then Return My.Resources.desature_knight
            If ChampionInstance = Name.Sage Then Return My.Resources.desature_druid
            If ChampionInstance = Name.Lord Then Return My.Resources.desature_lord
            If ChampionInstance = Name.Witch Then Return My.Resources.desature_witch
        End Function
        Public Function IsUsed()
            Return AbilityUsed
        End Function
        Public Sub UseAbility()
            AbilityUsed = True
            PictureBoxReference.Image = ReturnDesatureChampionImage()
        End Sub
        Public Function GetPictureBoxReference()
            Return PictureBoxReference
        End Function
        Public Sub ReplenishAbility()
            AbilityUsed = False
            Refresh()
        End Sub
    End Class
    Public Class BattlePower
        Private Power As Integer = 0
        Private textBoxRef As Button
        Public Function GetPower()
            Return Power
        End Function
        Public Sub New(ByRef reference As Object)
            textBoxRef = reference
            textBoxRef.text = Power
            My.Forms.Game.DynamicScale(reference)
        End Sub
        Public Sub boldtext(ByVal highlight As Boolean)
            If highlight = True Then
                textBoxRef.Font = New Font(textBoxRef.Font, FontStyle.Bold)
            Else
                textBoxRef.Font = New Font(textBoxRef.Font, FontStyle.Regular)
            End If
        End Sub
        Public Sub Add(ByVal Int As Double)
            Power += Math.Round(Int)
            textBoxRef.Text = Power
        End Sub
        Public Sub ResetPower()
            Power = 0
            textBoxRef.text = Power
        End Sub
    End Class
    Public Class Lifes
        Private Reference1 As PictureBox
        Private Reference2 As PictureBox
        Private lifes As Integer
        Public Sub New(ByRef InRef1 As Object, ByRef InRef2 As Object)
            Reference1 = InRef1
            Reference2 = InRef2
            My.Forms.Game.DynamicScale(InRef1)
            My.Forms.Game.DynamicScale(InRef2)
            lifes = 2
        End Sub
        Public Sub LooseLife()
            lifes = lifes - 1
            If lifes = 1 Then Reference1.Image = Nothing
            If lifes = 0 Then Reference2.image = Nothing
        End Sub
        Public Function GetLifes()
            Return lifes
        End Function
    End Class
    Public Class AI

        '======= dynamic variables=========
        Private BattlePower As Integer
        Private OpponentBattlePower As Integer
        Private Cards As New Collection
        Private PlayedCards As New Collection
        Private MeBuffPlayed As Boolean
        Private OpponentPlayedCards As New Collection
        Private OpponentPassed As Boolean
        Private OpponentCardCount As Integer
        Private opponentbuffplayed As Boolean
        Private OpponentLives As Integer
        Private Lives As Integer
        Private auraplayed As Boolean
        Public AbilityUsed As Boolean
        Private DiscardPileCount
        Private Difference As Integer = BattlePower - OpponentBattlePower

        '=======static variables======
        Private Champion As Champion.Name
        Private OpponentChampion As Champion.Name
        Private Player As Integer
        Private startingPlayer As Integer
        Private Enum PlayStyle
            Agressive
            Passive
            Slow
        End Enum
        '======usefulvariables======
        Private CardIndex As Integer
        Private MyPlayStyle As PlayStyle
        Private OpponentPlayStyle As PlayStyle


        Public Sub New(ByVal Player As Integer, ByVal Champion As Champion.Name, ByVal OpponentChampion As Champion.Name, startingPlayer As Integer)
            ' default is set to player 1
            Me.Player = Player
            Me.Champion = Champion
            AbilityUsed = False
            Me.OpponentChampion = OpponentChampion

            Me.startingPlayer = startingPlayer
            If startingPlayer = Player Then
                MyPlayStyle = PlayStyle.Agressive
            Else
                MyPlayStyle = PlayStyle.Passive
            End If
        End Sub
        Public Function getMoveIndex()
            Dim CardPlayList As New List(Of Game.CharacterCard)
            Dim SpellList As New List(Of Game.Spell)
            Dim CharacterCardPiority As Boolean = True

            ' seperate character cards from spells
            For i = 1 To Cards.Count
                If TypeOf Cards.Item(i) Is Game.CharacterCard Then
                    CardPlayList.Add(Cards.Item(i))
                Else
                    SpellList.Add(Cards.Item(i))
                End If
            Next

            'sort cards based on pay style
            If MyPlayStyle = PlayStyle.Agressive Then
                CharacterCardPiority = True
                SortCharacterCards(CardPlayList, False, True)
                CharacterCardPiority = Not SortSpellCards(SpellList)
            ElseIf MyPlayStyle = PlayStyle.Passive Then
                SortCharacterCards(CardPlayList, True, False)
                CharacterCardPiority = Not SortSpellCards(SpellList)
                CharacterCardPiority = False
            ElseIf MyPlayStyle = PlayStyle.Slow Then
                SortCharacterCards(CardPlayList, True, True)
                CharacterCardPiority = Not SortSpellCards(SpellList)
                If CardPlayList.Count > 0 Then
                    If CardPlayList.Item(0).IsSpecial Then CharacterCardPiority = True
                Else
                    CharacterCardPiority = False
                End If
            End If
            'get the card to be played
            Dim ReturnObj As Object = Nothing
            If CharacterCardPiority = True Then
                If CardPlayList.Count > 0 Then ReturnObj = CardPlayList.Item(0)
            ElseIf CharacterCardPiority = False Then
                If SpellList.Count <> 0 Then
                    ReturnObj = SpellList.Item(0)
                Else
                    If CardPlayList.Count > 0 Then ReturnObj = CardPlayList.Item(0)
                End If
            End If

            'get the index of the card to be played
            For i = 1 To Cards.Count
                If ReturnObj Is Cards.Item(i) Then
                    CardIndex = i
                End If
            Next

            'handle lower priority 
            If ReturnObj Is Nothing Then
                If MyPlayStyle = PlayStyle.Passive And Lives = 2 And Difference <> 0 Then
                    CardIndex = 0
                Else
                    Dim myindex() As Integer = LowPrioritySpells()
                    If myindex.Length = 0 Then
                        CardIndex = 0
                    Else
                        CardIndex = MathFunc.DiscreteRandomNumber(myindex)
                    End If
                End If
            End If
            Return CardIndex
        End Function
        Private Function LowPrioritySpells() As Integer()
            Dim myindex(0) As Integer
            Dim keepcard As Boolean
            For i = 1 To Cards.Count
                keepcard = True
                If Cards.Item(i).GetCardID = 47 Then
                    If RemoveGolden(OpponentPlayedCards).Count >= 2 Then
                        keepcard = True
                    ElseIf RemoveGolden(OpponentPlayedCards).Count = 1 And RemoveGolden(PlayedCards).Count = 1 Then
                        If Difference < 0 Then
                            keepcard = True
                        Else
                            keepcard = False
                        End If
                    Else
                        keepcard = False
                    End If
                End If

                If Cards.Item(i).GetCardID = 49 Then
                    If Difference < 0 And RemoveGolden(OpponentPlayedCards).Count >= 1 Then
                        keepcard = True
                    Else
                        keepcard = False
                    End If
                End If

                If keepcard = True Then
                    ReDim Preserve myindex(myindex.Length)
                    myindex(myindex.Length - 2) = i
                End If
            Next
            ReDim Preserve myindex(myindex.Length - 2)
            Return myindex
        End Function
        Private Function SortSpellCards(ByRef mylist As List(Of Spell))
            Dim tempspell As Spell
            Dim intarray As New List(Of Spell)
            Dim priority As Boolean

            For i = 0 To (mylist.Count - 1)
                tempspell = mylist.Item(i)
                If tempspell.GetCardID = 40 Then
                    If RemoveGolden(OpponentPlayedCards).Count >= 1 Then intarray.Add(tempspell)
                    If RemoveGolden(OpponentPlayedCards).Count >= 3 Then
                        PutCardAtTopOfList(intarray, tempspell)
                    End If
                End If

                If tempspell.GetCardID = 41 Then
                    If PlayedCards.Count >= 3 And MeBuffPlayed = False Then
                        intarray.Add(tempspell)
                        If PlayedCards.Count >= 5 Then
                            priority = True
                            PutCardAtTopOfList(intarray, tempspell)
                        End If
                    End If
                End If

                If tempspell.GetCardID = 42 Then
                    If My.Forms.Game.AURA.PeekCardID <> 42 Then
                        intarray.Add(tempspell)
                        If PlayedCards.Count >= 5 Then
                            priority = True
                            PutCardAtTopOfList(intarray, tempspell)
                        End If
                    ElseIf MyPlayStyle = PlayStyle.Slow Then
                        intarray.Add(tempspell)
                        priority = True
                        PutCardAtTopOfList(intarray, tempspell)
                    End If
                End If

                If tempspell.GetCardID = 43 Then
                    intarray.Add(tempspell)
                    If MyPlayStyle = PlayStyle.Slow Then
                        intarray.Add(tempspell)
                        priority = True
                        PutCardAtTopOfList(intarray, tempspell)
                    End If
                End If

                If tempspell.GetCardID = 44 Then
                    If PlayedCards.Count >= 3 And MeBuffPlayed = False Then
                        intarray.Add(tempspell)
                        If PlayedCards.Count >= 5 Then
                            priority = True
                            PutCardAtTopOfList(intarray, tempspell)
                        End If
                    End If
                End If
                If tempspell.GetCardID = 45 Then
                    If PlayedCards.Count >= 3 And MeBuffPlayed = False Then
                        intarray.Add(tempspell)
                        If PlayedCards.Count >= 5 Then
                            priority = True
                            PutCardAtTopOfList(intarray, tempspell)
                        End If
                    End If
                End If
                If tempspell.GetCardID = 46 Then
                    If My.Forms.Game.AURA.PeekCardID <> 46 Then
                        intarray.Add(tempspell)
                        If PlayedCards.Count >= 5 Then
                            priority = True
                            PutCardAtTopOfList(intarray, tempspell)
                        End If
                    ElseIf MyPlayStyle = PlayStyle.Slow Then
                        intarray.Add(tempspell)
                        priority = True
                        PutCardAtTopOfList(intarray, tempspell)
                    End If
                End If

                If tempspell.GetCardID = 47 Then
                    If RemoveGolden(OpponentPlayedCards).Count >= 2 Then
                        intarray.Add(tempspell)
                        If PlayedCards.Count = 0 Then
                            priority = True
                            PutCardAtTopOfList(intarray, tempspell)
                        End If
                    ElseIf MyPlayStyle = PlayStyle.Slow Then
                        intarray.Add(tempspell)
                        priority = True
                        PutCardAtTopOfList(intarray, tempspell)
                    End If
                End If

                If tempspell.GetCardID = 48 Then
                    If BattlePower >= 300 Then intarray.Add(tempspell)
                    If BattlePower >= 700 Then
                        priority = True
                        PutCardAtTopOfList(intarray, tempspell)
                    End If
                End If

                If tempspell.GetCardID = 49 Then
                    If RemoveGolden(OpponentPlayedCards).Count >= 3 Then
                        intarray.Add(tempspell)
                        If PlayedCards.Count = 0 Then
                            priority = True
                            PutCardAtTopOfList(intarray, tempspell)
                        End If
                    ElseIf MyPlayStyle = PlayStyle.Slow Then
                        intarray.Add(tempspell)
                        priority = True
                        PutCardAtTopOfList(intarray, tempspell)
                    End If
                End If

                If tempspell.GetCardID = 50 Then
                    If My.Forms.Game.CheckCardPlayed(Player, 30) Then
                        intarray.Add(tempspell)
                        priority = True
                        PutCardAtTopOfList(intarray, tempspell)
                    ElseIf MyPlayStyle = PlayStyle.Slow Then
                        intarray.Add(tempspell)
                        priority = True
                        PutCardAtTopOfList(intarray, tempspell)
                    End If
                End If

                If tempspell.GetCardID = 51 Then
                    If opponentbuffplayed = True Then
                        intarray.Add(tempspell)
                        priority = True
                        PutCardAtTopOfList(intarray, tempspell)
                    End If
                End If

                If tempspell.GetCardID = 52 Then
                    If auraplayed = True Then
                        intarray.Add(tempspell)
                    ElseIf MyPlayStyle = PlayStyle.Slow Then
                        intarray.Add(tempspell)
                        priority = True
                        PutCardAtTopOfList(intarray, tempspell)
                    End If
                End If

                If tempspell.GetCardID = 53 Then
                    If Me.DiscardPileCount > 5 Then
                        intarray.Add(tempspell)
                    ElseIf MyPlayStyle = PlayStyle.Slow Then
                        intarray.Add(tempspell)
                        priority = True
                        PutCardAtTopOfList(intarray, tempspell)
                    End If
                End If

                If tempspell.GetCardID = 54 Then
                    intarray.Add(tempspell)
                    If MyPlayStyle = PlayStyle.Slow Then
                        intarray.Add(tempspell)
                        priority = True
                        PutCardAtTopOfList(intarray, tempspell)
                    End If
                End If

                If tempspell.GetCardID = 55 Then
                    If Cards.Count <= 9 Then
                        intarray.Add(tempspell)
                    ElseIf MyPlayStyle = PlayStyle.Slow Then
                        intarray.Add(tempspell)
                        priority = True
                        PutCardAtTopOfList(intarray, tempspell)
                    End If
                End If

                If tempspell.GetCardID = 56 Then
                    If PlayedCards.Count >= 3 And MeBuffPlayed = False Then
                        intarray.Add(tempspell)
                        If PlayedCards.Count >= 5 Then
                            priority = True
                            PutCardAtTopOfList(intarray, tempspell)
                        End If
                    End If
                End If
                If tempspell.GetCardID = 57 Then
                    If PlayedCards.Count >= 5 Then
                        intarray.Add(tempspell)
                        If PlayedCards.Count >= 6 Then
                            priority = True
                            PutCardAtTopOfList(intarray, tempspell)
                        End If
                    End If
                End If

                If tempspell.GetCardID = 58 Then
                    If Cards.Count <= 8 Then
                        intarray.Add(tempspell)
                    ElseIf MyPlayStyle = PlayStyle.Slow Then
                        intarray.Add(tempspell)
                        priority = True
                        PutCardAtTopOfList(intarray, tempspell)
                    End If
                End If

                If tempspell.GetCardID = 59 Then
                    If AbilityUsed = True Then
                        intarray.Add(tempspell)
                        PutCardAtTopOfList(intarray, tempspell)
                    End If
                End If
                If tempspell.GetCardID = 60 Then
                    If Me.DiscardPileCount >= 4 Then
                        intarray.Add(tempspell)
                    ElseIf MyPlayStyle = PlayStyle.Slow Then
                        intarray.Add(tempspell)
                        priority = True
                        PutCardAtTopOfList(intarray, tempspell)
                    End If
                End If
                If tempspell.GetCardID = 61 Then
                    If My.Forms.Game.AURA.PeekCardID <> 61 Then
                        intarray.Add(tempspell)
                        If PlayedCards.Count >= 5 Then
                            priority = True
                            PutCardAtTopOfList(intarray, tempspell)
                        End If
                    ElseIf MyPlayStyle = PlayStyle.Slow Then
                        intarray.Add(tempspell)
                        priority = True
                        PutCardAtTopOfList(intarray, tempspell)
                    End If
                End If
                If tempspell.GetCardID = 62 Then
                    If My.Forms.Game.CheckCardPlayed(Player, 16) Then
                        intarray.Add(tempspell)
                    ElseIf MyPlayStyle = PlayStyle.Slow Then
                        intarray.Add(tempspell)
                        priority = True
                        PutCardAtTopOfList(intarray, tempspell)
                    End If
                End If
                If tempspell.GetCardID = 63 Then
                    If My.Forms.Game.CheckCardPlayed(Player, 30) And RemoveGolden(OpponentPlayedCards).Count >= 4 Then
                        intarray.Add(tempspell)
                        priority = True
                        PutCardAtTopOfList(intarray, tempspell)
                    ElseIf MyPlayStyle = PlayStyle.Slow Then
                        intarray.Add(tempspell)
                        priority = True
                        PutCardAtTopOfList(intarray, tempspell)
                    End If
                End If
                If tempspell.GetCardID = 64 Then
                    If My.Forms.Game.CheckCardPlayed(Player, 16) And RemoveGolden(OpponentPlayedCards).Count >= 1 Then
                        intarray.Add(tempspell)
                        priority = True
                        PutCardAtTopOfList(intarray, tempspell)
                    ElseIf MyPlayStyle = PlayStyle.Slow Then
                        intarray.Add(tempspell)
                        priority = True
                        PutCardAtTopOfList(intarray, tempspell)
                    End If

                End If
            Next
            mylist.Clear()

            For i = 0 To intarray.Count - 1
                mylist.Add(intarray.Item(i))
            Next

            Return priority
        End Function
        Private Sub PutCardAtTopOfList(ByRef mylist As List(Of Spell), ByRef spellobj As Spell)
            Dim temp As New List(Of Spell)
            mylist.Remove(spellobj)
            temp.Add(spellobj)
            For i = 0 To mylist.Count - 1
                temp.Add(mylist.Item(i))
            Next
            mylist = temp

        End Sub
        Private Sub SortCharacterCards(ByRef mylist As List(Of CharacterCard), ascending As Boolean, golden As Boolean)
            Dim temp As Game.CharacterCard
            For i = mylist.Count - 2 To 1 Step -1
                For x = 0 To i
                    If golden = True Then
                        If mylist.Item(x).IsSpecial = False And mylist.Item(x + 1).IsSpecial = True Then
                            temp = mylist.Item(x)
                            mylist.Item(x) = mylist.Item(x + 1)
                            mylist.Item(x + 1) = temp

                        ElseIf mylist.Item(x).IsSpecial = True And mylist.Item(x + 1).IsSpecial = True Then
                            If mylist.Item(x).GetAttack_Value < mylist.Item(x + 1).GetAttack_Value Then
                                temp = mylist.Item(x)
                                mylist.Item(x) = mylist.Item(x + 1)
                                mylist.Item(x + 1) = temp
                            End If

                        ElseIf mylist.Item(x).IsSpecial = False And mylist.Item(x + 1).IsSpecial = False Then
                            If ascending = True Then
                                If mylist.Item(x).GetAttack_Value > mylist.Item(x + 1).GetAttack_Value Then
                                    temp = mylist.Item(x)
                                    mylist.Item(x) = mylist.Item(x + 1)
                                    mylist.Item(x + 1) = temp
                                End If
                            Else
                                If mylist.Item(x).GetAttack_Value < mylist.Item(x + 1).GetAttack_Value Then
                                    temp = mylist.Item(x)
                                    mylist.Item(x) = mylist.Item(x + 1)
                                    mylist.Item(x + 1) = temp
                                End If
                            End If
                        End If
                    ElseIf golden = False Then

                        If mylist.Item(x).IsSpecial = True And mylist.Item(x + 1).IsSpecial = False Then
                            temp = mylist.Item(x)
                            mylist.Item(x) = mylist.Item(x + 1)
                            mylist.Item(x + 1) = temp
                        ElseIf mylist.Item(x).IsSpecial = True And mylist.Item(x + 1).IsSpecial = True Then
                            If mylist.Item(x).GetAttack_Value < mylist.Item(x + 1).GetAttack_Value Then
                                temp = mylist.Item(x)
                                mylist.Item(x) = mylist.Item(x + 1)
                                mylist.Item(x + 1) = temp
                            End If
                        ElseIf mylist.Item(x).IsSpecial = False And mylist.Item(x + 1).IsSpecial = False Then
                            If ascending = True Then
                                If mylist.Item(x).GetAttack_Value > mylist.Item(x + 1).GetAttack_Value Then
                                    temp = mylist.Item(x)
                                    mylist.Item(x) = mylist.Item(x + 1)
                                    mylist.Item(x + 1) = temp
                                End If
                            Else
                                If mylist.Item(x).GetAttack_Value < mylist.Item(x + 1).GetAttack_Value Then
                                    temp = mylist.Item(x)
                                    mylist.Item(x) = mylist.Item(x + 1)
                                    mylist.Item(x + 1) = temp
                                End If
                            End If
                        End If
                    End If
                Next
            Next

        End Sub
        Public Function GetPlayer()
            Return Player
        End Function
        Public Sub getInfo(ByVal inBattlePower As Integer, ByVal inOpponentPower As Integer, ByVal inCards As Collection, ByVal inPlayedCards As Collection,
                           ByVal inOpponentPLayedCards As Collection, ByVal inOpponentPassed As Boolean, ByVal inOpponentCardCount As Integer,
                           ByVal Lives As Integer, ByVal OpponentLives As Integer, ByVal opponentbuffplayed As Boolean, ByVal mebuffplayed As Boolean,
                           ByVal mediscardpilecount As Integer)
            BattlePower = inBattlePower
            OpponentBattlePower = inOpponentPower
            Cards = inCards
            PlayedCards = inPlayedCards
            OpponentPlayedCards = inOpponentPLayedCards
            OpponentPassed = inOpponentPassed
            OpponentCardCount = inOpponentCardCount
            Me.OpponentLives = OpponentLives
            Me.Lives = Lives
            Me.opponentbuffplayed = opponentbuffplayed
            Difference = BattlePower - OpponentBattlePower
            Me.MeBuffPlayed = mebuffplayed
            Me.DiscardPileCount = mediscardpilecount

            If Lives = 1 And Difference < -50 And OpponentLives = 2 Then
                MyPlayStyle = PlayStyle.Agressive
            ElseIf Lives = 2 And -100 < Difference And Difference < -50 And OpponentLives = 2 Then
                MyPlayStyle = PlayStyle.Agressive
            ElseIf Lives = 1 And OpponentLives = 1 Then
                MyPlayStyle = PlayStyle.Slow
            ElseIf Lives = 2 And OpponentLives = 1 And Cards.Count >= 6 Then
                MyPlayStyle = PlayStyle.Agressive
            ElseIf Lives = 2 And OpponentLives = 2 And OpponentPassed = True And Difference < 0 Then
                MyPlayStyle = PlayStyle.Agressive
            ElseIf Lives = 1 And OpponentLives = 2 And OpponentPassed = True And Difference < 0 Then
                MyPlayStyle = PlayStyle.Agressive
            Else
                MyPlayStyle = PlayStyle.Passive
            End If
        End Sub
        Public Sub UseChampionAbility()
            Dim e As New EventArgs
            If Player = 1 Then
                My.Forms.Game.ChampionBoxDoubleClick(My.Forms.Game.Player1Champion.GetPictureBoxReference, e)
            End If
            If Player = 2 Then
                My.Forms.Game.ChampionBoxDoubleClick(My.Forms.Game.Player2Champion.GetPictureBoxReference, e)
            End If
        End Sub
        Public Sub EndTurn()
            Dim sender As New Object
            Dim e As New EventArgs
            If Player = 1 Then
                Game.PlayerPassed(1)
            Else
                Game.PlayerPassed(2)
            End If
            My.Forms.Game.NewTurn()
        End Sub
        Public Sub PlayCard()
            Dim e As New EventArgs
            Dim CardIndex As Integer
            If Cards.Count <> 0 Then
                If Player = 1 Then
                    CardIndex = Me.getMoveIndex
                    If CardIndex <= 0 Or CardIndex > 10 Then
                        EndTurn()
                    Else
                        My.Forms.Game.PictureBoxClick(My.Forms.Game.Player1Hand.GetPictureBoxRefByIndex(CardIndex), e)
                    End If
                End If
                If Player = 2 Then
                    CardIndex = Me.getMoveIndex
                    If CardIndex <= 0 Or CardIndex > 10 Then
                        EndTurn()
                    Else
                        My.Forms.Game.PictureBoxClick(My.Forms.Game.Player2Hand.GetPictureBoxRefByIndex(CardIndex), e)
                    End If
                End If
            Else
                Game.AITimer.Stop()
            End If
        End Sub
        Public Sub MakeAction()
            My.Forms.Game.GiveAIInfo(Me)

            If OpponentPassed = True And BattlePower > OpponentBattlePower And Cards.Count < 10 Then
                EndTurn()
                GoTo done
            End If
            If MyPlayStyle = PlayStyle.Passive Then
                If SmartPass() Then
                    EndTurn()
                    My.Forms.Game.OpponentPassedDisplay()
                    GoTo done
                End If
                If UseAbility() Then
                    AbilityUsed = True
                    UseChampionAbility()
                    GoTo done
                End If
                PlayCard()
                GoTo done
            End If
            If MyPlayStyle = PlayStyle.Agressive Then
                If UseAbility() Then
                    AbilityUsed = True
                    UseChampionAbility()
                    GoTo done
                End If
                PlayCard()
                GoTo done
            End If
            If MyPlayStyle = PlayStyle.Slow Then
                If UseAbility() Then
                    AbilityUsed = True
                    UseChampionAbility()
                    GoTo done
                End If
                PlayCard()
                GoTo done
            End If
done:
        End Sub
        Private Function RemoveGolden(ByVal CardArray As Collection) As Collection
            Dim outarray As New Collection
            For i = 1 To CardArray.Count
                If CardArray.Item(i).isSpecial = False Then
                    outarray.Add(CardArray.Item(i))
                End If
            Next
            Return outArray
        End Function
        Public Function SmartPass()
            If Cards.Count < 10 Then
                If Difference = 0 Then
                    Return False
                End If
                If Lives = 2 And OpponentLives = 2 Then
                    If Cards.Count = OpponentCardCount Then
                        If Difference > 0 Then
                            Return True
                        End If
                    End If
                    If Difference > 200 Then
                        Return True
                    End If
                    If Difference < -200 Then
                        Return True
                    End If
                End If
                If Lives = 2 And OpponentLives = 1 Then
                    If Cards.Count = OpponentCardCount Then
                        If Difference > -80 Then
                            Return True
                        End If
                    End If
                    If Difference > 150 Then
                        Return True
                    End If
                    If Difference < -150 Then
                        Return True
                    End If
                End If
            End If
            Return False
        End Function
        Public Function UseAbility()
            ' make this procedure more complex
            If AbilityUsed = False Then
                If Champion = Game.Champion.Name.Knight Then
                    If Difference < -150 Then Return True
                End If
                If Champion = Game.Champion.Name.Lord Then
                    If PlayedCards.Count >= 4 And MeBuffPlayed = False Then Return True
                End If
                If Champion = Game.Champion.Name.Witch Then
                    If RemoveGolden(OpponentPlayedCards).Count >= 2 Then Return True
                End If
                If Champion = Game.Champion.Name.Sage Then
                    If opponentbuffplayed = True Then Return True
                End If
                If Cards.Count = 1 And Lives = 1 Then
                    Return True
                End If
            End If
            Return False
        End Function
    End Class
    Private Sub AITimer_Tick(sender As Object, e As EventArgs) Handles AITimer.Tick
        AITimer.Stop()
        AITimer.Interval = MathFunc.randomnumber(3000, 2000)
        AIPlayer.MakeAction()
    End Sub
    Private Sub SpellDisplayTimer_Tick(sender As Object, e As EventArgs) Handles SpellDisplayTimer.Tick
        My.Forms.SpellDisplay.Timer1.Start()
        SpellDisplayTimer.Stop()
    End Sub
    Private Sub DisplayLargeCard(ByVal Image As Bitmap, ByVal pnt As Point)
        My.Forms.CardDisplay.PictureBox1.Image = Image
        My.Forms.CardDisplay.Show()
    End Sub
    Private Sub DisplayLargeSpell(ByVal Image As Bitmap)
        SpellDisplayTimer.Stop()
        My.Forms.SpellDisplay.Hide()
        My.Forms.SpellDisplay.Timer1.Stop()
        My.Forms.SpellDisplay.Opacity = 1
        My.Forms.SpellDisplay.PictureBox1.Image = Image
        My.Forms.SpellDisplay.Show()
        SpellDisplayTimer.Start()
    End Sub
    Private Sub PlayerPassed(ByVal player As Integer)
        If player = AIPlayer.GetPlayer Then
            OpponentPassedDisplay()
        Else
            YourPassedDisplay()
        End If
        If player = 1 Then
            Player1Passed = True
            Passed1.Image = My.Resources.flying_flag__3_
        ElseIf player = 2 Then
            Player2Passed = True
            Passed2.Image = My.Resources.flying_flag__3_
        End If
    End Sub
    Private Sub YourPassedDisplay()
        DisplayMessage("You Passed", My.Resources.flying_flag__1_, My.Resources.cloth)
    End Sub
    Private Sub OpponentPassedDisplay()
        DisplayMessage("Your Opponent Has Passed", My.Resources.flying_flag__1_, My.Resources.cloth)
    End Sub
    Private Sub PlayerNoCardsLeft(ByVal player As Integer)
        If player = AIPlayer.GetPlayer Then
            DisplayMessage(("Your Opponent Has Ran Out Of Cards"), My.Resources.flying_flag__1_, My.Resources.cloth)
            AITimer.Stop()
        Else
            DisplayMessage(("You Have Run Out Of Cards"), My.Resources.flying_flag__1_, My.Resources.cloth)
        End If
        If player = 1 Then
            Player1Passed = True
            Passed1.Image = My.Resources.flying_flag__3_
        ElseIf player = 2 Then
            Player2Passed = True
            Passed2.Image = My.Resources.flying_flag__3_
        End If
    End Sub
    Private Sub WinRoundDisplay(ByVal player As Integer)
        If player = AIPlayer.GetPlayer Then
            DisplayMessage("Your Opponent Wins The Round", My.Resources.laurel_crown, My.Resources.cloth)
        ElseIf player = 0 Then
            DisplayMessage("You Draw The Round", My.Resources.laurel_crown, My.Resources.cloth)
        Else
            DisplayMessage("You Win The Round", My.Resources.laurel_crown, My.Resources.cloth)
        End If
    End Sub
    Private Sub DisplayWinner(ByVal player As Integer)
        My.Forms.Background_Music_Player.AxWindowsMediaPlayer1.Ctlcontrols.pause()
        If player = 0 Then
            My.Forms.WinnerDisplay.PictureBox1.Image = Player2Champion.ReturnChampionImage
            My.Forms.WinnerDisplay.Label1.Text = "Draw"
            My.Forms.WinnerDisplay.Label2.Text = "You Have Drawn With the " + Player1Champion.ReturnStringName()
            My.Forms.WinnerDisplay.PictureBox2.Image = My.Resources.pointy_sword
            My.Computer.Audio.Play(My.Resources.sword_unsheathe, AudioPlayMode.Background)
        End If
        If player = 1 Then
            My.Forms.WinnerDisplay.PictureBox1.Image = Player1Champion.ReturnChampionImage
            If player = AIPlayer.GetPlayer Then
                My.Forms.WinnerDisplay.Label1.Text = "Defeat"
                My.Forms.WinnerDisplay.Label2.Text = "Your Opponent Wins With The " + Player1Champion.ReturnStringName()
                My.Forms.WinnerDisplay.PictureBox2.Image = My.Resources.hasty_grave
                My.Computer.Audio.Play(My.Resources.sword_unsheathe, AudioPlayMode.Background)
            Else
                My.Forms.WinnerDisplay.Label1.Text = "Victory"
                My.Forms.WinnerDisplay.Label2.Text = "You Win With The " + Player1Champion.ReturnStringName()
                My.Forms.WinnerDisplay.PictureBox2.Image = My.Resources.laurel_crown
                My.Computer.Audio.Play(My.Resources.sword_unsheathe, AudioPlayMode.Background)
            End If
        ElseIf player = 2 Then
            My.Forms.WinnerDisplay.PictureBox1.Image = Player2Champion.ReturnChampionImage
            If player = AIPlayer.GetPlayer Then
                My.Forms.WinnerDisplay.Label1.Text = "Defeat"
                My.Forms.WinnerDisplay.Label2.Text = "Your Opponent Wins With The " + Player2Champion.ReturnStringName()
                My.Forms.WinnerDisplay.PictureBox2.Image = My.Resources.hasty_grave
                My.Computer.Audio.Play(My.Resources.sword_unsheathe, AudioPlayMode.Background)
            Else
                My.Forms.WinnerDisplay.Label1.Text = "Victory"
                My.Forms.WinnerDisplay.Label2.Text = "You Win With The " + Player2Champion.ReturnStringName()
                My.Forms.WinnerDisplay.PictureBox2.Image = My.Resources.laurel_crown
                My.Computer.Audio.Play(My.Resources.sword_unsheathe, AudioPlayMode.Background)
            End If
        End If
        My.Forms.BlurrBackGround.Owner = Me
        My.Forms.BlurrBackGround.Show()
        My.Forms.WinnerDisplay.ShowDialog(Me)
    End Sub

    Private Sub TurnDisplayTimer_Tick(sender As Object, e As EventArgs) Handles TurnDisplayTimer.Tick
        My.Forms.MessageDisplay.Timer1.Start()
        TurnDisplayTimer.Stop()
    End Sub
    Private Sub WinnerDisplayTimer_Tick(sender As Object, e As EventArgs) Handles WinnerDisplayTimer.Tick
        WinnerDisplayTimer.Stop()
        If WinnerDisplayTimer.Tag = 0 Then DisplayWinner(0)
        If WinnerDisplayTimer.Tag = 1 Then DisplayWinner(1)
        If WinnerDisplayTimer.Tag = 2 Then DisplayWinner(2)
    End Sub
    Private Sub ChampionAbilityDisplay(ByVal player As Integer)
        Dim message As String
        If player = AIPlayer.GetPlayer Then
            message = "Your Opponent Used "
        Else
            message = "You Used "
        End If
        If player = 1 Then
            message = message + Player1Champion.ReturnStringAbility
        ElseIf player = 2 Then
            message = message + Player2Champion.ReturnStringAbility
        End If
        DisplayMessage(message, My.Resources.pointy_sword, My.Resources.sword_unsheathe)
    End Sub

    Private Sub LoadingScreenTimer_Tick(sender As Object, e As EventArgs) Handles LoadingScreenTimer.Tick
        My.Forms.Background_Music_Player.PlayMusic()
        Me.Opacity = 1
        LoadingScreenTimer.Stop()
        DisplayVersusLoadingScreen()
        DisplayDiscardCardWindow()
        PlayerStartingTimer.Tag = PlayerTurn
        PlayerStartingTimer.Start()
    End Sub
    Private Sub DisplayStartingPlayer(ByVal Player As Integer)
        If Player = AIPlayer.GetPlayer Then
            DisplayMessage("Your Opponent Starts", My.Resources.card_play, My.Resources.cloth)
        Else
            DisplayMessage("You Start", My.Resources.card_play, My.Resources.cloth)
        End If
    End Sub
    Private Sub DisplayVersusLoadingScreen()
        My.Forms.VersusLoadingScreen.Setup(Player1Champion, Player2Champion)
        My.Forms.VersusLoadingScreen.ShowDialog(Me)
    End Sub
    Private Sub DisplayMessage(ByVal message As String, ByVal image As Bitmap, ByRef sound As IO.Stream)
        My.Forms.MessageDisplay.Hide()
        My.Forms.MessageDisplay.Timer1.Stop()
        My.Forms.MessageDisplay.Opacity = 0.9
        TurnDisplayTimer.Stop()
        My.Forms.MessageDisplay.Label1.Text = message
        My.Forms.MessageDisplay.PictureBox1.Image = image
        My.Forms.MessageDisplay.Show()
        My.Computer.Audio.Play(sound, AudioPlayMode.Background)
        TurnDisplayTimer.Start()
    End Sub
    Private Sub PlayerStartingTimer_Tick(sender As Object, e As EventArgs) Handles PlayerStartingTimer.Tick
        Dim int As Integer
        If PlayerStartingTimer.Tag = 1 Then int = 2 Else int = 1
        PlayerStartingTimer.Stop()
        DisplayStartingPlayer(int)
        NewTurn()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        My.Computer.Audio.Play(My.Resources.cloth, AudioPlayMode.Background)
        My.Forms.BlurrBackGround.Owner = Me
        My.Forms.BlurrBackGround.Show()
        My.Forms.OptionsDisplay.ShowDialog(Me)
    End Sub
    Public Sub DisplayDiscardCardWindow()
        Dim player As Integer = AIPlayer.GetPlayer
        If player = 1 Then
            player = 2
            My.Forms.DiscardCards.setUp(Player2Hand)
        ElseIf player = 2 Then
            player = 1
            My.Forms.DiscardCards.setUp(Player1Hand)
        End If
        My.Forms.DiscardCards.ShowDialog(Me)
        ManageDiscardedCards(My.Forms.DiscardCards.Tag, player)
    End Sub
    Public Sub ManageDiscardedCards(ByVal array() As Bitmap, ByVal player As Integer)
        Dim temp As Object
        If player = 1 Then
            For i = 1 To array.Length
                temp = Player1Hand.RemoveCardByIndexReference(Player1Hand.GetCardIndexByBitmapValue(array(i - 1)))
                Player1Deck.ShuffleCardBackIn(temp)
                Player1Hand.Add(Player1Deck.DealCard)
            Next
        ElseIf player = 2 Then
            For i = 1 To array.Length
                temp = Player2Hand.RemoveCardByIndexReference(Player2Hand.GetCardIndexByBitmapValue(array(i - 1)))
                Player2Deck.ShuffleCardBackIn(temp)
                Player2Hand.Add(Player2Deck.DealCard)
            Next
        End If
    End Sub

    Private Sub WInRoundTimer_Tick(sender As Object, e As EventArgs) Handles WInRoundTimer.Tick
        WinRoundDisplay(WInRoundTimer.Tag)
        WInRoundTimer.Stop()
        NewRoundTimer.Start()
    End Sub

    Private Sub NewRoundTimer_Tick(sender As Object, e As EventArgs) Handles NewRoundTimer.Tick
        NewRoundTimer.Stop()
        If Player2Life.GetLifes = 0 And Player1Life.GetLifes = 0 Then
            EndGame(0)
            GoTo Gamedone
        End If
        If Player1Life.GetLifes() = 0 Then
            EndGame(2)
            GoTo GameDone
        ElseIf Player2Life.GetLifes() = 0 Then
            EndGame(1)
            GoTo GameDone
        End If
        PlayableState = True
        Player1Passed = False
        Player2Passed = False
        PlayBuff(Nothing, 1)
        PlayBuff(Nothing, 2)
        ClearPlayerBoards(False)
        Player1BattlePower.ResetPower()
        Player2BattlePower.ResetPower()
        Player1Board.AURAPower = 0
        Player2Board.AURAPower = 0
        DrawCard(1)
        DrawCard(2)
        Passed1.Image = Nothing
        Passed2.Image = Nothing
        NewTurn()
Gamedone:
    End Sub
    Private Sub PopUpMessage(ByVal Message As String)
        My.Forms.PopupMessage.Owner = Me
        My.Forms.PopupMessage.Hide()
        My.Forms.PopupMessage.StopTimers()
        My.Forms.PopupMessage.Message = Message
        My.Computer.Audio.Play(My.Resources.cloth, AudioPlayMode.Background)
        My.Forms.PopupMessage.ShowDialog(Me)
    End Sub
    Private Sub HoveringMessage(ByVal Message As String)
        HoveringMessageTimer.Stop()
        HoveringMessageTimer.Tag = Message
        HoveringMessageTimer.Start()
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim mute As Boolean = My.Forms.Background_Music_Player.AxWindowsMediaPlayer1.settings.mute
        If mute Then My.Forms.Background_Music_Player.AxWindowsMediaPlayer1.settings.mute = False Else My.Forms.Background_Music_Player.AxWindowsMediaPlayer1.settings.mute = True
    End Sub
    Private Sub HoveringMessageTimer_Tick(sender As Object, e As EventArgs) Handles HoveringMessageTimer.Tick
        My.Forms.HoverMessage.Owner = Me
        My.Forms.HoverMessage.Message = HoveringMessageTimer.Tag
        My.Forms.HoverMessage.Show()
    End Sub
    Private Sub Button2_MouseEnter(sender As Object, e As EventArgs) Handles Button2.MouseEnter
        HoveringMessage("Mute/UnMute Music")
    End Sub
    Private Sub Button1_MouseEnter(sender As Object, e As EventArgs) Handles Button1.MouseEnter
        HoveringMessage("Options")
    End Sub
    Private Sub ButtonMouseLeave(sender As Object, e As EventArgs) Handles Button1.MouseLeave, Button2.MouseLeave
        My.Forms.Launcher.TopMost = True
        My.Forms.HoverMessage.Hide()
        My.Forms.Launcher.TopMost = False
        HoveringMessageTimer.Stop()
    End Sub
    Private Sub PictureBox47_MouseHover(sender As Object, e As EventArgs) Handles PictureBox47.MouseHover
        HoveringMessage(Player1Champion.ReturnAbilityDescription())
    End Sub
    Private Sub PictureBox46_MouseHover(sender As Object, e As EventArgs) Handles PictureBox46.MouseHover
        HoveringMessage(Player2Champion.ReturnAbilityDescription())
    End Sub
    Private Sub PictureBox47_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox47.MouseLeave
        My.Forms.Launcher.TopMost = True
        My.Forms.HoverMessage.Hide()
        My.Forms.Launcher.TopMost = False
        HoveringMessageTimer.Stop()
    End Sub
    Private Sub PictureBox46_MouseLeave(sender As Object, e As EventArgs) Handles PictureBox46.MouseLeave
        My.Forms.Launcher.TopMost = True
        My.Forms.HoverMessage.Hide()
        My.Forms.Launcher.TopMost = False
        HoveringMessageTimer.Stop()
    End Sub
    Private Sub Button66_Click(sender As Object, e As EventArgs) Handles Button66.Click
        My.Forms.BlurrBackGround.Owner = Me
        My.Forms.BlurrBackGround.Show()
        My.Forms.Document.LoadFile(My.Resources.Dreadmare_Instructions)
        My.Computer.Audio.Play(My.Resources.cloth, AudioPlayMode.Background)
        My.Forms.Document.ShowDialog()
    End Sub
    Private Sub Button66_MouseEnter(sender As Object, e As EventArgs) Handles Button66.MouseEnter
        HoveringMessage("Instructions")
    End Sub

    Private Sub Button66_MouseLeave(sender As Object, e As EventArgs) Handles Button66.MouseLeave
        My.Forms.Launcher.TopMost = True
        My.Forms.HoverMessage.Hide()
        My.Forms.Launcher.TopMost = False
        HoveringMessageTimer.Stop()
    End Sub
End Class

