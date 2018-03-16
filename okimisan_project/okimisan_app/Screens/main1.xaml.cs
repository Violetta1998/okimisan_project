﻿using System;
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

namespace okimisan_app.Screens
{
    /// <summary>
    /// Логика взаимодействия для Main1.xaml
    /// </summary>
    public partial class Main1 : Page
    {
        public Main1()
        {
            InitializeComponent();

            Logic.Logic.onLogicUpdate((l) =>
            {
                if (_availableScreens.Contains(l.general.currentPage))
                {
                    if (l.general.currentPage != currentPage)
                    {
                        frame.Content = l.general.getPage(l.general.currentPage);
                        currentPage = l.general.currentPage;
                    }
                }
                frame.Visibility = l.general.currentPage == Logic.General.PAGES.None ? Visibility.Collapsed : Visibility.Visible;
                image.Visibility = ((l.general.currentPage == Logic.General.PAGES.None) || (l.general.currentPage == Logic.General.PAGES.Main1)) ? Visibility.Visible : Visibility.Collapsed;
            });
        }

        private Logic.General.PAGES[] _availableScreens = new Logic.General.PAGES[] { Logic.General.PAGES.CreateOrder, Logic.General.PAGES.UserList, Logic.General.PAGES.OrderList };
        private Logic.General.PAGES currentPage = Logic.General.PAGES.None;
    }
}
