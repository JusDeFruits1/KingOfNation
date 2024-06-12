using System;
using Microsoft.VisualBasic.FileIO;
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
    /// Logique d'interaction pour Farfadet.xaml
    /// </summary>
    public partial class Farfadet : Window
    {
        public Farfadet()
        {
            WMPLib.WindowsMediaPlayer musicGame = ((App)Application.Current).musicGame;
            InitializeComponent();

            musicGame.controls.stop();
            musicGame.URL = "FarfadetM.mp3";
            musicGame.controls.play();
        }

    }
}
