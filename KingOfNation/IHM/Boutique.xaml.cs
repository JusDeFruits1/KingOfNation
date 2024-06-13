using KingOfNation.Code;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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

    public class Produit
    {
        public string Id { get; set; }
        public string Nom { get; set; }
        public string ImgPath { get; set; }
        public string Quantite { get; set; }
        public string Prix { get; set; }
    }

    public class Tresor
    {
        public string Nom { get; set; }
        public string Price { get; set; }
        public string ImagPath { get; set; }
    }

    /// <summary>
    /// Logique d'interaction pour Boutique.xaml
    /// </summary>
    public partial class Boutique : Window, INotifyPropertyChanged
    {
        private string buttonContent;
        private ObservableCollection<Produit> acheterItems;
        private ObservableCollection<Tresor> vendreItems;
        private ObservableCollection<object> currentItems;

        public string ButtonContent
        {
            get { return buttonContent; }
            set
            {
                buttonContent = value;
                OnPropertyChanged(nameof(ButtonContent));
                UpdateCurrentItems(); // Mettre à jour les éléments de la ListBox
            }
        }

        public ObservableCollection<object> CurrentItems
        {
            get { return currentItems; }
            set
            {
                currentItems = value;
                OnPropertyChanged(nameof(CurrentItems));
            }
        }

        public Boutique()
        {
            InitializeComponent();
            LoadAcheterItemsFromCsv("../../../CSV/boutique.csv"); // Spécifiez le chemin vers votre fichier CSV
            vendreItems = new ObservableCollection<Tresor>
            {
                new Tresor { Nom = "Tresor 1", Price = "100", ImagPath = "path/to/image1.png" },
                new Tresor { Nom = "Tresor 2", Price = "200", ImagPath = "path/to/image2.png" }
            };
            ButtonContent = "acheter";
            DataContext = this;
        }

        private void LoadAcheterItemsFromCsv(string filePath)
        {
            acheterItems = new ObservableCollection<Produit>();

            try
            {
                using (var reader = new StreamReader(filePath))
                {
                    reader.ReadLine();

                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var values = line.Split(';');
                        var produit = new Produit
                        {
                            Id = values[0],
                            Nom = values[1],
                            ImgPath = values[2],
                            Quantite = values[3],
                            Prix = values[4]
                        };
                        acheterItems.Add(produit);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error reading CSV file: {ex.Message}");
            }
        }


                private void UpdateCurrentItems()
        {
            if (ButtonContent == "acheter")
            {
                CurrentItems = new ObservableCollection<object>(acheterItems);
            }
            else
            {
                CurrentItems = new ObservableCollection<object>(vendreItems);
            }
        }

        private void Home(object sender, RoutedEventArgs e)
        {
            ((App)Application.Current).OpenGame();
            this.Close();
        }

        private void BuyButton(object sender, RoutedEventArgs e)
        {
            ButtonContent = "acheter";
        }

        private void SaleButton(object sender, RoutedEventArgs e)
        {
            ButtonContent = "vendre";
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
