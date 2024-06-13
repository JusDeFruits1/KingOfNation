using KingOfNation.Code;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace KingOfNation.IHM
{
    public partial class Choix : Window
    {
        private BitmapImage[] images;
        private int centerIndex;

        public Choix()
        {
            InitializeComponent();
            // Set initial images
            images = new BitmapImage[6];
            images[0] = new BitmapImage(new Uri("pack://application:,,,/img/Choix/EmpireRomain.jpg"));
            images[1] = new BitmapImage(new Uri("pack://application:,,,/img/Choix/EmpireBritannique.jpg"));
            images[2] = new BitmapImage(new Uri("pack://application:,,,/img/Choix/EmpireNippon.jpg"));
            images[3] = new BitmapImage(new Uri("pack://application:,,,/img/Choix/EmpireViking.jpg"));
            images[4] = new BitmapImage(new Uri("pack://application:,,,/img/Choix/EmpireEgypte.jpg"));
            images[5] = new BitmapImage(new Uri("pack://application:,,,/img/Choix/EmpireAzteque.jpg"));

            centerIndex = 2; // Initial center image index

            UpdateImages();
        }

        private void UpdateImages()
        {
<<<<<<< HEAD
            ((App)Application.Current).Joueur.Empire = "Romain";
            Game game = new Game(true);
            game.Show();
            this.Close();
=======
            LeftImage.Source = images[(centerIndex + 5) % 6];
            CenterImage.Source = images[centerIndex];
            RightImage.Source = images[(centerIndex + 1) % 6];

            ImageNameTextBlock.Text = System.IO.Path.GetFileNameWithoutExtension(images[centerIndex].UriSource.LocalPath);
>>>>>>> parent of 6a6084b (Merge remote-tracking branch 'origin/Yram')
        }

        private void LeftButton_Click(object sender, RoutedEventArgs e)
        {
<<<<<<< HEAD
            ((App)Application.Current).Joueur.Empire = "Britannique";
            Game game = new Game(true);
            game.Show();
            this.Close();
=======
            centerIndex = (centerIndex + 5) % 6;
            UpdateImages();
>>>>>>> parent of 6a6084b (Merge remote-tracking branch 'origin/Yram')
        }

        private void RightButton_Click(object sender, RoutedEventArgs e)
        {
<<<<<<< HEAD
            ((App)Application.Current).Joueur.Empire = "Nippon";
            Game game = new Game(true);
            game.Show();
            this.Close();
=======
            centerIndex = (centerIndex + 1) % 6;
            UpdateImages();
>>>>>>> parent of 6a6084b (Merge remote-tracking branch 'origin/Yram')
        }

        private void ValidateButton_Click(object sender, RoutedEventArgs e)
        {
<<<<<<< HEAD
            ((App)Application.Current).Joueur.Empire = "Viking";
            Game game = new Game(true);
            game.Show();
            this.Close();
        }

        private void Egypte(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).Joueur.Empire = "Egypte";
            Game game = new Game(true);
            game.Show();
            this.Close();
        }

        private void Azteque(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).Joueur.Empire = "Azteque";
            Game game = new Game(true);
            game.Show();
            this.Close();
        }

        #endregion





=======
            if (ImageNameTextBlock.Text == "EmpireRomain")
            {
                MessageBox.Show($"Vous avez sélectionné l'empire : {ImageNameTextBlock.Text}");
                ((App)Application.Current).Joueur.Empire = "Romain";
            }
            if (ImageNameTextBlock.Text == "EmpireBritannique")
            {
                MessageBox.Show($"Vous avez sélectionné l'empire : {ImageNameTextBlock.Text}");
                ((App)Application.Current).Joueur.Empire = "Britannique";
            }
            if (ImageNameTextBlock.Text == "EmpireViking")
            {
                MessageBox.Show($"Vous avez sélectionné l'empire : {ImageNameTextBlock.Text}");
                ((App)Application.Current).Joueur.Empire = "Viking";
            }
            if (ImageNameTextBlock.Text == "EmpireNippon")
            {
                MessageBox.Show($"Vous avez sélectionné l'empire : {ImageNameTextBlock.Text}");
                ((App)Application.Current).Joueur.Empire = "Nippon";
            }
            if (ImageNameTextBlock.Text == "EmpireEgypte")
            {
                MessageBox.Show($"Vous avez sélectionné l'empire : {ImageNameTextBlock.Text}");
                ((App)Application.Current).Joueur.Empire = "Egypte";
                
            }
            if (ImageNameTextBlock.Text == "EmpireAzteque")
            {
                MessageBox.Show($"Vous avez sélectionné l'empire : {ImageNameTextBlock.Text}");
                ((App)Application.Current).Joueur.Empire = "Azteque";
            }
            NouvellePartie nouvellePartie = new NouvellePartie();
            if (nouvellePartie.ShowDialog() == true)
            {
                ((App)Application.Current).Joueur.Pseudo = nouvellePartie.Pseudo;
                ((App)Application.Current).Joueur.NomVillage = nouvellePartie.NomVille;
                // Assuming the path to the existing CSV file
                string sourceFilePath = "../../../CSV/joueur.csv";
                string destinationFilePath = $"../../../CSV/" + ((App)Application.Current).Joueur.NomVillage + ".csv";

                try
                {
                    File.Copy(sourceFilePath, destinationFilePath);
                }
                catch (Exception ex)
                {
                    
                }

                ((App)Application.Current).gamestart = true;
                Game game = new Game(true);
                game.Show();
                this.Close();
            }
        }
>>>>>>> parent of 6a6084b (Merge remote-tracking branch 'origin/Yram')
    }
}
