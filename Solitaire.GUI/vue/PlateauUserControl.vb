Imports Solitaire.GUI.Controleur
Imports Solitaire.Modele
Imports Solitaire.Modele.Modele
Imports Solitaire.Modele.Modele.Collections.StackList
Imports Transitions

Namespace Vue

    Public Class PlateauUserControl

#Region "Constantes"

        Private Const ecartCarteNonVisibleColonne As Integer = 10

        Private Const ecartCarteVisibleColonne As Integer = 25

        Private Const largeurCarte As Integer = 115

        Private Const longueurCarte As Integer = 155

        Private Const tempsTransitionInversion As Integer = 50

        Private Const tempsTransitionVictoire As Integer = 100

        Private Const tempsTransitionCarte As Integer = 80

        Private Const pilePanelDebutNom As String = "PilePanel"

        Private Const colonnePanelDebutNom As String = "ColonnePanel"

#End Region

#Region "Déclarations"

        Private cartesVues As Dictionary(Of String, CarteVue)

        Private controleur As ControleurPlateau

        Private modele As Plateau

        Private Delegate Sub Delegue()

        ' cartes selectionnées autres que la carte courante
        Private cartesVuesSelectionnees As List(Of CarteVue)

        Private listeParents As List(Of List(Of Panel))

        Public Event Victoire()

#End Region

#Region "Constructeur"
        Public Sub New(ByRef pPlateau As Plateau)
            InitializeComponent()
            cartesVues = New Dictionary(Of String, CarteVue)
            modele = pPlateau
            controleur = New ControleurPlateau(modele, Me)
            InitialisationVue()
        End Sub


#End Region

#Region "Méthodes"

#Region "Initialisation"
        Public Sub InitialisationVue()
            InitialisationListeParents()
            InitialisationPioche()
            InitialisationColonnes()
            InitialisationPiles()
            cartesVuesSelectionnees = New List(Of CarteVue)
        End Sub

        Private Sub InitialisationPiles()
            For Each pile As StackList(Of Carte) In modele.Piles
                Dim pilePanel As Panel = listeParents(EParent.Pile)(modele.Piles.IndexOf(pile))
                Dim base As Point = pilePanel.Location
                If pile.Any() Then
                    For Each carte As Carte In pile
                        Dim image As PictureBox = New PictureBox()
                        image.Width = largeurCarte
                        image.Height = longueurCarte
                        image.Location = base
                        image.Name = carte.Type & "-" & carte.Valeur
                        Me.Controls.Add(image)
                        cartesVues.Add(image.Name, New CarteVue(pilePanel, image, carte, Me) With {.ParentType = EParent.Pile})
                    Next
                End If
            Next
        End Sub

        Private Sub InitialisationListeParents()
            listeParents = New List(Of List(Of Panel))
            For i As Integer = 0 To 2
                listeParents.Add(New List(Of Panel))
            Next
            listeParents(EParent.Pile) = Controls.OfType(Of Panel).Where(Function(p As Panel)
                                                                             Return p.Name.Contains(pilePanelDebutNom)
                                                                         End Function).OrderBy(Function(p As Panel) p.Name).ToList

            listeParents(EParent.Colonne) = Controls.OfType(Of Panel).Where(Function(c As Panel)
                                                                                Return c.Name.Contains(colonnePanelDebutNom)
                                                                            End Function).OrderBy(Function(c As Panel) c.Name).ToList
            listeParents(EParent.Pioche).Add(PiochePanelVues)
        End Sub

        Private Sub InitialisationPioche()
            If modele.PiochecartesNonVisibles.Any() Then
                For Each carte As Carte In modele.PiochecartesNonVisibles
                    Dim image As PictureBox = New PictureBox()
                    image.Width = largeurCarte
                    image.Height = longueurCarte
                    image.Name = carte.Type & "-" & carte.Valeur
                    image.Location = PiochePanelNonVues.Location
                    Me.Controls.Add(image)
                    cartesVues.Add(image.Name, New CarteVue(PiochePanelNonVues, image, carte, Me) With {.ParentType = EParent.Pioche})
                Next
            End If

            If modele.PiocheCartesVisibles.Any() Then
                For Each carte As Carte In modele.PiocheCartesVisibles
                    Dim image As PictureBox = New PictureBox()
                    image.Width = largeurCarte
                    image.Height = longueurCarte
                    image.Name = carte.Type & "-" & carte.Valeur
                    image.Location = PiochePanelVues.Location
                    Me.Controls.Add(image)
                    cartesVues.Add(image.Name, New CarteVue(PiochePanelVues, image, carte, Me) With {.ParentType = EParent.Pioche})
                Next
            End If
        End Sub

        Public Sub InitialisationColonnes()

            For Each colonne As List(Of Carte) In modele.Colonnes
                Dim colonnePanel As Panel = listeParents(EParent.Colonne)(modele.Colonnes.IndexOf(colonne))
                Dim base As Point = colonnePanel.Location
                If colonne.Any() Then
                    For Each carte In colonne
                        Dim image As PictureBox = New PictureBox()
                        image.Width = largeurCarte
                        image.Height = longueurCarte
                        image.Location = base
                        image.Name = carte.Type & "-" & carte.Valeur
                        Me.Controls.Add(image)
                        cartesVues.Add(image.Name, New CarteVue(colonnePanel, image, carte, Me) With {.ParentType = EParent.Colonne})
                        If carte.IsVisible Then
                            base.Y += ecartCarteVisibleColonne
                        Else
                            base.Y += ecartCarteNonVisibleColonne
                        End If
                    Next
                End If
            Next

        End Sub

