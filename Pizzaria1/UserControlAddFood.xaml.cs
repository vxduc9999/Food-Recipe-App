using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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

namespace Pizzaria1
{
    /// <summary>
    /// Interaction logic for UserControlAddFood.xaml
    /// </summary>
    public partial class UserControlAddFood : UserControl
    {
        public UserControlAddFood()
        {
            InitializeComponent();
        }
        int i = 1;
        int temp = -1;
        int indexAfterChangeStep = -1;
        BindingList<Recipes_> _list;
        ObservableCollection<string> listImages = new ObservableCollection<string>();

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            _list = new BindingList<Recipes_>();
            dataComboBox.ItemsSource = _list;
        }


        private void addStepButton_Click(object sender, RoutedEventArgs e)
        {
            if (temp == -1)
            {
                var tp = new Recipes_()
                {
                    Stepr = "Bước " + i.ToString(),
                    Description = descriptionStep.Text,
                    Imagess = new BindingList<string>()
                };
                foreach (string itemm in listImages)
                {
                    tp.Imagess.Add(itemm);
                }
                _list.Add(tp);
                descriptionStep.Text = "";
                listImages.Clear();
                listImageStep.ItemsSource = listImages;
                i++;
            }
            else
            {
                temp = -1;
                MessageBoxResult rs = System.Windows.MessageBox.Show("Bạn có chắc chắn muốn thay đổi!", "!", MessageBoxButton.OKCancel);
                if (rs == MessageBoxResult.OK)
                {
                    _list[indexAfterChangeStep].Description = descriptionStep.Text;
                    _list[indexAfterChangeStep].Imagess.Clear();
                    foreach (string itemm in listImages)
                    {
                        _list[indexAfterChangeStep].Imagess.Add(itemm);
                    }
                    descriptionStep.Text = "";
                    listImages.Clear();
                    listImageStep.ItemsSource = listImages;
                    addStepButton.Content = "AddStep";
                }
                dataComboBox.ItemsSource = _list;
                CollectionViewSource.GetDefaultView(dataComboBox.ItemsSource).Refresh();
            }
        }

