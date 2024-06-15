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
    public double buffBois = 1;
    public int minePConstruite = 0;
    public double minePNV = 1;
    public double buffPierre = 1;
    public int mineFConstruite = 0;
    public double mineFNV = 1;
    public double buffFer = 1;
    public int commerceConstruite = 0;
    public double commerceNV = 1;
    public double buffGold = 1;
    public int habitationConstruite = 0;
    public double habitationNV = 1;
    public double buffHab = 1;

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
        string idL = GetLieutenantId();
        foreach (CsvData elt in csvDataList)
        {
            if (elt.Nom == "Scierie")
            {
                if (elt.Niveau != "0")
                {
                    scierieConstruite = 1;
                    if (elt.Niveau != "1")
                    {
                        scierieNV = ((Convert.ToInt32(elt.Niveau)+ Convert.ToInt32(elt.Niveau)) * 0.2) + Convert.ToInt32(elt.Niveau)-1;
                    }
                    if (idL == "1")
                    {
                        buffBois = 1.2;
                    }
                    else
                    {
                        buffBois = 1;
                    }
                }
                else
                {
                    scierieConstruite = 0;
                }
            }
        }

     ((App)Application.Current).Joueur.Bois += (int)Math.Ceiling(10 * scierieConstruite * scierieNV * buffBois);
        return ((App)Application.Current).Joueur.Bois;
    }

    public int ProdPierre()
    {
        CanProduce(csvDataList);
        string idL = GetLieutenantId();
        foreach (CsvData elt in csvDataList)
        {
            if (elt.Nom == "Mine")
            {
                if (elt.Niveau != "0")
                {
                    minePConstruite = 1;
                    if (elt.Niveau != "1")
                    {
                        minePNV = ((Convert.ToInt32(elt.Niveau) + Convert.ToInt32(elt.Niveau)) * 0.2) + Convert.ToInt32(elt.Niveau) - 1;
                    }
                    if (idL == "2")
                    {
                        buffPierre = 1.2;
                    }
                    else
                    {
                        buffPierre = 1;
                    }
                }
                else
                {
                    minePConstruite = 0;
                }
            }
        }
        ((App)Application.Current).Joueur.Pierre += (int)Math.Ceiling(5 * minePConstruite * minePNV * buffPierre);
        return ((App)Application.Current).Joueur.Pierre;
    }

    public int ProdFer()
    {
        CanProduce(csvDataList);
        string idL = GetLieutenantId();
        foreach (CsvData elt in csvDataList)
        {
            if (elt.Nom == "Mine")
            {
                if (elt.Niveau == "3" || elt.Niveau == "4" || elt.Niveau == "5")
                {
                    mineFConstruite = 1;
                    mineFNV = ((Convert.ToInt32(elt.Niveau) + Convert.ToInt32(elt.Niveau)) * 0.2) + Convert.ToInt32(elt.Niveau) - 1;
                    if (idL == "3")
                    {
                        buffFer = 1.2;
                    }
                    else
                    {
                        buffFer = 1;
                    }
                }
                else
                {
                    mineFConstruite = 0;
                }
            }
        }
        ((App)Application.Current).Joueur.Fer += (int)Math.Ceiling(3 * mineFConstruite * mineFNV * buffFer);
        return ((App)Application.Current).Joueur.Fer;
    }

    public int ProdOr()
    {
        CanProduce(csvDataList);
        string idL = GetLieutenantId();
        foreach (CsvData elt in csvDataList)
        {
            if (elt.Nom == "Commerce")
            {
                if (elt.Niveau != "0")
                {
                    commerceConstruite = 1;
                    if (elt.Niveau != "1")
                    {
                        commerceNV = ((Convert.ToInt32(elt.Niveau) + Convert.ToInt32(elt.Niveau)) * 0.2) + Convert.ToInt32(elt.Niveau) - 1;
                    }
                    if (idL == "4")
                    {
                        buffGold = 1.2;
                    }
                    else
                    {
                        buffGold = 1;
                    }
                }
                else
                {
                    commerceConstruite = 0;

                }
            }
        }
        ((App)Application.Current).Joueur.Or += (int)Math.Ceiling(2 * commerceConstruite * commerceNV * buffGold);
        return ((App)Application.Current).Joueur.Or;
    }

    public int ProdHab()
    {
        CanProduce(csvDataList);
        string idL = GetLieutenantId();
        foreach (CsvData elt in csvDataList)
        {
            if (elt.Nom == "Habitation")
            {
                if (elt.Niveau != "0")
                {
                    habitationConstruite = 1;
                    if (elt.Niveau != "1")
                    {
                        habitationNV = ((Convert.ToInt32(elt.Niveau) + Convert.ToInt32(elt.Niveau)) * 0.2) + 1;
                    }
                    if (idL == "5")
                    {
                        buffHab = 1.2;
                    }
                    else
                    {
                        buffHab = 1;
                    }
                }
                else
                {
                    habitationConstruite = 0;

                }
            }
        }
        ((App)Application.Current).Joueur.Hab += (int)Math.Ceiling(30 * habitationConstruite * habitationNV * buffHab);
        return ((App)Application.Current).Joueur.Hab;
    }

    public void CanProduce(List<CsvData> csvDataList)
    {
        string filePath = "../../../CSV/" + ((App)Application.Current).Joueur.NomVillage + ".csv";
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
                            csvDataList.Add(new CsvData { Nom = fields[0], Niveau = fields[2] });
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }
    public string GetLieutenantId()
    {
        string idLieutenant = ""; // Définir une valeur par défaut ou une valeur spécifique si nécessaire

        // Vérifier si LieutenantList contient des éléments
        if (((App)Application.Current).Joueur.LieutenantList != null && ((App)Application.Current).Joueur.LieutenantList.Count > 0)
        {
            // Parcourir la liste des lieutenants
            foreach (Lieutenant lieutenant in ((App)Application.Current).Joueur.LieutenantList)
            {
                // Vérifier si les conditions pour récupérer l'id sont remplies
                // Par exemple, vous pourriez avoir une condition spécifique pour récupérer l'id souhaité
                // Dans cet exemple, on suppose que vous voulez l'id du premier lieutenant de la liste
                idLieutenant = lieutenant.Id;
                break; // Sortir de la boucle après avoir trouvé le premier lieutenant
            }
        }

        return idLieutenant;
    }
    #endregion
}