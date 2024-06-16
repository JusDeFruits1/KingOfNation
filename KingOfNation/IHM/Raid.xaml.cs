using KingOfNation.Code;
using Microsoft.VisualBasic.FileIO;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;

namespace KingOfNation.IHM
{
    /// <summary>
    /// Logique d'interaction pour Raid.xaml
    /// </summary>
    public partial class Raid : Window, INotifyPropertyChanged
    {

        #region Attributes

        List<CsvData> csvDataList = new List<CsvData>();
        private bool verifRaidForet = false;
        private bool verifRaidMontagne = false;
        public event PropertyChangedEventHandler PropertyChanged;
        List<Tresor> tresorRand = ((App)Application.Current).Joueur.TresorsJoueur;

        #endregion

        #region Properties

        public List<Tresor> TresorRand
        {
            get
            {
                return tresorRand;
            }
            set
            {
                tresorRand = value;
            }
        }

        #endregion

        #region Constructor

        public Raid()
        {
            WMPLib.WindowsMediaPlayer musicGame = ((App)Application.Current).musicGame;
            InitializeComponent();
            musicGame.controls.stop();
            musicGame.URL = "Rumbletown.mp3";
            musicGame.controls.play();
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
            LoadCsvData(csvDataList);

        }

        #endregion

        #region Operations

        private void Back(object sender, RoutedEventArgs e)
        {
            Game game = new Game(true);
            game.Show();
            this.Close();
        }

        private void backCaserne(object sender, RoutedEventArgs e)
        {
            Caserne caserne = new Caserne();
            caserne.Show();
        }

        private void peutRaidForet()
        {
            foreach (CsvData elt in csvDataList)
            {
                if (elt.Nom == "Hotel de ville")
                {
                    if (elt.Niveau == "2" || elt.Niveau == "3" || elt.Niveau == "4")
                    {
                        verifRaidForet = true;
                    }
                    else
                    {
                        verifRaidForet = false;
                    }
                }
            }
        }

        private void peutRaidMontagne()
        {
            foreach (CsvData elt in csvDataList)
            {
                if (elt.Nom == "Hotel de ville")
                {
                    if (elt.Niveau == "3" || elt.Niveau == "4")
                    {
                        verifRaidMontagne = true;
                    }
                    else
                    {
                        verifRaidMontagne = false;
                    }
                }
            }
        }

        private void raidPlaine(object sender, RoutedEventArgs e)
        {
            if (((App)Application.Current).Joueur.Leger.Nb == 0 && ((App)Application.Current).Joueur.Lourd.Nb == 0 && ((App)Application.Current).Joueur.Mdg.Nb == 0)
            {
                MessageBox.Show("Vous avez besoin d'une troupe de soldats pour lancer un raid !!");
                return;
            }

            Random rand = new Random();

            // Base chance de succès est 30%
            double chanceDeSucces = 0.3;

            // Chaque machine de guerre augmente la chance de 10%
            chanceDeSucces += ((App)Application.Current).Joueur.Mdg.Nb * 0.1;

            // Chaque soldat lourd diminue la chance de 5%
            chanceDeSucces -= ((App)Application.Current).Joueur.Lourd.Nb * 0.05;

            // Limiter les chances de succès entre 0% et 100%
            chanceDeSucces = Math.Max(0, Math.Min(1, chanceDeSucces));

            // Générer un nombre aléatoire pour déterminer le résultat
            bool victoire = rand.NextDouble() < chanceDeSucces;

            if (victoire)
            {
                CalculerPertesSoldatsPlaines(true);
                ChoisirTresorRandom("../../../CSV/Tresor.csv");
            }
            else
            {
                CalculerPertesSoldatsPlaines(false);
            }
        }

        private void raidForet(object sender, RoutedEventArgs e)
        {
            peutRaidForet();
            if (verifRaidForet == true)
            {
                if (((App)Application.Current).Joueur.Leger.Nb == 0 && ((App)Application.Current).Joueur.Lourd.Nb == 0 && ((App)Application.Current).Joueur.Mdg.Nb == 0)
                {
                    MessageBox.Show("Vous avez besoin d'une troupe de soldats pour lancer un raid !!");
                    return;
                }

                Random rand = new Random();

                // Base chance de succès est 30%
                double chanceDeSucces = 0.3;

                // Chaque soldat lourd augmente la chance de 10%
                chanceDeSucces += ((App)Application.Current).Joueur.Lourd.Nb * 0.1;

                // Chaque soldat léger diminue la chance de 5%
                chanceDeSucces -= ((App)Application.Current).Joueur.Leger.Nb * 0.05;

                // Limiter les chances de succès entre 0% et 100%
                chanceDeSucces = Math.Max(0, Math.Min(1, chanceDeSucces));

                // Générer un nombre aléatoire pour déterminer le résultat
                bool victoire = rand.NextDouble() < chanceDeSucces;

                if (victoire)
                {
                    CalculerPertesSoldatsForet(true);
                    ChoisirTresorRandom("../../../CSV/Tresor.csv");
                }
                else
                {
                    CalculerPertesSoldatsForet(false);
                }
            }
            else if (verifRaidForet == false)
            {
                MessageBox.Show("Veuillez monter le niveau de votre hôtel de ville afin d'accéder à ce raid");
            }
        }

