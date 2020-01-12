Imports Solitaire.Modele.Modele.Collections.StackList

Namespace Modele

    <Serializable()>
    Public Class Plateau

#Region "Constantes"
        Private Const _nbMaxValues As Integer = 13
#End Region

#Region "Déclarations"

        Private _colonnes As List(Of List(Of Carte))

        Private _piles As List(Of StackList(Of Carte))

        Private _piocheCartesVisibles As StackList(Of Carte)

        Private _piocheCartesNonVisibles As StackList(Of Carte)


#End Region

#Region "Constructeur"

        Public Sub New()

        End Sub

#End Region

#Region "Propriétés"

        Public Property PiochecartesNonVisibles() As StackList(Of Carte)
            Get
                Return _piocheCartesNonVisibles
            End Get
            Set(ByVal value As StackList(Of Carte))
                _piocheCartesNonVisibles = value
            End Set
        End Property

        Public Property PiocheCartesVisibles() As StackList(Of Carte)
            Get
                Return _piocheCartesVisibles
            End Get
            Set(ByVal value As StackList(Of Carte))
                _piocheCartesVisibles = value
            End Set
        End Property

        Public Property Piles() As List(Of StackList(Of Carte))
            Get
                Return _piles
            End Get
            Set(ByVal value As List(Of StackList(Of Carte)))
                _piles = value
            End Set
        End Property

        Public Property Colonnes() As List(Of List(Of Carte))
            Get
                Return _colonnes
            End Get
            Set(ByVal value As List(Of List(Of Carte)))
                _colonnes = value
            End Set
        End Property

#End Region

