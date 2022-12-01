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

namespace Simon
{
    /// <summary>
    /// Lógica de interacción para Pausa.xaml
    /// </summary>
    public partial class Pausa : Window
    {

        public Action OnClose { set; get; }
        public Action OnResume { get; set; }
           
        public Pausa()
        {
            InitializeComponent();
        }

        private void BtSortir_Click(object sender, RoutedEventArgs e)
        {
            MainWindow pantalla = new MainWindow();

            pantalla.Show();
            this.OnClose();
            this.Hide();

        }

        private void BtContinuar_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            this.OnResume();
        }
    }
}
