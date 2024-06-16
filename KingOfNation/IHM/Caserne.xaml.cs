using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using KingOfNation.Code;
using Microsoft.VisualBasic.FileIO;
namespace KingOfNation.IHM
{
    /// <summary>
    /// Logique d'interaction pour Caserne.xaml
    /// </summary>
    public partial class Caserne : Window
    {

        #region Attributes
        private Leger leger;
        private Lourd lourd;
        private Machine_de_guerre mdg;
        List<CsvData> csvDataList = new List<CsvData>();
        #endregion

        #region Properties     

        #endregion

        #region Constructor

        public Caserne()
        {
            InitializeComponent();
            LoadCSVData(csvDataList);
            DataContext = ((App)Application.Current).Joueur;


            if (((App)Application.Current).Joueur.Leger == null)
            {
                ((App)Application.Current).Joueur.Leger = new Leger("leger", 0);
            }
            if (((App)Application.Current).Joueur.Lourd == null)
            {
                ((App)Application.Current).Joueur.Lourd = new Lourd("lourd", 0);
            }
            if (((App)Application.Current).Joueur.Mdg == null)
            {
                ((App)Application.Current).Joueur.Mdg = new Machine_de_guerre("machines de guerre", 0);
            }

            troupeLeger.Text = ((App)Application.Current).Joueur.Leger.Nb.ToString();
            troupeLourd.Text = ((App)Application.Current).Joueur.Lourd.Nb.ToString();
            troupeMDG.Text = ((App)Application.Current).Joueur.Mdg.Nb.ToString();

            afficherMaxTroupe();
            afficherPoidsTotal();

            ((App)Application.Current).timerJ.Tick += afficherHab;
            ((App)Application.Current).timerJ.Start();
        }

        #endregion

        #region Operations

        private void Home(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).OpenGame();
            this.Close();
        }

        private void afficherHab(object sender, EventArgs e)
        {
            nbHab.Text = ((App)Application.Current).Joueur.Hab.ToString();
        }


        #endregion

        private void Former(object sender, RoutedEventArgs e)
        {
            int nbTroupeMax = GetNbTroupeMax();

            int leger_a_former = (int)nbLeger.Value;
            int lourd_a_former = (int)nbLourd.Value;
            int mdg_a_former = (int)nbMDG.Value;

            int coutLeger = 20;
            int coutLourd = 50;
            int coutMDG = 200;

            int poidsLeger = 1 * leger_a_former;
            int poidsLourd = 5 * lourd_a_former;
            int poidsMDG = 10 * mdg_a_former;
            int poidsTotalFormation = poidsLeger + poidsLourd + poidsMDG;

            int nb_Hab = ((App)Application.Current).Joueur.Hab;

            if (((App)Application.Current).Joueur.Leger == null)
            {
                ((App)Application.Current).Joueur.Leger = new Leger("leger", 0);
            }
            if (((App)Application.Current).Joueur.Lourd == null)
            {
                ((App)Application.Current).Joueur.Lourd = new Lourd("lourd", 0);
            }
            if (((App)Application.Current).Joueur.Mdg == null)
            {
                ((App)Application.Current).Joueur.Mdg = new Machine_de_guerre("machines de guerre", 0);
            }

            // Calcul du coût total en habitants pour former les troupes
            int coutTotalLeger = leger_a_former * coutLeger;
            int coutTotalLourd = lourd_a_former * coutLourd;
            int coutTotalMDG = mdg_a_former * coutMDG;

            int coutTotal = coutTotalLeger + coutTotalLourd + coutTotalMDG;

            // Calcul du poids total actuel des troupes
            int poidsTotalActuel = ((App)Application.Current).Joueur.Leger.Nb * 1 + ((App)Application.Current).Joueur.Lourd.Nb * 5 + ((App)Application.Current).Joueur.Mdg.Nb * 10;

            // Vérification des conditions
            if (leger_a_former == 0 && lourd_a_former == 0 && mdg_a_former == 0)
            {
                MessageBox.Show("Choisissez une troupe à former !");
                return;
            }

            if (nb_Hab < coutTotal)
            {
                MessageBox.Show("Vous n'avez pas assez d'habitants pour former toutes ces troupes !");
                return;
            }

            if (poidsTotalActuel + poidsTotalFormation > nbTroupeMax)
            {
                MessageBox.Show("Vous avez atteint le nombre maximum de place de troupes !");
                return;
            }

            // Mise à jour des valeurs
            nb_Hab -= coutTotal;
            ((App)Application.Current).Joueur.Hab = nb_Hab;

            // Mise à jour des nombres de soldats
            ((App)Application.Current).Joueur.Leger.Nb += leger_a_former;
            ((App)Application.Current).Joueur.Lourd.Nb += lourd_a_former;
            ((App)Application.Current).Joueur.Mdg.Nb += mdg_a_former;

            // Affichage des informations
            MessageBox.Show($"Vous avez formé {leger_a_former} soldats légers, {lourd_a_former} soldats lourds et {mdg_a_former} machines de guerre.");
            troupeLeger.Text = ((App)Application.Current).Joueur.Leger.Nb.ToString();
            troupeLourd.Text = ((App)Application.Current).Joueur.Lourd.Nb.ToString();
            troupeMDG.Text = ((App)Application.Current).Joueur.Mdg.Nb.ToString();
            
            afficherPoidsTotal();

        }

