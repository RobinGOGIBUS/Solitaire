Imports System.Xml.Serialization

Namespace Modele

    <Serializable()>
    Public Class Solitaire

#Region "Declarations"

        Private _plateau As Plateau = Nothing

        Private _partieEnCours As Boolean

#End Region

#Region "Constructeur"
        Public Sub New()
            _partieEnCours = False
        End Sub
#End Region

#Region "Propriétés"

        Public Property Plateau As Plateau
            Get
                Return _plateau
            End Get
            Set(ByVal value As Plateau)
                _plateau = value
            End Set
        End Property

        Public Property PartieEnCours As Boolean
            Get
                Return _partieEnCours
            End Get
            Set(ByVal value As Boolean)
                _partieEnCours = value
            End Set
        End Property


#End Region

#Region "Méthodes"

        Public Sub NouveauJeu()
            _partieEnCours = True
            _plateau = New Plateau()
            _plateau.Initialisation()
        End Sub
#End Region

    End Class
End Namespace

