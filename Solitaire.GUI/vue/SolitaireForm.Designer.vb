Namespace Vue
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
    Partial Class SolitaireForm
        Inherits System.Windows.Forms.Form

        'Form remplace la méthode Dispose pour nettoyer la liste des composants.
        <System.Diagnostics.DebuggerNonUserCode()>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            Try
                If disposing AndAlso components IsNot Nothing Then
                    components.Dispose()
                End If
            Finally
                MyBase.Dispose(disposing)
            End Try
        End Sub

        'Requise par le Concepteur Windows Form
        Private components As System.ComponentModel.IContainer

        'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
        'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
        'Ne la modifiez pas à l'aide de l'éditeur de code.
        <System.Diagnostics.DebuggerStepThrough()>
        Private Sub InitializeComponent()
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SolitaireForm))
            Me.MenuSolitaire = New System.Windows.Forms.MenuStrip()
            Me.JeuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.MenuItemNouveauJeu = New System.Windows.Forms.ToolStripMenuItem()
            Me.SauverPartieSousToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.SauverLaPartieToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.ChargerLeJeuToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
            Me.MenuItemQuit = New System.Windows.Forms.ToolStripMenuItem()
            Me.PlateauPanel = New System.Windows.Forms.Panel()
            Me.MenuSolitaire.SuspendLayout()
            Me.SuspendLayout()
            '
            'MenuSolitaire
            '
            Me.MenuSolitaire.AutoSize = False
            Me.MenuSolitaire.BackColor = System.Drawing.Color.WhiteSmoke
            Me.MenuSolitaire.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.JeuToolStripMenuItem})
            Me.MenuSolitaire.Location = New System.Drawing.Point(0, 0)
            Me.MenuSolitaire.Name = "MenuSolitaire"
            Me.MenuSolitaire.Size = New System.Drawing.Size(1264, 24)
            Me.MenuSolitaire.TabIndex = 2
            Me.MenuSolitaire.Text = "MenuStrip1"
            '
            'JeuToolStripMenuItem
            '
            Me.JeuToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MenuItemNouveauJeu, Me.SauverPartieSousToolStripMenuItem, Me.SauverLaPartieToolStripMenuItem, Me.ChargerLeJeuToolStripMenuItem, Me.MenuItemQuit})
            Me.JeuToolStripMenuItem.Name = "JeuToolStripMenuItem"
            Me.JeuToolStripMenuItem.Size = New System.Drawing.Size(36, 20)
            Me.JeuToolStripMenuItem.Text = "Jeu"
            '
            'MenuItemNouveauJeu
            '
            Me.MenuItemNouveauJeu.Name = "MenuItemNouveauJeu"
            Me.MenuItemNouveauJeu.Size = New System.Drawing.Size(190, 22)
            Me.MenuItemNouveauJeu.Text = "Nouveau Jeu"
            '
            'SauverPartieSousToolStripMenuItem
            '
            Me.SauverPartieSousToolStripMenuItem.Name = "SauverPartieSousToolStripMenuItem"
            Me.SauverPartieSousToolStripMenuItem.Size = New System.Drawing.Size(190, 22)
            Me.SauverPartieSousToolStripMenuItem.Text = "Sauver la partie sous..."
            '
            'SauverLaPartieToolStripMenuItem
            '
            Me.SauverLaPartieToolStripMenuItem.Enabled = False
            Me.SauverLaPartieToolStripMenuItem.Name = "SauverLaPartieToolStripMenuItem"
            Me.SauverLaPartieToolStripMenuItem.Size = New System.Drawing.Size(190, 22)
            Me.SauverLaPartieToolStripMenuItem.Text = "Sauver la partie"
            '
            'ChargerLeJeuToolStripMenuItem
            '
            Me.ChargerLeJeuToolStripMenuItem.Name = "ChargerLeJeuToolStripMenuItem"
            Me.ChargerLeJeuToolStripMenuItem.Size = New System.Drawing.Size(190, 22)
            Me.ChargerLeJeuToolStripMenuItem.Text = "Charger le Jeu"
            '
            'MenuItemQuit
            '
            Me.MenuItemQuit.Name = "MenuItemQuit"
            Me.MenuItemQuit.Size = New System.Drawing.Size(190, 22)
            Me.MenuItemQuit.Text = "Quitter le Jeu"
            '
            'PlateauPanel
            '
            Me.PlateauPanel.AutoSize = True
            Me.PlateauPanel.Dock = System.Windows.Forms.DockStyle.Fill
            Me.PlateauPanel.Location = New System.Drawing.Point(0, 24)
            Me.PlateauPanel.MinimumSize = New System.Drawing.Size(1264, 750)
            Me.PlateauPanel.Name = "PlateauPanel"
            Me.PlateauPanel.Size = New System.Drawing.Size(1264, 757)
            Me.PlateauPanel.TabIndex = 3
            '
            'SolitaireForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(1264, 781)
            Me.Controls.Add(Me.PlateauPanel)
            Me.Controls.Add(Me.MenuSolitaire)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.MainMenuStrip = Me.MenuSolitaire
            Me.MaximumSize = New System.Drawing.Size(1920, 1080)
            Me.MinimumSize = New System.Drawing.Size(1280, 820)
            Me.Name = "SolitaireForm"
            Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
            Me.Text = "Solitaire"
            Me.MenuSolitaire.ResumeLayout(False)
            Me.MenuSolitaire.PerformLayout()
            Me.ResumeLayout(False)
            Me.PerformLayout()

        End Sub
        Friend WithEvents MenuSolitaire As MenuStrip
        Friend WithEvents JeuToolStripMenuItem As ToolStripMenuItem
        Friend WithEvents MenuItemNouveauJeu As ToolStripMenuItem
        Friend WithEvents SauverLaPartieToolStripMenuItem As ToolStripMenuItem
        Friend WithEvents ChargerLeJeuToolStripMenuItem As ToolStripMenuItem
        Friend WithEvents MenuItemQuit As ToolStripMenuItem
        Friend WithEvents PlateauPanel As Panel
        Friend WithEvents SauverPartieSousToolStripMenuItem As ToolStripMenuItem
    End Class
End Namespace
