using System;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Threading;
using KingOfNation;
using KingOfNation.IHM;
using Microsoft.VisualBasic.FileIO;

public class Production
{

    #region Attributes

    private bool verifscierieConstruite = false;
    private int scierieConstruite = 0;

    private bool verifminePConstruite = false;
    private int mineConstruitePierre = 0;

    private bool verifmineFConstruite = false;
    private int mineConstruiteFer = 0;

    private bool verifcommerceConstruite = false;
    private int commerceConstruite = 0;

    private bool verifhabConstruite = false;
    private int habitationConstruite = 0;

    List<CsvData> csvDataList = new List<CsvData>();

    #endregion

    #region Properties



    #endregion

    #region Constructors
    public Production()
    {        
    }
    #endregion

    #region Operations

    public int ProdBois()
    {
        CanProduce(csvDataList);
        foreach (CsvData elt in csvDataList)
        {
            if (elt.Nom == "Scierie")
            {
                if( elt.Niveau != "0")
                {
                    scierieConstruite = 1;
                }
                else
                {
                    scierieConstruite = 0;
                }
            }
            
        }
        ((App)Application.Current).Joueur.Bois += 100 * scierieConstruite;
        return ((App)Application.Current).Joueur.Bois;

    }

    public int ProdPierre()
    {
        CanProduce(csvDataList);
        foreach (CsvData elt in csvDataList)
        {
            if (elt.Nom == "Mine")
            {
                if (elt.Niveau != "0")
                {
                    mineConstruitePierre = 1;
                }
                else
                {
                    mineConstruitePierre = 0;
                }
            }            
        }
        ((App)Application.Current).Joueur.Pierre += 50 * mineConstruitePierre;
        return ((App)Application.Current).Joueur.Pierre;
    }

    public int ProdFer()
    {
        CanProduce(csvDataList);
        foreach (CsvData elt in csvDataList)
        {
            if (elt.Nom == "Mine")
            {
                if (elt.Niveau == "3" || elt.Niveau == "4" || elt.Niveau == "5")
                {
                    mineConstruiteFer = 1;
                }
                else
                {
                    mineConstruiteFer = 0;
                }
            }            
        }
        ((App)Application.Current).Joueur.Fer += 30 * mineConstruiteFer;
        return ((App)Application.Current).Joueur.Fer;
    }

    public int ProdOr()
    {
        CanProduce(csvDataList);
        foreach (CsvData elt in csvDataList)
        {
            if (elt.Nom == "Commerce")
            {
                if (elt.Niveau != "0")
                {
                    mineConstruiteFer = 1;
                }
                else
                {
                    commerceConstruite = 0;
                }
            }
        }
        ((App)Application.Current).Joueur.Or += 20 * commerceConstruite;
        return ((App)Application.Current).Joueur.Or;
    }

    public int ProdHab()
    {
        CanProduce(csvDataList);
        foreach (CsvData elt in csvDataList)
        {
            if (elt.Nom == "Habitation")
            {
                if (elt.Niveau != "0")
                {
                    habitationConstruite = 1;
                }
                else
                {
                    habitationConstruite = 0;
                }
            }
        }
        ((App)Application.Current).Joueur.Hab += 30 * habitationConstruite;
        return ((App)Application.Current).Joueur.Hab;
    }

    public void CanProduce(List<CsvData> csvDataList)
    {
        string filePath = "../../../CSV/joueur.csv";
        try
        {
            using (TextFieldParser parser = new TextFieldParser(filePath))
            {

                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(";");

                // Lire la ligne d'en-t�te si elle existe
                if (!parser.EndOfData)
                {
                    string[] headers = parser.ReadFields();
                    // Vous pouvez utiliser les en-t�tes si n�cessaire
                }
                // Lire les lignes suivantes
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    if (fields.Length >= 11) // Assurez-vous qu'il y a au moins 11 colonnes
                    {
                        // Ajouter uniquement les lignes o� la seconde colonne est "0"
                        if (fields[1] == "1")
                        {
                            csvDataList.Add(new CsvData { Nom = fields[0], Niveau = fields[2], Description = fields[4] });
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Le fichier ne peut pas �tre lu: " + ex.Message);
        }
    }
    #endregion
}