        private void raidMontagne(object sender, RoutedEventArgs e)
        {
            peutRaidMontagne();
            if (verifRaidMontagne == true)
            {
                if (((App)Application.Current).Joueur.Leger.Nb == 0 && ((App)Application.Current).Joueur.Lourd.Nb == 0 && ((App)Application.Current).Joueur.Mdg.Nb == 0)
                {
                    MessageBox.Show("Vous avez besoin d'une troupe de soldats pour lancer un raid !!");
                    return;
                }


                Random rand = new Random();

                // Base chance de succès est 30%
                double chanceDeSucces = 0.3;

                // Chaque soldat léger augmente la chance de 10%
                chanceDeSucces += ((App)Application.Current).Joueur.Leger.Nb * 0.1;

                // Chaque machine de guerre diminue la chance de 5%
                chanceDeSucces -= ((App)Application.Current).Joueur.Mdg.Nb * 0.05;

                // Limiter les chances de succès entre 0% et 100%
                chanceDeSucces = Math.Max(0, Math.Min(1, chanceDeSucces));

                // Générer un nombre aléatoire pour déterminer le résultat
                bool victoire = rand.NextDouble() < chanceDeSucces;

                if (victoire)
                {
                    CalculerPertesSoldatsMontagnes(true);
                    ChoisirTresorRandom("../../../CSV/Tresor.csv");
                }
                else
                {
                    CalculerPertesSoldatsMontagnes(false);
                }
            }
            else if (verifRaidMontagne == false)
            {
                MessageBox.Show("Veuillez monter le niveau de votre hôtel de ville afin d'accéder à ce raid");
            }
        }

        private void CalculerPertesSoldatsPlaines(bool victoire)
        {
            if (victoire)
            {
                double tauxPerteLegers = 0.2;
                double tauxPerteLourds = 0.3;
                double tauxPerteMachines = 0.1;

                int pertesLegers = (int)(((App)Application.Current).Joueur.Leger.Nb * tauxPerteLegers);
                int pertesLourds = (int)(((App)Application.Current).Joueur.Lourd.Nb * tauxPerteLourds);
                int pertesMachines = (int)(((App)Application.Current).Joueur.Mdg.Nb * tauxPerteMachines);

                ((App)Application.Current).Joueur.Leger.Nb = Math.Max(0, ((App)Application.Current).Joueur.Leger.Nb - pertesLegers);
                ((App)Application.Current).Joueur.Lourd.Nb = Math.Max(0, ((App)Application.Current).Joueur.Lourd.Nb - pertesLourds);
                ((App)Application.Current).Joueur.Mdg.Nb = Math.Max(0, ((App)Application.Current).Joueur.Mdg.Nb - pertesMachines);

                MessageBox.Show("Victoire ! Vous avez réussi le raid sur la plaine.");
                MessageBox.Show($"Pertes :\nSoldats légers : {pertesLegers}\nSoldats lourds : {pertesLourds}\nMachines de guerre : {pertesMachines}");
            }
            else
            {
                ((App)Application.Current).Joueur.Leger.Nb = ((App)Application.Current).Joueur.Leger.Nb / 4;
                ((App)Application.Current).Joueur.Lourd.Nb = ((App)Application.Current).Joueur.Lourd.Nb / 4;
                ((App)Application.Current).Joueur.Mdg.Nb = ((App)Application.Current).Joueur.Mdg.Nb / 4;

                MessageBox.Show("Défaite. Vous avez échoué le raid sur la plaine. Vous perdez la moitié de vos soldats.");
            }
        }

        private void CalculerPertesSoldatsForet(bool victoire)
        {
            if (victoire)
            {
                double tauxPerteLegers = 0.3;
                double tauxPerteLourds = 0.1;
                double tauxPerteMachines = 0.2;

                int pertesLegers = (int)(((App)Application.Current).Joueur.Leger.Nb * tauxPerteLegers);
                int pertesLourds = (int)(((App)Application.Current).Joueur.Lourd.Nb * tauxPerteLourds);
                int pertesMachines = (int)(((App)Application.Current).Joueur.Mdg.Nb * tauxPerteMachines);

                ((App)Application.Current).Joueur.Leger.Nb = Math.Max(0, ((App)Application.Current).Joueur.Leger.Nb - pertesLegers);
                ((App)Application.Current).Joueur.Lourd.Nb = Math.Max(0, ((App)Application.Current).Joueur.Lourd.Nb - pertesLourds);
                ((App)Application.Current).Joueur.Mdg.Nb = Math.Max(0, ((App)Application.Current).Joueur.Mdg.Nb - pertesMachines);

                MessageBox.Show("Victoire ! Vous avez réussi le raid sur la plaine.");
                MessageBox.Show($"Pertes :\nSoldats légers : {pertesLegers}\nSoldats lourds : {pertesLourds}\nMachines de guerre : {pertesMachines}");
            }
            else
            {
                ((App)Application.Current).Joueur.Leger.Nb = ((App)Application.Current).Joueur.Leger.Nb / 4;
                ((App)Application.Current).Joueur.Lourd.Nb = ((App)Application.Current).Joueur.Lourd.Nb / 4;
                ((App)Application.Current).Joueur.Mdg.Nb = ((App)Application.Current).Joueur.Mdg.Nb / 4;

                MessageBox.Show("Défaite. Vous avez échoué le raid sur la plaine. Vous perdez la moitié de vos soldats.");
            }
        }

