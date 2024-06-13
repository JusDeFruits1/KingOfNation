using System;
using KingOfNation.IHM;
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
    /// Logique d'interaction pour Choix.xaml
    /// </summary>
    public partial class Choix : Window
    {

        #region Attributes
        #endregion

        #region Properties

        public string empire = "";
        public string Empire
        {
            get
            {
                return empire;
            }
            set
            {
                empire = value;
            }
        }

        #endregion

        #region Constructors

        public Choix()
        {
            InitializeComponent();
        }

        #endregion

        #region Operations

        private void Romain(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).Joueur.Empire = "Romain";
            Game game = new Game(true);
            game.Show();
            this.Close();
        }

        private void Britannique(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).Joueur.Empire = "Britannique";
            Game game = new Game(true);
            game.Show();
            this.Close();
        }

        private void Nippon(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).Joueur.Empire = "Nippon";
            Game game = new Game(true);
            game.Show();
            this.Close();
        }

        private void Viking(object sender, RoutedEventArgs e)
        {
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





    }
}
