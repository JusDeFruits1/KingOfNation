﻿using System;
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
    /// Logique d'interaction pour Taverne.xaml
    /// </summary>
    public partial class Taverne : Window
    {

        public Taverne()
        {
            InitializeComponent();

        }

        private void FarfadetMalicieux(object sender, RoutedEventArgs e)
        {
            Farfadet farfadet = new Farfadet();
            farfadet.Show();
            this.Close();
        }
    }
}
