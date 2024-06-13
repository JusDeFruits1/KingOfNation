using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Threading;
using System.Printing;
using System.Media;
using Microsoft.VisualBasic.FileIO;
using System.Diagnostics;
using KingOfNation.Code;
using System.Text.Json;

namespace KingOfNation.IHM
{

    /// <summary>
    /// Logique d'interaction pour Game.xaml
    /// </summary>
    public partial class Game : Window
    {
        #region Attributes

        List<CsvData> csvDataList = new List<CsvData>();
        string nv = "0";

        #endregion

        #region Properties
        #endregion

        #region Constructors

        public Game(bool play_music = false)
        {
            InitializeComponent();
            LoadCsvData(csvDataList);
            DataContext = this;
            if (play_music)
            {
                WMPLib.WindowsMediaPlayer musicGame = ((App)Application.Current).musicGame;
                musicGame.URL = "ClashofClans.mp3";
                musicGame.controls.play();
            }
            

            foreach (CsvData elt in csvDataList)
            {
                if (elt.Nom == "Hotel de ville")
                {
                    nv = elt.Niveau;
                }
            }

            imgVillage.Source = new BitmapImage(new Uri(@"../img/Village/" + ((App)Application.Current).Joueur.Empire + nv + ".png", UriKind.Relative));
            ((App)Application.Current).timerJ.Tick += afficherBois;
            ((App)Application.Current).timerJ.Tick += afficherPierre;
            ((App)Application.Current).timerJ.Tick += afficherFer;
            ((App)Application.Current).timerJ.Tick += afficherOr;
            ((App)Application.Current).timerJ.Tick += afficherHab;
            ((App)Application.Current).timerJ.Start();
        }

        #endregion

        #region Operations
        private void Save(object sender, EventArgs e)
        {
            // Sérialisation
            Joueur joueur = new Joueur(((App)Application.Current).Joueur.Pseudo, ((App)Application.Current).Joueur.Empire, ((App)Application.Current).Joueur.NomVillage, ((App)Application.Current).Joueur.Bois, ((App)Application.Current).Joueur.Pierre, ((App)Application.Current).Joueur.Fer, ((App)Application.Current).Joueur.Or, ((App)Application.Current).Joueur.Hab, ((App)Application.Current).Joueur.TresorsJoueur);
            ((App)Application.Current).Joueur.SerializeToFile("DataSave");
        }

        private void Village(object sender, RoutedEventArgs e)
        {
            Village village = new Village();
            village.Show();
            this.Close();
        }

        private void Taverne(object sender, RoutedEventArgs e)
        {
            foreach (CsvData elt in csvDataList)
            {
                if (elt.Nom == "Taverne")
                {
                    Taverne taverne = new Taverne();
                    taverne.Show();
                    this.Close();
                }
            }
        }

        private void Caserne(object sender, RoutedEventArgs e)
        {
            foreach (CsvData elt in csvDataList)
            {
                if (elt.Nom == "Caserne")
                {
                    Taverne taverne = new Taverne();
                    ((App)Application.Current).musicGame.controls.stop();
                    taverne.Show();
                    this.Close();
                }
            }
        }

        private void Boutique(object sender, RoutedEventArgs e)
        {
            foreach (CsvData elt in csvDataList)
            {
                if (elt.Nom == "Boutique")
                {
                    Boutique boutique = new Boutique();
                    ((App)Application.Current).musicGame.controls.stop();
                    boutique.Show();
                    this.Close();
                }
            }
        }

        private void Attaque(object sender, RoutedEventArgs e)
        {
            foreach (CsvData elt in csvDataList)
            {
                if (elt.Nom == "Camp d'entrainement")
                {
                    Taverne taverne = new Taverne();
                    ((App)Application.Current).musicGame.controls.stop();
                    taverne.Show();
                    this.Close();
                }
            }
        }

        private void afficherBois(object sender, EventArgs e)
        {
            nbBois.Text = ((App)Application.Current).Joueur.Bois.ToString();
        }

        private void afficherPierre(object sender, EventArgs e)
        {

            nbPierre.Text = ((App)Application.Current).Joueur.Pierre.ToString();
        }

        private void afficherFer(object sender, EventArgs e)
        {

            nbFer.Text = ((App)Application.Current).Joueur.Fer.ToString();
        }

        private void afficherOr(object sender, EventArgs e)
        {

            nbOr.Text = ((App)Application.Current).Joueur.Or.ToString();
        }

        private void afficherHab(object sender, EventArgs e)
        {

            nbHab.Text = ((App)Application.Current).Joueur.Hab.ToString();
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
                        if (fields.Length >= 11) // Assurez-vous qu'il y a au moins 11 colonnes
                        {
                            // Ajouter uniquement les lignes où la seconde colonne est "0"
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

        #endregion

    }
}
