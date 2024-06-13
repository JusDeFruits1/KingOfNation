using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Threading;
using KingOfNation;
using KingOfNation.Code;
using KingOfNation.IHM;
using Microsoft.VisualBasic.FileIO;

public class Production
{

    #region Attributes

    List<CsvData> csvDataList = new List<CsvData>();

    #endregion

    #region Properties

    public int scierieConstruite = 0;
    public int minePConstruite = 0;
    public int mineFConstruite = 0;
    public int commerceConstruite = 0;
    public int habitationConstruite = 0;

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
                if (elt.Niveau != "0")
                {
                    scierieConstruite = 1;
                }
                else
                {
                    scierieConstruite = 0;
                }
            }
        }
        ((App)Application.Current).Joueur.Bois += 10 * scierieConstruite;
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
                    minePConstruite = 1;
                }
                else
                {
                    minePConstruite = 0;
                }
            }
        }
        ((App)Application.Current).Joueur.Pierre += 5 * minePConstruite;
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
                    mineFConstruite = 1;
                }
                else
                {
                    mineFConstruite = 0;
                }
            }
        }
        ((App)Application.Current).Joueur.Fer += 3 * mineFConstruite;
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
                    commerceConstruite = 1;
                }
                else
                {
                    commerceConstruite = 0;

                }
            }
        }
        ((App)Application.Current).Joueur.Or += 2 * commerceConstruite;
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
        ((App)Application.Current).Joueur.Hab += 10 * habitationConstruite;
        return ((App)Application.Current).Joueur.Hab;
    }

    public void CanProduce(List<CsvData> csvDataList)
    {
        string filePath = "../../../CSV/"+ ((App)Application.Current).Joueur.NomVillage + ".csv";
        try
        {
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
                        // Ajouter uniquement les lignes où la seconde colonne est "1"
                        if (fields[1] == "1")
                        {
                            csvDataList.Add(new CsvData { Nom = fields[0], Niveau = fields[2]});
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            
        }
    }
    #endregion
}


