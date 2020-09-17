using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for UserControlLikeDishes.xaml
    /// </summary>
    public partial class UserControlLikeDishes : UserControl
    {
        public UserControlLikeDishes()
        {
            InitializeComponent();
        }
        BindingList<Recipes_> _data;
        public Recipes_ _f;
        public void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var folder = AppDomain.CurrentDomain.BaseDirectory;
            var database = $"{folder}AllFood.txt";
            var lines = File.ReadAllLines(database);
            int count = int.Parse(lines[0]);
            _data = new BindingList<Recipes_>();
            for (int i = 0; i < count; i++)
            {

                var line1 = lines[i * 6 + 1];
                var line2 = lines[i * 6 + 2];
                var line3 = lines[i * 6 + 3];
                var line4 = lines[i * 6 + 4];
                var line5 = lines[i * 6 + 5];
                var line6 = lines[i * 6 + 6];

                var recipes = new Recipes_()
                {
                    Title = line1,
                    Avatar = line2,
                    Description = line3,
                    Youtube = line4,
                    Color = line5,
                    Icon = line6
                };
                if (recipes.Color == "Yellow")
                {
                    _data.Add(recipes);
                }
            }
            if (_data.Count < 12)
                Bot.Visibility = Visibility.Hidden;
            else if (_data.Count > 12)
                this.Bot.Visibility = Visibility.Visible;

            info.CurrentPage = 1;
            info.RowsPerPage = 12;
            info.Count = _data.Count;
            info.TotalPages = (info.Count / info.RowsPerPage) +
                (info.Count % info.RowsPerPage == 0 ? 0 : 1);

            Thread thread = new Thread(delegate ()
            {
                // Cập nhật UI
                Dispatcher.Invoke(() =>
                {
                    dataListView.ItemsSource = _data.Take(info.RowsPerPage);
                    dataListView.SelectionChanged += CategoriesComboBox_SelectionChanged;
                    dataListView.SelectedIndex = 0;
                });
            });
            thread.Start();
        }
        
        public Recipes_ NewRecipes_ { get; set; } = null;
        PagingInfo info = new PagingInfo();
        public class PagingInfo : INotifyPropertyChanged
        {
            public int TotalPages { get; set; }

            public int currentPage = 1;
            public int CurrentPage
            {
                get => currentPage;
                set
                {
                    currentPage = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentPage"));
                }
            }

            public int Count { get; set; }
            public int RowsPerPage { get; set; }
            public event PropertyChangedEventHandler PropertyChanged;
        }
        private void Prev_Click(object sender, RoutedEventArgs e)
        {
            if (info.CurrentPage <= info.TotalPages)
            {
                info.CurrentPage--;
                dataListView.ItemsSource =
                _data
                    .Skip((info.CurrentPage - 1) * info.RowsPerPage)
                    .Take(info.RowsPerPage);
                if (info.CurrentPage <= 1)
                {
                    info.CurrentPage = 1;
                }
            }
        }
        private void CategoriesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            info.RowsPerPage = 12;
            info.Count = _data.Count;
            info.TotalPages = (info.Count / info.RowsPerPage) +
                (info.Count % info.RowsPerPage == 0 ? 0 : 1);
            dataListView.ItemsSource =
            _data
                .Skip((info.CurrentPage - 1) * info.RowsPerPage)
                .Take(info.RowsPerPage);
            pagingInfoStackPanel.DataContext = info;
        }
        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (info.CurrentPage < info.TotalPages)
            {
                info.CurrentPage++;
                dataListView.ItemsSource =
                _data
                    .Skip((info.CurrentPage - 1) * info.RowsPerPage)
                    .Take(info.RowsPerPage);
            }
        }
    }
}
