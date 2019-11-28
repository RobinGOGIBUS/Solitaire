Option Explicit On
Option Strict On

Imports Solitaire.GUI.Vue
Imports System.Threading
Imports System.Windows.Forms

Namespace App


    Public Class Program

        Public Shared Sub Main()

            Dim mutex As Mutex = New Mutex(False, "solitaire")
            If Not mutex.WaitOne(0, False) Then
                mutex.Close()
                MessageBox.Show("Une instance de l'application est déjà ouverte.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                End
            End If
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New SolitaireForm)
        End Sub

    End Class
End Namespace