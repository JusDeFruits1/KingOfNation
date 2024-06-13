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
                            if (fields[1] == "1")
                            {
                                csvDataList.Add(new CsvData { Nom = fields[0], Niveau = fields[2], NiveauMax = fields[3] });
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
<<<<<<< HEAD
                    int coutMateriau1 = 0;
                    int coutMateriau2 = 0;
                    int coutAmelioration = 0;

                    string? nomMateriau1 = selectedData.Mat_Amelio1;
                    string? nomMateriau2 = selectedData.Mat_Amelio2;

                    int nb_Bois = ((App)Application.Current).Joueur.Bois;
                    int nb_Pierre = ((App)Application.Current).Joueur.Pierre;
                    int nb_Fer = ((App)Application.Current).Joueur.Fer;
                    int nb_Or = ((App)Application.Current).Joueur.Or;

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
                                if (nb_Bois >= coutMateriau1 && nb_Or >= coutAmelioration)
                                {
                                    nb_Bois -= coutMateriau1;
                                    nb_Or -= coutAmelioration;

                                    ((App)Application.Current).Joueur.Bois = nb_Bois;
                                    ((App)Application.Current).Joueur.Or = nb_Or;

                                    nbBois.Text = nb_Bois.ToString();
                                    nbOr.Text = nb_Or.ToString();

                                    // Mise à jour du coût d'amélioration                                                                                  
                                    LoadCsvData();

                                    UpdateCsv(selectedData);
                                    MessageBox.Show($"Bâtiment amélioré ! Matériaux utilisés : {coutMateriau1} de {nomMateriau1} et {coutMateriau2} de {nomMateriau2} \n Coût d'amélioration : {coutAmelioration} Gold");
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
                                    nb_Or -= coutAmelioration;

                                    ((App)Application.Current).Joueur.Bois = nb_Bois;
                                    ((App)Application.Current).Joueur.Pierre = nb_Pierre;
                                    ((App)Application.Current).Joueur.Or = nb_Or;

                                    nbBois.Text = nb_Bois.ToString();
                                    nbPierre.Text = nb_Pierre.ToString();
                                    nbOr.Text = nb_Or.ToString();


                                    // Mise à jour du coût d'amélioration                                                                                  
                                    LoadCsvData();

                                    UpdateCsv(selectedData);
                                    MessageBox.Show($"Bâtiment amélioré  ! Matériaux utilisés : {coutMateriau1} de {nomMateriau1} et {coutMateriau2} de {nomMateriau2} \n Coût d'amélioration : {coutAmelioration} Gold");
                                }
                                else
                                {
                                    MessageBox.Show("Vous n'avez pas assez de matériaux pour construire ce bâtiment");
                                }
                            }

                            else if (nomMateriau2 == "Fer")
                            {
                                if (nb_Bois >= coutMateriau1 && nb_Fer >= coutMateriau2 && nb_Or >= coutAmelioration)
                                {
                                    nb_Bois -= coutMateriau1;
                                    nb_Fer -= coutMateriau2;
                                    nb_Or -= coutAmelioration;

                                    ((App)Application.Current).Joueur.Bois = nb_Bois;
                                    ((App)Application.Current).Joueur.Fer = nb_Fer;
                                    ((App)Application.Current).Joueur.Or = nb_Or;

                                    nbBois.Text = nb_Bois.ToString();
                                    nbFer.Text = nb_Fer.ToString();
                                    nbOr.Text = nb_Or.ToString();

                                    LoadCsvData();

                                    UpdateCsv(selectedData);
                                    MessageBox.Show($"Bâtiment amélioré ! Matériaux utilisés : {coutMateriau1} de {nomMateriau1} et {coutMateriau2} de {nomMateriau2} \n Coût d'amélioration : {coutAmelioration} Gold");

                                }
                                else
                                {
                                    MessageBox.Show("Vous n'avez pas assez de matériaux pour construire ce bâtiment");
                                }
                            }

                            else if (nomMateriau2 == "Gold")
                            {
                                if (nb_Bois >= coutMateriau1 && nb_Or >= coutMateriau2 + coutAmelioration)
                                {
                                    nb_Bois -= coutMateriau1;
                                    nb_Or -= coutMateriau2;
                                    nb_Or -= coutAmelioration;

                                    ((App)Application.Current).Joueur.Bois = nb_Bois;
                                    ((App)Application.Current).Joueur.Or = nb_Or;

                                    nbBois.Text = nb_Bois.ToString();
                                    nbOr.Text = nb_Or.ToString();

                                    LoadCsvData();

                                    UpdateCsv(selectedData);
                                    MessageBox.Show($"Bâtiment amélioré ! Matériaux utilisés : {coutMateriau1} de {nomMateriau1} et {coutMateriau2} de {nomMateriau2} \n Coût d'amélioration : {coutAmelioration} Gold");
                                }
                            }

                        }

                        else if (nomMateriau1 == "Pierre")
                        {
                            if (nomMateriau2 == "NULL")
                            {
                                if (nb_Pierre >= coutMateriau1 && nb_Or >= coutAmelioration)
                                {
                                    nb_Pierre -= coutMateriau1;
                                    nb_Or -= coutAmelioration;

                                    ((App)Application.Current).Joueur.Pierre = nb_Pierre;
                                    ((App)Application.Current).Joueur.Or = nb_Or;

                                    nbPierre.Text = nb_Pierre.ToString();
                                    nbOr.Text = nb_Or.ToString();
                                    // Mise à jour du coût d'amélioration                                                                                  
                                    LoadCsvData();

                                    UpdateCsv(selectedData);
                                    MessageBox.Show($"Bâtiment amélioré ! Matériaux utilisés : {coutMateriau1} de {nomMateriau1} et {coutMateriau2} de {nomMateriau2} \n Coût d'amélioration : {coutAmelioration} Gold");
                                }

                                else
                                {
                                    MessageBox.Show("Vous n'avez pas assez de pierre pour construire ce bâtiment");
                                }
                            }

                            else if (nomMateriau2 == "Bois")
                            {
                                if (nb_Pierre >= coutMateriau1 && nb_Bois >= coutMateriau2 && nb_Or >= coutAmelioration)
                                {
                                    nb_Pierre -= coutMateriau1;
                                    nb_Bois -= coutMateriau2;
                                    nb_Or -= coutAmelioration;

                                    ((App)Application.Current).Joueur.Pierre = nb_Pierre;
                                    ((App)Application.Current).Joueur.Bois = nb_Bois;
                                    ((App)Application.Current).Joueur.Or = nb_Or;

                                    nbPierre.Text = nb_Pierre.ToString();
                                    nbBois.Text = nb_Bois.ToString();
                                    nbOr.Text = nb_Or.ToString();

                                    // Mise à jour du coût d'amélioration                                                                                  
                                    LoadCsvData();

                                    UpdateCsv(selectedData);
                                    MessageBox.Show($"Bâtiment amélioré ! Matériaux utilisés : {coutMateriau1} de {nomMateriau1} et {coutMateriau2} de {nomMateriau2} \n Coût d'amélioration : {coutAmelioration} Gold");
                                }
                                else
                                {
                                    MessageBox.Show("Vous n'avez pas assez de ressource pour construire ce bâtiment");
                                }
                            }

                            else if (nomMateriau2 == "Fer")
                            {
                                if (nb_Pierre >= coutMateriau1 && nb_Fer >= coutMateriau2 && nb_Or >= coutAmelioration)
                                {
                                    nb_Pierre -= coutMateriau1;
                                    nb_Fer -= coutMateriau2;
                                    nb_Or -= coutAmelioration;

                                    ((App)Application.Current).Joueur.Pierre = nb_Pierre;
                                    ((App)Application.Current).Joueur.Fer = nb_Fer;
                                    ((App)Application.Current).Joueur.Or = nb_Or;

                                    nbPierre.Text = nb_Pierre.ToString();
                                    nbFer.Text = nb_Fer.ToString();
                                    nbOr.Text = nb_Or.ToString();

                                    // Mise à jour du coût d'amélioration                                                                                  
                                    LoadCsvData();

                                    UpdateCsv(selectedData);
                                    MessageBox.Show($"Bâtiment amélioré ! Matériaux utilisés : {coutMateriau1} de {nomMateriau1} et {coutMateriau2} de {nomMateriau2} \n Coût d'amélioration : {coutAmelioration} Gold");
                                }
                                else
                                {
                                    MessageBox.Show("Vous n'avez pas assez de ressource pour construire ce bâtiment");
                                }
                            }

                            else if (nomMateriau2 == "Gold")
                            {
                                if (nb_Pierre >= coutMateriau1 && nb_Or >= coutMateriau2 + coutAmelioration)
                                {
                                    nb_Pierre -= coutMateriau1;
                                    nb_Or -= coutMateriau2;
                                    nb_Or -= coutAmelioration;

                                    ((App)Application.Current).Joueur.Pierre = nb_Pierre;
                                    ((App)Application.Current).Joueur.Or = nb_Or;

                                    nbPierre.Text = nb_Pierre.ToString();
                                    nbOr.Text = nb_Or.ToString();

                                    // Mise à jour du coût d'amélioration                                                                                  
                                    LoadCsvData();

                                    UpdateCsv(selectedData);
                                    MessageBox.Show($"Bâtiment amélioré ! Matériaux utilisés : {coutMateriau1} de {nomMateriau1} et {coutMateriau2} de {nomMateriau2} \n Coût d'amélioration : {coutAmelioration} Gold");
                                }
                                else
                                {
                                    MessageBox.Show("Vous n'avez pas assez de ressource pour construire ce bâtiment");
                                }
                            }
                        }

                        else if (nomMateriau1 == "Fer")
                        {
                            if (nomMateriau2 == "NULL")
                            {
                                if (nb_Fer >= coutMateriau1 && nb_Or >= coutAmelioration)
                                {
                                    nb_Fer -= coutMateriau1;
                                    nb_Or -= coutAmelioration;

                                    ((App)Application.Current).Joueur.Fer = nb_Fer;
                                    ((App)Application.Current).Joueur.Or = nb_Or;

                                    nbFer.Text = nb_Fer.ToString();
                                    nbOr.Text = nb_Or.ToString();

                                    // Mise à jour du coût d'amélioration                                                                                  
                                    LoadCsvData();

                                    UpdateCsv(selectedData);
                                    MessageBox.Show($"Bâtiment amélioré ! Matériaux utilisés : {coutMateriau1} de {nomMateriau1} et {coutMateriau2} de {nomMateriau2} \n Coût d'amélioration : {coutAmelioration} Gold");
                                }


                            }

                            else if (nomMateriau2 == "Bois")
                            {
                                if (nb_Fer >= coutMateriau1 && nb_Bois >= coutMateriau2 && nb_Or >= coutAmelioration)
                                {
                                    nb_Fer -= coutMateriau1;
                                    nb_Bois -= coutMateriau2;
                                    nb_Or -= coutAmelioration;

                                    ((App)Application.Current).Joueur.Fer = nb_Fer;
                                    ((App)Application.Current).Joueur.Bois = nb_Bois;
                                    ((App)Application.Current).Joueur.Or = nb_Or;

                                    nbFer.Text = nb_Fer.ToString();
                                    nbBois.Text = nb_Bois.ToString();
                                    nbOr.Text = nb_Or.ToString();

                                    // Mise à jour du coût d'amélioration                                                                                  
                                    LoadCsvData();

                                    UpdateCsv(selectedData);
                                    MessageBox.Show($"Bâtiment amélioré ! Matériaux utilisés : {coutMateriau1} de {nomMateriau1} et {coutMateriau2} de {nomMateriau2} \n Coût d'amélioration : {coutAmelioration} Gold");
                                }

                            }

                            else if (nomMateriau2 == "Pierre")
                            {
                                if (nb_Fer >= coutMateriau1 && nb_Pierre >= coutMateriau2 && nb_Or >= coutAmelioration)
                                {
                                    nb_Fer -= coutMateriau1;
                                    nb_Pierre -= coutMateriau2;
                                    nb_Or -= coutAmelioration;

                                    ((App)Application.Current).Joueur.Fer = nb_Fer;
                                    ((App)Application.Current).Joueur.Pierre = nb_Pierre;
                                    ((App)Application.Current).Joueur.Or = nb_Or;

                                    nbFer.Text = nb_Fer.ToString();
                                    nbPierre.Text = nb_Pierre.ToString();
                                    nbOr.Text = nb_Or.ToString();

                                    // Mise à jour du coût d'amélioration                                                                                  
                                    LoadCsvData();

                                    UpdateCsv(selectedData);
                                    MessageBox.Show($"Bâtiment amélioré  ! Matériaux utilisés : {coutMateriau1} de {nomMateriau1} et {coutMateriau2} de {nomMateriau2} \n Coût d'amélioration : {coutAmelioration} Gold");
                                }
                            }

                            else if (nomMateriau2 == "Gold")
                            {
                                if (nb_Fer >= coutMateriau1 && nb_Or >= coutMateriau2 + coutAmelioration)
                                {
                                    nb_Fer -= coutMateriau1;
                                    nb_Or -= coutMateriau2;
                                    nb_Or -= coutAmelioration;

                                    ((App)Application.Current).Joueur.Fer = nb_Fer;
                                    ((App)Application.Current).Joueur.Or = nb_Or;

                                    nbFer.Text = nb_Fer.ToString();
                                    nbOr.Text = nb_Or.ToString();

                                    // Mise à jour du coût d'amélioration                                                                                  
                                    LoadCsvData();

                                    UpdateCsv(selectedData);
                                    MessageBox.Show($"Bâtiment amélioré ! Matériaux utilisés : {coutMateriau1} de {nomMateriau1} et {coutMateriau2} de {nomMateriau2} \n Coût d'amélioration : {coutAmelioration} Gold");
                                }
                            }
                        }

                        else if (nomMateriau1 == "Gold")
                        {
                            if (nomMateriau2 == "NULL")
                            {
                                if (nb_Or >= coutMateriau1 + coutAmelioration)
                                {
                                    nb_Or -= coutMateriau1;
                                    nb_Or -= coutAmelioration;

                                    ((App)Application.Current).Joueur.Or = nb_Or;

                                    nbOr.Text = nb_Or.ToString();

                                    // Mise à jour du coût d'amélioration                                                                                  
                                    LoadCsvData();

                                    UpdateCsv(selectedData);
                                    MessageBox.Show($"Bâtiment amélioré ! Matériaux utilisés : {coutMateriau1} de {nomMateriau1} et {coutMateriau2} de {nomMateriau2} \n Coût d'amélioration : {coutAmelioration} Gold");
                                }
                            }

                            else if (nomMateriau2 == "Bois")
                            {
                                if (nb_Bois >= coutMateriau2 && nb_Or >= coutMateriau1 + coutAmelioration)
                                {
                                    nb_Or -= coutMateriau1;
                                    nb_Bois -= coutMateriau2;
                                    nb_Or -= coutAmelioration;

                                    ((App)Application.Current).Joueur.Bois = nb_Bois;
                                    ((App)Application.Current).Joueur.Or = nb_Or;

                                    nbBois.Text = nb_Bois.ToString();
                                    nbOr.Text = nb_Or.ToString();

                                    // Mise à jour du coût d'amélioration                                                                                  
                                    LoadCsvData();

                                    UpdateCsv(selectedData);
                                    MessageBox.Show($"Bâtiment amélioré ! Matériaux utilisés : {coutMateriau1} de {nomMateriau1} et {coutMateriau2} de {nomMateriau2} \n Coût d'amélioration : {coutAmelioration} Gold");
                                }
                            }

                            else if (nomMateriau2 == "Pierre")
                            {
                                if (nb_Or >= coutMateriau1 + coutAmelioration && nb_Pierre >= coutMateriau2)
                                {
                                    nb_Or -= coutMateriau1;
                                    nb_Pierre -= coutMateriau2;
                                    nb_Or -= coutAmelioration;

                                    ((App)Application.Current).Joueur.Pierre = nb_Pierre;
                                    ((App)Application.Current).Joueur.Or = nb_Or;

                                    nbPierre.Text = nb_Pierre.ToString();
                                    nbOr.Text = nb_Or.ToString();

                                    // Mise à jour du coût d'amélioration                                                                                  
                                    LoadCsvData();

                                    UpdateCsv(selectedData);
                                    MessageBox.Show($"Bâtiment amélioré ! Matériaux utilisés : {coutMateriau1} de {nomMateriau1} et {coutMateriau2} de {nomMateriau2} \n Coût d'amélioration : {coutAmelioration} Gold");
                                }
                            }

                            else if (nomMateriau2 == "Fer")
                            {
                                if (nb_Or >= coutMateriau1 + coutAmelioration && nb_Fer >= coutMateriau2)
                                {
                                    nb_Or -= coutMateriau1;
                                    nb_Fer -= coutMateriau2;
                                    nb_Or -= coutAmelioration;

                                    ((App)Application.Current).Joueur.Fer = nb_Fer;
                                    ((App)Application.Current).Joueur.Or = nb_Or;

                                    nbFer.Text = nb_Fer.ToString();
                                    nbOr.Text = nb_Or.ToString();

                                    // Mise à jour du coût d'amélioration                                                                                  
                                    LoadCsvData();

                                    UpdateCsv(selectedData);
                                    MessageBox.Show($"Bâtiment amélioré ! Matériaux utilisés : {coutMateriau1} de {nomMateriau1} et {coutMateriau2} de {nomMateriau2} \n Coût d'amélioration : {coutAmelioration} Gold");
                                }
                            }
                        }

                        else
                        {
                            UpdateCsv(selectedData);
                        }

                    }
