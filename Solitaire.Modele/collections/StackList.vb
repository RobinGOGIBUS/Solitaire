Namespace Modele.Collections
    Public Class StackList
        Public Class StackList(Of T)
            Inherits List(Of T)
#Region "Méthodes"

            Public Sub Push(ByRef pItem As T)
                Add(pItem)
            End Sub

            Public Function Pop() As T
                If Any() Then
                    Dim item As T = MyBase.LastOrDefault()
                    Remove(item)
                    Return item
                Else
                    Return CType(Nothing, T)
                End If
            End Function

            Public Function Peek() As T
                If Any() Then
                    Return MyBase.LastOrDefault()
                Else
                    Return CType(Nothing, T)
                End If
            End Function

#End Region
        End Class
    End Class
End Namespace

