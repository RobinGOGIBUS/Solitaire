Namespace Modele
    Public Class Solitaire

#Region "Declarations"
        Private _plateau As Plateau = Nothing

        Private jeu As Solitaire = Nothing

        Private instance As Solitaire = Nothing
#End Region

#Region "Constructeur"
        Public Sub New()

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


#End Region

#Region "Méthodes"

        Public Sub NouveauJeu()
            _plateau = New Plateau()
        End Sub
#End Region

    End Class
End Namespace

