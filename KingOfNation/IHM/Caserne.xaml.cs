using System;
using System.Windows;
using KingOfNation.Code;
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
        #endregion

        #region Properties

        #endregion

        #region Constructor

        public Caserne()
        {
            InitializeComponent();            

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

        private void prodLeger(object sender, RoutedEventArgs e)
        {
            int coutLeger = 20;
            int nb_Hab = ((App)Application.Current).Joueur.Hab;

        }

        private void prodLourd(object sender, RoutedEventArgs e)
        {

        }

        private void prodMDG(object sender, RoutedEventArgs e)
        {

        }

    }
}
