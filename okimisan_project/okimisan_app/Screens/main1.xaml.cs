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
                info.Content = l.auth.userType.ToString() + l.auth.name + l.auth.password;
            });
        }
    }
}
