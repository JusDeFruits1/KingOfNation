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
    }
}
