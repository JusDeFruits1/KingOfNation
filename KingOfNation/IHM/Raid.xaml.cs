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
    /// Logique d'interaction pour Raid.xaml
    /// </summary>
    public partial class Raid : Window
    {
        public Raid()
        {
            InitializeComponent();
            DataContext = ((App)Application.Current).Joueur;
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            Game game = new Game();
            game.Show();
            this.Close();
        }

        private void backCaserne(object sender, RoutedEventArgs e)
        {
            Caserne caserne = new Caserne();
            caserne.Show();            
        }

        private void raidPlaine(object sender, RoutedEventArgs e)
        {

        }
    }
}
