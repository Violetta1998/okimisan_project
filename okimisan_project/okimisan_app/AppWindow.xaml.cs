using okimisan_app.Managers;
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
            DataBaseManager.getInstance();
            Logic.Logic.execute((l) => {
                l.general.InitializeScreens();
                currentPage = l.general.currentPage;
                this.Content = l.general.getPage(currentPage);
            });

            Logic.Logic.onLogicUpdate((l) =>
            {
                if (_availableScreens.Contains(l.general.currentPage))
                {
                    //this.Topmost = l.general.currentPage != Logic.General.PAGES.Auth;
                    if (l.general.currentPage != currentPage)
                    {
                        this.Content = _main1Screens.Contains(l.general.currentPage) ? l.general.getPage(Logic.General.PAGES.Main1) : l.general.getPage(l.general.currentPage);
                        currentPage = l.general.currentPage;
                    }
                }
            });
        }

        private Logic.General.PAGES[] _main1Screens = new Logic.General.PAGES[] { Logic.General.PAGES.CreateOrder, Logic.General.PAGES.None, Logic.General.PAGES.ClientsList, Logic.General.PAGES.OrderList, Logic.General.PAGES.Categories };
        private Logic.General.PAGES[] _availableScreens = new Logic.General.PAGES[] { Logic.General.PAGES.Auth, Logic.General.PAGES.Main1, Logic.General.PAGES.None, Logic.General.PAGES.CreateOrder, Logic.General.PAGES.ClientsList, Logic.General.PAGES.OrderList, Logic.General.PAGES.Categories };
        private Logic.General.PAGES currentPage = Logic.General.PAGES.None;
    }
}
