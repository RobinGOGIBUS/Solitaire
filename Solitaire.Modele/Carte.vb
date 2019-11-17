Namespace Modele
    Public Class Carte

#Region "Déclarations"

        Private _type As ETypes

        Private _valeur As EValeurs

        Private _couleur As ECouleurs

        Private _isVisible As Boolean

#End Region

#Region "Constructeur"
        Public Sub New(ByVal pType As ETypes, ByVal pValeur As EValeurs, ByVal pIsVisible As Boolean, ByVal pCouleur As ECouleurs)
            _type = pType
            _valeur = pValeur
            _isVisible = pIsVisible
            _couleur = pCouleur
        End Sub
#End Region

#Region "Propriétés"

        Public ReadOnly Property Type() As ETypes
            Get
                Return _type
            End Get
        End Property

        Public ReadOnly Property Valeur() As EValeurs
            Get
                Return _valeur
            End Get
        End Property

        Public ReadOnly Property Couleur() As ECouleurs
            Get
                Return _couleur
            End Get
        End Property


        Public Property IsVisible() As Boolean
            Get
                Return _isVisible
            End Get
            Set(ByVal value As Boolean)
                _isVisible = value
            End Set
        End Property
#End Region

    End Class
End Namespace
