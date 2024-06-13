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
    public double scierieNV = 1;
    public int minePConstruite = 0;
    public double minePNV = 1;
    public int mineFConstruite = 0;
    public double mineFNV = 1;
    public int commerceConstruite = 0;
    public double commerceNV = 1;
    public int habitationConstruite = 0;
    public double habitationNV = 1;

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
                    if (elt.Niveau != "1") 
                    {
                        scierieNV = (Convert.ToInt32(elt.Niveau)*0.1)+1;
                    }
                }
                else
                {
                    scierieConstruite = 0;
                }
            }
        }

        ((App)Application.Current).Joueur.Bois += (int)Math.Ceiling(10 * scierieConstruite*scierieNV);
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
                    if (elt.Niveau != "1")
                    {
                        minePNV = (Convert.ToInt32(elt.Niveau) * 0.1) + 1;
                    }
                }
                else
                {
                    minePConstruite = 0;
                }
            }
        }
        ((App)Application.Current).Joueur.Pierre += (int)Math.Ceiling(5 * minePConstruite*minePNV);
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
                    mineFNV = (Convert.ToInt32(elt.Niveau) * 0.1) + 1;
                }
                else
                {
                    mineFConstruite = 0;
                }
            }
        }
        ((App)Application.Current).Joueur.Fer += (int)Math.Ceiling(3 * mineFConstruite * mineFNV);
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
                    if (elt.Niveau != "1")
                    {
                        commerceNV = (Convert.ToInt32(elt.Niveau) * 0.1) + 1;
                    }
                }
                else
                {
                    commerceConstruite = 0;

                }
            }
        }
        ((App)Application.Current).Joueur.Or += (int)Math.Ceiling(2 * commerceConstruite*commerceNV);
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
                    if (elt.Niveau != "1")
                    {
                        habitationNV = (Convert.ToInt32(elt.Niveau) * 0.1) + 1;
                    }
                }
                else
                {
                    habitationConstruite = 0;

                }
            }
        }
        ((App)Application.Current).Joueur.Hab += (int)Math.Ceiling(30 * habitationConstruite*habitationNV);
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
                        // Ajouter uniquement les lignes o� la seconde colonne est "1"
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