#End Region

#Region "Affichage"
        Public Sub AfficheInversionPioches()
            DesactiveToutesActionsPioches()
            Dim transitions(modele.PiochecartesNonVisibles.Count - 1) As Transition
            Dim action = Sub() AppelDelegueActivationCartesPiocheNonVisible(transitions)
            For Each carte As Carte In modele.PiochecartesNonVisibles
                cartesVues(carte.Type & "-" & carte.Valeur).Parent = PiochePanelNonVues
                cartesVues(carte.Type & "-" & carte.Valeur).ParentType = EParent.Pioche
                cartesVues(carte.Type & "-" & carte.Valeur).LocalisationCourante = PiochePanelNonVues.Location
                cartesVues(carte.Type & "-" & carte.Valeur).NonVisiblePioche()
                Dim t As Transition = New Transition(New TransitionType_Linear(tempsTransitionInversion))
                t.add(cartesVues(carte.Type & "-" & carte.Valeur).Image, "Left", PiochePanelNonVues.Location.X)
                transitions(modele.PiochecartesNonVisibles.IndexOf(carte)) = t
            Next
            AddHandler transitions.LastOrDefault().TransitionCompletedEvent, action
            Transition.runChain(transitions)
            Sounds.playMove()
        End Sub

        Public Sub DeplacementCarte(ByRef pSender As Object)
            Dim ok As Boolean = False
            For Each pile As Panel In listeParents(EParent.Pile)
                If pile.Bounds.IntersectsWith(pSender.Bounds) Then
                    Dim indiceEme As Integer = listeParents(EParent.Pile).IndexOf(pile)
                    Dim indiceDest As Integer = listeParents(cartesVues(pSender.Name).ParentType).IndexOf(cartesVues(pSender.Name).Parent)
                    controleur.DeplacementVersPile(cartesVues(pSender.Name), cartesVues(pSender.Name).ParentType, indiceEme, indiceDest)
                    ok = True
                    Exit For
                End If
            Next
            If Not ok Then
                For Each colonne As Panel In listeParents(EParent.Colonne)
                    If colonne.Bounds.IntersectsWith(pSender.Bounds) Then
                        Dim indiceEme As Integer = listeParents(EParent.Colonne).IndexOf(colonne)
                        Dim indiceDest As Integer = listeParents(cartesVues(pSender.Name).ParentType).IndexOf(cartesVues(pSender.Name).Parent)
                        controleur.DeplacementVersColonne(cartesVues(pSender.Name), cartesVues(pSender.Name).ParentType, indiceDest, indiceEme)
                        ok = True
                        Exit For
                    End If
                Next
                If Not ok Then
                    MiseAjourCarteVueNonValide(cartesVues(pSender.Name))
                End If
            End If
        End Sub


        Public Sub MiseAjourCarteVueNonValide(ByRef pCarte As CarteVue)
            pCarte.Image.Location = pCarte.LocalisationCourante
            If cartesVuesSelectionnees.Count > 0 Then
                For Each carteVue As CarteVue In cartesVuesSelectionnees
                    carteVue.Image.Location = carteVue.LocalisationCourante
                Next
            End If
        End Sub

        Public Sub MiseAjourCarteVueValide(ByRef pCarteVue As CarteVue, ByVal pDestType As EParent, ByVal pIndiceD As Integer, ByVal Optional pVictoire As Boolean = False)
            Dim localisationCourante As Point = listeParents(pDestType)(pIndiceD).Location
            If pDestType = EParent.Colonne AndAlso modele.Colonnes(pIndiceD).Count > 1 AndAlso Not pCarteVue.Modele.Equals(modele.Colonnes(pIndiceD).FirstOrDefault()) Then
                Dim carte = modele.Colonnes(pIndiceD)(modele.Colonnes(pIndiceD).IndexOf(pCarteVue.Modele) - 1)
                localisationCourante = cartesVues(carte.Type & "-" & carte.Valeur).LocalisationCourante
                localisationCourante.Y += ecartCarteVisibleColonne
            End If
            pCarteVue.LocalisationCourante = localisationCourante
            pCarteVue.Image.Location = localisationCourante
            If pCarteVue.ParentType = EParent.Colonne Then
                MiseAjourColonne(pCarteVue.Parent)
            End If
            pCarteVue.ParentType = pDestType
            pCarteVue.Parent = listeParents(pDestType)(pIndiceD)
            pCarteVue.Image.BringToFront()
            If pDestType = EParent.Colonne AndAlso cartesVuesSelectionnees.Count > 0 Then
                For Each carteVue As CarteVue In cartesVuesSelectionnees
                    localisationCourante.Y += ecartCarteVisibleColonne
                    carteVue.LocalisationCourante = localisationCourante
                    carteVue.Image.Location = localisationCourante
                    carteVue.ParentType = pDestType
                    carteVue.Parent = listeParents(pDestType)(pIndiceD)
                    carteVue.Image.BringToFront()
                Next
            End If
            Sounds.playSoundClick()
            If pVictoire Then
                AffichageVictoire()
            End If
        End Sub

        Private Sub AffichageVictoire()
            MessageBox.Show("Vous avez gagné !!!", "", MessageBoxButtons.OK, MessageBoxIcon.Asterisk)
            DesactiveToutesActions()
            Dim transitions As List(Of Transition) = New List(Of Transition)
            Dim action = Sub() AppelDelegueVictoire(transitions)
            For Each carteVue As CarteVue In cartesVues.Values.ToList
                carteVue.Parent = PiochePanelNonVues
                carteVue.ParentType = EParent.Pioche
                carteVue.LocalisationCourante = PiochePanelNonVues.Location
                carteVue.NonVisiblePioche()
                Dim t As Transition = New Transition(New TransitionType_Linear(tempsTransitionVictoire))
                t.add(carteVue.Image, "Left", PiochePanelNonVues.Location.X)
                transitions.Add(t)
            Next
            AddHandler transitions.LastOrDefault().TransitionCompletedEvent, action
            Transition.runChain(transitions.ToArray)
        End Sub

        Public Sub DesactiveToutesActions()
            For Each carteVue As CarteVue In cartesVues.Values.ToList
                carteVue.Image.Enabled = False
                carteVue.DesactiverActions()
            Next
        End Sub

        Public Sub MiseAjourColonne(ByRef pColonne As Panel)
            Dim listeCartesColonneE As List(Of Carte) = modele.Colonnes(listeParents(EParent.Colonne).IndexOf(pColonne))
            Dim carte As Carte = Nothing
            If listeCartesColonneE.Count > 0 Then
                carte = listeCartesColonneE.LastOrDefault()
            End If
            If Not carte Is Nothing AndAlso carte.IsVisible AndAlso (Not cartesVues(carte.Type & "-" & carte.Valeur).IsVisible) Then
                cartesVues(carte.Type & "-" & carte.Valeur).Visible()
                cartesVues(carte.Type & "-" & carte.Valeur).ActiverActionsVisible()
            End If
        End Sub

