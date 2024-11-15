﻿using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace KingOfNation.IHM
{
    /// <summary>
    /// Logique d'interaction pour Construction.xaml
    /// </summary>
    public partial class Construction : Window
    {
        #region Attributes
        #endregion

        #region Properties
        #endregion

        #region Constructors

        public Construction()
        {
            InitializeComponent();
            LoadCsvData();

            ((App)Application.Current).timerJ.Tick += afficherRessource;
            ((App)Application.Current).timerJ.Start();
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
                            if (fields[1] == "0")
                            {
                                csvDataList.Add(new CsvData { Nom = fields[0], Ressource_produit = fields[5], Cout_construction = fields[10], Rs_construction1 = fields[11], Qt_rs_constru1 = fields[12], Rs_construction2 = fields[13], Qt_rs_constru2 = fields[14] });
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
                string? mat1 = selectedData.Rs_construction1;
                string? mat2 = selectedData.Rs_construction2;


                // Effectuer l'opération souhaitée avec coutMateriaux1
                //MessageBox.Show($"mat 1: {mat1}, coutMat1 : {coutMateriau1}, bat selec : {batselec} ");
                if (string.IsNullOrEmpty(selectedData.Qt_rs_constru1) || selectedData.Qt_rs_constru1 == "NULL")
                {
                    coutMateriau1 = 0;
                }
                else
                {
                    coutMateriau1 = Convert.ToInt32(selectedData.Qt_rs_constru1);
                }

                //MessageBox.Show($"mat 1: {mat1}, coutMat1 : {coutMateriau1}, bat selec : {batselec} ");
                if (string.IsNullOrEmpty(selectedData.Qt_rs_constru2) || selectedData.Qt_rs_constru2 == "NULL")
                {
                    coutMateriau2 = 0;
                }
                else
                {
                    coutMateriau2 = Convert.ToInt32(selectedData.Qt_rs_constru2);
                }
                if(coutMateriau1 == coutMateriau2 && mat2 == "NULL" && mat2 == "NULL")
                {
                    MessageBox.Show("Vous avez besoin d'aucune ressource pour construire ce bâtiment");
                }
                else
                {
                    MessageBox.Show($"Vous avez besoin de {coutMateriau1} de {mat1} et {coutMateriau2} de {mat2} pour construire ce bâtiment");
                }
                
            }
        }

        private void Construire(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CsvDataListView.SelectedItem is CsvData selectedData)
                {

                    int coutMateriau1;
                    int coutMateriau2;
                    int coutConstruction;

                    string? nomMateriau1 = selectedData.Rs_construction1;
                    string? nomMateriau2 = selectedData.Rs_construction2;

                    int nb_Bois = ((App)Application.Current).Joueur.Bois;
                    int nb_Pierre = ((App)Application.Current).Joueur.Pierre;
                    int nb_Fer = ((App)Application.Current).Joueur.Fer;
                    int nb_Or = ((App)Application.Current).Joueur.Or;
                    

                    if (string.IsNullOrEmpty(selectedData.Cout_construction) || selectedData.Cout_construction == "NULL")
                    {
                        coutConstruction = 0;
                    }
                    else
                    {
                        coutConstruction = Convert.ToInt32(selectedData.Cout_construction);
                    }

                    if (string.IsNullOrEmpty(selectedData.Qt_rs_constru1) || selectedData.Qt_rs_constru1 == "NULL")
                    {
                        coutMateriau1 = 0;
                    }                  
                    else
                    {
                        coutMateriau1 = Convert.ToInt32(selectedData.Qt_rs_constru1);
                    }

                    if(string.IsNullOrEmpty(selectedData.Qt_rs_constru2) || selectedData.Qt_rs_constru2 == "NULL")
                    {
                        coutMateriau2 = 0;
                    }
                    else
                    {
                        coutMateriau2 = Convert.ToInt32(selectedData.Qt_rs_constru2);
                    }
                    
                    if ( nb_Or < coutConstruction)
                    {
                        MessageBox.Show("Vous n'avez pas assez d'or pour construire ce bâtiment");
                    }
                    else if(nb_Or > coutConstruction)
                    {
                        if (nomMateriau1 == "Bois")
                        {
                            if (nomMateriau2 == "NULL")
                            {
                                if (nb_Bois >= coutMateriau1)
                                {
                                    nb_Bois -= coutMateriau1;

                                    ((App)Application.Current).Joueur.Bois = nb_Bois;
                                    ((App)Application.Current).Joueur.Or -= coutConstruction;
                                    
                                    nbBois.Text = nb_Bois.ToString();
                                    nbOr.Text = nb_Or.ToString();

                                    UpdateCsv(selectedData);

                                    MessageBox.Show($"Bâtiment construit ! Matériaux utilisés : {coutMateriau1} de {nomMateriau1} \n Coût en or : {coutConstruction} Gold");
                                }

                                else
                                {
                                    MessageBox.Show("Vous n'avez pas assez de bois pour construire ce bâtiment");
                                }
                            }

                            else if (nomMateriau2 == "Pierre")
                            {
                                if (nb_Bois >= coutMateriau1 && nb_Pierre >= coutMateriau2)
                                {
                                    nb_Bois -= coutMateriau1;
                                    nb_Pierre -= coutMateriau2;

                                    ((App)Application.Current).Joueur.Bois = nb_Bois;
                                    ((App)Application.Current).Joueur.Pierre = nb_Pierre;
                                    ((App)Application.Current).Joueur.Or -= coutConstruction;

                                    nbBois.Text += nb_Bois.ToString();
                                    nbPierre.Text = nb_Pierre.ToString();
                                    UpdateCsv(selectedData);
                                    MessageBox.Show($"Bâtiment construit ! Matériaux utilisés : {coutMateriau1} de {nomMateriau1} et {coutMateriau2} de {nomMateriau2} \n Coût en or : {coutConstruction} Gold");
                                }

                                else
                                {
                                    MessageBox.Show("Vous n'avez pas assez de ressource pour construire ce bâtiment");
                                }

                            }

                            else if (nomMateriau2 == "Fer")
                            {
                                if (nb_Bois >= coutMateriau1 && nb_Fer >= coutMateriau2)
                                {
                                    nb_Bois -= coutMateriau1;
                                    nb_Fer -= coutMateriau2;

                                    ((App)Application.Current).Joueur.Bois = nb_Bois;
                                    ((App)Application.Current).Joueur.Fer = nb_Fer;
                                    ((App)Application.Current).Joueur.Or -= coutConstruction;
                                    nbBois.Text += nb_Bois.ToString();
                                    nbFer.Text = nb_Fer.ToString();
                                    UpdateCsv(selectedData);
                                    MessageBox.Show($"Bâtiment construit ! Matériaux utilisés : {coutMateriau1} de {nomMateriau1} et {coutMateriau2} de {nomMateriau2} \n Coût en or : {coutConstruction} Gold");
                                }
                                else
                                {
                                    MessageBox.Show("Vous n'avez pas assez de ressource pour construire ce bâtiment");
                                }
                            }

                            else if (nomMateriau2 == "Gold")
                            {
                                if (nb_Bois >= coutMateriau1 && nb_Or >= coutMateriau2 + coutConstruction)
                                {
                                    nb_Bois -= coutMateriau1;
                                    nb_Or -= coutMateriau2;

                                    ((App)Application.Current).Joueur.Bois = nb_Bois;
                                    ((App)Application.Current).Joueur.Or = nb_Or;
                                    ((App)Application.Current).Joueur.Or -= coutConstruction;

                                    nbBois.Text += nb_Bois.ToString();
                                    nbOr.Text = nb_Or.ToString();
                                    UpdateCsv(selectedData);
                                    MessageBox.Show($"Bâtiment construit ! Matériaux utilisés : {coutMateriau1} de {nomMateriau1} et {coutMateriau2} de {nomMateriau2} \n Coût en or : {coutConstruction} Gold");
                                }
                                else
                                {
                                    MessageBox.Show("Vous n'avez pas assez de ressource pour construire ce bâtiment");
                                }
                            }
                        }

                        else if (nomMateriau1 == "Pierre")
                        {
                            if (nomMateriau2 == "NULL")
                            {
                                if (nb_Pierre >= coutMateriau1)
                                {
                                    nb_Pierre -= coutMateriau1;
                                    ((App)Application.Current).Joueur.Pierre = nb_Pierre;
                                    ((App)Application.Current).Joueur.Or -= coutConstruction;
                                    nbPierre.Text = nb_Pierre.ToString();
                                    UpdateCsv(selectedData);
                                    MessageBox.Show($"Bâtiment construit ! Matériaux utilisés : {coutMateriau1} de {nomMateriau1} \n Coût en or : {coutConstruction} Gold");
                                }

                                else
                                {
                                    MessageBox.Show("Vous n'avez pas assez de pierre pour construire ce bâtiment");
                                }
                            }

                            else if (nomMateriau2 == "Bois")
                            {
                                if (nb_Pierre >= coutMateriau1 && nb_Bois >= coutMateriau2)
                                {
                                    nb_Pierre -= coutMateriau1;
                                    nb_Bois -= coutMateriau2;

                                    ((App)Application.Current).Joueur.Pierre = nb_Pierre;
                                    ((App)Application.Current).Joueur.Bois = nb_Bois;
                                    ((App)Application.Current).Joueur.Or -= coutConstruction;

                                    nbPierre.Text += nb_Pierre.ToString();
                                    nbBois.Text = nb_Bois.ToString();
                                    UpdateCsv(selectedData);
                                    MessageBox.Show($"Bâtiment construit ! Matériaux utilisés : {coutMateriau1} de {nomMateriau1} et {coutMateriau2} de {nomMateriau2} \n Coût en or : {coutConstruction} Gold");
                                }
                                else
                                {
                                    MessageBox.Show("Vous n'avez pas assez de ressource pour construire ce bâtiment");
                                }
                            }

                            else if (nomMateriau2 == "Fer")
                            {
                                if (nb_Pierre >= coutMateriau1 && nb_Fer >= coutMateriau2)
                                {
                                    nb_Pierre -= coutMateriau1;
                                    nb_Fer -= coutMateriau2;

                                    ((App)Application.Current).Joueur.Pierre = nb_Pierre;
                                    ((App)Application.Current).Joueur.Fer = nb_Fer;
                                    ((App)Application.Current).Joueur.Or -= coutConstruction;

                                    nbPierre.Text += nb_Pierre.ToString();
                                    nbFer.Text = nb_Fer.ToString();
                                    UpdateCsv(selectedData);
                                    MessageBox.Show($"Bâtiment construit ! Matériaux utilisés : {coutMateriau1} de {nomMateriau1} et {coutMateriau2} de {nomMateriau2} \n Coût en or : {coutConstruction} Gold");
                                }
                                else
                                {
                                    MessageBox.Show("Vous n'avez pas assez de ressource pour construire ce bâtiment");
                                }
                            }

                            else if (nomMateriau2 == "Gold")
                            {
                                if (nb_Pierre >= coutMateriau1 && nb_Or >= coutMateriau2 + coutConstruction)
                                {
                                    nb_Pierre -= coutMateriau1;
                                    nb_Or -= coutMateriau2;

                                    ((App)Application.Current).Joueur.Pierre = nb_Pierre;
                                    ((App)Application.Current).Joueur.Or = nb_Or;
                                    ((App)Application.Current).Joueur.Or -= coutConstruction;

                                    nbPierre.Text += nb_Pierre.ToString();
                                    nbOr.Text = nb_Or.ToString();
                                    UpdateCsv(selectedData);
                                    MessageBox.Show($"Bâtiment construit ! Matériaux utilisés : {coutMateriau1} de {nomMateriau1} et {coutMateriau2} de {nomMateriau2} \n Coût en or : {coutConstruction} Gold");
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
                                if (nb_Fer >= coutMateriau1)
                                {
                                    nb_Fer -= coutMateriau1;
                                    ((App)Application.Current).Joueur.Fer = nb_Fer;
                                    ((App)Application.Current).Joueur.Or -= coutConstruction;
                                    nbFer.Text = nb_Fer.ToString();
                                    UpdateCsv(selectedData);
                                    MessageBox.Show($"Bâtiment construit ! Matériaux utilisés : {coutMateriau1} de {nomMateriau1} \n Coût en or : {coutConstruction} Gold");
                                }

                                else
                                {
                                    MessageBox.Show("Vous n'avez pas assez de fer pour construire ce bâtiment");
                                }
                            }

                            else if (nomMateriau2 == "Bois")
                            {
                                if (nb_Fer >= coutMateriau1 && nb_Bois >= coutMateriau2)
                                {
                                    nb_Fer -= coutMateriau1;
                                    nb_Bois -= coutMateriau2;

                                    ((App)Application.Current).Joueur.Fer = nb_Fer;
                                    ((App)Application.Current).Joueur.Bois = nb_Bois;
                                    ((App)Application.Current).Joueur.Or -= coutConstruction;

                                    nbFer.Text += nb_Fer.ToString();
                                    nbBois.Text = nb_Bois.ToString();
                                    UpdateCsv(selectedData);
                                    MessageBox.Show($"Bâtiment construit ! Matériaux utilisés : {coutMateriau1} de {nomMateriau1} et {coutMateriau2} de {nomMateriau2} \n Coût en or : {coutConstruction} Gold");
                                }
                                else
                                {
                                    MessageBox.Show("Vous n'avez pas assez de ressource pour construire ce bâtiment");
                                }
                            }

                            else if (nomMateriau2 == "Pierre")
                            {
                                if (nb_Fer >= coutMateriau1 && nb_Pierre >= coutMateriau2)
                                {
                                    nb_Fer -= coutMateriau1;
                                    nb_Pierre -= coutMateriau2;

                                    ((App)Application.Current).Joueur.Fer = nb_Fer;
                                    ((App)Application.Current).Joueur.Pierre = nb_Pierre;
                                    ((App)Application.Current).Joueur.Or -= coutConstruction;

                                    nbFer.Text += nb_Fer.ToString();
                                    nbPierre.Text = nb_Pierre.ToString();
                                    UpdateCsv(selectedData);
                                    MessageBox.Show($"Bâtiment construit ! Matériaux utilisés : {coutMateriau1} de {nomMateriau1} et {coutMateriau2} de {nomMateriau2} \n Coût en or : {coutConstruction} Gold");
                                }
                                else
                                {
                                    MessageBox.Show("Vous n'avez pas assez de ressource pour construire ce bâtiment");
                                }
                            }

                            else if (nomMateriau2 == "Gold")
                            {
                                if (nb_Fer >= coutMateriau1 && nb_Or >= coutMateriau2 + coutConstruction)
                                {
                                    nb_Fer -= coutMateriau1;
                                    nb_Or -= coutMateriau2;

                                    ((App)Application.Current).Joueur.Fer = nb_Fer;
                                    ((App)Application.Current).Joueur.Or = nb_Or;
                                    ((App)Application.Current).Joueur.Or -= coutConstruction;

                                    nbFer.Text += nb_Fer.ToString();
                                    nbOr.Text = nb_Or.ToString();
                                    UpdateCsv(selectedData);
                                    MessageBox.Show($"Bâtiment construit ! Matériaux utilisés : {coutMateriau1} de {nomMateriau1} et {coutMateriau2} de {nomMateriau2} \n Coût en or : {coutConstruction} Gold");
                                }
                                else
                                {
                                    MessageBox.Show("Vous n'avez pas assez de ressource pour construire ce bâtiment");
                                }
                            }

                        }

                        else if (nomMateriau1 == "Gold")
                        {
                            if (nomMateriau2 == "NULL")
                            {
                                if (nb_Or >= coutMateriau1 + coutConstruction)
                                {
                                    nb_Or -= coutMateriau1;
                                    ((App)Application.Current).Joueur.Or = nb_Or;
                                    ((App)Application.Current).Joueur.Or -= coutConstruction;
                                    nbOr.Text = nb_Or.ToString();
                                    UpdateCsv(selectedData);
                                    MessageBox.Show($"Bâtiment construit ! Matériaux utilisés : {coutMateriau1} de {nomMateriau1} \n Coût en or : {coutConstruction} Gold");
                                }

                                else
                                {
                                    MessageBox.Show("Vous n'avez pas assez d'or pour construire ce bâtiment");
                                }
                            }

                            else if (nomMateriau2 == "Bois")
                            {
                                if (nb_Or >= coutMateriau1 + coutConstruction && nb_Bois >= coutMateriau2 )
                                {
                                    nb_Or -= coutMateriau1;
                                    nb_Bois -= coutMateriau2;

                                    ((App)Application.Current).Joueur.Or = nb_Or;
                                    ((App)Application.Current).Joueur.Bois = nb_Bois;
                                    ((App)Application.Current).Joueur.Or -= coutConstruction;

                                    nbOr.Text += nb_Or.ToString();
                                    nbBois.Text = nb_Bois.ToString();
                                    UpdateCsv(selectedData);
                                    MessageBox.Show($"Bâtiment construit ! Matériaux utilisés : {coutMateriau1} de {nomMateriau1} et {coutMateriau2} de {nomMateriau2} \n Coût en or : {coutConstruction} Gold");
                                }
                                else
                                {
                                    MessageBox.Show("Vous n'avez pas assez de ressource pour construire ce bâtiment");
                                }
                            }

                            else if (nomMateriau2 == "Pierre")
                            {
                                if (nb_Or >= coutMateriau1 + coutConstruction && nb_Pierre >= coutMateriau2)
                                {
                                    nb_Or -= coutMateriau1;
                                    nb_Pierre -= coutMateriau2;

                                    ((App)Application.Current).Joueur.Or = nb_Or;
                                    ((App)Application.Current).Joueur.Pierre = nb_Pierre;
                                    ((App)Application.Current).Joueur.Or -= coutConstruction;

                                    nbOr.Text += nb_Or.ToString();
                                    nbPierre.Text = nb_Pierre.ToString();
                                    UpdateCsv(selectedData);
                                    MessageBox.Show($"Bâtiment construit ! Matériaux utilisés : {coutMateriau1} de {nomMateriau1} et {coutMateriau2} de {nomMateriau2} \n Coût en or : {coutConstruction} Gold");
                                }
                                else
                                {
                                    MessageBox.Show("Vous n'avez pas assez de ressource pour construire ce bâtiment");
                                }
                            }

                            else if (nomMateriau2 == "Fer")
                            {
                                if (nb_Or >= coutMateriau1 + coutConstruction && nb_Fer >= coutMateriau2)
                                {
                                    nb_Or -= coutMateriau1;
                                    nb_Fer -= coutMateriau2;

                                    ((App)Application.Current).Joueur.Or = nb_Or;
                                    ((App)Application.Current).Joueur.Fer = nb_Fer;
                                    ((App)Application.Current).Joueur.Or -= coutConstruction;

                                    nbOr.Text += nb_Or.ToString();
                                    nbFer.Text = nb_Fer.ToString();
                                    UpdateCsv(selectedData);
                                    MessageBox.Show($"Bâtiment construit ! Matériaux utilisés : {coutMateriau1} de {nomMateriau1} et {coutMateriau2} de {nomMateriau2} \n Coût en or : {coutConstruction} Gold");
                                }
                                else
                                {
                                    MessageBox.Show("Vous n'avez pas assez de ressource pour construire ce bâtiment");
                                }
                            }

                        }
                        else
                        {
                            nb_Or -= coutConstruction;
                            ((App)Application.Current).Joueur.Or = nb_Or;
                            nbOr.Text = nb_Or.ToString();
                            MessageBox.Show($"Bâtiment construit ! Aucun matériaux utilisés. \n Coût en or : {coutConstruction} Gold");
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
                    if (fields.Length >= 11 && fields[0] == selectedData.Nom && fields[5] == selectedData.Ressource_produit && fields[10] == selectedData.Cout_construction && fields[11] == selectedData.Rs_construction1 && fields[12] == selectedData.Qt_rs_constru1 && fields[13] == selectedData.Rs_construction2 && fields[14] == selectedData.Qt_rs_constru2)
                    {
                        fields[1] = "1";
                        fields[2] = "1";
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

        private void afficherRessource(object sender, EventArgs e)
        {
            nbBois.Text = ((App)Application.Current).Joueur.Bois.ToString();
            nbPierre.Text = ((App)Application.Current).Joueur.Pierre.ToString();
            nbFer.Text = ((App)Application.Current).Joueur.Fer.ToString();
            nbOr.Text = ((App)Application.Current).Joueur.Or.ToString();
            nbHab.Text = ((App)Application.Current).Joueur.Hab.ToString();
        }
    }

    #endregion

}



