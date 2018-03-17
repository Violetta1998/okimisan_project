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
using System.Data.OleDb;
using System.Data;
using okimisan_app.Managers;
using si = System.Windows.Forms.SystemInformation;

namespace okimisan_app.Screens
{
    /// <summary>
    /// Логика взаимодействия для ClientsList.xaml
    /// </summary>
    public partial class ClientsList : Page
    {
        const int itemCount = 20;
        const int headerHeight = 40;
        const double contentProcent = 0.64;

        public ClientsList()
        {
            InitializeComponent();

            table.RowDefinitions.Add(new RowDefinition { Height = new GridLength(headerHeight, GridUnitType.Pixel) });
            for (int i = 0; i < itemCount; i++)
            {
                table.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            }

            maxPage = (DataBaseManager.getInstance().clients.Count() / itemCount) + (DataBaseManager.getInstance().clients.Count() % itemCount > 0 ? 1 : 0);
            updatePaginatorInfo();

            Logic.Logic.onLogicUpdate(l =>
            {
                for (int i = -1; i < l.clients.clients.Count(); i++)
                {
                    //PHONE
                    Label label = new Label();
                    label.HorizontalAlignment = HorizontalAlignment.Left;
                    label.VerticalAlignment = VerticalAlignment.Center;
                    label.Foreground = Brushes.White;
                    label.FontSize = i == -1 ? 18 : 16;
                    label.Content = i == -1 ? "Телефон" : l.clients.clients[i].phone;
                    Grid.SetColumn(label, 0);

                    //NAME
                    Label label2 = new Label();
                    label2.HorizontalAlignment = HorizontalAlignment.Left;
                    label2.VerticalAlignment = VerticalAlignment.Center;
                    label2.Foreground = Brushes.White;
                    label2.FontSize = i == -1 ? 18 : 16;
                    label2.Content = i == -1 ? "Имя" : l.clients.clients[i].name;
                    Grid.SetColumn(label2, 1);

                    //ADDRESS
                    Label label3 = new Label();
                    label3.HorizontalAlignment = HorizontalAlignment.Left;
                    label3.VerticalAlignment = VerticalAlignment.Center;
                    label3.Foreground = Brushes.White;
                    label3.FontSize = i == -1 ? 18 : 16;
                    label3.Content = i == -1 ? "Адрес" : l.clients.clients[i].street;
                    Grid.SetColumn(label3, 2);

                    //DISCOUNT
                    Label label4 = new Label();
                    label4.HorizontalAlignment = HorizontalAlignment.Right;
                    label4.VerticalAlignment = VerticalAlignment.Center;
                    label4.Foreground = Brushes.White;
                    label4.FontSize = i == -1 ? 18 : 16;
                    label4.Content = i == -1 ? "Скидка" : l.clients.clients[i].discount.ToString() + '%';
                    Grid.SetColumn(label4, 3);

                    //ORDERS
                    Label label5 = new Label();
                    label5.HorizontalAlignment = HorizontalAlignment.Right;
                    label5.VerticalAlignment = VerticalAlignment.Center;
                    label5.Foreground = Brushes.White;
                    label5.FontSize = i == -1 ? 18 : 16;
                    label5.Content = i == -1 ? "Кол-во З." : l.clients.clients[i].orders.ToString();
                    Grid.SetColumn(label5, 4);

                    //LastOrder
                    Label label6 = new Label();
                    label6.HorizontalAlignment = HorizontalAlignment.Left;
                    label6.VerticalAlignment = VerticalAlignment.Center;
                    label6.Foreground = Brushes.White;
                    label6.FontSize = i == -1 ? 18 : 16;
                    label6.Content = i == -1 ? "Посл. З." : l.clients.clients[i].last_order;
                    Grid.SetColumn(label6, 5);

                    //RECTANGLE
                    Rectangle rect = new Rectangle();
                    rect.StrokeThickness = i == -1 ? 3 : 1;
                    rect.Stroke = Brushes.White;
                    Grid.SetColumnSpan(rect, 7);
                    Grid newGrid = new Grid();
                    AddColumnDefinitions(newGrid);
                    newGrid.MaxWidth = si.VirtualScreen.Width * contentProcent;
                    newGrid.Children.Add(rect);
                    newGrid.Children.Add(label);
                    newGrid.Children.Add(label2);
                    newGrid.Children.Add(label3);
                    newGrid.Children.Add(label4);
                    newGrid.Children.Add(label5);
                    newGrid.Children.Add(label6);
                    table.Children.Add(newGrid);
                    Grid.SetRow(newGrid, i + 1);
                }
            });
        }
        
        private void phoneTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            phonePlaceHolder.Visibility = phoneTextBox.Text.Equals(string.Empty) ? Visibility.Visible : Visibility.Collapsed;        
        }

        private int currentPage = 1;
        private int maxPage = 0;

        private void table_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdatePage(currentPage);
        }

        private void UpdatePage()
        {
            UpdatePage(currentPage);
        }

        private void UpdatePage(int page)
        {
            table.Children.Clear();

            Logic.Logic.execute(l =>
            {
                l.clients.clients = DataBaseManager.getInstance().clients.Where(x => true).Skip(itemCount * (page - 1)).Take(itemCount).ToArray();
            });            
        }

        private void RightArrow_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage + 1 <= maxPage)
            {
                currentPage++;
                updatePaginatorInfo();
                UpdatePage();
            }
        }

        private void LeftArrow_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage - 1 >= 0)
            {
                currentPage--;
                updatePaginatorInfo();
                UpdatePage();
            }
        }

        private void updatePaginatorInfo()
        {
            PaginatorInfo.Content = string.Format("{0}/{1}", currentPage, maxPage);
        }

        private void AddColumnDefinitions(Grid grid)
        {
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(150, GridUnitType.Pixel) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(160, GridUnitType.Pixel) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(90, GridUnitType.Pixel) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(90, GridUnitType.Pixel) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(90, GridUnitType.Pixel) });
        }
    }
}
