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
            PantallaJoc j = new PantallaJoc();
            if (txNom.Text.Length == 0)
            {
                lbError.Visibility = Visibility.Visible;
                return;
            }

            singleton.Nom = txNom.Text;

            j.Show();

            this.Hide();
        }

        private void BtHelp_Click(object sender, RoutedEventArgs e)
        {
            Ajuda ajuda = new Ajuda();

            ajuda.Show();
            this.Close();
        }
    }
}
