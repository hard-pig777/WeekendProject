using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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
using System.Windows.Threading;
using System.IO;
using static System.Net.Mime.MediaTypeNames;

namespace WeekendProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int complexcity = 0;
        public bool isGaming = false;
        public string squirrelCords = "";
        public int score;
        public int health = 4;
        public int time = 1000;
        public int bestScoreEasy;
        public int bestScoreNormal;
        public int bestScoreDeadly;
        public bool isBestScore;
        public bool isSquirrelTapped = true;
        DispatcherTimer timer = new DispatcherTimer();
        public MainWindow()
        {
            InitializeComponent();
            D1.Visibility = Visibility.Hidden;
            D2.Visibility = Visibility.Hidden;
            D3.Visibility = Visibility.Hidden;
            A1.Visibility = Visibility.Hidden;
            A2.Visibility = Visibility.Hidden;
            A3.Visibility = Visibility.Hidden;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = TimeSpan.FromMilliseconds(time);
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            A1.Background = Brushes.White;
            B1.Background = Brushes.White;
            C1.Background = Brushes.White;
            D1.Background = Brushes.White;

            A2.Background = Brushes.White;
            B2.Background = Brushes.White;
            C2.Background = Brushes.White;
            D2.Background = Brushes.White;

            A3.Background = Brushes.White;
            B3.Background = Brushes.White;
            C3.Background = Brushes.White;
            D3.Background = Brushes.White;
            if(isSquirrelTapped == false)
            {
                Damage();
            }
            isSquirrelTapped = false;
            NewSquirrel();
        }
        public void NewSquirrel()
        {
            Random newSq = new Random();
            int newSqInt;
            if (complexcity == 0)
            {
                newSqInt = newSq.Next(1, 6 + 1);
            }else if (complexcity == 1)
            {
                newSqInt = newSq.Next(1, 8 + 1);
            }
            else
            {
                newSqInt = newSq.Next(1, 12 + 1);
            }
            if(newSqInt == 1)
            {
                squirrelCords = ("B1");
                B1.Background = Brushes.Green;
            }
            else if (newSqInt == 2)
            {
                squirrelCords = ("C1");
                C1.Background = Brushes.Green;
            }
            else if (newSqInt == 3)
            {
                squirrelCords = ("B2");
                B2.Background = Brushes.Green;
            }
            else if (newSqInt == 4)
            {
                squirrelCords = ("C2");
                C2.Background = Brushes.Green;
            }
            else if (newSqInt == 5)
            {
                squirrelCords = ("B3");
                B3.Background = Brushes.Green;
            }
            else if (newSqInt == 6)
            {
                squirrelCords = ("C3");
                C3.Background = Brushes.Green;
            }
            else if (newSqInt == 7)
            {
                squirrelCords = ("A2");
                A2.Background = Brushes.Green;
            }
            else if (newSqInt == 8)
            {
                squirrelCords = ("D2");
                D2.Background = Brushes.Green;
            }
            else if (newSqInt == 9)
            {
                squirrelCords = ("A1");
                A1.Background = Brushes.Green;
            }
            else if (newSqInt == 10)
            {
                squirrelCords = ("D1");
                D1.Background = Brushes.Green;
            }
            else if (newSqInt == 11)
            {
                squirrelCords = ("A3");
                A3.Background = Brushes.Green;
            }
            else 
            {
                squirrelCords = ("D3");
                D3.Background = Brushes.Green;
            }
        }
        private void ChoosingComplexcity(object sender, RoutedEventArgs e)
        { 
            normalButton.Background = Brushes.White;
            easyButton.Background = Brushes.White;
            deadlyButton.Background = Brushes.White;

            isGaming = false;

            Button whatChooseBut = (Button)sender;
            whatChooseBut.Background = Brushes.Yellow;
            if (whatChooseBut == easyButton )
            {
                time = 1000;
                complexcity = 0;
                D1.Visibility = Visibility.Hidden;
                D2.Visibility = Visibility.Hidden;
                D3.Visibility = Visibility.Hidden;
                A1.Visibility = Visibility.Hidden;
                A2.Visibility = Visibility.Hidden;
                A3.Visibility = Visibility.Hidden;
                string scoreString = ("Best Score: " + bestScoreEasy.ToString());
                TotalScore.Content = scoreString;
            }
            else if (whatChooseBut == normalButton )
            {
                time = 800;
                complexcity = 1;
                D1.Visibility = Visibility.Hidden;
                D2.Visibility = Visibility.Visible;
                D3.Visibility = Visibility.Hidden;
                A1.Visibility = Visibility.Hidden;
                A2.Visibility = Visibility.Visible;
                A3.Visibility = Visibility.Hidden;
                string scoreString = ("Best Score: " + bestScoreNormal.ToString());
                TotalScore.Content = scoreString;
            }
            else if(whatChooseBut == deadlyButton )
            {
                time = 600;
                complexcity = 2;
                D1.Visibility = Visibility.Visible;
                D2.Visibility = Visibility.Visible;
                D3.Visibility = Visibility.Visible;
                A1.Visibility = Visibility.Visible;
                A2.Visibility = Visibility.Visible;
                A3.Visibility = Visibility.Visible;
                string scoreString = ("Best Score: " + bestScoreDeadly.ToString());
                TotalScore.Content = scoreString;
            }
        }

        private void Start(object sender, RoutedEventArgs e)
        {
            timer.Start();
            isGaming = true;
        }

        private void Stop(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            isGaming = false;
            score = 0;
            string scoreString = ("Score: " + score.ToString());
            Score.Content = scoreString;
            health = 4;
            string path = WhichImage("Images/Heart");
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(path);
            bitmapImage.EndInit();
            Heart4.Source = bitmapImage;

            string path2 = WhichImage("Images/Heart");
            BitmapImage bitmapImage2 = new BitmapImage();
            bitmapImage2.BeginInit();
            bitmapImage2.UriSource = new Uri(path2);
            bitmapImage2.EndInit();
            Heart3.Source = bitmapImage2;

            string path3 = WhichImage("Images/Heart");
            BitmapImage bitmapImage3 = new BitmapImage();
            bitmapImage3.BeginInit();
            bitmapImage3.UriSource = new Uri(path3);
            bitmapImage3.EndInit();
            Heart2.Source = bitmapImage3;
            A1.Background = Brushes.White;
            B1.Background = Brushes.White;
            C1.Background = Brushes.White;
            D1.Background = Brushes.White;

            A2.Background = Brushes.White;
            B2.Background = Brushes.White;
            C2.Background = Brushes.White;
            D2.Background = Brushes.White;

            A3.Background = Brushes.White;
            B3.Background = Brushes.White;
            C3.Background = Brushes.White;
            D3.Background = Brushes.White;
        }
        public void PotentialSquirrelClick(object sender, RoutedEventArgs e)
        {
            Button squirrelBut = (Button)sender;
            if (isGaming == true)
            {
                if(squirrelBut.Name == squirrelCords)
                {
                    isSquirrelTapped = true;
                    score++;
                    A1.Background = Brushes.White;
                    B1.Background = Brushes.White;
                    C1.Background = Brushes.White;
                    D1.Background = Brushes.White;

                    A2.Background = Brushes.White;
                    B2.Background = Brushes.White;
                    C2.Background = Brushes.White;
                    D2.Background = Brushes.White;

                    A3.Background = Brushes.White;
                    B3.Background = Brushes.White;
                    C3.Background = Brushes.White;
                    D3.Background = Brushes.White;
                    squirrelCords = "";
                    string scoreString;
                    if (complexcity == 0 && score > bestScoreEasy)
                    {
                        bestScoreEasy = score;
                        scoreString = ("Best Score: " + bestScoreEasy.ToString());
                        TotalScore.Content = scoreString;
                    }
                    else if (complexcity == 1 && score > bestScoreNormal)
                    {
                        bestScoreNormal = score;
                        scoreString = ("Best Score: " + bestScoreNormal.ToString());
                        TotalScore.Content = scoreString;
                    }
                    else if (complexcity == 2 && score > bestScoreDeadly)
                    {
                        bestScoreDeadly = score;
                        scoreString = ("Best Score: " + bestScoreDeadly.ToString());
                        TotalScore.Content = scoreString;
                    }
                    scoreString = ("Score: " + score.ToString());
                    Score.Content = scoreString;
                }
                else
                {
                    Damage();
                }
            }
        }
        public void Damage()
        {
            health--;
            if(health < 1)
            {
                timer.Stop();
                score = 0;
                string scoreString = ("Score: " + score.ToString());
                Score.Content = scoreString;
                scoreString = ("Score: " + score.ToString());
                Score.Content = scoreString;
                isGaming = false;
                health = 4;
                A1.Background = Brushes.White;
                B1.Background = Brushes.White;
                C1.Background = Brushes.White;
                D1.Background = Brushes.White;

                A2.Background = Brushes.White;
                B2.Background = Brushes.White;
                C2.Background = Brushes.White;
                D2.Background = Brushes.White;

                A3.Background = Brushes.White;
                B3.Background = Brushes.White;
                C3.Background = Brushes.White;
                D3.Background = Brushes.White;

                string path = WhichImage("Images/Heart");
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(path);
                bitmapImage.EndInit();
                Heart4.Source = bitmapImage;

                string path2 = WhichImage("Images/Heart");
                BitmapImage bitmapImage2 = new BitmapImage();
                bitmapImage2.BeginInit();
                bitmapImage2.UriSource = new Uri(path2);
                bitmapImage2.EndInit();
                Heart3.Source = bitmapImage2;

                string path3 = WhichImage("Images/Heart");
                BitmapImage bitmapImage3 = new BitmapImage();
                bitmapImage3.BeginInit();
                bitmapImage3.UriSource = new Uri(path3);
                bitmapImage3.EndInit();
                Heart2.Source = bitmapImage3;
            }
            else if (health == 3)
            {
                string path = WhichImage("Images/BrokenHeart");
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(path);
                bitmapImage.EndInit();
                Heart4.Source = bitmapImage;
            }
            else if (health == 2)
            {
                string path = WhichImage("Images/BrokenHeart");
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(path);
                bitmapImage.EndInit();
                Heart3.Source = bitmapImage;
            }
            else if (health == 1)
            {
                string path = WhichImage("Images/BrokenHeart");
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(path);
                bitmapImage.EndInit();
                Heart2.Source = bitmapImage;
            }
        }
        private string WhichImage(string path)
        {
            path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "/" + path;


            if (File.Exists(path + ".jpg"))
            {
                return path + ".jpg";
            }
            if (File.Exists(path + ".png"))
            {
                return path + ".png";
            }
            if (File.Exists(path + ".jpeg"))
            {
                return path + ".jpeg";
            }
            if (File.Exists(path + ".webp"))
            {
                return path + ".webp";
            }
            return "";
        }
    }
}
