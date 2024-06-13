using System;
using System.Collections.Generic;
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
    /// Logique d'interaction pour Boutique.xaml
    /// </summary>
    public partial class Boutique : Window
    {
        private string buttonContent;
        public string ButtonContent
        {
            get { return buttonContent; }
            set { buttonContent = value; }
        }
        public Boutique()
        {
            InitializeComponent();
            ButtonContent = "acheter";
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
    }
}
