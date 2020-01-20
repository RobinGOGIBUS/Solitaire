Option Explicit On
Option Strict On

Imports Solitaire.GUI.Vue
Imports System.Threading
Imports System.Windows.Forms

Namespace App


    Public Class Program

        Public Shared Sub Main()

            Const nomApp As String = "Solitaire"
            Dim mutex As Mutex = New Mutex(True, nomApp)

            If Not mutex.WaitOne(0, False) Then
                mutex.Close()
                MessageBox.Show("Une instance de l'application est déjà ouverte.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Environment.Exit(0)
            Else
                Application.EnableVisualStyles()
                Application.SetCompatibleTextRenderingDefault(False)
                Application.Run(New SolitaireForm)
                mutex.ReleaseMutex()
            End If
        End Sub

    End Class
End Namespace