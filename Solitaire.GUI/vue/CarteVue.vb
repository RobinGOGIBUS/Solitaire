Imports Solitaire.Modele
Imports Solitaire.Modele.Modele

Namespace Vue
    Public Class CarteVue

#Region "Declarations"

        Private _image As PictureBox

        Private _parent As Panel

        Private _modele As Carte

        Private _fondImage As Image

        Private _x, _y As Integer

        Private _point As Point

        Private _vuePlateau As PlateauUserControl

        Private _localisationCourante As Point

        Private _parentType As EParent

        Private _isVisible As Boolean

#End Region

#Region "Constructeur"
        Public Sub New(ByVal pParent As Panel, ByRef pImage As PictureBox, ByRef pModele As Carte, ByRef pVuePlateau As PlateauUserControl)
            _image = pImage
            _parent = pParent
            _modele = pModele
            _vuePlateau = pVuePlateau
            Initialisation()
        End Sub


#End Region

#Region "Propriétés"

        Public ReadOnly Property IsVisible() As Boolean
            Get
                Return _isVisible
            End Get
        End Property

        Public Property Parent() As Panel
            Get
                Return _parent
            End Get
            Set(ByVal value As Panel)
                _parent = value
            End Set
        End Property

        Public Property Image() As PictureBox
            Get
                Return _image
            End Get
            Set(ByVal value As PictureBox)
                _image = value
            End Set
        End Property

        Public ReadOnly Property Modele() As Carte
            Get
                Return _modele
            End Get
        End Property

        Public Property X() As Integer
            Get
                Return _x
            End Get
            Set(ByVal value As Integer)
                _x = value
            End Set
        End Property

        Public Property Y() As Integer
            Get
                Return _y
            End Get
            Set(ByVal value As Integer)
                _y = value
            End Set
        End Property

        Public Property LocalisationCourante() As Point
            Get
                Return _localisationCourante
            End Get
            Set(ByVal value As Point)
                _localisationCourante = value
            End Set
        End Property

        Public Property ParentType() As EParent
            Get
                Return _parentType
            End Get
            Set(ByVal value As EParent)
                _parentType = value
            End Set
        End Property

#End Region

#Region "Méthodes"

        Public Sub Visible()
            _image.BackgroundImage = _fondImage
            _image.BringToFront()
            _isVisible = True
        End Sub

        Public Sub ActiverActionsVisible()
            AddHandler _image.MouseDown, AddressOf _vuePlateau.CarteVue_MouseDown
            AddHandler _image.MouseUp, AddressOf _vuePlateau.CarteVue_MouseUp
            AddHandler _image.MouseMove, AddressOf _vuePlateau.CarteVue_MouseMove
            DesactiverActionsNonVisiblePioche()
        End Sub

        Public Sub ActiverActionsNonVisiblePioche()
            DesactiverActionsVisible()
            AddHandler _image.MouseClick, AddressOf _vuePlateau.carteVuePioche_Click
        End Sub

        Public Sub DesactiverActionsVisible()
            RemoveHandler _image.MouseDown, AddressOf _vuePlateau.CarteVue_MouseDown
            RemoveHandler _image.MouseUp, AddressOf _vuePlateau.CarteVue_MouseUp
            RemoveHandler _image.MouseMove, AddressOf _vuePlateau.CarteVue_MouseMove
        End Sub

        Public Sub DesactiverActionsNonVisiblePioche()
            RemoveHandler _image.MouseClick, AddressOf _vuePlateau.carteVuePioche_Click
        End Sub

        Public Sub NonVisiblePioche()
            _image.BackgroundImage = My.Resources.Fond
            _image.BringToFront()
            _isVisible = False
        End Sub

        Public Sub NonVisible()
            _image.BackgroundImage = My.Resources.Fond
            _image.BringToFront()
            _isVisible = False
            RemoveHandler _image.MouseDown, AddressOf _vuePlateau.CarteVue_MouseDown
            RemoveHandler _image.MouseUp, AddressOf _vuePlateau.CarteVue_MouseUp
            RemoveHandler _image.MouseMove, AddressOf _vuePlateau.CarteVue_MouseMove
        End Sub

        Public Sub Initialisation()
            _fondImage = _vuePlateau.imageListeCartes.Images(IndexCarte())
            _localisationCourante = _image.Location
            If _modele.IsVisible Then
                Visible()
                ActiverActionsVisible()
            Else
                If _parent.Equals(_vuePlateau.PiochePanelNonVues) Then
                    NonVisiblePioche()
                    ActiverActionsNonVisiblePioche()
                Else
                    NonVisible()
                End If
            End If
            _image.BackgroundImageLayout = ImageLayout.Stretch
        End Sub

        Private Function IndexCarte() As Integer
            Dim index As Integer
            If _modele.Type = 0 Then
                index = _modele.Valeur - 1
            Else
                index = (_modele.Valeur - 1) + 13 * _modele.Type
            End If
            Return index
        End Function

        Public Sub DesactiverActions()
            RemoveHandler _image.MouseDown, AddressOf _vuePlateau.CarteVue_MouseDown
            RemoveHandler _image.MouseUp, AddressOf _vuePlateau.CarteVue_MouseUp
            RemoveHandler _image.MouseMove, AddressOf _vuePlateau.CarteVue_MouseMove
            RemoveHandler _image.MouseClick, AddressOf _vuePlateau.carteVuePioche_Click
        End Sub


#End Region


    End Class

End Namespace
