using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for UserControlDetailFood.xaml
    /// </summary>
    public partial class UserControlDetailFood : UserControl
    {
        private Recipes_ _data;
        BindingList<Recipes_> _list;
        ObservableCollection<string> listImages = new ObservableCollection<string>();
        string nameTest;
        public UserControlDetailFood(Recipes_ r)
        {
            InitializeComponent();
            _data = r;
            nameTest = _data.Title;
        }
        int i = 4, j = 1;
        string step = "Bước";
        public string NameTest { get => nameTest; set => nameTest = value; }

        private void PackIcon_PreviewMouseDoubleClick(object sender, RoutedEventArgs e)
        {
                UserControlEscolha _home = new UserControlEscolha();
                _detail.Children.Clear();
                _detail.Children.Add(_home);
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            System.Diagnostics.Process.Start(e.Uri.AbsoluteUri);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _list = new BindingList<Recipes_>();
            dataComboBox.ItemsSource = _list;
            //nameTest = name.Text;
            String appStartPath = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
            appStartPath = appStartPath + $"\\ListFood\\{NameTest}\\";
            string fileTxt = appStartPath + $"{NameTest}.txt";
            var readTest = File.ReadAllLines(fileTxt);

            Foodname.Text = readTest[0];
            Discription.Text = readTest[1];
            tenlinkyt.Text = readTest[2];
            linkyt.NavigateUri = new Uri(tenlinkyt.Text);

            ImageSource imageSource = new BitmapImage(new Uri(appStartPath + readTest[3]));
            img.ImageSource = imageSource;
            while (i < readTest.Length)
            {
                var g = new Recipes_()
                {
                    Title = readTest[0],
                    Stepr = "",
                    Description = "",
                    Imagess = new BindingList<string>()
                };
                ObservableCollection<string> listImages = new ObservableCollection<string>();
                if (step + " " + j.ToString() == readTest[i])
                {
                    g.Stepr = readTest[i];
                    g.Description = readTest[i + 1];
                    i += 2;
                    for (int k = i, temp = j + 1; ; k++)
                    {
                        if (k >= readTest.Length)
                        {
                            i = k;
                            j++;
                            break;
                        }

                        if (step + " " + temp.ToString() == readTest[k] && k < readTest.Length)
                        {
                            i = k;
                            j++;
                            break;
                        }
                        g.Imagess.Add(appStartPath + readTest[k]);
                    }
                    _list.Add(g);
                }
            }
        }
    }
}
