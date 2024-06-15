using KingOfNation.Code;
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

namespace KingOfNation.IHM
{
    /// <summary>
    /// Logique d'interaction pour Taverne.xaml
    /// </summary>
    public partial class Taverne : Window
    {
        public Taverne()
        {
            WMPLib.WindowsMediaPlayer musicGame = ((App)Application.Current).musicGame;
            InitializeComponent();
            musicGame.controls.stop();
            musicGame.URL = "Taverne.mp3";
            musicGame.controls.play();

            // Charger les lieutenants
            LoadLieutenants();
        }

        private void Home(object sender, RoutedEventArgs e)
        {
            Game game = new Game(true);
            game.Show();
            this.Close();
        }

        private void FarfadetMalicieux(object sender, RoutedEventArgs e)
        {
            Farfadet farfadet = new Farfadet();
            farfadet.Show();
            this.Close();
        }

        private void LoadLieutenants()
        {
            List<Lieutenant> lieutenants = new List<Lieutenant>
            {
                new Lieutenant { Id = "1", Nom = "Gimli", Prix = 200, Metier = "Bucheron", buff = "Améliore de 20% la production de bois", ImagPath = "../img/Lieutenant/nain.jpg" },
                new Lieutenant { Id = "2", Nom = "Etienne Lantier", Prix = 300, Metier = "Galibot", buff = "Améliore de 20% la production de pierre", ImagPath = "../img/Lieutenant/mineur.jpg" },
                new Lieutenant { Id = "3", Nom = "Hephaïstos", Prix = 400, Metier = "dieu", buff = "Améliore de 20% la production de fer", ImagPath = "../img/Lieutenant/mineurF.jpg" },
                new Lieutenant { Id = "4", Nom = "Arthur Morgan", Prix = 550, Metier = "Chercher d'or", buff = "Améliore de 20% la production d'or", ImagPath = "../img/Lieutenant/chercheurOr.jpg" },
                new Lieutenant { Id = "5", Nom = "Mia", Prix = 150, Metier = "déesse", buff = "Améliore de 20% la production d'habitant", ImagPath = "../img/Lieutenant/deesse.jpg" }
            };

            LieutenantListBox.ItemsSource = lieutenants;
        }
        private void LouerService_Click(object sender, RoutedEventArgs e)
        {
            if (LieutenantListBox.SelectedItem is Lieutenant selectedLieutenant)
            {
                // Vérifier si la liste des lieutenants loués n'est pas vide
                string idLieutenant = ((App)Application.Current).Joueur.LieutenantList.Count > 0 ? ((App)Application.Current).Joueur.LieutenantList[0].Id : null;

                if (idLieutenant == null || idLieutenant != selectedLieutenant.Id)
                {
                    // Vérifier si le joueur a assez d'or pour louer le lieutenant
                    if (((App)Application.Current).Joueur.Or >= selectedLieutenant.Prix)
                    {
                        // Déduire le prix du lieutenant de l'or du joueur
                        ((App)Application.Current).Joueur.Or -= selectedLieutenant.Prix;

                        // Afficher un message de confirmation
                        MessageBox.Show($"Vous avez loué les service de {selectedLieutenant.Nom} pour {selectedLieutenant.Prix} d'or");

                        // Supprime l'ancien lieutenant et ajouter le nouveau lieutenant à la liste lieutenant loué
                        ((App)Application.Current).Joueur.LieutenantList.Clear();
                        ((App)Application.Current).Joueur.LieutenantList.Add(selectedLieutenant);
                    }
                    else
                    {
                        // Afficher un message si le joueur n'a pas assez d'or
                        MessageBox.Show("Vous n'avez pas assez d'or pour louer les service de ce lieutenant.");
                    }
                }
                else
                {
                    MessageBox.Show("Ce lieutenant est déjà à votre service");
                }
            }
            else
            {
                // Afficher un message si aucun lieutenant n'est sélectionné
                MessageBox.Show("Veuillez sélectionner un lieutenant.");
            }
        }
    }
}

