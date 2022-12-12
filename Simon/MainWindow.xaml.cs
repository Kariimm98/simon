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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Simon
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Singleton singleton;
        public MainWindow()
        {
            InitializeComponent();
            init();
        }

        private void init()
        {
            lbError.Visibility = Visibility.Hidden;
            singleton =  Singleton.Instance;
        }

        private void BtStart_Click(object sender, RoutedEventArgs e)
        {
            if (txNom.Text.Length == 0)
            {
                lbError.Visibility = Visibility.Visible;
                return;
            }

            PantallaJoc pantalla = initPantallaJoc();

            pantalla.Show();

            this.Hide();
        }

        private PantallaJoc initPantallaJoc()
        {
            PantallaJoc res = new PantallaJoc(txNom.Text);
            res.KeyDown += res.Window_KeyDown;
            res.KeyUp += res.Window_KeyUp;
            res.UnableKeys = () =>
            {
                res.KeyDown -= res.Window_KeyDown;
                res.KeyUp -= res.Window_KeyUp;
            };
            res.EnableKeys = () =>
            {
                res.KeyDown -= res.Window_KeyDown;
                res.KeyUp -= res.Window_KeyUp;
            };

            return res;
        }

        private void BtHelp_Click(object sender, RoutedEventArgs e)
        {
            Ajuda ajuda = new Ajuda();

            ajuda.Show();
            this.Close();
        }
    }
}
