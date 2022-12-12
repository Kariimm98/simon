using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using System.Windows.Input;
using System.Windows.Media;


namespace Simon
{
    /// <summary>
    /// Lógica de interacción para PantallaJoc.xaml
    /// </summary>
    public partial class PantallaJoc : Window
    {
        Stack<int> actual = new Stack<int>();
        List<int> historic= new List<int>();
        List<ColorButton> butons = new List<ColorButton>();
        public Action UnableKeys { get; set; }
        public Action EnableKeys { get; set; }
        ColorButton currentButton;
        Random random = new Random();
        String nom = "";
        int puntuacio = 0;
        int interval = 1000;

        System.Windows.Threading.DispatcherTimer dispatcher = new System.Windows.Threading.DispatcherTimer();

        public PantallaJoc(String nom)
        {               
            this.nom = nom;
            InitializeComponent();
            init();
        }

        private void init()
        {
            initButonsColors();
            lbError.Visibility = Visibility.Hidden;
            lbScore.Content = "0";
            initTimer();
            initListButtons();
            historic = new List<int>();
            int number =getNextNumber();
            historic.Add(number);
            initSequence();
        }

        private void initButonsColors()
        {
            btW.CurrentColor = Colors.Red;
            btW.UnselectColor = Color.FromRgb(97,0,0);
            btA.CurrentColor = Color.FromRgb(0,210,0);
            btA.UnselectColor = Color.FromRgb(0,97,0);
            btS.CurrentColor = Colors.Yellow;
            btS.UnselectColor = Color.FromRgb(97,97,0);
            btD.CurrentColor = Colors.Blue;
            btD.UnselectColor = Color.FromRgb(0,0,97);
        }

        private void test()
        {
            int number = getNextNumber();
            historic.Add(number);

            number = getNextNumber();
            historic.Add(number);

            number = getNextNumber();
            historic.Add(number);
        }

        private void initSequence()
        {
            this.Background = new SolidColorBrush(Color.FromRgb(220, 220, 220));
            changeDifficult();
            removeListeners();
            actual = new Stack<int>(historic);
            dispatcher.Start();

        }

        private void reset()
        {
            resetColorButtons();
            lbScore.Content = "0";
            dispatcher = new System.Windows.Threading.DispatcherTimer();
            init();
        }

        private void changeDifficult()
        {
            int score = Int32.Parse(lbScore.Content.ToString());

            if (score <= 2)
            {

                //interval = 1000;
                interval = 250;
            }
            else if (score <= 5)
            {
                //interval = 750;
                interval = 100;

            }
            /*
            else if (score <= 7)
            {
                //interval = 500;
                    interval = 50;
            }
            else if (score <= 10)
            {

                    interval = 250;
            }
            else if (score <= 15)
            {

                    interval = 100;
            }
            */
            else
            {
                interval = 50;
            }

            dispatcher.Interval = TimeSpan.FromMilliseconds(interval);
        }

        private void removeListeners()
        {
            btW.MouseLeftButtonDown -= MouseDownEvent;
            btA.MouseLeftButtonDown -= MouseDownEvent;
            btS.MouseLeftButtonDown -= MouseDownEvent;
            btD.MouseLeftButtonDown -= MouseDownEvent;

            btW.MouseLeftButtonUp-= MouseUpEvent;
            btA.MouseLeftButtonUp -= MouseUpEvent;
            btS.MouseLeftButtonUp -= MouseUpEvent;
            btD.MouseLeftButtonUp -= MouseUpEvent;
           // if(this.UnableKeys!=null)
             //   this.UnableKeys();
        }

        private void implementEvents()
        {
            btW.MouseLeftButtonDown += MouseDownEvent;
            btA.MouseLeftButtonDown += MouseDownEvent;
            btS.MouseLeftButtonDown += MouseDownEvent;
            btD.MouseLeftButtonDown += MouseDownEvent;

            btW.MouseLeftButtonUp += MouseUpEvent;
            btA.MouseLeftButtonUp += MouseUpEvent;
            btS.MouseLeftButtonUp += MouseUpEvent;
            btD.MouseLeftButtonUp += MouseUpEvent;
          //  if (this.EnableKeys != null)
            //     this.EnableKeys();
        }

        private void initTimer()
        {
            dispatcher.Tick += new EventHandler(initSequence);
            dispatcher.Interval = TimeSpan.FromMilliseconds(300);

        }

        private void initListButtons()
        {
            butons.Add(btW);
            butons.Add(btA);
            butons.Add(btS);
            butons.Add(btD);
        }

