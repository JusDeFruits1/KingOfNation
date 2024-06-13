using KingOfNation.Code;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Logique d'interaction pour Village.xaml
    /// </summary>
    public partial class Village : Window
    {
        #region Attributes

        #endregion

        #region Properties

        #endregion

        #region Constructors
        public Village()
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
                        if (fields.Length >= 4) // Assurez-vous qu'il y a au moins 2 colonnes
                        {
                            // Ajouter uniquement les lignes où la seconde colonne est "1"
                            if (fields[1] == "1")
                            {
                                csvDataList.Add(new CsvData { Nom = fields[0], Niveau = fields[2], Description = fields[4] });
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
        private void Home(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).OpenGame();
            this.Close();
        }
        private void OpenConstruction(object sender, RoutedEventArgs e)
        {
            Construction construction = new Construction();
            construction.Show();
            this.Close();
        }
        private void OpenAmelioration(object sender, RoutedEventArgs e)
        {
            Amelioration amelioration = new Amelioration();
            amelioration.Show();
            this.Close();
        }
        private void OpenDetruire(object sender, RoutedEventArgs e)
        {
            Destruction destruction = new Destruction();
            destruction.Show();
            this.Close();
        }

        #endregion

    }

    public class CsvData
    {
        #region Attributes
        private string? _coutEnOr;
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Properties

        public string Nom { get; set; }
        public string Niveau { get; set; }
        public string NiveauMax { get; set; }
        public string Description { get; set; }
        public string Ressource_produit { get; set; }
        public string Cout_construction { get; set; }
        public string Rs_construction1 { get; set; }
        public string Rs_construction2 { get; set; }
        public string Qt_rs_constru1 { get; set; }
        public string Qt_rs_constru2 { get; set; }
        public string? Cout_en_Or
        {
            get => _coutEnOr;
            set
            {
                _coutEnOr = value;
                OnPropertyChanged(nameof(Cout_en_Or));
            }
        }
        public string? Mat_Amelio1 { get; set; }
        public string? Cout_Mat_Amelio1 { get; set; }
        public string? Mat_Amelio2 { get; set; }
        public string? Cout_Mat_Amelio2 { get; set; }
        public string? Mult_Amelio { get; set; }
        #endregion

        #region Constructors
        #endregion

        #region Operations
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion        
    }
}