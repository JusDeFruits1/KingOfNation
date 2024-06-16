using KingOfNation.Code;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Logique d'interaction pour ChargerPartie.xaml
    /// </summary>
    public partial class ChargerPartie : Window
    {

        #region Attributes

        private string directoryPath = @"DataSave";

        #endregion

        #region Properties



        #endregion

        #region Constructor

        public ChargerPartie()
        {
            InitializeComponent();
            LoadJsonFiles();
        }

        #endregion

        #region Operations

        private void LoadJsonFiles()
        {
            if (Directory.Exists(directoryPath))
            {
                string[] jsonFiles = Directory.GetFiles(directoryPath, "*.json");

                foreach (string filePath in jsonFiles)
                {
                    fileListBox.Items.Add(System.IO.Path.GetFileName(filePath));
                }
            }
            else
            {
                MessageBox.Show("Le répertoire spécifié n'existe pas.");
            }
        }

        private void Valider_Click(object sender, RoutedEventArgs e)
        {
            if (fileListBox.SelectedItem != null)
            {
                string selectedFileName = (string)fileListBox.SelectedItem;
                string filePath = System.IO.Path.Combine(directoryPath, selectedFileName);

                ((App)Application.Current).Joueur = Joueur.DeserializeFromFile(filePath);
                Game game = new Game(true);
                game.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner un fichier JSON.");
            }
        }
    }

    #endregion

}

