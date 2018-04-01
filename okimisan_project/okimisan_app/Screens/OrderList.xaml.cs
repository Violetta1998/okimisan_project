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
using System.Windows.Media.Animation;
using okimisan_app.Data;

namespace okimisan_app.Screens
{
    /// <summary>
    /// Логика взаимодействия для OrderList.xaml
    /// </summary>
    public partial class OrderList : Page
    {
        const int itemCount = 11;
        const int headerHeight = 40;
        const double contentProcent = 0.64;

        private bool isFilterHide= true;
        private bool isInfoHide = true;

        public OrderList()
        {
            InitializeComponent();

            leftButtonAssist.Content = ">";
            rightButtonAssist.Content = "<";

            tableOrder.RowDefinitions.Add(new RowDefinition { Height = new GridLength(headerHeight, GridUnitType.Pixel) });
            for (int i = 0; i < itemCount; i++)
            {
                tableOrder.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            }

            Logic.Logic.onLogicUpdate(l =>
            {
                tableOrder.Children.Clear();
                l.orders.orders = l.orders.allOrders.Where(x => !x.deleted).OrderByDescending(x => x.id).Skip(itemCount * (l.orders.currentPage - 1)).Take(itemCount).ToArray();
                for (int i = -1; i < l.orders.orders.Count(); i++)
                {
                    double rowHeight = (tableOrder.RenderSize.Height - 40) / itemCount;
                    rowHeight = rowHeight > 10 ? rowHeight - 10 : rowHeight > 0 ? rowHeight : 30;

                    //ORDER
                    Grid OrderGrid = new Grid();
                    if (i == -1)
                    {
                        Label titleLabel = new Label();
                        titleLabel.HorizontalAlignment = HorizontalAlignment.Left;
                        titleLabel.VerticalAlignment = VerticalAlignment.Center;
                        titleLabel.Foreground = Brushes.White;
                        titleLabel.FontSize = 18;
                        titleLabel.Content = "Заказ";
                        OrderGrid.Children.Add(titleLabel);
                    }else
                    {
                        Label label = new Label();
                        label.HorizontalAlignment = HorizontalAlignment.Left;
                        label.VerticalAlignment = VerticalAlignment.Top;
                        label.Margin = new Thickness(0, 0 * rowHeight / 4, 0, 0);
                        label.Foreground = Brushes.White;
                        label.FontSize = 13;
                        label.Content = string.Format("№{0}", l.orders.orders[i].id);
                        OrderGrid.Children.Add(label);

                        label = new Label();
                        label.HorizontalAlignment = HorizontalAlignment.Left;
                        label.VerticalAlignment = VerticalAlignment.Top;
                        label.Margin = new Thickness(6, 1 * rowHeight / 4, 0, 0);
                        label.Foreground = Brushes.White;
                        label.FontSize = 11;
                        label.Content = l.orders.orders[i].moment;
                        OrderGrid.Children.Add(label);

                        label = new Label();
                        label.HorizontalAlignment = HorizontalAlignment.Left;
                        label.VerticalAlignment = VerticalAlignment.Top;
                        label.Margin = new Thickness(6, 2 * rowHeight / 4, 0, 0);
                        label.Foreground = Brushes.White;
                        label.FontSize = 11;
                        label.Content = string.Format("Сумма: {0} руб", l.orders.orders[i].total);
                        OrderGrid.Children.Add(label);

                        label = new Label();
                        label.HorizontalAlignment = HorizontalAlignment.Left;
                        label.VerticalAlignment = VerticalAlignment.Top;
                        label.Margin = new Thickness(6, 3 * rowHeight / 4, 0, 0);
                        label.Foreground = Brushes.White;
                        label.FontSize = 11;
                        label.Content = string.Format("Скидка: {0}%", l.orders.orders[i].discount);
                        OrderGrid.Children.Add(label);
                    }
                    Grid.SetColumn(OrderGrid, 0);

                    //CLIENT
                    Grid ClientGrid = new Grid();
                    if (i == -1)
                    {
                        Label titleLabel = new Label();
                        titleLabel.HorizontalAlignment = HorizontalAlignment.Left;
                        titleLabel.VerticalAlignment = VerticalAlignment.Center;
                        titleLabel.Foreground = Brushes.White;
                        titleLabel.FontSize = 18;
                        titleLabel.Content = "Клиент";
                        ClientGrid.Children.Add(titleLabel);
                    }
                    else
                    {
                        Client client = l.clients.allClients.FirstOrDefault(x => x.id == l.orders.orders[i].id_client);

                        Label label = new Label();
                        label.HorizontalAlignment = HorizontalAlignment.Left;
                        label.VerticalAlignment = VerticalAlignment.Top;
                        label.Margin = new Thickness(0, 0 * rowHeight / 4, 0, 0);
                        label.Foreground = Brushes.White;
                        label.FontSize = 13;
                        label.Content = client != null ? client.name : string.Empty;
                        ClientGrid.Children.Add(label);

                        label = new Label();
                        label.HorizontalAlignment = HorizontalAlignment.Left;
                        label.VerticalAlignment = VerticalAlignment.Top;
                        label.Margin = new Thickness(6, 1 * rowHeight / 4, 0, 0);
                        label.Foreground = Brushes.White;
                        label.FontSize = 11;
                        label.Content = client != null ? string.Format("Телефон: {0}", client.phone) : string.Empty;
                        ClientGrid.Children.Add(label);

                        label = new Label();
                        label.HorizontalAlignment = HorizontalAlignment.Left;
                        label.VerticalAlignment = VerticalAlignment.Top;
                        label.Margin = new Thickness(6, 2 * rowHeight / 4, 0, 0);
                        label.Foreground = Brushes.White;
                        label.FontSize = 11;
                        label.Content = client != null ? client.getFirstAddress() : string.Empty;
                        ClientGrid.Children.Add(label);

                        label = new Label();
                        label.HorizontalAlignment = HorizontalAlignment.Left;
                        label.VerticalAlignment = VerticalAlignment.Top;
                        label.Margin = new Thickness(6, 3 * rowHeight / 4, 0, 0);
                        label.Foreground = Brushes.White;
                        label.FontSize = 11;
                        label.Content = client != null ? string.Format("Кол-во заказов: {0}", client.orders) : string.Empty;
                        ClientGrid.Children.Add(label);
                    }
                    Grid.SetColumn(ClientGrid, 1);

                    //COMPOSITION
                    Grid CompositionGrid = new Grid();
                    if (i == -1)
                    {
                        Label titleLabel = new Label();
                        titleLabel.HorizontalAlignment = HorizontalAlignment.Left;
                        titleLabel.VerticalAlignment = VerticalAlignment.Center;
                        titleLabel.Foreground = Brushes.White;
                        titleLabel.FontSize = 18;
                        titleLabel.Content = "Состав";
                        CompositionGrid.Children.Add(titleLabel);
                    }
                    else
                    {
                        List<OrderItem> composition = l.orders.allOrderItems.Where(x => x.order_id == l.orders.orders[i].id).ToList();

                        for (int index=0; index < composition.Count(); index++)
                        {
                            Label label = new Label();
                            label.HorizontalAlignment = HorizontalAlignment.Left;
                            label.VerticalAlignment = VerticalAlignment.Top;
                            label.Margin = new Thickness(0, index * rowHeight / 4, 0, 0);
                            label.Foreground = Brushes.White;
                            label.FontSize = 11;
                            label.Content = composition[index].name;
                            CompositionGrid.Children.Add(label);
                        }                     
                    }
                    Grid.SetColumn(CompositionGrid, 2);

                    //INFO
                    Grid InfoGrid = new Grid();
                    if (i == -1)
                    {
                        Label titleLabel = new Label();
                        titleLabel.HorizontalAlignment = HorizontalAlignment.Left;
                        titleLabel.VerticalAlignment = VerticalAlignment.Center;
                        titleLabel.Foreground = Brushes.White;
                        titleLabel.FontSize = 18;
                        titleLabel.Content = "Инфо";
                        InfoGrid.Children.Add(titleLabel);
                    }
                    else
                    {
                        Label label = new Label();
                        label.HorizontalAlignment = HorizontalAlignment.Left;
                        label.VerticalAlignment = VerticalAlignment.Top;
                        label.Margin = new Thickness(0, 0 * rowHeight / 4, 0, 0);
                        label.Foreground = Brushes.White;
                        label.FontSize = 13;
                        label.Content = l.auth.usersFromDB.First(x=>x.id== l.orders.orders[i].id_user).name;
                        InfoGrid.Children.Add(label);

                        label = new Label();
                        label.HorizontalAlignment = HorizontalAlignment.Left;
                        label.VerticalAlignment = VerticalAlignment.Top;
                        label.Margin = new Thickness(6, 1 * rowHeight / 4, 0, 0);
                        label.Foreground = Brushes.White;
                        label.FontSize = 11;
                        label.Content = string.Format("Версия: {0}", l.orders.allOrderLogs.Where(x => x.id_order == l.orders.orders[i].id).Max(x => x.version));
                        InfoGrid.Children.Add(label);

                        label = new Label();
                        label.HorizontalAlignment = HorizontalAlignment.Left;
                        label.VerticalAlignment = VerticalAlignment.Top;
                        label.Margin = new Thickness(6, 2 * rowHeight / 4, 0, 0);
                        label.Foreground = Brushes.White;
                        label.FontSize = 11;
                        label.Content = string.Format("Печать: {0}", l.orders.orders[i].printed);
                        InfoGrid.Children.Add(label);
                    }
                    Grid.SetColumn(InfoGrid, 3);


                    //RECTANGLE
                    Rectangle rect = new Rectangle();
                    rect.StrokeThickness = i == -1 ? 3 : 1;
                    rect.Stroke = Brushes.White;
                    Grid.SetColumnSpan(rect, 4);

                    Grid newGrid = new Grid();
                    AddColumnDefinitions(newGrid);
                    mainColumn.Width = new GridLength(si.VirtualScreen.Width * contentProcent);
                    additionalColumn1.Width = new GridLength((si.VirtualScreen.Width * (1 - contentProcent)) / 2 - 50);
                    additionalColumn2.Width = new GridLength((si.VirtualScreen.Width * (1 - contentProcent)) / 2 - 50);
                    newGrid.Children.Add(rect);
                    newGrid.Children.Add(OrderGrid);
                    newGrid.Children.Add(ClientGrid);
                    newGrid.Children.Add(CompositionGrid);
                    newGrid.Children.Add(InfoGrid);

                    tableOrder.Children.Add(newGrid);
                    Grid.SetRow(newGrid, i + 1);
                }
                maxPage = (l.orders.allOrders.Where(x => !x.deleted).Count() / itemCount) + (l.orders.allOrders.Count() % itemCount > 0 ? 1 : 0);
                updatePaginatorInfo(l.orders.currentPage);
                updateArrows(l.orders);
            });
        }
        private int maxPage = 0;
        private void table_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Logic.Logic.execute(l => l.orders.currentPage = l.orders.currentPage);
        }

