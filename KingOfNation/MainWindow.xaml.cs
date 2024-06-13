using System.Text;
using KingOfNation.IHM;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KingOfNation
{
    public partial class MainWindow : Window
    {
        #region Attributes

        #endregion
        
        #region Properties
        #endregion

        #region Constructors        
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion

        #region Operations
        private void Start(object sender, RoutedEventArgs e)
        {
            Choix choix = new Choix();
            choix.Show();

            this.Close();
        }
        #endregion
    }
}