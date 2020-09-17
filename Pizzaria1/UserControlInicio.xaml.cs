using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
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
    /// Interação lógica para UserControlInicio.xam
    /// </summary>
    public partial class UserControlInicio : UserControl
    {
        public UserControlInicio()
        {
            InitializeComponent();
        }
        List<string> _banner = new List<string>()
        {
            "/Assets/bannerDA.png", "/Assets/banner2DA.png","/Assets/banner3DA.png","/Assets/banner4DA.png"
        };
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this._usercontrolInicio.Children.Clear();
            GridPrincipal.Children.Add(new UserControlEscolha());
        }

    }
}
