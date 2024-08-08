using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Resources.ResXFileRef;

namespace ClientPOP3
{
    public partial class ClientPOP3 : Form
    {
        /*Propriété permettant de récupérer la valeur du numéricUpDown correspondant au numero du message que l'on souhaite afficher*/
        public int NumeroMail { get { return (int)NudNumMail.Value; } }
        /*Propriété permettant de s'avoir la connexion au socket et valide ou non */
        public Boolean Connecter { get; set; }

        public ClientPOP3()
        {
            InitializeComponent();
            GestionBoutons(false);
            WriteAffichage("Démarrage du client POP3 - Version 2024 SSL");

            /* Connexion au serveur POP3 */
            Boolean resultat = Communication.Initialise(this);

            if (resultat)
            {
                WriteAffichage("Connecté au serveur - Prêt !");

                /* Identification */
                Communication.Identification();

                /* envoi STAT pour recuperer nb messages */
                Communication.Stat();
                GestionBoutons(true);
            }
            else
            {
                GestionBoutons(false);
            }

            Connecter = resultat;
        }

        #region Méthodes d'écriture dans les zones d'affichage utilisateur et verbose (debug)
        public void WriteAffichage(string line)
        {
            listBoxAffichage.Items.Add(line);
            // permet "l'auto-scroll" : défiler l'affichage de la fenêtre jusqu'à la dernière ligne
            listBoxAffichage.SelectedIndex = listBoxAffichage.Items.Count - 1;
            listBoxAffichage.SelectedIndex = -1;
        }

        public void WriteVerbose(string line)
        {
            listBoxVerbose.Items.Add(line);
            // permet "l'auto-scroll" : défiler l'affichage de la fenêtre jusqu'à la dernière ligne
            listBoxVerbose.SelectedIndex = listBoxVerbose.Items.Count - 1;
            listBoxVerbose.SelectedIndex = -1;
        }
        #endregion

        private void ButtonQUIT_Click(object sender, EventArgs e)
        {

            Communication.Quit();
            MessageBox.Show("Fin du client");
            this.Dispose();
        }

        private void ButtonSTAT_Click(object sender, EventArgs e)
        {
            Communication.Stat();
        }

        private void ButtonLIST_Click(object sender, EventArgs e)
        {
            Communication.List();
        }

        // Cette méthode déclenche la récupération d'un mail dont le chiffre est passé en paramètre
        private void AfficherMessage_Click(object sender, EventArgs e)
        {
            {
                Communication.Retr2(NumeroMail);
            }


        }
        // Cette méthode de déclenché la récupération de tous les mails
        private void AfficherTsMessage_Click(object sender, EventArgs e)
        {
            Communication.TsLesRetr();
        }

        // Cette méthode déclenche la récupération d'un mail dont le chiffre est passé en paramètre
        private void AfficherMessageEntier_Click(object sender, EventArgs e)
        {
            Communication.Retr1(NumeroMail);
        }

        private void MessageAmeliore_Click(object sender, EventArgs e)
        {
            Communication.Retr3(NumeroMail);
        }
        // Cette méthode permet de couper la connexion au socket pour de relancé le processus de connexion
        private void buttonDeco_Click(object sender, EventArgs e)
        {
            Connecter = false;

            if (Connecter)
            {
                Communication.Quit();
            }

            Boolean estConnecter = Communication.Initialise(this);

            /* Identification */
            if (estConnecter)
            {
                Communication.Identification();
                WriteAffichage("Connecté au serveur - Prêt !");
                GestionBoutons(true);
                buttonDeco.Text = "déconnexion";
            }
            else
            {
                GestionBoutons(false);
                buttonDeco.Text = "reconnexion";
            }
        }

        // Cette méthode permet d'activer ou désactiver les boutons du client
        private void GestionBoutons(Boolean booleen)
        {
            buttonSTAT.Enabled = booleen;
            buttonLIST.Enabled = booleen;
            AfficherTsMessage.Enabled = booleen;
            AfficherMessageEntier.Enabled = booleen;
            AfficherMessage.Enabled = booleen;
            MessageAmeliore.Enabled = booleen;
            MessageSimple.Enabled = booleen;
        }

        // Cette méthode permet de supprimer l'ensemble des éléments contenu dans la listBox
        private void buttonNettoyerListBox_Click(object sender, EventArgs e)
        {
            listBoxAffichage.Items.Clear();
        }

        // Cette méthode permet de récupérer déclenché la récupération d'un mail au format simple
        private void MessageSimple_Click(object sender, EventArgs e)
        {
            Communication.RetrSimple(NumeroMail);
        }

        // Cette méthode permet de déclenché la récupération du contenu et l'affiche dans un MessageBox d'un mail simple
        // en fonction du numéro de mail contenu dans le texte du mail simple .
        private void listBoxAffichage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Connecter)
            {
                // Récupère l'élément sélectionné dans la ListBox
                ListBox listBox = (ListBox)sender;

                // Affiche un MessageBox avec l'élément sélectionné
                if (listBox.SelectedItem != null)
                {
                    string selectedItem = listBox.SelectedItem.ToString();
                    if (selectedItem.Contains("Num:"))
                    {
                        int extraireNumMail = Int32.Parse(selectedItem.Split(' ')[1]);
                        String message = Communication.RetrMessage(extraireNumMail);
                        if (message.Length > 300)
                        {
                            message = message.Substring(0, 300) + "...";
                        }
                        MessageBox.Show(message);
                    }
                }
            }
        }
    }
}
