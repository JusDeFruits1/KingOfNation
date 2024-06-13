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
    /// Logique d'interaction pour Amelioration.xaml
    /// </summary>
    public partial class Amelioration : Window
    {
        #region Attributes



        #endregion

        #region Properties



        #endregion

        #region Constructors

        public Amelioration()
        {
            InitializeComponent();
            LoadCsvData();

            ((App)Application.Current).timerJ.Tick += afficherBois;
            ((App)Application.Current).timerJ.Tick += afficherPierre;
            ((App)Application.Current).timerJ.Tick += afficherFer;
            ((App)Application.Current).timerJ.Tick += afficherOr;
            ((App)Application.Current).timerJ.Tick += afficherHab;
            ((App)Application.Current).timerJ.Start();

        }

        #endregion

        #region Operations

        private void LoadCsvData()
        {
            string filePath = "../../../CSV/joueur.csv";

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
                        string[]? fields = parser.ReadFields();
                        if (fields.Length >= 11) // Assurez-vous qu'il y a au moins 11 colonnes
                        {
                            // Ajouter uniquement les lignes où la seconde colonne est "0"
                            if (fields[1] == "1")
                            {
                                csvDataList.Add(new CsvData { Nom = fields[0], Niveau = fields[2], NiveauMax = fields[3], Cout_en_Or = fields[16], Mat_Amelio1 = fields[17], Cout_Mat_Amelio1 = fields[18], Mat_Amelio2 = fields[19], Cout_Mat_Amelio2 = fields[20], Mult_Amelio = fields[21] });
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

        private void CsvDataListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CsvDataListView.SelectedItem is CsvData selectedData)
            {
                // Accéder à la valeur de la 5ème colonne (Qt_rs_constru1)
                int coutMateriau1;
                int coutMateriau2;
                string? batselec = selectedData.Nom;
                string? mat1 = selectedData.Mat_Amelio1;
                string? mat2 = selectedData.Mat_Amelio2;


                // Effectuer l'opération souhaitée avec coutMateriaux1
                //MessageBox.Show($"mat 1: {mat1}, coutMat1 : {coutMateriau1}, bat selec : {batselec} ");
                if (string.IsNullOrEmpty(selectedData.Cout_Mat_Amelio1) || selectedData.Cout_Mat_Amelio1 == "NULL")
                {
                    coutMateriau1 = 0;
                }
                else
                {
                    coutMateriau1 = Convert.ToInt32(selectedData.Cout_Mat_Amelio1);
                }

                //MessageBox.Show($"mat 1: {mat1}, coutMat1 : {coutMateriau1}, bat selec : {batselec} ");
                if (string.IsNullOrEmpty(selectedData.Cout_Mat_Amelio2) || selectedData.Cout_Mat_Amelio2 == "NULL")
                {
                    coutMateriau2 = 0;
                }
                else
                {
                    coutMateriau2 = Convert.ToInt32(selectedData.Cout_Mat_Amelio2);
                }
                if (coutMateriau1 == coutMateriau2 && mat2 == "NULL" && mat2 == "NULL")
                {
                    MessageBox.Show("Vous avez besoin d'aucune ressource pour améliorer ce bâtiment");
                }
                else
                {
                    MessageBox.Show($"Vous avez besoin de {coutMateriau1} de {mat1} et {coutMateriau2} de {mat2} pour améliorer ce bâtiment");
                }

            }
        }

        private void Améliorer(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CsvDataListView.SelectedItem is CsvData selectedData)
                {
                    int coutMateriau1 = 0;
                    int coutMateriau2 = 0;
                    int coutAmelioration = 0;

                    string? nomMateriau1 = selectedData.Mat_Amelio1;
                    string? nomMateriau2 = selectedData.Mat_Amelio2;

                    int nb_Bois = ((App)Application.Current).bois;
                    int nb_Pierre = ((App)Application.Current).pierre;
                    int nb_Fer = ((App)Application.Current).fer;
                    int nb_Or = ((App)Application.Current).or;

                    // Convertir multiAmelio en entier
                    int multiAmelio = 1; // Valeur par défaut
                    if (!string.IsNullOrEmpty(selectedData.Mult_Amelio) && int.TryParse(selectedData.Mult_Amelio, out int tempMultiAmelio))
                    {
                        multiAmelio = tempMultiAmelio;
                    }

                    if (!string.IsNullOrEmpty(selectedData.Cout_en_Or) && selectedData.Cout_en_Or != "NULL" && int.TryParse(selectedData.Cout_en_Or, out int tempCoutAmelioration))
                    {
                        coutAmelioration = tempCoutAmelioration;
                    }

                    if (!string.IsNullOrEmpty(selectedData.Cout_Mat_Amelio1) && selectedData.Cout_Mat_Amelio1 != "NULL" && int.TryParse(selectedData.Cout_Mat_Amelio1, out int tempCoutMateriau1))
                    {
                        coutMateriau1 = tempCoutMateriau1;
                    }

                    if (!string.IsNullOrEmpty(selectedData.Cout_Mat_Amelio2) && selectedData.Cout_Mat_Amelio2 != "NULL" && int.TryParse(selectedData.Cout_Mat_Amelio2, out int tempCoutMateriau2))
                    {
                        coutMateriau2 = tempCoutMateriau2;
                    }

                    if (nb_Or < coutAmelioration)
                    {
                        MessageBox.Show("Vous n'avez pas assez d'or pour construire ce bâtiment");
                    }
                    else
                    {
                        if (nomMateriau1 == "Bois")
                        {
                            if (nomMateriau2 == "NULL")
                            {
                                if (nb_Bois >= coutMateriau1)
                                {
                                    nb_Bois -= coutMateriau1;
                                    nb_Or -= coutAmelioration; // Déduire le coût en or aussi
                                    ((App)Application.Current).bois = nb_Bois;
                                    ((App)Application.Current).or = nb_Or;
                                    nbBois.Text = nb_Bois.ToString();

                                    // Mise à jour du coût d'amélioration
                                    coutAmelioration *= multiAmelio;
                                    selectedData.Cout_en_Or = coutAmelioration.ToString();

                                    UpdateCsv(selectedData);
                                    MessageBox.Show($"Bâtiment construit ! Coût matériaux 1: {coutMateriau1}");
                                }
                                else
                                {
                                    MessageBox.Show("Vous n'avez pas assez de bois pour construire ce bâtiment");
                                }
                            }
                            else if (nomMateriau2 == "Pierre")
                            {
                                if (nb_Bois >= coutMateriau1 && nb_Pierre >= coutMateriau2 && nb_Or >= coutAmelioration)
                                {
                                    nb_Bois -= coutMateriau1;
                                    nb_Pierre -= coutMateriau2;
                                    nb_Or -= coutAmelioration; // Déduire le coût en or aussi
                                    ((App)Application.Current).bois = nb_Bois;
                                    ((App)Application.Current).pierre = nb_Pierre;
                                    ((App)Application.Current).or = nb_Or;
                                    nbBois.Text = nb_Bois.ToString();

                                    

                                    // Mise à jour du coût d'amélioration
                                    //coutAmelioration *= multiAmelio;            
                                    selectedData.Cout_en_Or = coutAmelioration.ToString();
                                    LoadCsvData();
                                    MessageBox.Show($"Cout en or = {coutAmelioration}");
                                    UpdateCsv(selectedData);
                                    MessageBox.Show($"Bâtiment construit ! Coût matériaux 1: {coutMateriau1}");
                                }
                                else
                                {
                                    MessageBox.Show("Vous n'avez pas assez de matériaux pour construire ce bâtiment");
                                }
                            }
                        }
                        else
                        {
                            // Si d'autres matériaux sont utilisés, mettre à jour en conséquence
                            // Code de gestion pour d'autres matériaux
                            UpdateCsv(selectedData);
                        }
                    }
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

        private void afficherBois(object sender, EventArgs e)
        {
            nbBois.Text = ((App)Application.Current).bois.ToString();
        }

        private void afficherPierre(object sender, EventArgs e)
        {
            nbPierre.Text = ((App)Application.Current).pierre.ToString();
        }

        private void afficherFer(object sender, EventArgs e)
        {
            nbFer.Text = ((App)Application.Current).fer.ToString();
        }

        private void afficherOr(object sender, EventArgs e)
        {
            nbOr.Text = ((App)Application.Current).or.ToString();
        }

        private void afficherHab(object sender, EventArgs e)
        {
            nbHab.Text = ((App)Application.Current).hab.ToString();
        }

        private void UpdateCsv(CsvData selectedData)
        {
            string filePath = "../../../CSV/joueur.csv";
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

                // Mettre à jour la troisième colonne de la ligne sélectionnée
                for (int i = 0; i < lines.Count; i++)
                {
                    string[] fields = lines[i].Split(';');
                    if (fields.Length >= 11 && fields[0] == selectedData.Nom && fields[2] == selectedData.Niveau && fields[3] == selectedData.NiveauMax && fields[2][0] < fields[3][0] && fields[16] == selectedData.Cout_en_Or && fields[18] == selectedData.Cout_Mat_Amelio1 && fields[18] == selectedData.Cout_Mat_Amelio2 && fields[21] == selectedData.Mult_Amelio)
                    {
                        int nv = Int32.Parse(fields[2]);
                        nv++;
                        int cout = Int32.Parse(fields[16]);
                        int mult = Int32.Parse(fields[21]);

                        cout *= mult;

                        fields[16] = Convert.ToString(cout);
                        fields[21] = Convert.ToString(mult);
                        fields[2] = Convert.ToString(nv);

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