        private void updateArrows(Logic.Orders logic)
        {
            if (logic.isFilterHide!= isFilterHide)
            {
                isFilterHide = logic.isFilterHide;

                animation1 = new ThicknessAnimation();
                animation1.FillBehavior = FillBehavior.HoldEnd;
                animation1.From = new Thickness(isFilterHide ? 0 : 225, 0, isFilterHide ? 0 : -225, 0);
                animation1.To = new Thickness(isFilterHide ? 225 : 0, 0, isFilterHide ? -225 : 0, 0);
                animation1.Duration = TimeSpan.FromSeconds(0.5);

                FilterGrid1.BeginAnimation(MarginProperty, animation1, HandoffBehavior.Compose);
                
                leftButtonAssist.Content = isFilterHide ? "<" : ">";
            }
            if (logic.isInfoHide != isInfoHide)
            {
                isInfoHide = logic.isInfoHide;

                animation2 = new ThicknessAnimation();
                animation2.FillBehavior = FillBehavior.HoldEnd;
                animation2.From = new Thickness(isInfoHide ? 0 : -225, 0, isInfoHide ? 0 : 225, 0);
                animation2.To = new Thickness(isInfoHide ? -225 : 0, 0, isInfoHide ? 225 : 0, 0);
                animation2.Duration = TimeSpan.FromSeconds(0.5);

                FilterGrid2.BeginAnimation(MarginProperty, animation2, HandoffBehavior.Compose);
                
                rightButtonAssist.Content = isInfoHide ? ">" : "<";
            }
        }