#End Region

#Region "Contrôles"
        Public Sub DesactiveToutesActionsPioches()
            For Each carteVue As CarteVue In cartesVues.Values.ToList.FindAll(Function(carteV As CarteVue)
                                                                                  Return carteV.Parent.Name = PiochePanelVues.Name Or carteV.Parent.Name = PiochePanelNonVues.Name
                                                                              End Function)
                carteVue.Image.Enabled = False
                carteVue.DesactiverActions()
            Next
        End Sub


        Public Sub ActivationToutEvenementsCartesPioches()
            For Each carteVue As CarteVue In cartesVues.Values.ToList.FindAll(Function(carteV As CarteVue)
                                                                                  Return carteV.Parent.Name = PiochePanelVues.Name
                                                                              End Function)
                carteVue.Image.Enabled = True
                carteVue.ActiverActionsVisible()
            Next
            For Each carteVue As CarteVue In cartesVues.Values.ToList.FindAll(Function(carteV As CarteVue)
                                                                                  Return carteV.Parent.Name = PiochePanelNonVues.Name
                                                                              End Function)
                carteVue.Image.Enabled = True
                carteVue.ActiverActionsNonVisiblePioche()
            Next
        End Sub

        Public Sub AppelDelegueActivationCartesPiocheVisible(pT As Transition)
            Dim action = Sub() AppelDelegueActivationCartesPiocheVisible(pT)
            RemoveHandler pT.TransitionCompletedEvent, action
            Invoke(New Delegue(AddressOf ActivationToutEvenementsCartesPioches))
        End Sub

        Public Sub AppelDelegueActivationCartesPiocheNonVisible(pTransitions As Transition())
            Dim action = Sub() AppelDelegueActivationCartesPiocheNonVisible(pTransitions)
            RemoveHandler pTransitions.LastOrDefault().TransitionCompletedEvent, action
            Invoke(New Delegue(AddressOf ActivationToutEvenementsCartesPioches))
        End Sub

        Public Sub AppelDelegueVictoire(pTransitions As List(Of Transition))
            Dim action = Sub() AppelDelegueVictoire(pTransitions)
            RemoveHandler pTransitions.LastOrDefault().TransitionCompletedEvent, action
            Invoke(New Delegue(AddressOf AppelRazPlateau))
        End Sub

        Public Sub AppelRazPlateau()
            RaiseEvent Victoire()
        End Sub

        Public Function GetCartesVuesSelectionnees(ByRef pCarteVue As CarteVue) As List(Of CarteVue)
            Dim cartesSelectionnees As List(Of Carte) = modele.GetCartesSelectionnees(pCarteVue.Modele, listeParents(EParent.Colonne).IndexOf(pCarteVue.Parent))
            Dim cartesVuesSelects As List(Of CarteVue) = New List(Of CarteVue)
            If cartesSelectionnees.Count > 0 Then
                For Each carte In cartesSelectionnees
                    cartesVuesSelects.Add(cartesVues(carte.Type & "-" & carte.Valeur))
                Next
            End If
            Return cartesVuesSelects
        End Function


