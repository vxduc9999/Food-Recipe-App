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

namespace Pizzaria1
{
    /// <summary>
    /// Interaction logic for UserControlSettings.xaml
    /// </summary>
    public partial class UserControlSettings : UserControl
    {
        public UserControlSettings()
        {
            InitializeComponent();

        }

        MainWindow a = new MainWindow();
        

        private void _one_Click(object sender, RoutedEventArgs e)
        {
            a.Hide();
            a._background.Background = new SolidColorBrush(Color.FromRgb(20, 117, 193));
            a.GridCursor.Background= new SolidColorBrush(Color.FromRgb(34, 34, 34));
            a._bgtop.Background = new SolidColorBrush(Color.FromRgb(34, 34, 34));
            a.ShowDialog();
        }

        private void _two_Click(object sender, RoutedEventArgs e)
        {
            a._background.Background = new SolidColorBrush(Color.FromRgb(51, 153, 102));
            a.GridCursor.Background = new SolidColorBrush(Color.FromRgb(255, 187, 255));
            a._bgtop.Background = new SolidColorBrush(Color.FromRgb(255, 187, 255));
            a.ShowDialog();
        }

        private void _three_Click(object sender, RoutedEventArgs e)
        {
            a._background.Background = new SolidColorBrush(Color.FromRgb(192, 0, 0));
            a.GridCursor.Background = new SolidColorBrush(Color.FromRgb(205, 92, 92));
            a._bgtop.Background = new SolidColorBrush(Color.FromRgb(205, 92, 92));
            a.ShowDialog();
        }

        private void _four_Click(object sender, RoutedEventArgs e)
        {
            a._background.Background = new SolidColorBrush(Color.FromRgb(205, 92, 92));
            a.GridCursor.Background = new SolidColorBrush(Color.FromRgb(255, 187, 255));
            a._bgtop.Background = new SolidColorBrush(Color.FromRgb(255, 187, 255));
            a.ShowDialog();
        }

        private void _five_Click(object sender, RoutedEventArgs e)
        {
            a._background.Background = new SolidColorBrush(Color.FromRgb(34, 34, 34));
            a.ShowDialog();
        }
    }
}