        private void UpdatePage(int page)
        {
            Logic.Logic.execute(l =>
            {
                l.orders.currentPage = page;
            });
        }

        private void RightArrow_Click(object sender, RoutedEventArgs e)
        {
            Logic.Logic.execute(l =>
            {
                if (l.orders.currentPage + 1 <= maxPage)
                {
                    l.orders.currentPage++;
                }
            });
        }

        private void LeftArrow_Click(object sender, RoutedEventArgs e)
        {
            Logic.Logic.execute(l =>
            {
                if (l.orders.currentPage - 1 > 0)
                {
                    l.orders.currentPage--;
                }
            });
        }

        private void updatePaginatorInfo(int currentPage)
        {
            PaginatorInfo.Content = string.Format("{0}/{1}", currentPage, maxPage);
        }

        private void AddColumnDefinitions(Grid grid)
        {
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(150, GridUnitType.Pixel) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(150, GridUnitType.Pixel) });
        }
        
        ThicknessAnimation animation1;
        ThicknessAnimation animation2;

        private void ShowFilterButton_Click(object sender, RoutedEventArgs e)
        {
            Logic.Logic.execute(l => l.orders.isFilterHide = !l.orders.isFilterHide);
        }

        private void ShowInfoButton_Click(object sender, RoutedEventArgs e)
        {
            Logic.Logic.execute(l => l.orders.isInfoHide = !l.orders.isInfoHide);

        }
        //private void phoneTextBox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    phonePlaceHolder.Visibility = phoneTextBox.Text.Equals(string.Empty) ? Visibility.Visible : Visibility.Collapsed;

        //    Logic.Logic.execute(l =>
        //    {
        //        maxPage = (l.orders.allOrders.Where(x => !x.deleted).Where(phoneFilter).Where(discountFilter).Count() / itemCount) + (l.orders.allOrders.Where(phoneFilter).Where(discountFilter).Count() % itemCount > 0 ? 1 : 0);
        //        updatePaginatorInfo(l.orders.currentPage);
        //    });
        //}

    }
}
