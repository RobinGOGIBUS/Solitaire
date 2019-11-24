
Imports System.IO
Imports System.Xml.Serialization
Imports Solitaire.GUI.Vue

Namespace Controleur
    Public Class ControleurSolitaire
#Region "Déclarations"

        Private _modele As Solitaire.Modele.Modele.Solitaire

        Private _vue As SolitaireForm

        Private sauvegardeCourante As String = Nothing

#End Region

#Region "Constructeur"
        Public Sub New(ByRef pModele As Solitaire.Modele.Modele.Solitaire, ByRef pVue As SolitaireForm)
            _modele = pModele
            _vue = pVue
        End Sub
#End Region

#Region "Méthodes"
        Public Sub SauverPartieSous(ByVal pNomFichier As String)
            sauvegardeCourante = pNomFichier
            Dim xs As XmlSerializer = New XmlSerializer(GetType(Solitaire.Modele.Modele.Solitaire))
            Using rd As StreamWriter = New StreamWriter(sauvegardeCourante)
                xs.Serialize(rd, _modele)
            End Using
        End Sub

        Public Sub ChargerLeJeu(ByVal pNomFichier As String)
            Dim xs As XmlSerializer = New XmlSerializer(GetType(Solitaire.Modele.Modele.Solitaire))
            Using rd As StreamReader = New StreamReader(sauvegardeCourante)
                _modele = DirectCast(xs.Deserialize(rd), Solitaire.Modele.Modele.Solitaire)
                _vue.ChargerVueJeu()
            End Using
        End Sub

#End Region

    End Class

End Namespace
