Namespace Modele

    <Serializable()>
    Public Class Carte

#Region "Déclarations"

        Private _type As ETypes

        Private _valeur As EValeurs

        Private _couleur As ECouleurs

        Private _estVisible As Boolean

#End Region

#Region "Constructeurs"

        Public Sub New()

        End Sub

        Public Sub New(ByVal pType As ETypes, ByVal pValeur As EValeurs, ByVal pIsVisible As Boolean, ByVal pCouleur As ECouleurs)
            _type = pType
            _valeur = pValeur
            _estVisible = pIsVisible
            _couleur = pCouleur
        End Sub
#End Region

#Region "Propriétés"

        Public Property Type() As ETypes
            Get
                Return _type
            End Get
            Set(ByVal value As ETypes)
                _type = value
            End Set
        End Property

        Public Property Valeur() As EValeurs
            Get
                Return _valeur
            End Get
            Set(ByVal value As EValeurs)
                _valeur = value
            End Set
        End Property

        Public Property Couleur() As ECouleurs
            Get
                Return _couleur
            End Get
            Set(ByVal value As ECouleurs)
                _couleur = value
            End Set
        End Property


        Public Property EstVisible() As Boolean
            Get
                Return _estVisible
            End Get
            Set(ByVal value As Boolean)
                _estVisible = value
            End Set
        End Property
#End Region

    End Class
End Namespace
