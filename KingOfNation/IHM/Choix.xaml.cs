using System;
using KingOfNation.IHM;
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
    /// Logique d'interaction pour Choix.xaml
    /// </summary>
    public partial class Choix : Window
    {
        public string empire = "";
        public Choix()
        {
            InitializeComponent();
        }

        private void Romain(object sender, RoutedEventArgs e)
        {
            empire = "Romain";

            this.Close();
        }

        private void Gaulois(object sender, RoutedEventArgs e)
        {
            empire = "Gaulois";

            this.Close();
        }

        private void Nippon(object sender, RoutedEventArgs e)
        {
            empire = "Nippon";

            this.Close();
        }

        private void Viking(object sender, RoutedEventArgs e)
        {
            empire = "Viking";

            this.Close();
        }

        private void Egypte(object sender, RoutedEventArgs e)
        {
            empire = "Egypte";

            this.Close();
        }

        private void Azteque(object sender, RoutedEventArgs e)
        {
            empire = "Azteque";

            this.Close();
        }
    }
}
