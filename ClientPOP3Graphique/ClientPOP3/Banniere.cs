using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientPOP3
{
    public partial class Banniere : Form
    {

        // Permet de récupérer le text de la textBox serveur (correspond à l'adresse pop.laposte.net par exemple)
        public string Machine { get { return textMachine.Text; } }
        // Permet de récupérer le text de la textBox Identifiant 
        public string Indentifiant { get { return textIdentifiant.Text; } }
        // Permet de récupérer le text de la textBox Mdp
        public string Mdp { get { return textMdp.Text; } }

        public Banniere()
        {
            InitializeComponent();
        }

        // Cette méthode permet de désactiver le bouton OK du dialogue si une ou plusieurs textBox sont vides
        private void TextOnChange(object sender, EventArgs e)
        {
            if (!Machine.Equals(String.Empty) && !Indentifiant.Equals(String.Empty) && !Mdp.Equals(String.Empty))
            {
                buttonConnexion.Enabled=true;
            } else
            {
                buttonConnexion.Enabled=false;
            }
        }

    }
}
