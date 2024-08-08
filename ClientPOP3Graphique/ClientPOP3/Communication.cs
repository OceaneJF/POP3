using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ClientPOP3
{
    public static class Communication
    {
        private static ClientPOP3 clientPOP3;

        private static TcpClient socketClient;

        private static SslStream ssl;

        private static StreamReader sr;
        private static StreamWriter sw;

        // Changement de la signature de la méthode afin de retourner un booléen qui permet de savoir si la connexion au socket est faite ou non
        public static Boolean Initialise(ClientPOP3 client)
        {
            // Besoin d'un accès à la vue pour les affichages, utilisateur et verbose
            clientPOP3 = client;

            // Ouverture du dialog de permettant de récupérer les identifiants et serveur de connexion (pop.laposte.net par exemple)
            Banniere banniere = new Banniere();
            if (banniere.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                // Changement des variables attribues de la classe utilitaire Preferences 
                Preferences.nomServeur = banniere.Machine;
                Preferences.username = banniere.Indentifiant;
                Preferences.password = banniere.Mdp;

                // Connexion au serveur
                socketClient = new TcpClient();   // équivaut à la primitive Socket (avec mode TCP)
                // Tentaive de connexion au socket 
                try
                {
                    socketClient.Connect(Preferences.nomServeur, Preferences.port);
                    // Authentification SSL
                    // ssl de type SslStream est affecté à partir de socketClient.GetStream()
                    AuthentificationSSL();

                    // Mise en place des Streams pour lecture et écriture par ligne sur la socket
                    sr = new StreamReader(ssl, Encoding.UTF8); // caractères accentués dans les mails
                    sw = new StreamWriter(ssl, Encoding.Default)
                    {
                        AutoFlush = true
                    };

                    return true;
                }
                // Si la tentative échoue alors affichage de l'erreur
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return false;
                }
            }

            return false;
        }

        #region Méthodes pour l'Authentification SSL
        // https://learn.microsoft.com/fr-fr/dotnet/api/system.net.security.sslstream?view=netframework-4.7.1
        // https://stackoverflow.com/questions/39304612/c-sharp-ssl-tls-with-socket-tcp
        // The following method is invoked by the RemoteCertificateValidationDelegate.
        public static bool ValidateServerCertificate(
              object sender,
              X509Certificate certificate,
              X509Chain chain,
              SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            Console.WriteLine("Certificate error: {0}", sslPolicyErrors);

            // Do not allow this client to communicate with unauthenticated servers.
            return false;
        }
        public static void AuthentificationSSL()
        {
            // Create an SSL stream that will close the client's stream.
            ssl = new SslStream(
                socketClient.GetStream(),
                false,
                new RemoteCertificateValidationCallback(ValidateServerCertificate),
                null);
            // The server name must match the name on the server certificate.
            try
            {
                ssl.AuthenticateAsClient(Preferences.nomServeur);
            }
            catch (AuthenticationException e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
                if (e.InnerException != null)
                {
                    Console.WriteLine("Inner exception: {0}", e.InnerException.Message);
                }
                Console.WriteLine("Authentication failed - closing the connection.");
                socketClient.Close();
                return;
            }
        }
        #endregion

        #region Méthodes de lecture et écriture d'une ligne dans la socket de communication
        private static string LireLigne()
        {
            // Lecture d'une ligne dans le Stream associé à la socket (en provenance du serveur POP3)
            string ligne = sr.ReadLine();
            // Affichage dans la fenêtre VERBOSE
            clientPOP3.WriteVerbose("recu  >> " + ligne);
            return ligne;
        }
        private static void EcrireLigne(string ligne)
        {
            // Ecriture d'une ligne dans le Stream associé à la socket (à destination du serveur POP3)
            sw.WriteLine(ligne);
            // Affichage dans la fenêtre VERBOSE
            clientPOP3.WriteVerbose("envoi << " + ligne);
        }
        #endregion

        public static void Identification()
        {
            string ligne, tampon;

            /* réception banniere +OK ... */
            ligne = LireLigne();
            if (!ligne[0].Equals('+'))
            {
                MessageBox.Show("Pas de banniere. Abandon");
                Environment.Exit(1);
            };

            /* envoi identification */
            tampon = "USER " + Preferences.username;
            EcrireLigne(tampon);
            ligne = LireLigne();
            if (!ligne[0].Equals('+'))
            {
                MessageBox.Show("USER rejeté. Abandon");
                Environment.Exit(1);
            };

            /* envoi mot de passe */
            tampon = "PASS " + Preferences.password;
            EcrireLigne(tampon);
            ligne = LireLigne();
            if (!ligne[0].Equals('+'))
            {
                MessageBox.Show("PASS rejeté. Abandon");
                Environment.Exit(1);
            }
        }

        public static void Quit()
        {
            // Condition permettant d'appeler les méthodes liées au socket uniquement s'il est connecté
            if (clientPOP3.Connecter)
            {
                string ligne, tampon;

                /* envoi QUIT pour arrêter l'échange avec le serveur */
                tampon = "QUIT";
                EcrireLigne(tampon);
                ligne = LireLigne(); // lecture du +OK

                // Fermeture de la socket de communication
                socketClient.Close();
            }
        }

        public static void Stat()
        {
            string ligne, tampon;
            /* envoi STAT pour récupérer nb messages */
            tampon = "STAT";
            EcrireLigne(tampon);
            /* réception de +OK nombreMessages tailleBoite */
            ligne = LireLigne();
            if (!ligne[0].Equals('+'))
            {
                MessageBox.Show("ERR : STAT a échoué");
            }
            else
            {
                /* découpage pour récupérer nombreMessages et tailleBoite, et les afficher pour l'utilisateur */
                string[] lesValeurs = ligne.Split(' ');
                int nombreMessages = Int32.Parse(lesValeurs[1]);
                int tailleBoite = Int32.Parse(lesValeurs[2]);
                clientPOP3.WriteAffichage("Il y a " + nombreMessages.ToString() + " messages dans la boite.");
                clientPOP3.WriteAffichage("La taille totale est de " + tailleBoite.ToString() + " octets.");
            }
        }

        /* Récupère et affiche la liste des messages */
        public static void List()
        {
            // *** BLOC A DECOMMENTER  : Ctrl+e puis u  ***
            string ligne, tampon;
            /*Envoi LIST pour afficher la liste des messages*/
            tampon = "LIST";
            EcrireLigne(tampon);
            /* réception de +OK numeroMessage tailleMessage */
            ligne = LireLigne();
            if (!ligne[0].Equals('+'))
            {
                MessageBox.Show("ERR : LIST a échoué");
            }
            else
            {
                clientPOP3.WriteAffichage("Voici la liste des mails :");
                /* lecture liste ligne par ligne jusqu'au "." final seul sur une ligne */
                ligne = LireLigne();
                while (!ligne.Equals("."))
                {
                    /*découpage pour récupérer le numéro du mail et la taille du mail et les afficher pour l'utilisateur */
                    string[] lesValeurs = ligne.Split(' ');
                    int numMail = Int32.Parse(lesValeurs[0]);
                    int tailleMail = Int32.Parse(lesValeurs[1]);
                    clientPOP3.WriteAffichage("La taille du mail n° : " + numMail.ToString() + " est de " + tailleMail.ToString() + " octets.");
                    ligne = LireLigne();

                }
            }
        }
        // Récupère et affiche le message ligne a ligne dont le numero est passé en parametre */
        public static void Retr1(int numeroMail)
        {
            string ligne, tampon;
            /*Envoi retr pour récuperer le message dont le numéro est choisi avec le numericUpDown*/
            tampon = "retr " + numeroMail;
            EcrireLigne(tampon);
            /* réception de +OK message */
            ligne = LireLigne();
            if (!ligne[0].Equals('+'))
            {
                MessageBox.Show("ERR : RETR a échoué");
            }
            else
            {
                ligne = LireLigne();
                /*On parcour chaque ligne jusqu'a la fin du message ( caractérisé par '.') */
                while (!ligne.Equals("."))
                {
                    /*Le message peut contenir des saut de ligne, pour eviter les erreurs on ajoute une condition*/
                    if (ligne.Length != 0)
                    {
                        /*On détermine les lignes qui commence par un point et on retire le point en trop */
                        if (ligne[0] == '.')
                        {
                            ligne = ligne.Remove(0, 1);
                        }
                    }
                    /*On affiche les lignes une par une cotés client*/
                    clientPOP3.WriteAffichage(ligne);
                    ligne = LireLigne();
                }
            }
        }
        /*Récupère et affiche le message dont le numero est passé en parametre */
        public static void Retr2(int numeroMail)
        {
            string ligne, tampon;
            /*Envoi retr pour récuperer le message dont le numéro est choisi avec le numericUpDown*/
            tampon = "retr " + numeroMail;
            EcrireLigne(tampon);
            /* réception de +OK message */
            ligne = LireLigne();
            if (!ligne[0].Equals('+'))
            {
                MessageBox.Show("ERR : RETR a échoué");
            }
            else
            {
                ligne = LireLigne();
                clientPOP3.WriteAffichage("-----------------------------");
                clientPOP3.WriteAffichage("Message " + numeroMail);
                string Date = "";
                string Expediteur = "";
                string Subject = "";
                /*On parcour chaque ligne jusqu'a la fin du message ( caractérisé par '.') */
                while (!ligne.Equals("."))
                {
                    /*Recherche et concaténation des information à afficher dans l'entete*/
                    string[] lesValeurs = ligne.Split(' ');
                    switch (lesValeurs[0])
                    {
                        case "Date:":
                            Date = "Ecrit le: " + Concatener(lesValeurs);
                            break;
                        case "From:":
                            Expediteur = "Expediteur: " + Concatener(lesValeurs);
                            break;
                        case "Subject:":
                            Subject = "Sujet: " + Concatener(lesValeurs);
                            break;
                    }
                    ligne = LireLigne();

                }
                /*Affichage cotés client de la date, l'expéditeur et le sujet du message */
                clientPOP3.WriteAffichage(Date);
                clientPOP3.WriteAffichage(Expediteur);
                clientPOP3.WriteAffichage(Subject);
                clientPOP3.WriteAffichage("-----------------------------");

            }
        }

        /*Methode permettant de concatener un tableau de chaine passé en paramètre*/
        public static string Concatener(string[] tab)
        {
            string phrase = "";
            for (int i = 1; i < tab.Length; i++)
            {
                phrase += " " + tab[i];
            }
            return phrase;
        }
        /*Affiche uniquement la date, le nom de l’expéditeur et le sujet  pour tous les messages*/
        public static void TsLesRetr()
        {
            string ligne, tampon;
            /* envoi STAT pour récupérer nb messages */
            tampon = "STAT";
            EcrireLigne(tampon);
            /* réception de +OK nombreMessages tailleBoite */
            ligne = LireLigne();
            int nombreMessages = 0;
            if (!ligne[0].Equals('+'))
            {
                MessageBox.Show("ERR : STAT a échoué");
            }
            else
            {
                /* découpage pour récupérer nombreMessages */
                string[] lesValeurs = ligne.Split(' ');
                nombreMessages = Int32.Parse(lesValeurs[1]);

            }
            for (int i = 1; i < nombreMessages; i++)
            {
                /*Condition pour ignorer un message de 500Mo qui fait planter l'application à la lecture*/
                Retr2(i);
            }

        }

        /*Récupère et affiche le message de façon plus concise dont le numero est passé en parametre */
        public static void Retr3(int numeroMail)
        {
            string ligne, tampon;
            /*Envoi retr pour récuperer le message dont le numéro est choisi avec le numericUpDown*/
            tampon = "retr " + numeroMail;
            EcrireLigne(tampon);
            /* réception de +OK message */
            ligne = LireLigne();
            if (!ligne[0].Equals('+'))
            {
                MessageBox.Show("ERR : RETR a échoué");
            }
            else
            {
                ligne = LireLigne();
                clientPOP3.WriteAffichage("-----------------------------");
                string Date = "";
                string Expediteur = "";
                string Subject = "";
                /*On parcour chaque ligne jusqu'a la fin du message ( caractérisé par '.') */
                while (!ligne.Equals("."))
                {
                    /*Recherche et concaténation des information à afficher dans l'entete*/
                    string[] lesValeurs = ligne.Split(' ');
                    switch (lesValeurs[0])
                    {
                        case "Date:":
                            if (Concatener(lesValeurs).Contains("CET"))
                            {
                                Date = Concatener(lesValeurs);
                            }
                            else
                            {
                                DateTime conversionDeLaDate = DateTime.Parse(Concatener(lesValeurs));
                                Date = conversionDeLaDate.ToString();
                            }
                            break;
                        case "From:":
                            Expediteur = "Expediteur: " + Concatener(lesValeurs);
                            break;
                        case "Subject:":
                            Subject = "Sujet: " + Concatener(lesValeurs);
                            break;
                    }
                    ligne = LireLigne();

                }
                /*Affichage cotés client de la date, l'expéditeur et le sujet du message */
                clientPOP3.WriteAffichage(Date);
                clientPOP3.WriteAffichage(Expediteur);
                clientPOP3.WriteAffichage(Subject);
                // récupération du message contenu dans le mail et affichage de celui ci
                clientPOP3.WriteAffichage(RetrMessage(numeroMail));
                clientPOP3.WriteAffichage("-----------------------------");

            }
        }

        // Récupère et affiche le message sur une seul ligne dont le numero est passé en parametre 
        public static void RetrSimple(int numeroMail)
        {
            string ligne, tampon;
            /*Envoi retr pour récuperer le message dont le numéro est choisi avec le numericUpDown*/
            tampon = "retr " + numeroMail;
            EcrireLigne(tampon);
            /* réception de +OK message */
            ligne = LireLigne();
            if (!ligne[0].Equals('+'))
            {
                MessageBox.Show("ERR : RETR a échoué");
            }
            else
            {
                ligne = LireLigne();
                string Date = "";
                string Expediteur = "";
                string Subject = "";
                /*On parcour chaque ligne jusqu'a la fin du message ( caractérisé par '.') */
                while (!ligne.Equals("."))
                {
                    /*Recherche et concaténation des information à afficher dans l'entete*/
                    string[] lesValeurs = ligne.Split(' ');

                    switch (lesValeurs[0])
                    {
                        case "Date:":
                            if (Concatener(lesValeurs).Contains("CET"))
                            {
                                Date = Concatener(lesValeurs);
                            }
                            else
                            {
                                DateTime conversionDeLaDate = DateTime.Parse(Concatener(lesValeurs));
                                Date = conversionDeLaDate.ToString();
                            }
                            break;
                        case "From:":
                            Expediteur = Concatener(lesValeurs);
                            break;
                        case "Subject:":
                            Subject = Concatener(lesValeurs);
                            break;
                    }
                    ligne = LireLigne();

                }
                /*Affichage cotés client de la date, l'expéditeur et le sujet du message */
                clientPOP3.WriteAffichage("Num: " + numeroMail + " " + Date + " : " + Expediteur + " | " + Subject);
            }
        }

        // Récupère et retourne le contenu du mail en fonction du contentType plain html multipart dont le numero est passé en parametre
        public static String RetrMessage(int numeroMail)
        {
            string ligne, tampon;
            bool estMultiPart = false; // Pour suivre si le message est multipart
            string boundary = ""; // Pour stocker le délimiteur (boundary)
            string message = ""; // Pour stocker le délimiteur (boundary)

            /*Envoi retr pour récupérer le message dont le numéro est choisi avec le numericUpDown*/
            tampon = "retr " + numeroMail;
            EcrireLigne(tampon);
            /* réception de +OK message */
            ligne = LireLigne();
            if (!ligne[0].Equals('+'))
            {
                MessageBox.Show("ERR : RETR a échoué");
            }
            else
            {
                bool contentTypeTrouvé = false; // Pour suivre si le champ Content-Type a été trouvé
                /*On parcourt chaque ligne jusqu'à la fin du message (caractérisé par '.') */
                while (!ligne.Equals("."))
                {
                    /* Si on trouve le champ "Content-Type", on vérifie s'il est multipart */
                    if (ligne.StartsWith("Content-Type:"))
                    {
                        if (ligne.Contains("multipart"))
                        {
                            estMultiPart = true;
                            /* Extrait le délimiteur (boundary) s'il existe */
                            var indice = ligne.IndexOf("boundary=");
                            if (indice != -1)
                            {
                                boundary = ligne.Substring(indice + "boundary=".Length).Trim();
                                // supprimer les "" du boundary
                                boundary = boundary.Trim('"');
                            }
                        }
                        contentTypeTrouvé = true;
                    }
                    else if (contentTypeTrouvé && !estMultiPart && !ligne.StartsWith("Content-Transfer-Encoding:") && !ligne.Contains("X-AV-Checked"))
                    {
                        // Affiche la ligne seulement si le champ "Content-Type" a été trouvé et que le message n'est pas multipart
                        message += " " + ligne;
                    }
                    else if (estMultiPart && !ligne.StartsWith("--" + boundary) && !ligne.StartsWith("Content-Type:") && !ligne.StartsWith("Content-Transfer-Encoding:"))
                    {
                        // Si le message est multipart, affiche les parties du message (sauf les délimiteurs et les en-têtes)
                        message += " " + ligne;
                    }
                    ligne = LireLigne();
                }
            }

            return message;
        }
    }
}