#End Region


#End Region

#Region "Evenements"
        Public Sub CarteVue_MouseDown(pSender As Object, pE As MouseEventArgs)
            cartesVuesSelectionnees = New List(Of CarteVue)
            If pE.Button = Windows.Forms.MouseButtons.Left Then
                cartesVues(pSender.Name).X = Control.MousePosition.X - pSender.Location.X
                cartesVues(pSender.Name).Y = Control.MousePosition.Y - pSender.Location.Y
                pSender.BringToFront()
                If cartesVues(pSender.Name).Parent.Name.Contains(colonnePanelDebutNom) Then
                    cartesVuesSelectionnees = GetCartesVuesSelectionnees(cartesVues(pSender.Name))
                    Dim base As Point = cartesVues(pSender.Name).Image.Location
                    For Each carteVue As CarteVue In cartesVuesSelectionnees
                        base.Y += ecartCarteVisibleColonne
                        carteVue.Image.Location = base
                        carteVue.Image.BringToFront()
                    Next
                End If
                Sounds.playSoundClick()
            End If
        End Sub


        Public Sub CarteVue_MouseUp(pSender As Object, pE As MouseEventArgs)
            Try
                DeplacementCarte(pSender)
            Catch ex As Exception
                Sounds.playError()
                MessageBox.Show(ex.Message(), "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Public Sub CarteVue_MouseMove(pSender As Object, pE As MouseEventArgs)
            If pE.Button = Windows.Forms.MouseButtons.Left Then
                Dim point As Point = Control.MousePosition
                point.X = point.X - (cartesVues(pSender.Name).X)
                point.Y = point.Y - (cartesVues(pSender.Name).Y)
                pSender.Location = point
                If cartesVuesSelectionnees.Count > 0 Then
                    Dim base As Point = cartesVues(pSender.Name).Image.Location
                    For Each carteVue As CarteVue In cartesVuesSelectionnees
                        base.Y += ecartCarteVisibleColonne
                        carteVue.Image.Location = base
                    Next
                End If
            End If

        End Sub

        Public Sub carteVuePioche_Click(pSender As Object, pE As EventArgs)
            Dim t As Transition = New Transition(New TransitionType_Linear(tempsTransitionCarte))
            Dim action = Sub() AppelDelegueActivationCartesPiocheVisible(t)
            AddHandler t.TransitionCompletedEvent, action
            Try
                If controleur.VerifDeplacementCarteVersPiocheVisible(cartesVues(pSender.Name).Modele) Then
                    cartesVues(pSender.Name).DesactiverActionsNonVisiblePioche()
                    cartesVues(pSender.Name).Parent = PiochePanelVues
                    cartesVues(pSender.Name).LocalisationCourante = PiochePanelVues.Location
                    DesactiveToutesActionsPioches()
                    cartesVues(pSender.Name).Visible()
                    t.add(cartesVues(pSender.Name).Image, "Left", PiochePanelVues.Location.X)
                    t.run()
                    Sounds.playSoundDraw()
                End If
            Catch ex As Exception
                Sounds.playError()
                MessageBox.Show(ex.Message(), "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub

        Private Sub PiochePanelNonVues_Click(pSender As Object, pE As MouseEventArgs) Handles PiochePanelNonVues.MouseClick
            Try
                controleur.InversionPioches()
            Catch ex As Exception
                Sounds.playError()
                MessageBox.Show(ex.Message(), "", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End Sub


#End Region

    End Class
End Namespace

