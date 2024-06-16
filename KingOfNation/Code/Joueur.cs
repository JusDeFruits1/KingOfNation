using KingOfNation.IHM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace KingOfNation.Code
{
    public class Joueur
    {
        private string pseudo;
        public string Pseudo
        {
            get { return pseudo; }
            set { pseudo = value; }
        }
        private string empire;
        public string Empire
        {
            get { return empire; }
            set { empire = value; }
        }
        private string nomVillage;
        public string NomVillage
        {
            get { return nomVillage; }
            set { nomVillage = value; }
        }
        private int bois = 0;
        public int Bois
        {
            get { return bois; }
            set { bois = value; }
        }
        private int pierre = 0;
        public int Pierre
        {
            get { return pierre; }
            set { pierre = value; }
        }
        private int fer = 0;
        public int Fer
        {
            get { return fer; }
            set { fer = value; }
        }
        private int or = 175;
        public int Or
        {
            get { return or; }
            set { or = value; }
        }
        private int hab = 10;
        public int Hab
        {
            get { return hab; }
            set { hab = value; }
        }
        private List<Lieutenant> lieutenantList = new List<Lieutenant>();
        public List<Lieutenant> LieutenantList
        {
            get { return lieutenantList; }
            set { lieutenantList = value; }
        }
        private List<Tresor> tresorsJoueur = new List<Tresor>();
        public List<Tresor> TresorsJoueur
        {
            get { return tresorsJoueur; }
            set { tresorsJoueur = value; }
        }
        

        private Leger leger;
        public Leger Leger
        {
            get
            {
                return leger;
            }
            set 
            { 
                leger = value; 
            }
        }

        private Lourd lourd;
        public Lourd Lourd
        {
            get
            {
                return lourd;
            }
            set
            {
                lourd = value;
            }
        }

        private Machine_de_guerre mdg;
        public Machine_de_guerre Mdg
        {
            get
            {
                return mdg;
            }
            set
            {
                mdg = value;
            }
        }

        // Constructeur sans paramètres pour la désérialisation
        public Joueur() { }

        // (Optionnel) Constructeur avec paramètres pour initialisation facile
        public Joueur(string pseudo, string empire, string nomVillage, int bois, int pierre, int fer, int or, int hab, List<Lieutenant> lieutenantsList, List<Tresor> tresorsJoueur, Leger leger,Lourd lourd,Machine_de_guerre mdg)
        {
            Pseudo = pseudo;
            Empire = empire;
            NomVillage = nomVillage;
            Bois = bois;
            Pierre = pierre;
            Fer = fer;
            Or = or;
            Hab = hab;
            LieutenantList = lieutenantList; 
            TresorsJoueur = tresorsJoueur;
            Leger = leger;
            Lourd = lourd;
            Mdg = mdg;
        }
        
        // Méthode pour sérialiser l'objet dans un fichier avec le nom du joueur
        public void SerializeToFile(string directoryPath)
        {
            string jsonString = JsonSerializer.Serialize(this);
            string fileName = $"{Pseudo}.json"; // Utilisation du pseudo comme nom de fichier
            string filePath = Path.Combine(directoryPath, fileName);
            File.WriteAllText(filePath, jsonString);
        }
        public static Joueur DeserializeFromFile(string filePath)
        {
            string jsonString = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<Joueur>(jsonString);
        }
    }
}