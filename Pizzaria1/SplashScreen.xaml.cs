using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace Pizzaria1
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        public SplashScreen()
        {
            InitializeComponent();
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        ObservableCollection<string> desserts = new ObservableCollection<string>();

        string DataFile = "";
        string check;
        int item;

        List<string> _banner = new List<string>()
        {
            "/Assets/bannerDA.png", "/Assets/banner2DA.png","/Assets/banner3DA.png","/Assets/banner4DA.png"
        };

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string folder = AppDomain.CurrentDomain.BaseDirectory;
            DataFile = $"{folder}check.txt";

            var value1 = File.ReadAllLines(DataFile);
            check = value1[0];

            if (check == "0")
            {
                var p = new Random();
                int i = p.Next(0, _banner.Count);
                int index = i;
                var uri = new Uri(_banner[i], UriKind.Relative);
                avatarImage.Source = new BitmapImage(uri);
            }
            else if (check == "01")
            {
                this.Hide();
                var screen = new MainWindow();
                var choose = screen.ShowDialog();
                
            }
        }

        private void chbMain_Click(object sender, RoutedEventArgs e)
        {
            if (check == "0")
            {
                bool value;
                value = chbMain.IsChecked.Value;
                string folder = AppDomain.CurrentDomain.BaseDirectory;
                DataFile = $"{folder}check.txt";
                if (value == true)
                    item = 1;
                else
                    item = 0;
                File.AppendAllText(DataFile, $"{item}");
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (check == "0")
            {
                File.WriteAllLines(DataFile, desserts);
                File.AppendAllText(DataFile, $"{item}");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var screen = new MainWindow();
            screen.ShowDialog();
        }
    }
}
