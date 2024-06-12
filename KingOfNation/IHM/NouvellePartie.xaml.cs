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
    public partial class NouvellePartie : Window
    {
        public string Pseudo { get; private set; }
        public string NomVille { get; private set; }

        public NouvellePartie()
        {
            InitializeComponent();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            Pseudo = PseudoTextBox.Text;
            NomVille = NomVillageTextBox.Text;

            if (string.IsNullOrEmpty(Pseudo))
            {
                MessageBox.Show("Le pseudo du joueur ne peut pas être vide.");
                return;
            }
            if (string.IsNullOrEmpty(NomVille))
            {
                MessageBox.Show("Le nom du village ne peut pas être vide.");
                return;
            }

            this.DialogResult = true;
            this.Close();
        }
    }
}
