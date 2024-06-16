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

    public class RelayCommand : ICommand
    {
        private readonly Action<object> execute;
        private readonly Predicate<object> canExecute;

        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null || canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }

    public partial class Boutique : Window, INotifyPropertyChanged
    {
        private string buttonContent;
        private ObservableCollection<Produit> acheterItems;
        private ObservableCollection<Tresor> vendreItems;
        private ObservableCollection<object> currentItems;
        private object selectedItem;

        public string ButtonContent
        {
            get { return buttonContent; }
            set
            {
                if (buttonContent != value)
                {
                    buttonContent = value;
                    OnPropertyChanged(nameof(ButtonContent));
                    UpdateCurrentItems(); // Mettre à jour les éléments de la ListBox
                }
            }
        }

        public ObservableCollection<object> CurrentItems
        {
            get { return currentItems; }
            set
            {
                if (currentItems != value)
                {
                    currentItems = value;
                    OnPropertyChanged(nameof(CurrentItems));
                }
            }
        }

        public object SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if (selectedItem != value)
                {
                    selectedItem = value;
                    OnPropertyChanged(nameof(SelectedItem));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand BuyCommand { get; }

        public Boutique()
        {
            InitializeComponent();
            DataContext = this;
            ((App)Application.Current).timerJ.Tick += afficherBois;
            ((App)Application.Current).timerJ.Tick += afficherPierre;
            ((App)Application.Current).timerJ.Tick += afficherFer;
            ((App)Application.Current).timerJ.Tick += afficherOr;
            ((App)Application.Current).timerJ.Tick += afficherHab;
            ((App)Application.Current).timerJ.Start();
            LoadAcheterItemsFromCsv("../../../CSV/boutique.csv"); // Spécifiez le chemin vers votre fichier CSV
            vendreItems = new ObservableCollection<Tresor>();
            foreach (Tresor elt in ((App)Application.Current).Joueur.TresorsJoueur)
            {
                vendreItems.Add(elt);
            }
            ButtonContent = "acheter";
            vendreItems.CollectionChanged += VendreItems_CollectionChanged; // Ajoutez ceci pour vous abonner à l'événement CollectionChanged
            BuyCommand = new RelayCommand(BuyItem, CanBuyItem);
        }

        private bool CanBuyItem(object parameter)
        {
            return SelectedItem != null;
        }

        private void BuyItem(object parameter)
        {
            if (SelectedItem is Produit produit)
            {
                int.TryParse(produit.Prix, out int price);
                if (((App)Application.Current).Joueur.Or >= price)
                {
                    ((App)Application.Current).Joueur.Or -= price;
                    if (produit.Id == "1")
                    {
                        ((App)Application.Current).Joueur.Bois += 50;
                    }
                    else if (produit.Id == "2")
                    {
                        ((App)Application.Current).Joueur.Pierre += 50;
                    }
                    else if (produit.Id == "3")
                    {
                        ((App)Application.Current).Joueur.Fer += 50;
                    }
                    else if (produit.Id == "4")
                    {
                        ((App)Application.Current).Joueur.Bois += 100;
                    }
                    else if (produit.Id == "5")
                    {
                        ((App)Application.Current).Joueur.Pierre += 100;
                    }
                    else if (produit.Id == "6")
                    {
                        ((App)Application.Current).Joueur.Fer += 100;
                    }
                    else if (produit.Id == "7")
                    {
                        ((App)Application.Current).Joueur.Bois += 250;
                    }
                    else if (produit.Id == "8")
                    {
                        ((App)Application.Current).Joueur.Pierre += 250;
                    }
                    else if (produit.Id == "9")
                    {
                        ((App)Application.Current).Joueur.Fer += 250;
                    }

                    MessageBox.Show($"Achat réussi: {produit.Nom} pour {price} or.");
                }
                else
                {
                    MessageBox.Show("Vous n'avez pas assez d'or pour acheter cet article.");
                }
            }
            else if (SelectedItem is Tresor tresor)
            {
                int.TryParse(tresor.Price, out int price);
                ((App)Application.Current).Joueur.TresorsJoueur.Remove(tresor);                
                vendreItems.Remove(tresor);
                ((App)Application.Current).Joueur.Or += price;
                MessageBox.Show($"Vous avez vendu {tresor.Nom} pour {price} or.");
            }
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
            if (ButtonContent == "Acheter")
            {
                CurrentItems = new ObservableCollection<object>(acheterItems);
            }
            else
            {
                CurrentItems = new ObservableCollection<object>(vendreItems);
            }
        }

        private void VendreItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (ButtonContent == "Vendre")
            {
                CurrentItems = new ObservableCollection<object>(vendreItems);
            }
        }

        private void Home(object sender, RoutedEventArgs e)
        {
            Game game = new Game();
            game.Show();
            this.Close();
        }

        private void BuyButton(object sender, RoutedEventArgs e)
        {
            ButtonContent = "Acheter";
        }

        private void SaleButton(object sender, RoutedEventArgs e)
        {
            ButtonContent = "Vendre";
        }

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
    }
}