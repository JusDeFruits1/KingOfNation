using System;
using Microsoft.VisualBasic.FileIO;
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
    /// Logique d'interaction pour Farfadet.xaml
    /// </summary>
    public partial class Farfadet : Window
    {
        private Random _random;
        public Farfadet()
        {
            WMPLib.WindowsMediaPlayer musicGame = ((App)Application.Current).musicGame;
            InitializeComponent();
            _random = new Random();

            musicGame.controls.stop();
            musicGame.URL = "FarfadetM.mp3";
            musicGame.controls.play();
            FarfadetMalicieux.Text = "Bienvenue dans la tanière du Farfadet malicieux. \n Ici je réalise moult espieglerie \n Souhaite tu que je te réalise un petit tour ? \n Je peux en échange de 800 pièces d'or";
        }
        private void RefuserClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Le Farfadet malicieux se vexe de votre morosité \n Il vous expédie hors de sa tanière");
            ((App)Application.Current).gamestart = true;
            Game game = new Game(true);
            game.Show();
            this.Close();
        }
        private void AccepterClick(object sender, RoutedEventArgs e)
        {
            if (((App)Application.Current).Joueur.Or >= 800)
            {
                ((App)Application.Current).Joueur.Or -= 800;
                int randomNumber = _random.Next(1, 8);
                if (randomNumber == 1)
                {
                    MessageBox.Show("Le farfadet malicieux use de sa magie");
                    ((App)Application.Current).Joueur.Or *= 2;
                    WMPLib.WindowsMediaPlayer musicGame = ((App)Application.Current).musicGame;
                    musicGame.controls.stop();
                    ((App)Application.Current).gamestart = true;
                    Game game = new Game(true);
                    game.Show();
                    this.Close();
                }
                else if (randomNumber == 2)
                {
                    MessageBox.Show("Le farfadet malicieux fait appel a sa magie");
                    ((App)Application.Current).Joueur.Bois /= 2;
                    ((App)Application.Current).Joueur.Pierre /= 2;
                    WMPLib.WindowsMediaPlayer musicGame = ((App)Application.Current).musicGame;
                    musicGame.controls.stop();
                    ((App)Application.Current).gamestart = true;
                    Game game = new Game(true);
                    game.Show();
                    this.Close();
                }
                else if (randomNumber == 3)
                {
                    MessageBox.Show("Le farfadet malicieux fait appel a sa fourberie");
                    MessageBox.Show("Une épidemié de peste noir s'abat sur votre village");
                    ((App)Application.Current).Joueur.Hab = 20;
                    WMPLib.WindowsMediaPlayer musicGame = ((App)Application.Current).musicGame;
                    musicGame.controls.stop();
                    ((App)Application.Current).gamestart = true;
                    Game game = new Game(true);
                    game.Show();
                    this.Close();
                }
                else if (randomNumber == 4)
                {
                    MessageBox.Show("Le farfadet malicieux lance un sort");
                    MessageBox.Show("Quelque chose d'étrange s'est produit");
                    WMPLib.WindowsMediaPlayer musicGame = ((App)Application.Current).musicGame;
                    musicGame.controls.stop();
                    ((App)Application.Current).gamestart = true;
                    Game game = new Game(true);
                    game.Show();
                    this.Close();
                }
                else if (randomNumber == 5)
                {
                    MessageBox.Show("Le farfadet malicieux fait usage d'un enchantement");
                    ((App)Application.Current).Joueur.Bois += 500;
                    ((App)Application.Current).Joueur.Pierre += 500;
                    ((App)Application.Current).Joueur.Fer += 400;
                    ((App)Application.Current).Joueur.Or += 400;
                    WMPLib.WindowsMediaPlayer musicGame = ((App)Application.Current).musicGame;
                    musicGame.controls.stop();
                    ((App)Application.Current).gamestart = true;
                    Game game = new Game(true);
                    game.Show();
                    this.Close();
                }
                else if (randomNumber == 6)
                {
                    MessageBox.Show("En utilisant la cabale, le farfadet malicieux conjura sa sournoisie");
                    ((App)Application.Current).Joueur.Bois *= 3;
                    ((App)Application.Current).Joueur.Fer *= 2;
                    WMPLib.WindowsMediaPlayer musicGame = ((App)Application.Current).musicGame;
                    musicGame.controls.stop();
                    ((App)Application.Current).gamestart = true;
                    Game game = new Game(true);
                    game.Show();
                    this.Close();
                }
                else if (randomNumber == 7)
                {
                    WMPLib.WindowsMediaPlayer musicGame = ((App)Application.Current).musicGame;
                    musicGame.controls.stop();
                    MessageBox.Show("Le farfadet malicieux fait usage de son sort le plus puissant");
                    musicGame.URL = "rire.mp3";
                    musicGame.controls.play();
                    MessageBox.Show("Vous avez une drole de sensation et \n vous apprecevez le farfadet vous faire un drôle de sourire...");
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Le farfadet malicieux n'aime pas ceux qui essais de l'arnaquer \n il vous jetes un sort pour vous faire sortir de sa tanière");
                ((App)Application.Current).gamestart = true;
                Game game = new Game(true);
                game.Show();
                this.Close();
            }
        }
    }
}


