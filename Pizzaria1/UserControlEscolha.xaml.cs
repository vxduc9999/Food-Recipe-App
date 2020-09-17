using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interação lógica para UserControlEscolha.xam
    /// </summary>
    public partial class UserControlEscolha : UserControl
    {
        public UserControlEscolha()
        {
            InitializeComponent();
        }
        ObservableCollection<Recipes_> _data;
        public Recipes_ NewRecipes_ { get; set; } = null;

        public static string RemoveUnicode(string text)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
            "đ",
            "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
            "í","ì","ỉ","ĩ","ị",
            "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
            "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
            "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
            "d",
            "e","e","e","e","e","e","e","e","e","e","e",
            "i","i","i","i","i",
            "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
            "u","u","u","u","u","u","u","u","u","u","u",
            "y","y","y","y","y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return text;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var folder = AppDomain.CurrentDomain.BaseDirectory;
            var database = $"{folder}AllFood.txt";
            var lines = File.ReadAllLines(database);
            int count = int.Parse(lines[0]);
            _data = new ObservableCollection<Recipes_>();
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

                _data.Add(recipes);
            }
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


        private void ButtonSearch_Click(object sender, MouseButtonEventArgs e)
        {
            // Nếu ô tìm kiếm rỗng, thì lấy tất cả sản phẩm
            if (SearchTexbox.Text.Length == 0)
            {
                dataListView.ItemsSource = _data.Take(12);
                this.Bot.Visibility = Visibility.Visible;
            }
            // Nếu ô tìm kiếm có nội dung   
            else
            {
                // Tạo mới danh sách sản phẩm có tên chứa nội dung ô tìm kiếm
                ObservableCollection<Recipes_> searchRecipes = new ObservableCollection<Recipes_>();
                for (int i = 0; i < _data.Count; i++)
                {
                    if (RemoveUnicode(_data[i].Title).ToLower().Contains(RemoveUnicode(SearchTexbox.Text).ToLower()) || RemoveUnicode(_data[i].Title).ToUpper().Contains(RemoveUnicode(SearchTexbox.Text).ToUpper())) // Nếu tìm thấy tên phù hợp
                    {
                        searchRecipes.Add(_data[i]); // Thì thêm vào danh sách mới
                    }
                }

                // Nếu tìm thấy ít nhất 1 sản phẩm thì hiển thị, không thì thông báo
                if (searchRecipes.Count > 0)
                {
                    this.Bot.Visibility = Visibility.Collapsed;
                    dataListView.ItemsSource = searchRecipes.Take(12);
                }
                else
                {
                    MessageBox.Show("Không tồn tại món ăn!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                // Làm trống ô tìm kiếm
                SearchTexbox.Text = "";
            }
        }
       
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
        private void ButtonFavorite_Click(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext;
            int index = dataListView.Items.IndexOf(item)+(12 * (info.CurrentPage-1));
           

            if (_data[index].Color == "Yellow" && _data[index].Icon == "Star")
            {
                
                _data[index].Color = "White";
                _data[index].Icon = "StarOutline";
                Thread thread = new Thread(delegate ()
                {
                    Dispatcher.Invoke(() =>
                    {
                        dataListView.ItemsSource = _data.Skip((info.CurrentPage-1) * info.RowsPerPage).Take(info.RowsPerPage);
                    });

                });
                thread.Start();
                var folder = AppDomain.CurrentDomain.BaseDirectory;
                var database = $"{folder}AllFood.txt";
                var lines = File.ReadAllLines(database);
                lines[index * 6 + 5] = "White";
                lines[index * 6 + 6] = "StarOutline";
                File.WriteAllLines(database, lines);
                
            }
            else
            {

                _data[index].Color = "Yellow";
                _data[index].Icon = "Star";
                NewRecipes_ = _data[index];
                Thread thread = new Thread(delegate ()
                {
                    Dispatcher.Invoke(() =>
                    {
                        dataListView.ItemsSource = _data.Skip((info.CurrentPage-1) * info.RowsPerPage).Take(info.RowsPerPage);
                    });

                });
                thread.Start();
                var folder = AppDomain.CurrentDomain.BaseDirectory;
                var db = $"{folder}AllFood.txt";
                var lnie = File.ReadAllLines(db);
                lnie[index * 6 + 5] = "Yellow";
                lnie[index * 6 + 6] = "Star";
                File.WriteAllLines(db, lnie);
            }
        }


        private void dataListview_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as ListView).SelectedItem as Recipes_;

            if (item != null)
            {
                _escolha.Children.Clear();
                _escolha.Children.Add(new UserControlDetailFood(item));
            }
        }

        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            //Nếu ô tìm kiếm rỗng, thì lấy tất cả sản phẩm
            if (SearchTexbox.Text.Length == 0)
            {
                dataListView.ItemsSource = _data.Take(12);
            }
            else
            {// Tạo mới danh sách sản phẩm có tên chứa nội dung ô tìm kiếm
                ObservableCollection<Recipes_> searchTrips = new ObservableCollection<Recipes_>();
                for (int i = 0; i < _data.Count; i++)
                {
                    if (RemoveUnicode(_data[i].Title).ToLower().Contains(RemoveUnicode(SearchTexbox.Text).ToLower())
                    || RemoveUnicode(_data[i].Title).ToUpper().Contains(RemoveUnicode(SearchTexbox.Text).ToUpper())) // Nếu tìm thấy tên phù hợp
                    {
                        searchTrips.Add(_data[i]); // Thì thêm vào danh sách mới
                    }
                }
                // Nếu tìm thấy ít nhất 1 sản phẩm thì hiển thị, không thì thông báo
                if (searchTrips.Count > 0 && searchTrips.Count <= 12)
                {
                    this.Bot.Visibility = Visibility.Collapsed;
                    dataListView.ItemsSource = searchTrips.Take(info.RowsPerPage);
                }
                else if (searchTrips.Count > 12)
                {
                    this.Bot.Visibility = Visibility.Visible;
                    dataListView.ItemsSource = searchTrips.Take(info.RowsPerPage);
                }
                else
                {
                    MessageBox.Show("Không tồn tại món ăn!!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                // Làm trống ô tìm kiếm
                SearchTexbox.Text = "";
            }
        }
    }
}
