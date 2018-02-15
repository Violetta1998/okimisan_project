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

namespace okimisan_app
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class ActivatorWindow : Window
    {
        public ActivatorWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Process();
        }

        public void Process()
        {
            if (textBox.Text.Equals("slowno"))
            {
                new AppWindow().Show();
                this.Close();
            }
            else
            {
                textBox.Clear();
                label.Content = "Неверный пароль.";
            }
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key==Key.Enter)
            {
                Process();
            }
        }
    }
}
