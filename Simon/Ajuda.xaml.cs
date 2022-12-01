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
    /// Lógica de interacción para Ajuda.xaml
    /// </summary>
    public partial class Ajuda : Window
    {
        public Ajuda()
        {
            InitializeComponent();
        }

        private void BtEnrere_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();

            main.Show();
            this.Hide();
        }
    }
}
