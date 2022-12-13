using System;
using System.Collections.Generic;
using System.IO;
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
    /// 
    public struct MyData
    {
        public int puntuacio { set; get; }
        public string nom { set; get; }
    }
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
            getScores();
            lbError.Visibility = Visibility.Hidden;
            singleton =  Singleton.Instance;
        }

        private void getScores()
        {
            Dictionary<String, int> map = new Dictionary<string, int>();
            DataGridTextColumn col1 = new DataGridTextColumn();
            DataGridTextColumn col2 = new DataGridTextColumn();

            tbPuntuacions.Columns.Add(col1);
            tbPuntuacions.Columns.Add(col2);

            col1.Binding = new Binding("nom");
            col2.Binding = new Binding("puntuacio");

            col1.Header = "Nom";
            col2.Header = "Puntuació";

            StreamReader fichero = new StreamReader(System.IO.Path.GetFullPath(@"..\..\") + "/Resources/puntuacions.txt");
            String fitx = fichero.ReadToEnd();

            String[] puntuacions = fitx.Split(new string[] { Environment.NewLine },StringSplitOptions.None);

            foreach(String punt in puntuacions)
            {
                String[] row = punt.Split(' ');
                map.Add(row[0], int.Parse(row[1]));
            }


            foreach (var item in map.OrderByDescending(key => key.Value))
            {
                tbPuntuacions.Items.Add(new MyData
                {
                    puntuacio = item.Value,
                    nom = item.Key
                });
            }
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
            pantalla.start();

            this.Hide();
        }

        private PantallaJoc initPantallaJoc()
        {
            PantallaJoc res = new PantallaJoc(txNom.Text);
            res.KeyDown += res.Window_KeyDown;
            res.KeyUp += res.Window_KeyUp;

            res.UnableListener = () =>
            {
                res.KeyDown -= res.Window_KeyDown;
                res.KeyUp -= res.Window_KeyUp;
            };

            res.EnableListener = () =>
            {
                res.KeyDown += res.Window_KeyDown;
                res.KeyUp += res.Window_KeyUp;
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
