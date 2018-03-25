using okimisan_app.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        const string BUTTON_TEXT_EDIT = "Принять изменения";
        const string BUTTON_TEXT_ADD = "Добавить";
        const string ORDERS_QUALITY_TEXT = "Кол-во заказов: "; 
        const string LAST_ORDER_TEXT = "Послед заказ: ";
        private int[] discounts = new int[] { 0, 5, 10, 20, 30, 50, 100 };

        public EditClient()
        {
            InitializeComponent();

            Logic.Logic.onLogicUpdate(l =>
            {
                HeaderLabel.Content = l.clients.editMode ? HEADER_LABEL_EDIT : HEADER_LABEL_ADD;
                applyButtonLabel.Content = l.clients.editMode ? BUTTON_TEXT_EDIT : BUTTON_TEXT_ADD;
                phone.Text = l.clients.editMode && l.clients.selectedClient != null ? l.clients.selectedClient.phone : string.Empty;
                name.Text = l.clients.editMode && l.clients.selectedClient != null ? l.clients.selectedClient.name : string.Empty;
                street.Text = l.clients.editMode && l.clients.selectedClient != null ? l.clients.selectedClient.street : string.Empty;
                house.Text = l.clients.editMode && l.clients.selectedClient != null ? l.clients.selectedClient.house : string.Empty;
                build.Text = l.clients.editMode && l.clients.selectedClient != null ? l.clients.selectedClient.build : string.Empty;
                gateway.Text = l.clients.editMode && l.clients.selectedClient != null ? l.clients.selectedClient.gateway : string.Empty;
                floor.Text = l.clients.editMode && l.clients.selectedClient != null ? l.clients.selectedClient.floor : string.Empty;
                room.Text = l.clients.editMode && l.clients.selectedClient != null ? l.clients.selectedClient.room : string.Empty;
                intercom.Text = l.clients.editMode && l.clients.selectedClient != null ? l.clients.selectedClient.intercom : string.Empty;
                street2.Text = l.clients.editMode && l.clients.selectedClient != null ? l.clients.selectedClient.street2 : string.Empty;
                house2.Text = l.clients.editMode && l.clients.selectedClient != null ? l.clients.selectedClient.house2 : string.Empty;
                build2.Text = l.clients.editMode && l.clients.selectedClient != null ? l.clients.selectedClient.build2 : string.Empty;
                gateway2.Text = l.clients.editMode && l.clients.selectedClient != null ? l.clients.selectedClient.gateway2 : string.Empty;
                floor2.Text = l.clients.editMode && l.clients.selectedClient != null ? l.clients.selectedClient.floor2 : string.Empty;
                room2.Text = l.clients.editMode && l.clients.selectedClient != null ? l.clients.selectedClient.room2 : string.Empty;
                intercom2.Text = l.clients.editMode && l.clients.selectedClient != null ? l.clients.selectedClient.intercom2 : string.Empty;                
                discount.SelectedIndex = l.clients.editMode && l.clients.selectedClient != null && discounts.ToList().IndexOf(l.clients.selectedClient.discount) >=0 ? discounts.ToList().IndexOf(l.clients.selectedClient.discount) : 0;
                more.Document.Blocks.Clear();
                more.Document.Blocks.Add(new Paragraph(new Run(l.clients.editMode && l.clients.selectedClient != null ? l.clients.selectedClient.more : string.Empty)));
                ordersCount.Content = ORDERS_QUALITY_TEXT + (l.clients.editMode && l.clients.selectedClient != null ? l.clients.selectedClient.orders.ToString() : "0");
                lastOrder.Content = LAST_ORDER_TEXT + (l.clients.editMode && l.clients.selectedClient != null ? l.clients.selectedClient.last_order.ToString() : "-");
            });
        }

        private void Apply_Button_Click(object sender, RoutedEventArgs e)
        {
            Logic.Logic.execute(l => {
                if (!l.clients.editMode)
                    l.clients.selectedClient = new Data.Client();
                l.clients.selectedClient.phone = phone.Text;
                l.clients.selectedClient.name = name.Text;
                l.clients.selectedClient.street = street.Text;
                l.clients.selectedClient.house = house.Text;
                l.clients.selectedClient.build = build.Text;
                l.clients.selectedClient.gateway = gateway.Text;
                l.clients.selectedClient.floor = floor.Text;
                l.clients.selectedClient.room = room.Text;
                l.clients.selectedClient.intercom = intercom.Text;
                l.clients.selectedClient.street2 = street2.Text;
                l.clients.selectedClient.house2 = house2.Text;
                l.clients.selectedClient.build2 = build2.Text;
                l.clients.selectedClient.gateway2 = gateway2.Text;
                l.clients.selectedClient.floor2 = floor2.Text;
                l.clients.selectedClient.room2 = room2.Text;
                l.clients.selectedClient.intercom2 = intercom2.Text;
                l.clients.selectedClient.more = new TextRange(more.Document.ContentStart, more.Document.ContentEnd).Text;
                l.clients.selectedClient.discount = discounts[discount.SelectedIndex];

                DataBaseManager.getInstance().saveClient(l, l.clients.selectedClient);                

                l.clients.selectedClient = null;
                l.general.currentModalPage = Logic.General.MODAL_PAGES.None;
            });
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            Logic.Logic.execute(l =>
            {
                l.clients.selectedClient = null;
                l.general.currentModalPage = Logic.General.MODAL_PAGES.None;
            });
        }

        #region placehodlers
        private void street_TextChanged(object sender, TextChangedEventArgs e)
        {
            streetPlaceholder.Visibility = street.Text.Length == 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void house_TextChanged(object sender, TextChangedEventArgs e)
        {
            housePlaceholder.Visibility = house.Text.Length == 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void build_TextChanged(object sender, TextChangedEventArgs e)
        {
            buildPlaceholder.Visibility = build.Text.Length == 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void gateway_TextChanged(object sender, TextChangedEventArgs e)
        {
            gatewayPlaceholder.Visibility = gateway.Text.Length == 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void floor_TextChanged(object sender, TextChangedEventArgs e)
        {
            floorPlaceholder.Visibility = floor.Text.Length == 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void room_TextChanged(object sender, TextChangedEventArgs e)
        {
            roomPlaceholder.Visibility = room.Text.Length == 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void intercom_TextChanged(object sender, TextChangedEventArgs e)
        {
            intercomPlaceholder.Visibility = intercom.Text.Length == 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void street2_TextChanged(object sender, TextChangedEventArgs e)
        {
            street2Placeholder.Visibility = street2.Text.Length == 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void house2_TextChanged(object sender, TextChangedEventArgs e)
        {
            house2Placeholder.Visibility = house2.Text.Length == 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void build2_TextChanged(object sender, TextChangedEventArgs e)
        {
            build2Placeholder.Visibility = build2.Text.Length == 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void gateway2_TextChanged(object sender, TextChangedEventArgs e)
        {
            gateway2Placeholder.Visibility = gateway2.Text.Length == 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void floor2_TextChanged(object sender, TextChangedEventArgs e)
        {
            floor2Placeholder.Visibility = floor2.Text.Length == 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void room2_TextChanged(object sender, TextChangedEventArgs e)
        {
            room2Placeholder.Visibility = room2.Text.Length == 0 ? Visibility.Visible : Visibility.Collapsed;
        }

        private void intercom2_TextChanged(object sender, TextChangedEventArgs e)
        {
            intercom2Placeholder.Visibility = intercom2.Text.Length == 0 ? Visibility.Visible : Visibility.Collapsed;
        }
        #endregion placeholders

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
