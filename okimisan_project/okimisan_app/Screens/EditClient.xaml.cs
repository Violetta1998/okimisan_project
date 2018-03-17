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
    /// Логика взаимодействия для EditClient.xaml
    /// </summary>
    public partial class EditClient : Page
    {
        const string HEADER_LABEL_EDIT = "Редактирование";
        const string HEADER_LABEL_ADD = "Добавление";

        public EditClient()
        {
            InitializeComponent();

            Logic.Logic.onLogicUpdate(l =>
            {
                HeaderLabel.Content = l.clients.editMode ? HEADER_LABEL_EDIT : HEADER_LABEL_ADD;
            });
        }

        private void Apply_Button_Click(object sender, RoutedEventArgs e)
        {
            Logic.Logic.execute(l => l.clients.editMode = !l.clients.editMode);
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            Logic.Logic.execute(l =>
            {
                l.clients.selectedClient = null;
                l.general.currentModalPage = Logic.General.MODAL_PAGES.None;
            });
        }
    }
}
