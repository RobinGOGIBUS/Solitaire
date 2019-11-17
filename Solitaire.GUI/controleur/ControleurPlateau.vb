Imports Solitaire.GUI.Vue
Imports Solitaire.Modele
Imports Solitaire.Modele.Modele

Namespace Controleur
    Public Class ControleurPlateau
#Region "Déclarations"

        Private _modele As Plateau

        Private _vue As PlateauUserControl

#End Region

#Region "Constructeur"
        Public Sub New(ByRef pModele As Plateau, ByRef pVue As PlateauUserControl)
            _modele = pModele
            _vue = pVue
        End Sub

#End Region

#Region "Méthodes"
        Public Function VerifDeplacementCarteVersPiocheVisible(ByVal pCarte As Carte) As Boolean
            Dim valide As Boolean = True
            If Not _modele.DeplacementCarteVersPiocheVisible(pCarte) Then
                valide = False
                Throw New Exception("Un problème a eu lieu : La partie logique et la vue ne correspondent pas")
            End If
            Return valide
        End Function

        Public Sub InversionPioches()
            If _modele.InversionPioches() Then
                _vue.AfficheInversionPioches()
            End If
        End Sub

        Public Sub DeplacementVersPile(ByRef pCarte As CarteVue, ByVal pParentType As EParent, ByVal pIndiceD As Integer, Optional ByVal pIndiceE As Integer = 0)
            If _modele.DeplacementVersPile(pCarte.Modele, pParentType, pIndiceD, pIndiceE) Then
                _vue.MiseAjourCarteVueValide(pCarte, EParent.Pile, pIndiceD, _modele.VerificationVictoire())
            Else
                _vue.MiseAjourCarteVueNonValide(pCarte)
            End If
        End Sub

        Public Sub DeplacementVersColonne(ByRef pCarte As CarteVue, ByVal pParentType As EParent, ByVal pIndiceE As Integer, ByVal pIndiceD As Integer)
            If _modele.DeplacementVersColonne(pCarte.Modele, pParentType, pIndiceD, pIndiceE) Then
                _vue.MiseAjourCarteVueValide(pCarte, EParent.Colonne, pIndiceD)
            Else
                _vue.MiseAjourCarteVueNonValide(pCarte)
            End If
        End Sub

#End Region

    End Class
End Namespace

