using System;
using System.ComponentModel;
using System.IO;
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
        private string saveDirectory = "DataSave";
        #endregion

        #region Properties

        #endregion

        #region Constructor

        public Caserne()
        {
            InitializeComponent();
            DataContext = ((App)Application.Current).Joueur;            

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

            if (((App)Application.Current).Joueur.Leger == null)
            {
                ((App)Application.Current).Joueur.Leger = new Leger("leger", 0);
            }
            if (lourd == null)
            {
                ((App)Application.Current).Joueur.Lourd = new Lourd("lourd", 0);
            }
            if (mdg == null)
            {
                ((App)Application.Current).Joueur.Mdg = new Machine_de_guerre("machines de guerre", 0);
            }

            // Calcul du coût total en habitants pour former les troupes
            int coutTotalLeger = leger_a_former * coutLeger;
            int coutTotalLourd = lourd_a_former * coutLourd;
            int coutTotalMDG = mdg_a_former * coutMDG;

            int coutTotal = coutTotalLeger + coutTotalLourd + coutTotalMDG;

            // Vérification des conditions
            if (leger_a_former == 0 && lourd_a_former == 0 && mdg_a_former == 0)
            {
                MessageBox.Show("Choisissez une troupe à former !");
                return;
            }

            if (nb_Hab < coutTotal)
            {
                MessageBox.Show("Vous n'avez pas assez d'habitants pour former toutes ces troupes !");
                return;
            }

            // Mise à jour des valeurs
            nb_Hab -= coutTotal;
            ((App)Application.Current).Joueur.Hab = nb_Hab;

            // Mise à jour des nombres de soldats
            ((App)Application.Current).Joueur.Leger.Nb += leger_a_former;
            ((App)Application.Current).Joueur.Lourd.Nb += lourd_a_former;
            ((App)Application.Current).Joueur.Mdg.Nb += mdg_a_former;

            // Affichage des informations
            MessageBox.Show($"Vous avez formé {leger_a_former} soldats légers, {lourd_a_former} soldats lourds et {mdg_a_former} machines de guerre.");
            troupeLeger.Text = ((App)Application.Current).Joueur.Leger.Nb.ToString();
            troupeLourd.Text = ((App)Application.Current).Joueur.Lourd.Nb.ToString();
            troupeMDG.Text = ((App)Application.Current).Joueur.Mdg.Nb.ToString();
            
        }        
    }
}
