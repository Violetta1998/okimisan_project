using System;
using System.Collections.Generic;
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

namespace okimisan_app
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class ActivatorWindow : Window
    {
        const string ACTIVATE_PASSWORD = "G7n8GH45Fs";
        const string HIDDEN_PASSWORD = "AD67Gb8S3N";
        const string FOLDER_NAME = "Okimisan";
        const string FILE_NAME = "security.pass";

        private string filePath;
        public ActivatorWindow()
        {
            InitializeComponent();

            string appData = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).FullName;
            var folderPath = System.IO.Path.Combine(appData, "Roaming", FOLDER_NAME);
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            filePath = System.IO.Path.Combine(appData, "Roaming", FOLDER_NAME, FILE_NAME);
            if (File.Exists(filePath))
            {
                var hiddenPassword = File.ReadAllText(filePath);
                if (hiddenPassword.Equals(HIDDEN_PASSWORD))
                {
                    Continue();
                }
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            Process();
        }

        public void Process()
        {
            if (textBox.Text.Equals(ACTIVATE_PASSWORD))
            {
                File.WriteAllText(filePath, HIDDEN_PASSWORD);
                Continue();
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

        private void Continue()
        {
            new AppWindow().Show();
            this.Close();
        }
    }
}
