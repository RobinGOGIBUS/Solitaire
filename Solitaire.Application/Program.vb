Option Explicit On
Option Strict On

Imports Solitaire.GUI.Vue
Imports System.Windows.Forms

Namespace App


    Public Class Program

        <STAThread()>
        Public Shared Sub Main()
            Application.EnableVisualStyles()
            Application.SetCompatibleTextRenderingDefault(False)
            Application.Run(New SolitaireForm)
        End Sub

    End Class
End Namespace