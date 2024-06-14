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

        private void Former(object sender, RoutedEventArgs e)
        {
            int leger_a_former = (int)nbLeger.Value;
            int lourd_a_former = (int)nbLourd.Value;
            int mdg_a_former = (int)nbMDG.Value;

            int coutLeger = 20;
            int coutLourd = 50;
            int coutMDG = 200;

            int nb_Hab = ((App)Application.Current).Joueur.Hab;

            int coutTotalLeger = leger_a_former * coutLeger;
            int coutTotalLourd = lourd_a_former * coutLourd;
            int coutTotalMDG = mdg_a_former * coutMDG;

            int coutTotal = coutTotalLeger + coutTotalLourd + coutTotalMDG;            

            
            if (leger_a_former == 0 && lourd_a_former == 0 && mdg_a_former == 0)
            {
                MessageBox.Show($"Choisissez une troupe à former !");
            }

            if (leger_a_former >= 1 || lourd_a_former >=1 || mdg_a_former >=1)
            {                
                nb_Hab -= coutLeger;
                nb_Hab -= coutLourd;
                nb_Hab -= coutMDG;
                leger.Nb += leger_a_former;
                leger = new Leger("leger", leger.Nb);
                MessageBox.Show($"Vous avez formé {leger_a_former} soldats {leger.Nom}, {lourd_a_former} soldats {lourd.Nom} et {mdg_a_former} {mdg.Nom}");
                soldatsLeger.Text = leger.Nb.ToString();
            }
        }

        private void prodLourd(object sender, RoutedEventArgs e)
        {

        }

        private void prodMDG(object sender, RoutedEventArgs e)
        {

        }

    }
}