        private void CalculerPertesSoldatsMontagnes(bool victoire)
        {
            if (victoire)
            {
                double tauxPerteLegers = 0.1;
                double tauxPerteLourds = 0.2;
                double tauxPerteMachines = 0.3;

                int pertesLegers = (int)(((App)Application.Current).Joueur.Leger.Nb * tauxPerteLegers);
                int pertesLourds = (int)(((App)Application.Current).Joueur.Lourd.Nb * tauxPerteLourds);
                int pertesMachines = (int)(((App)Application.Current).Joueur.Mdg.Nb * tauxPerteMachines);

                ((App)Application.Current).Joueur.Leger.Nb = Math.Max(0, ((App)Application.Current).Joueur.Leger.Nb - pertesLegers);
                ((App)Application.Current).Joueur.Lourd.Nb = Math.Max(0, ((App)Application.Current).Joueur.Lourd.Nb - pertesLourds);
                ((App)Application.Current).Joueur.Mdg.Nb = Math.Max(0, ((App)Application.Current).Joueur.Mdg.Nb - pertesMachines);

                MessageBox.Show("Victoire ! Vous avez réussi le raid sur la plaine.");
                MessageBox.Show($"Pertes :\nSoldats légers : {pertesLegers}\nSoldats lourds : {pertesLourds}\nMachines de guerre : {pertesMachines}");
            }
            else
            {
                ((App)Application.Current).Joueur.Leger.Nb = ((App)Application.Current).Joueur.Leger.Nb / 4;
                ((App)Application.Current).Joueur.Lourd.Nb = ((App)Application.Current).Joueur.Lourd.Nb / 4;
                ((App)Application.Current).Joueur.Mdg.Nb = ((App)Application.Current).Joueur.Mdg.Nb / 4;

                MessageBox.Show("Défaite. Vous avez échoué le raid sur la plaine. Vous perdez une grande partie de vos soldats.");
            }
        }

        private void ChoisirTresorRandom(string filePath)
        {
            try
            {
                List<Tresor> tresorsTemp = new List<Tresor>();

                using (var reader = new StreamReader(filePath))
                {
                    reader.ReadLine(); // Lire et ignorer la première ligne (headers)

                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var values = line.Split(';');
                        Tresor tresor = new Tresor
                        {
                            Nom = values[0],
                            Price = values[1],
                            Description = values[2],
                            ImagPath = values[3],
                        };
                        tresorsTemp.Add(tresor);
                    }

                    // Vérifier si la liste temporaire contient des trésors
                    if (tresorsTemp.Count == 0)
                    {
                        MessageBox.Show("Aucun trésor disponible dans le fichier CSV.");
                        return;
                    }

                    // Générer un nombre aléatoire pour sélectionner un trésor
                    Random rand = new Random();
                    int index = rand.Next(0, tresorsTemp.Count);

                    // Sélectionner le trésor aléatoire
                    Tresor tresorChoisi = tresorsTemp[index];

                    // Afficher le trésor obtenu
                    MessageBox.Show($"Trésor obtenu :\nNom : {tresorChoisi.Nom}\nDescription : {tresorChoisi.Description}");

                    // Ajouter le trésor choisi à la liste des trésors du joueur
                    ((App)Application.Current).Joueur.TresorsJoueur.Add(tresorChoisi);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la lecture du fichier CSV : {ex.Message}");
            }
        }

        private void LoadCsvData(List<CsvData> csvDataList)
        {
            string filePath = "../../../CSV/" + ((App)Application.Current).Joueur.NomVillage + ".csv";

            try
            {
                using (TextFieldParser parser = new TextFieldParser(filePath))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(";");

                    if (!parser.EndOfData)
                    {
                        string[] headers = parser.ReadFields();
                    }
                    while (!parser.EndOfData)
                    {
                        string[] fields = parser.ReadFields();
                        if (fields.Length >= 4)
                        {
                            if (fields[1] == "1")
                            {
                                csvDataList.Add(new CsvData { Nom = fields[0], Niveau = fields[2], Description = fields[4] });
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

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

    }

}
