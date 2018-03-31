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
                        label.FontSize = 12;
                        label.Content = string.Format("№{0}", l.orders.orders[i].id);
                        OrderGrid.Children.Add(label);

                        label = new Label();
                        label.HorizontalAlignment = HorizontalAlignment.Left;
                        label.VerticalAlignment = VerticalAlignment.Top;
                        label.Margin = new Thickness(4, 1 * rowHeight / 4, 0, 0);
                        label.Foreground = Brushes.White;
                        label.FontSize = 10;
                        label.Content = l.orders.orders[i].moment;
                        OrderGrid.Children.Add(label);

                        label = new Label();
                        label.HorizontalAlignment = HorizontalAlignment.Left;
                        label.VerticalAlignment = VerticalAlignment.Top;
                        label.Margin = new Thickness(4, 2 * rowHeight / 4, 0, 0);
                        label.Foreground = Brushes.White;
                        label.FontSize = 10;
                        label.Content = string.Format("Сумма: {0} руб", l.orders.orders[i].total);
                        OrderGrid.Children.Add(label);

                        label = new Label();
                        label.HorizontalAlignment = HorizontalAlignment.Left;
                        label.VerticalAlignment = VerticalAlignment.Top;
                        label.Margin = new Thickness(4, 3 * rowHeight / 4, 0, 0);
                        label.Foreground = Brushes.White;
                        label.FontSize = 10;
                        label.Content = string.Format("Скидка: {0}%", l.orders.orders[i].discount);
                        OrderGrid.Children.Add(label);
                    }
                    Grid.SetColumn(OrderGrid, 0);

                    //CONTENT
                    Label label2 = new Label();
                    label2.HorizontalAlignment = HorizontalAlignment.Left;
                    label2.VerticalAlignment = VerticalAlignment.Center;
                    label2.Foreground = Brushes.White;
                    label2.FontSize = i == -1 ? 18 : 16;
                    label2.Content = i == -1 ? "Состав" : l.orders.orders[i].content;
                    Grid.SetColumn(label2, 1);

                    //CLIENT
                    Label label3 = new Label();
                    label3.HorizontalAlignment = HorizontalAlignment.Left;
                    label3.VerticalAlignment = VerticalAlignment.Center;
                    label3.Foreground = Brushes.White;
                    label3.FontSize = i == -1 ? 18 : 16;
                    string name = "";
                    if (i > 0)
                    {
                        for (int j = 0; j < l.clients.allClients.Count(); j++)
                        {
                           
                            if (l.orders.orders[i].id_client == l.clients.allClients[j].id)
                            {
                                name = l.clients.allClients[j].name;
                            }
                        }
                    }
                   
                    label3.Content = i == -1 ? "Клиент" : name;
                    Grid.SetColumn(label3, 2);

                    //PHONE
                    Label label4 = new Label();
                    label4.HorizontalAlignment = HorizontalAlignment.Left;
                    label4.VerticalAlignment = VerticalAlignment.Center;
                    label4.Foreground = Brushes.White;
                    label4.FontSize = i == -1 ? 18 : 16;
                    string phone = "";
                    if (i > 0)
                    {
                        for (int j = 0; j < l.clients.allClients.Count(); j++)
                        {

                            if (l.orders.orders[i].id_client == l.clients.allClients[j].id)
                            {
                                phone = l.clients.allClients[j].phone;
                            }
                        }
                    }
                    
                    label4.Content = i == -1 ? "Номер телефона клиента" : phone;
                    Grid.SetColumn(label4, 3);

                    //SUMM
                    Label label5 = new Label();
                    label5.HorizontalAlignment = HorizontalAlignment.Left;
                    label5.VerticalAlignment = VerticalAlignment.Center;
                    label5.Foreground = Brushes.White;
                    label5.FontSize = i == -1 ? 18 : 16;
                    label5.Content = i == -1 ? "Сумма заказа" : l.orders.orders[i].total.ToString();
                    Grid.SetColumn(label5, 4);

                    //DISCOUNT
                    Label label6 = new Label();
                    label6.HorizontalAlignment = HorizontalAlignment.Left;
                    label6.VerticalAlignment = VerticalAlignment.Center;
                    label6.Foreground = Brushes.White;
                    label6.FontSize = i == -1 ? 18 : 16;
                    label6.Content = i == -1 ? "Скидка" : l.orders.orders[i].discount.ToString();
                    Grid.SetColumn(label6, 5);

                    //RECTANGLE
                    Rectangle rect = new Rectangle();
                    rect.StrokeThickness = i == -1 ? 3 : 1;
                    rect.Stroke = Brushes.White;
                    Grid.SetColumnSpan(rect, 7);

                    Grid newGrid = new Grid();
                    AddColumnDefinitions(newGrid);
                    mainColumn.Width = new GridLength(si.VirtualScreen.Width * contentProcent);
                    additionalColumn1.Width = new GridLength((si.VirtualScreen.Width * (1 - contentProcent)) / 2 - 50);
                    additionalColumn2.Width = new GridLength((si.VirtualScreen.Width * (1 - contentProcent)) / 2 - 50);
                    newGrid.Children.Add(rect);
                    newGrid.Children.Add(OrderGrid);
                    newGrid.Children.Add(label2);
                    newGrid.Children.Add(label3);
                    newGrid.Children.Add(label4);
                    newGrid.Children.Add(label5);
                    newGrid.Children.Add(label6);

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