        private void SaveToDB(object sender, RoutedEventArgs e)
        {
            if (FoodName.Text.Trim() != "" && ImageDescriptionOfRecipe.ImageSource != null && Descipt.Text.Trim() != "")
            {

                MessageBoxResult result = MessageBox.Show("Do you want to save", "", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    // Luu ten, mo ta, hinh anh, youtube => xuat ra man hinh Home
                    var folder = AppDomain.CurrentDomain.BaseDirectory;
                    var avatar = $"{folder}Page";
                    var database = $"{folder}AllFood.txt";
                    var lines = File.ReadAllLines(database);
                    int count = int.Parse(lines[0]);
                    count++;
                    lines[0] = (count++).ToString();
                    File.WriteAllLines(database, lines);
                    using (StreamWriter sw = File.AppendText(database))
                    {
                        sw.WriteLine(FoodName.Text);
                        string imgdes =$"/Page/" +  System.IO.Path.GetFileName(ImageDescriptionOfRecipe.ImageSource.ToString());
                        sw.WriteLine(imgdes);
                        sw.WriteLine(Descipt.Text);
                        sw.WriteLine(LinkYtb.Text);
                        sw.WriteLine("White");
                        sw.WriteLine("StarOutline");
                    }
                    string imgdesr = System.IO.Path.GetFileName(ImageDescriptionOfRecipe.ImageSource.ToString());
                    var imgAv = ((BitmapImage)ImageDescriptionOfRecipe.ImageSource).UriSource.ToString().Remove(0, 8);
                    var appStartPath11 = String.Format(avatar + "\\" + imgdesr);
                    File.Copy(imgAv, appStartPath11, true);


                    BindingList<Recipes_> t = _list;
                    String appStartPath = System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName);
                    appStartPath = appStartPath + "\\ListFood";
                    string path2 = System.IO.Path.Combine(appStartPath, FoodName.Text);

                    if (!Directory.Exists(path2))
                    {
                        Directory.CreateDirectory(path2);
                        string path3 = path2 + $"\\{FoodName.Text}.txt";
                        if (File.Exists(path3))
                        {
                            File.Delete(path3);
                        }
                        using (StreamWriter sw = File.CreateText(path3))
                        {
                            // Lưu mô tả và hình ảnh đại diện cho món ăn
                            sw.WriteLine(FoodName.Text);
                            string imgdes = System.IO.Path.GetFileName(ImageDescriptionOfRecipe.ImageSource.ToString());
                            var imgdes2 = ((BitmapImage)ImageDescriptionOfRecipe.ImageSource).UriSource.ToString().Remove(0, 8);
                            
                            sw.WriteLine(Descipt.Text);
                            sw.WriteLine(LinkYtb.Text);
                            sw.WriteLine(imgdes);
                            appStartPath = String.Format(path2 + "\\" + imgdes);
                            File.Copy(imgdes2, appStartPath, true);

                            // Lưu từng bước gồm hình ảnh, mô tả, link youtobe cho món ăn
                            for (int i = 0; i < t.Count; i++)
                            {
                                sw.WriteLine(t[i].Stepr);
                                sw.WriteLine(t[i].Description);
                                foreach (string nameImg in t[i].Imagess)
                                {
                                    string name = System.IO.Path.GetFileName(nameImg);
                                    sw.WriteLine(name);
                                    appStartPath = String.Format(path2 + "\\" + name);
                                    File.Copy(nameImg, appStartPath, true);
                                }
                            }
                        }
                    }
                    UserControlAddFood _home = new UserControlAddFood();
                    fooda.Children.Clear();
                    fooda.Children.Add(_home);
                }
            }
            else
                MessageBox.Show("You did not enter the title, description or avatar image!!!");
        }

        private void ChooseImages(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Multiselect = false;
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            bool? result = open.ShowDialog();
            if (result == true)
            {
                var img = open.FileNames;
                ImageSource imgsource = new BitmapImage(new Uri(img[0].ToString()));
                ImageDescriptionOfRecipe.ImageSource = imgsource;
            }
        }

        private void Change(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext;
            int index = dataComboBox.Items.IndexOf(item);

            descriptionStep.Text = _list[index].Description;
            listImageStep.ItemsSource = _list[index].Imagess;
            foreach (string items in _list[index].Imagess)
            {
                listImages.Add(items);
            }
            temp = 0;

            indexAfterChangeStep = index;
            addStepButton.Content = "OK";
        }

        private void RemoveStep(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext;
            int index = dataComboBox.Items.IndexOf(item);

            _list.Remove(_list[index]);
            int k = 1;
            i--;
            foreach (var recipe in _list)
            {
                _list[k - 1].Stepr = "Bước " + k.ToString();
                k++;
            }
            dataComboBox.ItemsSource = _list;
            CollectionViewSource.GetDefaultView(dataComboBox.ItemsSource).Refresh();
        }

        private void addImageStep_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Multiselect = true;
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            bool? result = open.ShowDialog();
            if (result == true)
            {
                if (open.FileNames.Length <= 10)
                {
                    foreach (string item in open.FileNames)
                    {
                        listImages.Add(item);
                    }
                    listImageStep.ItemsSource = listImages;
                }
                else
                {
                    System.Windows.MessageBox.Show("Ảnh không được quá 10");
                }
            }
        }

        private void btnRemoveImg(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext;
            int index = listImageStep.Items.IndexOf(item);
            listImages.RemoveAt(index);
            listImageStep.ItemsSource = listImages;
        }

        private void btnEditImg(object sender, RoutedEventArgs e)
        {
            var item = (sender as FrameworkElement).DataContext;
            int index = listImageStep.Items.IndexOf(item);

            OpenFileDialog open = new OpenFileDialog();
            open.Multiselect = false;
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            bool? result = open.ShowDialog();
            if (result == true)
            {
                string newImg = open.FileNames[0];
                MessageBoxResult resultChangeImage = System.Windows.MessageBox.Show("Bạn có chắc chắn muốn sửa!", "!", MessageBoxButton.OKCancel);
                if (resultChangeImage == MessageBoxResult.OK)
                {
                    listImages[index] = newImg;
                    listImageStep.ItemsSource = listImages;
                }
            }
        }

        private void Upload_Click(object sender, MouseButtonEventArgs e)
        {

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            UserControlEscolha _home = new UserControlEscolha();
            fooda.Children.Clear();
            fooda.Children.Add(_home);
        }
    }
}
