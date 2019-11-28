Imports Solitaire.GUI.Controleur

Namespace Vue
    Public Class SolitaireForm

#Region "Déclarations"

        Private jeu As Solitaire.Modele.Modele.Solitaire = Nothing

        Private controleur As ControleurSolitaire = Nothing

        Private WithEvents plateauVue As PlateauUserControl = Nothing


#End Region

        Public Sub New()
            InitializeComponent()
            jeu = New Solitaire.Modele.Modele.Solitaire()
            controleur = New ControleurSolitaire(jeu, Me)
        End Sub

#Region "Méthodes"
        Public Sub ChargerVueJeu(ByRef pModele As Modele.Modele.Solitaire)
            Cursor.Current = Cursors.WaitCursor
            jeu = pModele
            PlateauPanel.Controls.Clear()
            plateauVue = New PlateauUserControl(jeu.Plateau)
            PlateauPanel.Controls.Add(plateauVue)
            SauverLaPartieToolStripMenuItem.Enabled = True
            Cursor.Current = Cursors.Default
        End Sub

#End Region

#Region "Evenements"

        Private Sub MenuItemQuit_Click(sender As Object, e As EventArgs) Handles MenuItemQuit.Click
            Me.Close()
        End Sub

        Private Sub MenuItemNewGame_Click() Handles MenuItemNewGame.Click
            Try
                Cursor.Current = Cursors.WaitCursor
                Me.Enabled = False
                PlateauPanel.Controls.Clear()
                jeu.NouveauJeu()
                plateauVue = New PlateauUserControl(jeu.Plateau)
                PlateauPanel.Controls.Add(plateauVue)
            Catch ex As Exception
                Sounds.playError()
                MessageBox.Show(ex.Message(), "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                Me.Enabled = True
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

        Private Sub ChargerLeJeuToolStripMenuItem_Click(pSender As Object, pE As EventArgs) Handles ChargerLeJeuToolStripMenuItem.Click
            Try
                Cursor.Current = Cursors.WaitCursor
                Me.Enabled = False
                Dim openFileDialog As OpenFileDialog = New OpenFileDialog() With {.Filter = "Fichiers de sauvegarde (*.xml)|*.xml", .CheckFileExists = True, .CheckPathExists = True}
                If openFileDialog.ShowDialog() = DialogResult.OK Then
                    controleur.ChargerLeJeu(openFileDialog.FileName())
                End If

            Catch ex As Exception
                Sounds.playError()
                MessageBox.Show(ex.Message(), "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                Me.Enabled = True
                Cursor.Current = Cursors.Default
            End Try

        End Sub

        Private Sub SauverPartieSousToolStripMenuItem_Click(pSender As Object, pE As EventArgs) Handles SauverPartieSousToolStripMenuItem.Click
            Try
                If jeu.PartieEnCours() Then
                    Dim saveFileDialog As SaveFileDialog = New SaveFileDialog() With {.Filter = "Fichiers de sauvegarde (*.xml)|*.xml"}
                    If saveFileDialog.ShowDialog() = DialogResult.OK Then
                        controleur.SauverPartieSous(saveFileDialog.FileName())
                        SauverLaPartieToolStripMenuItem.Enabled = True
                    End If
                Else
                    MessageBox.Show("Pas de partie en cours.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End If
            Catch ex As Exception
                Sounds.playError()
                MessageBox.Show(ex.Message(), "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                SauverLaPartieToolStripMenuItem.Enabled = False
            End Try
        End Sub

        Private Sub SauverLaPartieToolStripMenuItem_Click(pSender As Object, pE As EventArgs) Handles SauverLaPartieToolStripMenuItem.Click
            Try
                Cursor.Current = Cursors.WaitCursor
                Me.Enabled = False
                controleur.SauverPartie()
            Catch ex As Exception
                Sounds.playError()
                MessageBox.Show(ex.Message(), "", MessageBoxButtons.OK, MessageBoxIcon.Error)
                SauverLaPartieToolStripMenuItem.Enabled = False
            Finally
                Me.Enabled = True
                Cursor.Current = Cursors.Default
            End Try

        End Sub


#End Region
    End Class

End Namespace
