using okimisan_app.Assets;
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
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Page
    {
        public Auth()
        {
            InitializeComponent();
        }

        private string password_label = "ПАРОЛЬ";
        private string name_label = "ИМЯ";

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Logic.Logic.execute((l) =>
            {
                l.auth.name = additionalTextBox.Text;
                l.auth.password = additionalPasswordBox.Password.ToString();
                l.auth.userType = (UserType)(comboBox.SelectedIndex+1);
                try
                {
                    l.auth.LogIn();
                    notification.Content = string.Empty;
                }
                catch(Exception ex)
                {
                    notification.Content = ex.Message;
                }
            });
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (additionalLabel != null)
            {
                additionalLabel.Content = NameTextBoxAllow() ? name_label : password_label;
                additionalTextBox.Visibility = NameTextBoxAllow() ? Visibility.Visible : Visibility.Collapsed;
                additionalPasswordBox.Visibility = NameTextBoxAllow() ? Visibility.Collapsed : Visibility.Visible;
            }
        }

        private bool NameTextBoxAllow()
        {
            return comboBox.SelectedIndex == 0;
        }
    }
}