#Region "Méthodes"
        Public Sub Initialisation()
            _piocheCartesVisibles = New StackList(Of Carte)
            InitJeuDeCartes()
            InitColonnes()
            InitPiles()
        End Sub

        Public Sub InitPiles()
            _piles = New List(Of StackList(Of Carte))
            For i As Integer = 0 To 3
                _piles.Add(New StackList(Of Carte))
            Next
        End Sub

        Public Sub InitColonnes()
            _colonnes = New List(Of List(Of Carte))
            For i As Integer = 0 To 6
                _colonnes.Add(New List(Of Carte))
                For j As Integer = 0 To i
                    If i <> 0 AndAlso j < i Then
                        _colonnes(i).Add(_piocheCartesNonVisibles.Pop())
                    End If
                Next
                _piocheCartesNonVisibles.Peek().EstVisible = True
                _colonnes(i).Add(_piocheCartesNonVisibles.Pop())
            Next
        End Sub

        Public Sub BrassageJeuDeCartes(ByRef pJeuDeCartes As List(Of Carte))
            Dim jeuDeCartes As List(Of Carte) = pJeuDeCartes
            Dim r As Random = New Random()
            For i As Integer = 0 To jeuDeCartes.Count - 1
                Dim index As Integer = r.Next(i, jeuDeCartes.Count)
                If index <> i Then
                    Dim e As Carte = jeuDeCartes(i)
                    jeuDeCartes(i) = jeuDeCartes(index)
                    jeuDeCartes(index) = e
                End If
            Next

            ' affectation des valeurs à la pioche
            For Each carte As Carte In jeuDeCartes
                _piocheCartesNonVisibles.Push(carte)
            Next


        End Sub

        Public Sub InitJeuDeCartes()
            _piocheCartesNonVisibles = New StackList(Of Carte)
            Dim jeuDeCartes As List(Of Carte) = New List(Of Carte)
            Dim types As Array = System.Enum.GetValues(GetType(ETypes))
            Dim valeurs As Array = System.Enum.GetValues(GetType(EValeurs))
            For Each type As Integer In types
                For Each valeur As Integer In valeurs
                    If type = ETypes.Pique Or type = ETypes.Trefle Then
                        jeuDeCartes.Add(New Carte(type, valeur, False, ECouleurs.Noir))
                    Else
                        jeuDeCartes.Add(New Carte(type, valeur, False, ECouleurs.Rouge))
                    End If
                Next
            Next
            BrassageJeuDeCartes(jeuDeCartes)
        End Sub

        Public Function DeplacementCarteVersPiocheVisible(ByVal pCarte As Carte) As Boolean
            Dim valide As Boolean = False
            Dim val = _piocheCartesNonVisibles.Peek()
            If Equals(pCarte, _piocheCartesNonVisibles.Peek()) Then
                valide = True
                _piocheCartesNonVisibles.Peek().EstVisible = True
                _piocheCartesVisibles.Push(_piocheCartesNonVisibles.Pop())
            End If
            Return valide
        End Function

        Public Function InversionPioches() As Boolean
            Dim inversion As Boolean = False
            If _piocheCartesVisibles.Count > 0 And _piocheCartesNonVisibles.Count = 0 Then
                For i As Integer = 0 To _piocheCartesVisibles.Count - 1
                    _piocheCartesVisibles.Peek().EstVisible = False
                    _piocheCartesNonVisibles.Push(_piocheCartesVisibles.Pop())
                Next
                inversion = True
            End If
            Return inversion
        End Function

        Public Function DeplacementVersPile(ByRef pCarte As Carte, ByVal pParentType As EParent, ByVal pIndiceD As Integer, Optional ByVal pIndiceE As Integer = 0) As Boolean
            Dim ok As Boolean = False
            Select Case pParentType
                Case EParent.Pile
                    ok = DeplacementCartePileVersPile(pCarte, pIndiceD, pIndiceE)
                Case EParent.Pioche
                    ok = DeplacementCartePiocheVersPile(pCarte, pIndiceD)
                Case EParent.Colonne
                    ok = DeplacementCarteColonneVersPile(pCarte, pIndiceD, pIndiceE)
            End Select
            Return ok
        End Function

        Public Function DeplacementCarteColonneVersPile(ByRef pCarte As Carte, ByVal pIndiceD As Integer, ByVal pIndiceE As Integer) As Boolean
            Dim cartesSelectionnees As Boolean = VerificationCartesSelectionnees(pCarte, pIndiceE)
            If _piles(pIndiceD).Count > 0 Then
                Dim SommetPile As Carte = _piles(pIndiceD).Peek()
                If Not cartesSelectionnees AndAlso (pCarte.Valeur = SommetPile.Valeur + 1) AndAlso (pCarte.Type = SommetPile.Type) Then
                    _piles(pIndiceD).Push(_colonnes(pIndiceE).LastOrDefault())
                    _colonnes(pIndiceE).Remove(pCarte)
                    If _colonnes(pIndiceE).Count > 0 AndAlso Not _colonnes(pIndiceE).LastOrDefault().EstVisible Then
                        _colonnes(pIndiceE).LastOrDefault().EstVisible = True
                    End If
                    Return True
                Else
                    Return False
                End If
            Else
                If Not cartesSelectionnees AndAlso pCarte.Valeur = EValeurs.Un Then
                    _piles(pIndiceD).Push(_colonnes(pIndiceE).LastOrDefault())
                    _colonnes(pIndiceE).Remove(pCarte)
                    If _colonnes(pIndiceE).Count > 0 AndAlso Not _colonnes(pIndiceE).LastOrDefault().EstVisible Then
                        _colonnes(pIndiceE).LastOrDefault().EstVisible = True
                    End If
                    Return True
                Else
                    Return False
                End If
            End If
        End Function

        Public Function DeplacementCartePiocheVersPile(ByRef pCarte As Carte, ByVal pIndiceD As Integer) As Boolean
            If _piles(pIndiceD).Count > 0 Then
                Dim SommetPile As Carte = _piles(pIndiceD).Peek()
                If (pCarte.Valeur = SommetPile.Valeur + 1) AndAlso (pCarte.Type = SommetPile.Type) Then
                    _piles(pIndiceD).Push(_piocheCartesVisibles.Pop())
                    Return True
                Else
                    Return False
                End If
            Else
                If pCarte.Valeur = EValeurs.Un Then
                    _piles(pIndiceD).Push(_piocheCartesVisibles.Pop())
                    Return True
                Else
                    Return False
                End If
            End If
        End Function

        Public Function DeplacementCartePileVersPile(ByRef pCarte As Carte, ByVal pIndiceD As Integer, ByVal pIndiceE As Integer) As Boolean
            If _piles(pIndiceD).Count = 0 AndAlso pCarte.Valeur = EValeurs.Un Then
                _piles(pIndiceD).Push(_piles(pIndiceE).Pop())
                Return True
            Else
                Return False
            End If
        End Function

        Public Function DeplacementVersColonne(ByRef pCarte As Carte, ByVal pParentType As EParent, ByVal pIndiceD As Integer, Optional ByVal pIndiceE As Integer = 0) As Boolean
            Dim ok As Boolean = False
            Select Case pParentType
                Case EParent.Pile
                    ok = DeplacementCartePileVersColonne(pCarte, pIndiceD, pIndiceE)
                Case EParent.Pioche
                    ok = DeplacementCartePiocheVersColonne(pCarte, pIndiceD)
                Case EParent.Colonne
                    ok = DeplacementCarteColonneVersColonne(pCarte, pIndiceD, pIndiceE)
            End Select
            Return ok
        End Function

        Public Function DeplacementCartePileVersColonne(ByRef pCarte As Carte, ByVal pIndiceD As Integer, ByVal pIndiceE As Integer) As Boolean
            If _colonnes(pIndiceD).Count = 0 Then
                If pCarte.Valeur = EValeurs.Roi Then
                    _colonnes(pIndiceD).Add(_piles(pIndiceE).Pop())
                    Return True
                Else
                    Return False
                End If
            Else
                If pCarte.Valeur = _colonnes(pIndiceD).LastOrDefault().Valeur - 1 AndAlso Not pCarte.Couleur = _colonnes(pIndiceD).LastOrDefault().Couleur Then
                    _colonnes(pIndiceD).Add(_piles(pIndiceE).Pop())
                    Return True
                Else
                    Return False
                End If
            End If
        End Function

        Public Function DeplacementCartePiocheVersColonne(ByRef pCarte As Carte, ByVal pIndiceD As Integer) As Boolean
            If _colonnes(pIndiceD).Count = 0 Then
                If pCarte.Valeur = EValeurs.Roi Then
                    _colonnes(pIndiceD).Add(_piocheCartesVisibles.Pop())
                    Return True
                Else
                    Return False
                End If
            Else
                If pCarte.Valeur = _colonnes(pIndiceD).LastOrDefault().Valeur - 1 AndAlso Not pCarte.Couleur = _colonnes(pIndiceD).LastOrDefault().Couleur Then
                    _colonnes(pIndiceD).Add(_piocheCartesVisibles.Pop())
                    Return True
                Else
                    Return False
                End If
            End If
        End Function

        Public Function DeplacementCarteColonneVersColonne(ByRef pCarte As Carte, ByVal pIndiceD As Integer, ByVal pIndiceE As Integer) As Boolean
            Dim cartesSelectionnees As Boolean = VerificationCartesSelectionnees(pCarte, pIndiceE)
            Dim indiceCarte As Integer = _colonnes(pIndiceE).IndexOf(pCarte)
            Dim ok As Boolean = False
            If _colonnes(pIndiceD).Count = 0 Then
                If pCarte.Valeur = EValeurs.Roi Then
                    ok = True
                End If
            Else
                If pCarte.Valeur = _colonnes(pIndiceD).LastOrDefault().Valeur - 1 AndAlso Not pCarte.Couleur = _colonnes(pIndiceD).LastOrDefault().Couleur Then
                    ok = True
                End If
            End If
            If ok Then
                If cartesSelectionnees Then
                    For Each carte As Carte In _colonnes(pIndiceE).FindAll(Function(c As Carte) Colonnes(pIndiceE).IndexOf(c) >= indiceCarte)
                        _colonnes(pIndiceD).Add(carte)
                        _colonnes(pIndiceE).Remove(carte)
                    Next
                Else
                    _colonnes(pIndiceD).Add(_colonnes(pIndiceE)(indiceCarte))
                    _colonnes(pIndiceE).Remove(_colonnes(pIndiceE)(indiceCarte))
                End If
                If _colonnes(pIndiceE).Count > 0 AndAlso Not _colonnes(pIndiceE).LastOrDefault().EstVisible Then
                    _colonnes(pIndiceE).LastOrDefault().EstVisible = True
                End If
            End If
            Return ok
        End Function

        Public Function VerificationCartesSelectionnees(ByRef pCarte As Carte, pIndiceColonne As Integer) As Boolean
            Dim IndexCarteSelectionnee As Integer = _colonnes(pIndiceColonne).IndexOf(pCarte)
            Return _colonnes(pIndiceColonne).Count - 1 > IndexCarteSelectionnee
        End Function

        Public Function GetCartesSelectionnees(ByRef pCarte As Carte, pIndiceColonne As Integer) As List(Of Carte)
            Dim IndexCarteSelectionnee As Integer = _colonnes(pIndiceColonne).IndexOf(pCarte)
            Dim cartesSelectionnees As List(Of Carte) = _colonnes(pIndiceColonne).FindAll(Function(carte As Carte)
                                                                                              Return _colonnes(pIndiceColonne).IndexOf(carte) > IndexCarteSelectionnee
                                                                                          End Function).ToList
            Return cartesSelectionnees
        End Function

        Public Function VerificationVictoire() As Boolean
            Dim ok As Boolean = True
            For Each pile In _piles
                If pile.Count < _nbMaxValues Then
                    ok = False
                    Exit For
                End If
            Next
            If Not _piocheCartesNonVisibles.Any() And Not _piocheCartesVisibles.Any() And _colonnes.ToArray.Length = 0 Then
                ok = ok And True
            End If
            Return ok
        End Function

#End Region

    End Class
End Namespace