        private ColorButton findButtonByTag(int tag)
        {
            currentButton = butons.Find(buto => buto.Tag.Equals(tag + ""));

            return currentButton;
        }

        private int getNextNumber()
        {
            return random.Next(1, 5);
        }



        private void initSequence(object sender, EventArgs e)
        {
            
            if (interval != dispatcher.Interval.TotalMilliseconds)
                return;

            if (currentButton!=null)
            {
                currentButton.Background = new SolidColorBrush(currentButton.UnselectColor);
                currentButton = null;
                return;
            }
            else if(actual.Count>0)
            {

                findButtonByTag(actual.Pop());
                currentButton.Background = new SolidColorBrush(currentButton.CurrentColor);
                return;
            }
            else
            {
                actual = new Stack<int>(historic);
                implementEvents();
                this.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                dispatcher.Stop();
            }
        }

        private void Timer_Tick()
        {
            var now = DateTime.Now;
            var nowMilliseconds = (int)now.TimeOfDay.TotalMilliseconds;
            var timerInterval = interval -
             nowMilliseconds % interval + 5;//5: sometimes the tick comes few millisecs early
            dispatcher.Interval = TimeSpan.FromMilliseconds(timerInterval);
        }

            private void validarOpcio(ColorButton btn)
        {
            int tag = Int32.Parse(btn.Tag.ToString());

            if (tag == actual.Peek())
            {
                actual.Pop();
            }
            else
            {
                lbError.Visibility = Visibility.Visible;
                Pausa pausa = new Pausa();

                initButonsColors();

                pausa.OnClose = () =>
                {
                    MainWindow main = new MainWindow();
                    main.Show();
                    this.Hide();
                };

                pausa.OnResume = () =>
                {
                    this.reset();
                };

                pausa.ShowDialog();
                
            }
        }

        private void resetColorButtons()
        {
            btW.Background = new SolidColorBrush(btW.UnselectColor);
            btA.Background = new SolidColorBrush(btA.UnselectColor);
            btS.Background = new SolidColorBrush(btS.UnselectColor);
            btD.Background = new SolidColorBrush(btD.UnselectColor);
        }


        private void MouseDownEvent(object sender, MouseButtonEventArgs e)
        {   

            ColorButton btn = sender as ColorButton;
            mouseDownMethod(btn);
        }

        private void MouseUpEvent(object sender, MouseButtonEventArgs e)
        {
            ColorButton btn = sender as ColorButton;
            mouseUpMethod(btn);
        }

        private void mouseUpMethod(ColorButton btn)
        {
            btn.Background = new SolidColorBrush(btn.UnselectColor);
            if (actual.Count == 0 && lbError.Visibility == Visibility.Hidden)
            {
                int puntuacio = Int32.Parse(lbScore.Content.ToString()) + 1;
                lbScore.Content = puntuacio + "";
                historic.Insert(0, getNextNumber());
                initSequence();
            }
            else
            {

            }
        }

        private void mouseDownMethod(ColorButton btn)
        {
            btn.Background = new SolidColorBrush(btn.CurrentColor);
            validarOpcio(btn);
        }

        private void BtBack_Click(object sender, RoutedEventArgs e)
        {
            Pausa pantalla = new Pausa();

            dispatcher.Stop();
            pantalla.OnResume = () =>this.dispatcher.Start();
            pantalla.OnClose = () => this.Hide();

            pantalla.ShowDialog();
            
        }

        public void Window_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.Key == Key.W)
            {
                mouseDownMethod(findButtonByTag(1));
            }else if (e.Key == Key.A)
            {
                mouseDownMethod(findButtonByTag(2));
            }
            else if (e.Key == Key.S)
            {
                mouseDownMethod(findButtonByTag(3));
            }
            else if (e.Key == Key.D)
            {
                mouseDownMethod(findButtonByTag(4));
            }
        }

        public void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W)
            {
                mouseUpMethod(findButtonByTag(1));
            }
            else if (e.Key == Key.A)
            {
                mouseUpMethod(findButtonByTag(2));
            }
            else if (e.Key == Key.S)
            {
                mouseUpMethod(findButtonByTag(3));
            }
            else if (e.Key == Key.D)
            {
                mouseUpMethod(findButtonByTag(4));
            }
        }

        private void MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            int tag = Int32.Parse(((TextBlock)sender).Tag.ToString());
            ColorButton btn = findButtonByTag(tag);
            mouseUpMethod(btn);
        }

        private void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            int tag = Int32.Parse(((TextBlock)sender).Tag.ToString());
            ColorButton btn = findButtonByTag(tag);
            mouseDownMethod(btn);
        }


    }
}
