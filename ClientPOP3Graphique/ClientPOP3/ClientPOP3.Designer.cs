namespace ClientPOP3
{
    partial class ClientPOP3
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.listBoxAffichage = new System.Windows.Forms.ListBox();
            this.labelAffichage = new System.Windows.Forms.Label();
            this.listBoxVerbose = new System.Windows.Forms.ListBox();
            this.labelVerbose = new System.Windows.Forms.Label();
            this.buttonSTAT = new System.Windows.Forms.Button();
            this.buttonQUIT = new System.Windows.Forms.Button();
            this.buttonLIST = new System.Windows.Forms.Button();
            this.NudNumMail = new System.Windows.Forms.NumericUpDown();
            this.AfficherMessage = new System.Windows.Forms.Button();
            this.AfficherTsMessage = new System.Windows.Forms.Button();
            this.AfficherMessageEntier = new System.Windows.Forms.Button();
            this.MessageAmeliore = new System.Windows.Forms.Button();
            this.buttonDeco = new System.Windows.Forms.Button();
            this.buttonNettoyerListBox = new System.Windows.Forms.Button();
            this.MessageSimple = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.NudNumMail)).BeginInit();
            this.SuspendLayout();
            // 
            // listBoxAffichage
            // 
            this.listBoxAffichage.FormattingEnabled = true;
            this.listBoxAffichage.Location = new System.Drawing.Point(24, 38);
            this.listBoxAffichage.Margin = new System.Windows.Forms.Padding(2);
            this.listBoxAffichage.Name = "listBoxAffichage";
            this.listBoxAffichage.Size = new System.Drawing.Size(649, 433);
            this.listBoxAffichage.TabIndex = 0;
            this.listBoxAffichage.SelectedIndexChanged += new System.EventHandler(this.listBoxAffichage_SelectedIndexChanged);
            // 
            // labelAffichage
            // 
            this.labelAffichage.AutoSize = true;
            this.labelAffichage.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelAffichage.Location = new System.Drawing.Point(21, 13);
            this.labelAffichage.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelAffichage.Name = "labelAffichage";
            this.labelAffichage.Size = new System.Drawing.Size(199, 17);
            this.labelAffichage.TabIndex = 1;
            this.labelAffichage.Text = "Affichage pour l\'utilisateur";
            // 
            // listBoxVerbose
            // 
            this.listBoxVerbose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.listBoxVerbose.FormattingEnabled = true;
            this.listBoxVerbose.Location = new System.Drawing.Point(701, 38);
            this.listBoxVerbose.Margin = new System.Windows.Forms.Padding(2);
            this.listBoxVerbose.Name = "listBoxVerbose";
            this.listBoxVerbose.Size = new System.Drawing.Size(484, 433);
            this.listBoxVerbose.TabIndex = 2;
            // 
            // labelVerbose
            // 
            this.labelVerbose.AutoSize = true;
            this.labelVerbose.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelVerbose.Location = new System.Drawing.Point(699, 13);
            this.labelVerbose.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelVerbose.Name = "labelVerbose";
            this.labelVerbose.Size = new System.Drawing.Size(330, 17);
            this.labelVerbose.TabIndex = 3;
            this.labelVerbose.Text = "VERBOSE : Communication \"brute\" avec le serveur";
            // 
            // buttonSTAT
            // 
            this.buttonSTAT.Location = new System.Drawing.Point(974, 511);
            this.buttonSTAT.Margin = new System.Windows.Forms.Padding(2);
            this.buttonSTAT.Name = "buttonSTAT";
            this.buttonSTAT.Size = new System.Drawing.Size(77, 40);
            this.buttonSTAT.TabIndex = 4;
            this.buttonSTAT.Text = "STAT";
            this.buttonSTAT.UseVisualStyleBackColor = true;
            this.buttonSTAT.Click += new System.EventHandler(this.ButtonSTAT_Click);
            // 
            // buttonQUIT
            // 
            this.buttonQUIT.Location = new System.Drawing.Point(1098, 510);
            this.buttonQUIT.Margin = new System.Windows.Forms.Padding(2);
            this.buttonQUIT.Name = "buttonQUIT";
            this.buttonQUIT.Size = new System.Drawing.Size(77, 40);
            this.buttonQUIT.TabIndex = 5;
            this.buttonQUIT.Text = "QUIT";
            this.buttonQUIT.UseVisualStyleBackColor = true;
            this.buttonQUIT.Click += new System.EventHandler(this.ButtonQUIT_Click);
            // 
            // buttonLIST
            // 
            this.buttonLIST.Location = new System.Drawing.Point(846, 511);
            this.buttonLIST.Name = "buttonLIST";
            this.buttonLIST.Size = new System.Drawing.Size(77, 40);
            this.buttonLIST.TabIndex = 12;
            this.buttonLIST.Text = "LIST";
            this.buttonLIST.UseVisualStyleBackColor = true;
            this.buttonLIST.Click += new System.EventHandler(this.ButtonLIST_Click);
            // 
            // NudNumMail
            // 
            this.NudNumMail.Location = new System.Drawing.Point(68, 522);
            this.NudNumMail.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NudNumMail.Name = "NudNumMail";
            this.NudNumMail.Size = new System.Drawing.Size(120, 20);
            this.NudNumMail.TabIndex = 13;
            this.NudNumMail.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // AfficherMessage
            // 
            this.AfficherMessage.Location = new System.Drawing.Point(194, 489);
            this.AfficherMessage.Name = "AfficherMessage";
            this.AfficherMessage.Size = new System.Drawing.Size(108, 35);
            this.AfficherMessage.TabIndex = 14;
            this.AfficherMessage.Text = "Afficher entete message";
            this.AfficherMessage.UseVisualStyleBackColor = true;
            this.AfficherMessage.Click += new System.EventHandler(this.AfficherMessage_Click);
            // 
            // AfficherTsMessage
            // 
            this.AfficherTsMessage.Location = new System.Drawing.Point(716, 511);
            this.AfficherTsMessage.Name = "AfficherTsMessage";
            this.AfficherTsMessage.Size = new System.Drawing.Size(108, 39);
            this.AfficherTsMessage.TabIndex = 15;
            this.AfficherTsMessage.Text = "Afficher tous les messages ";
            this.AfficherTsMessage.UseVisualStyleBackColor = true;
            this.AfficherTsMessage.Click += new System.EventHandler(this.AfficherTsMessage_Click);
            // 
            // AfficherMessageEntier
            // 
            this.AfficherMessageEntier.Location = new System.Drawing.Point(194, 530);
            this.AfficherMessageEntier.Name = "AfficherMessageEntier";
            this.AfficherMessageEntier.Size = new System.Drawing.Size(108, 44);
            this.AfficherMessageEntier.TabIndex = 16;
            this.AfficherMessageEntier.Text = "Afficher le message en entier";
            this.AfficherMessageEntier.UseVisualStyleBackColor = true;
            this.AfficherMessageEntier.Click += new System.EventHandler(this.AfficherMessageEntier_Click);
            // 
            // MessageAmeliore
            // 
            this.MessageAmeliore.Location = new System.Drawing.Point(308, 489);
            this.MessageAmeliore.Name = "MessageAmeliore";
            this.MessageAmeliore.Size = new System.Drawing.Size(144, 35);
            this.MessageAmeliore.TabIndex = 17;
            this.MessageAmeliore.Text = "Afficher message amélioré";
            this.MessageAmeliore.UseVisualStyleBackColor = true;
            this.MessageAmeliore.Click += new System.EventHandler(this.MessageAmeliore_Click);
            // 
            // buttonDeco
            // 
            this.buttonDeco.Location = new System.Drawing.Point(1098, 568);
            this.buttonDeco.Name = "buttonDeco";
            this.buttonDeco.Size = new System.Drawing.Size(87, 23);
            this.buttonDeco.TabIndex = 18;
            this.buttonDeco.Text = "déconnexion";
            this.buttonDeco.UseVisualStyleBackColor = true;
            this.buttonDeco.Click += new System.EventHandler(this.buttonDeco_Click);
            // 
            // buttonNettoyerListBox
            // 
            this.buttonNettoyerListBox.Location = new System.Drawing.Point(974, 568);
            this.buttonNettoyerListBox.Name = "buttonNettoyerListBox";
            this.buttonNettoyerListBox.Size = new System.Drawing.Size(75, 23);
            this.buttonNettoyerListBox.TabIndex = 19;
            this.buttonNettoyerListBox.Text = "nettoyer";
            this.buttonNettoyerListBox.UseVisualStyleBackColor = true;
            this.buttonNettoyerListBox.Click += new System.EventHandler(this.buttonNettoyerListBox_Click);
            // 
            // MessageSimple
            // 
            this.MessageSimple.Location = new System.Drawing.Point(309, 531);
            this.MessageSimple.Name = "MessageSimple";
            this.MessageSimple.Size = new System.Drawing.Size(143, 43);
            this.MessageSimple.TabIndex = 20;
            this.MessageSimple.Text = "Afficher messge simple";
            this.MessageSimple.UseVisualStyleBackColor = true;
            this.MessageSimple.Click += new System.EventHandler(this.MessageSimple_Click);
            // 
            // ClientPOP3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1216, 603);
            this.Controls.Add(this.MessageSimple);
            this.Controls.Add(this.buttonNettoyerListBox);
            this.Controls.Add(this.buttonDeco);
            this.Controls.Add(this.MessageAmeliore);
            this.Controls.Add(this.AfficherMessageEntier);
            this.Controls.Add(this.AfficherTsMessage);
            this.Controls.Add(this.AfficherMessage);
            this.Controls.Add(this.NudNumMail);
            this.Controls.Add(this.buttonLIST);
            this.Controls.Add(this.buttonQUIT);
            this.Controls.Add(this.buttonSTAT);
            this.Controls.Add(this.labelVerbose);
            this.Controls.Add(this.listBoxVerbose);
            this.Controls.Add(this.labelAffichage);
            this.Controls.Add(this.listBoxAffichage);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ClientPOP3";
            this.Text = "ClientPOP3";
            ((System.ComponentModel.ISupportInitialize)(this.NudNumMail)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxAffichage;
        private System.Windows.Forms.Label labelAffichage;
        private System.Windows.Forms.ListBox listBoxVerbose;
        private System.Windows.Forms.Label labelVerbose;
        private System.Windows.Forms.Button buttonSTAT;
        private System.Windows.Forms.Button buttonQUIT;
        private System.Windows.Forms.Button buttonLIST;
        private System.Windows.Forms.NumericUpDown NudNumMail;
        private System.Windows.Forms.Button AfficherMessage;
        private System.Windows.Forms.Button AfficherTsMessage;
        private System.Windows.Forms.Button AfficherMessageEntier;
        private System.Windows.Forms.Button MessageAmeliore;
        private System.Windows.Forms.Button buttonDeco;
        private System.Windows.Forms.Button buttonNettoyerListBox;
        private System.Windows.Forms.Button MessageSimple;
    }
}

