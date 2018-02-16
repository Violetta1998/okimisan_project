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
using System.Windows.Shapes;

namespace okimisan_app
{
    /// <summary>
    /// Логика взаимодействия для AppWindow.xaml
    /// </summary>
    public partial class AppWindow : Window
    {
        public AppWindow()
        {
            InitializeComponent();

            Logic.Logic.onLogicUpdate((l) =>
            {
                //this.Topmost = l.general.currentPage != Logic.General.PAGES.Auth;
                if (l.general.currentPage!=currentPage)
                    this.frame.Navigate(new Uri("Screens/" + l.general.currentPage + ".xaml", UriKind.Relative));
            });
        }

        private Logic.General.PAGES currentPage = Logic.General.PAGES.Auth;
    }
}
