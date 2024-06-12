using KingOfNation.Code;
using Microsoft.VisualBasic.FileIO;
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
    /// Logique d'interaction pour Destruction.xaml
    /// </summary>
    public partial class Destruction : Window
    {

        #region Attributes

        #endregion

        #region Properties
        #endregion

        #region Constructors

        public Destruction()
        {
            InitializeComponent();
            LoadCsvData();
        }

        #endregion

        #region Operations

        private void LoadCsvData()
        {
            string filePath = "../../../CSV/" + ((App)Application.Current).Joueur.NomVillage + ".csv";

            try
            {
                List<CsvData> csvDataList = new List<CsvData>();
                using (TextFieldParser parser = new TextFieldParser(filePath))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters(";");

                    // Lire la ligne d'en-tête si elle existe
                    if (!parser.EndOfData)
                    {
                        string[] headers = parser.ReadFields();
                        // Vous pouvez utiliser les en-têtes si nécessaire
                    }

                    // Lire les lignes suivantes
                    while (!parser.EndOfData)
                    {
                        string[] fields = parser.ReadFields();
                        if (fields.Length >= 11) // Assurez-vous qu'il y a au moins 11 colonnes
                        {
                            // Ajouter uniquement les lignes où la seconde colonne est "0"
                            if (fields[1] == "1" && fields[0] != "Hotel de ville")
                            {
                                csvDataList.Add(new CsvData { Nom = fields[0], Ressource_produit = fields[5], Cout_construction = fields[10] });
                            }
                        }
                    }
                }

                // Lier les données au ListView
                CsvDataListView.ItemsSource = csvDataList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Le fichier ne peut pas être lu: " + ex.Message);
            }
        }
        private void Back(object sender, RoutedEventArgs e)
        {
            Village village = new Village();
            village.Show();
            this.Close();
        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CsvDataListView.SelectedItem is CsvData selectedData)
                {
                    UpdateCsv(selectedData);
                }
                else
                {
                    MessageBox.Show("Veuillez sélectionner une ligne à mettre à jour.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la mise à jour: " + ex.Message);
            }
        }

        private void UpdateCsv(CsvData selectedData)
        {
            string filePath = "../../../CSV/" + ((App)Application.Current).Joueur.NomVillage + ".csv";
            List<string> lines = new List<string>();

            try
            {
                // Lire toutes les lignes du fichier CSV
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        lines.Add(line);
                    }
                }

                // Mettre à jour la deuxième et la troisième colonne de la ligne sélectionnée
                for (int i = 0; i < lines.Count; i++)
                {
                    string[] fields = lines[i].Split(';');
                    if (fields.Length >= 11 && fields[0] == selectedData.Nom && fields[5] == selectedData.Ressource_produit && fields[10] == selectedData.Cout_construction)
                    {
                        fields[1] = "0";
                        fields[2] = "0";
                        lines[i] = string.Join(";", fields);
                        
                    }
                }

                // Attendre un petit moment avant d'écrire pour s'assurer que tous les flux sont correctement fermés
                System.Threading.Thread.Sleep(100);

                // Écrire toutes les lignes dans le fichier CSV
                using (StreamWriter sw = new StreamWriter(filePath))
                {
                    foreach (string line in lines)
                    {
                        sw.WriteLine(line);
                    }
                }

                // Recharger les données du CSV
                LoadCsvData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la mise à jour du CSV: " + ex.Message);
            }
        }

        #endregion

    }
}