=======
                    UpdateCsv(selectedData);
>>>>>>> parent of 6a6084b (Merge remote-tracking branch 'origin/Yram')
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

<<<<<<< HEAD
        private void afficherBois(object sender, EventArgs e)
        {
            nbBois.Text = ((App)Application.Current).Joueur.Bois.ToString();
        }

        private void afficherPierre(object sender, EventArgs e)
        {
            nbPierre.Text = ((App)Application.Current).Joueur.Pierre.ToString();
        }

        private void afficherFer(object sender, EventArgs e)
        {
            nbFer.Text = ((App)Application.Current).Joueur.Fer.ToString();
        }

        private void afficherOr(object sender, EventArgs e)
        {
            nbOr.Text = ((App)Application.Current).Joueur.Or.ToString();
        }

        private void afficherHab(object sender, EventArgs e)
        {
            nbHab.Text = ((App)Application.Current).Joueur.Hab.ToString();
        }

=======
>>>>>>> parent of 6a6084b (Merge remote-tracking branch 'origin/Yram')
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

                // Mettre à jour la troisième colonne de la ligne sélectionnée
                for (int i = 0; i < lines.Count; i++)
                {
                    string[] fields = lines[i].Split(';');
                    if (fields.Length >= 11 && fields[0] == selectedData.Nom && fields[2] == selectedData.Niveau && fields[3] == selectedData.NiveauMax && fields[2][0] < fields[3][0])
                    {
                        int nv = Int32.Parse(fields[2]);
                        nv++;
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
