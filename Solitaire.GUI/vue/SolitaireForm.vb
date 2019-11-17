
Imports Solitaire.GUI.Controleur

Namespace Vue
    Public Class SolitaireForm
#Region "Déclarations"

        Private jeu As Solitaire.Modele.Modele.Solitaire = Nothing

        Private constroleur As ControleurSolitaire = Nothing

        Private WithEvents plateauVue As PlateauUserControl = Nothing

#End Region


        Public Sub New()
            InitializeComponent()
        End Sub

#Region "Evenements"

        Private Sub MenuItemQuit_Click(sender As Object, e As EventArgs) Handles MenuItemQuit.Click
            Me.Close()
        End Sub

        Private Sub MenuItemNewGame_Click() Handles MenuItemNewGame.Click
            Try
                Cursor.Current = Cursors.WaitCursor
                PlateauPanel.Controls.Clear()
                jeu = New Solitaire.Modele.Modele.Solitaire()
                jeu.NouveauJeu()
                plateauVue = New PlateauUserControl(jeu.Plateau)
                PlateauPanel.Controls.Add(plateauVue)
            Catch ex As Exception
                Sounds.playError()
                MessageBox.Show(ex.Message(), "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                Cursor.Current = Cursors.Default
            End Try
        End Sub

        Private Sub PlateauUserControl_SizeChanged() Handles MyBase.SizeChanged
            If Not plateauVue Is Nothing Then
                Me.plateauVue.Size = Me.PlateauPanel.Size
            End If
        End Sub

        Private Sub PlateauUserControl_Victoire() Handles plateauVue.Victoire
            If MessageBox.Show("Voulez-vous refaire une partie ?", "Rejouer une partie", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
                MenuItemNewGame_Click()
            Else
                PlateauPanel.Controls.Clear()
            End If
        End Sub

#End Region
    End Class

End Namespace
