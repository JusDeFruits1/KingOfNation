using KingOfNation.IHM;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Threading;

using Microsoft.VisualBasic.FileIO;
using KingOfNation.Code;

namespace KingOfNation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Attributes

        public bool gamestart = false;

        public DispatcherTimer timer = new DispatcherTimer();
        public DispatcherTimer timerJ = new DispatcherTimer();

        private DispatcherTimer? timerBois;
        private DispatcherTimer? timerPierre;
        private DispatcherTimer? timerFer;
        private DispatcherTimer? timerOr;
        private DispatcherTimer? timerHab;

        public Production Bois = new Production();
        public Production Pierre = new Production();
        public Production Fer = new Production();
        public Production Or = new Production();
        public Production Hab = new Production();

        public Joueur Joueur;
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Properties
        public WMPLib.WindowsMediaPlayer musicGame { get; set; }

        #endregion

        #region Operations

        protected override void OnStartup(StartupEventArgs e)
        {
            Joueur = new Joueur();
            musicGame = new WMPLib.WindowsMediaPlayer();
            timerJ.Interval = TimeSpan.FromSeconds(0);

            //Initialize and start timers
            timerBois = new DispatcherTimer();
            timerBois.Interval = TimeSpan.FromSeconds(6);
            timerBois.Tick += ProdBoisHandler;
            timerBois.Start();

            timerPierre = new DispatcherTimer();
            timerPierre.Interval = TimeSpan.FromSeconds(6);
            timerPierre.Tick += ProdPierreHandler;
            timerPierre.Start();

            timerFer = new DispatcherTimer();
            timerFer.Interval = TimeSpan.FromSeconds(7);
            timerFer.Tick += ProdFerHandler;
            timerFer.Start();

            timerOr = new DispatcherTimer();
            timerOr.Interval = TimeSpan.FromSeconds(8);
            timerOr.Tick += ProdOrHandler;
            timerOr.Start();

            timerHab = new DispatcherTimer();
            timerHab.Interval = TimeSpan.FromSeconds(10);
            timerHab.Tick += ProdHabHandler;
            timerHab.Start();

            MainWindow w = new MainWindow();
            w.Show();

        }

        private void ProdBoisHandler(object sender, EventArgs e)
        {
            ((App)Application.Current).Joueur.Bois = Bois.ProdBois();
        }

        private void ProdPierreHandler(object sender, EventArgs e)
        {
            ((App)Application.Current).Joueur.Pierre = Pierre.ProdPierre();
        }

        private void ProdFerHandler(object sender, EventArgs e)
        {
            ((App)Application.Current).Joueur.Fer = Fer.ProdFer();
        }

        private void ProdOrHandler(object sender, EventArgs e)
        {
            ((App)Application.Current).Joueur.Or = Or.ProdOr();
        }

        private void ProdHabHandler(object sender, EventArgs e)
        {
            ((App)Application.Current).Joueur.Hab = Hab.ProdHab();
        }

        public void OpenGame()
        {
            Game g = new Game(false);
            g.Show();
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}