        private void afficherMaxTroupe()
        {           
            maxTroupe.Text = GetNbTroupeMax().ToString();
        }

        private int GetNbTroupeMax()
        {
            int nbTroupeMax = 0;
            foreach (CsvData elt in csvDataList)
            {
                if (elt.Nom == "Caserne")
                {
                    if (elt.Niveau == "1")
                    {
                        nbTroupeMax = 30;
                    }
                    else if (elt.Niveau == "2")
                    {
                        nbTroupeMax = 50;
                    }
                    else if (elt.Niveau == "3")
                    {
                        nbTroupeMax = 75;
                    }
                    else if (elt.NiveauMax == "4")
                    {
                        nbTroupeMax = 110;
                    }
                }
            }
            return nbTroupeMax;
        }

        private void afficherPoidsTotal()
        {            
                int poidsTotalActuel = 0;
                if (((App)Application.Current).Joueur.Leger != null)
                {
                    poidsTotalActuel += ((App)Application.Current).Joueur.Leger.Nb * 1;
                }
                if (((App)Application.Current).Joueur.Lourd != null)
                {
                    poidsTotalActuel += ((App)Application.Current).Joueur.Lourd.Nb * 5;
                }
                if (((App)Application.Current).Joueur.Mdg != null)
                {
                    poidsTotalActuel += ((App)Application.Current).Joueur.Mdg.Nb * 10;
                }
                poidsTotal.Text = poidsTotalActuel.ToString();
        }        

        private void LoadCSVData(List<CsvData> csvDataList)
        {
            string filePath = "../../../CSV/" + ((App)Application.Current).Joueur.NomVillage + ".csv";

            try
            {
                using (TextFieldParser parser = new TextFieldParser(filePath))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(";");

                    // Lire la ligne d'en-tête si elle existe
                    if (!parser.EndOfData)
                    {
                        string[] headers = parser.ReadFields();
                        // Vous pouvez utiliser les en-têtes si nécessaire
                    }

                    // Lire les lignes suivantes
                    while (!parser.EndOfData)
                    {
                        string[] fields = parser.ReadFields();
                        if (fields.Length >= 4) // Assurez-vous qu'il y a au moins 4 colonnes
                        {
                            // Ajouter uniquement les lignes où la seconde colonne est "1"
                            if (fields[1] == "1")
                            {
                                csvDataList.Add(new CsvData { Nom = fields[0], Niveau = fields[2], Description = fields[4]});
                            }
                        }
                    }
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Le fichier ne peut pas être lu: " + ex.Message);
            }
        }

        private void infoLeger(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
            "Les soldats légers bénéficieront d'un avantage lors des assauts sur des terrains montagneux mais seront en désavantages dans la forêt. \n Les soldats légers prennent 1 place dans la caserne",
            "Info sur les soldats légers",
            MessageBoxButton.OK,
            MessageBoxImage.Information);
        }

        private void infoLourd(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
            "Les soldats lourds bénéficieront d'un avantage lors des raids dans les forêts mais seront en désavantages sur les plaines. \n Les soldats lourds prennent 5 places dans la caserne",
            "Info sur les soldats lourds",
            MessageBoxButton.OK,
            MessageBoxImage.Information);
        }

        private void infoMDG(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
            "Les machines de guerres bénéficieront d'un avantage lors des raids sur les plaines mais seront en désavantages dans les montagnes. \n Les machines de guerres prennent 10 places dans la caserne",
            "Info sur les machines de guerres",
            MessageBoxButton.OK,
            MessageBoxImage.Information);
        }
    }
}
