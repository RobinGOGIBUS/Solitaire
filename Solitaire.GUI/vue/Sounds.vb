Imports System.Media

Namespace Vue
    Public NotInheritable Class Sounds
        Public Shared Sub playSoundClick()
            Dim player As SoundPlayer = New SoundPlayer(My.Resources.clic)
            player.Play()
        End Sub

        Public Shared Sub playSoundDraw()
            Dim player As SoundPlayer = New SoundPlayer(My.Resources.piocher)
            player.Play()
        End Sub

        Public Shared Sub playMove()
            Dim player As SoundPlayer = New SoundPlayer(My.Resources.deplacer)
            player.Play()
        End Sub

        Public Shared Sub playError()
            Dim player As SoundPlayer = New SoundPlayer(My.Resources.erreur)
            player.Play()
        End Sub

    End Class
End Namespace

