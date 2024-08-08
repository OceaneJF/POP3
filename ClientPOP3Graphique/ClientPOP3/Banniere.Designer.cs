namespace ClientPOP3
{
    partial class Banniere
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textMachine = new System.Windows.Forms.TextBox();
            this.textIdentifiant = new System.Windows.Forms.TextBox();
            this.textMdp = new System.Windows.Forms.TextBox();
            this.buttonConnexion = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Machine";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 137);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Identifiant";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 174);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Mot de passe";
            // 
            // textMachine
            // 
            this.textMachine.Location = new System.Drawing.Point(124, 97);
            this.textMachine.Name = "textMachine";
            this.textMachine.Size = new System.Drawing.Size(208, 20);
            this.textMachine.TabIndex = 3;
            this.textMachine.Text = "pop.laposte.net";
            this.textMachine.TextChanged += new System.EventHandler(this.TextOnChange);
            // 
            // textIdentifiant
            // 
            this.textIdentifiant.Location = new System.Drawing.Point(124, 134);
            this.textIdentifiant.Name = "textIdentifiant";
            this.textIdentifiant.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textIdentifiant.Size = new System.Drawing.Size(208, 20);
            this.textIdentifiant.TabIndex = 4;
            this.textIdentifiant.Text = "iutinfo";
            this.textIdentifiant.TextChanged += new System.EventHandler(this.TextOnChange);
            // 
            // textMdp
            // 
            this.textMdp.Location = new System.Drawing.Point(123, 173);
            this.textMdp.Name = "textMdp";
            this.textMdp.Size = new System.Drawing.Size(209, 20);
            this.textMdp.TabIndex = 5;
            this.textMdp.Text = "Iutinfo2022!";
            this.textMdp.TextChanged += new System.EventHandler(this.TextOnChange);
            // 
            // buttonConnexion
            // 
            this.buttonConnexion.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonConnexion.Location = new System.Drawing.Point(713, 415);
            this.buttonConnexion.Name = "buttonConnexion";
            this.buttonConnexion.Size = new System.Drawing.Size(75, 23);
            this.buttonConnexion.TabIndex = 6;
            this.buttonConnexion.Text = "ok";
            this.buttonConnexion.UseVisualStyleBackColor = true;
            // 
            // Banniere
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonConnexion);
            this.Controls.Add(this.textMdp);
            this.Controls.Add(this.textIdentifiant);
            this.Controls.Add(this.textMachine);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Banniere";
            this.Text = "Banniere";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textMachine;
        private System.Windows.Forms.TextBox textIdentifiant;
        private System.Windows.Forms.TextBox textMdp;
        private System.Windows.Forms.Button buttonConnexion;
    }
}