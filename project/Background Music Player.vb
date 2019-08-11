Public Class Background_Music_Player
    Private Sub Background_Music_Player_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Me.AxWindowsMediaPlayer1.settings.volume = 30
            Me.Owner = My.Forms.Launcher
            ' file path set to the application directory
            Dim path As String = My.Application.Info.DirectoryPath + "\back_groundmusic.wav"

            ' Create or overwrite the file.
            Dim fs As IO.FileStream = IO.File.Create(path)
            Dim myMemStream As New System.IO.MemoryStream
            My.Resources.background_music.CopyTo(myMemStream)
            fs.Close()
            Dim myBytes() As Byte = myMemStream.ToArray

            ' Add data to the file.
            My.Computer.FileSystem.WriteAllBytes(path, myBytes, False)

            ' play the music on the windows media player object
            Me.AxWindowsMediaPlayer1.URL = path
            Me.AxWindowsMediaPlayer1.settings.setMode("loop", True)
            Me.AxWindowsMediaPlayer1.Ctlcontrols.pause()
        Catch ex As Exception
        End Try
    End Sub
    Public Sub PlayMusic()
        AxWindowsMediaPlayer1.Ctlcontrols.play()
    End Sub
End Class
