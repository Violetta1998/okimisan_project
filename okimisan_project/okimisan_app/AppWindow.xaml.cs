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
            Logic.Logic.execute((l) => {
                l.general.InitializeScreens();
                currentPage = l.general.currentPage;
                this.Content = l.general.getPage(currentPage);
            });
            Logic.Logic.onLogicUpdate((l) =>
            {
                //this.Topmost = l.general.currentPage != Logic.General.PAGES.Auth;
                if (l.general.currentPage != currentPage)
                    this.Content = l.general.getPage(l.general.currentPage);
            });
        }

        private Logic.General.PAGES currentPage = Logic.General.PAGES.Auth;
    }
}
