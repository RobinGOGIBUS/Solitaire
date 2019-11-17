Option Explicit On
Option Strict On

Imports Solitaire.GUI.Vue
Imports System.Windows.Forms

Namespace App


    Public Class Program

        <STAThread()>
        Public Shared Sub Main()
            System.Windows.Forms.Application.EnableVisualStyles()
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(False)
            System.Windows.Forms.Application.Run(New SolitaireForm)
        End Sub

    End Class
End Namespace