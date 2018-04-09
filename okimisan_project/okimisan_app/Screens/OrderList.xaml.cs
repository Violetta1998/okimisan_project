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
        const int itemCount = 8;
        const int headerHeight = 40;
        const double contentProcent = 0.64;

        const int MY_PROGRAMM_HEIGHT = 987;
        const int FONT_SIZE_NORMAL = 13;
        const int FONT_SIZE_HIGHLIGHT = 15;
        const int FONT_SIZE_TITLE = 20;
        const int ADDITIONAL_PANELS_BUTTONS_WIDTH = 25;

        private double additionalPanelsButtonsWidth = 0;
        private bool isFilterHide= true;
        private bool isInfoHide = true;
        private double _scaleFont;
        private double scaleFont
        {
            get
            {
                return _scaleFont <= 0 ? 1 : _scaleFont;
            }
            set { _scaleFont = value; }
        }

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
                l.orders.orders = l.orders.allOrders.Where(x => !x.deleted && x.id_client!=0).OrderByDescending(x => x.id).Skip(itemCount * (l.orders.currentPage - 1)).Take(itemCount).ToArray();

                double rowHeight = (tableOrder.RenderSize.Height - 40) / itemCount;
                rowHeight = rowHeight > 10 ? rowHeight - 10 : rowHeight > 0 ? rowHeight : 10;

                for (int i = -1; i < l.orders.orders.Count(); i++)
                {
                    scaleFont = this.ActualHeight/MY_PROGRAMM_HEIGHT;

                    //ORDER
                    Grid OrderGrid = new Grid();
                    if (i == -1)
                    {
                        Label titleLabel = new Label();
                        titleLabel.HorizontalAlignment = HorizontalAlignment.Left;
                        titleLabel.VerticalAlignment = VerticalAlignment.Center;
                        titleLabel.Foreground = Brushes.White;
                        titleLabel.FontSize = FONT_SIZE_TITLE * scaleFont;
                        titleLabel.Content = "Заказ";
                        OrderGrid.Children.Add(titleLabel);
                    }else
                    {
                        Label label = new Label();
                        label.HorizontalAlignment = HorizontalAlignment.Left;
                        label.VerticalAlignment = VerticalAlignment.Top;
                        label.Margin = new Thickness(0, 0 * rowHeight / 4, 0, 0);
                        label.Foreground = Brushes.White;
                        label.FontSize = FONT_SIZE_HIGHLIGHT * scaleFont;
                        label.Content = string.Format("№{0}", l.orders.orders[i].id);
                        label.Height = rowHeight;
                        OrderGrid.Children.Add(label);

                        label = new Label();
                        label.HorizontalAlignment = HorizontalAlignment.Left;
                        label.VerticalAlignment = VerticalAlignment.Top;
                        label.Margin = new Thickness(6, 1 * rowHeight / 4, 0, 0);
                        label.Foreground = Brushes.White;
                        label.FontSize = FONT_SIZE_NORMAL * scaleFont;
                        try
                        {
                            label.Content = l.orders.orders[i].moment.Split(new char[] { ' ' })[0];
                        }catch {
                            label.Content = l.orders.orders[i].moment;
                        }
                        label.Height = rowHeight;
                        OrderGrid.Children.Add(label);

                        label = new Label();
                        label.HorizontalAlignment = HorizontalAlignment.Left;
                        label.VerticalAlignment = VerticalAlignment.Top;
                        label.Margin = new Thickness(6, 2 * rowHeight / 4, 0, 0);
                        label.Foreground = Brushes.White;
                        label.FontSize = FONT_SIZE_NORMAL * scaleFont;
                        label.Height = rowHeight;
                        label.Content = string.Format("Сумма: {0} руб", l.orders.orders[i].total);
                        OrderGrid.Children.Add(label);

                        label = new Label();
                        label.HorizontalAlignment = HorizontalAlignment.Left;
                        label.VerticalAlignment = VerticalAlignment.Top;
                        label.Margin = new Thickness(6, 3 * rowHeight / 4, 0, 0);
                        label.Foreground = Brushes.White;
                        label.FontSize = FONT_SIZE_NORMAL * scaleFont;
                        label.Height = rowHeight;
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
                        titleLabel.FontSize = FONT_SIZE_TITLE * scaleFont;
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
                        label.FontSize = FONT_SIZE_HIGHLIGHT * scaleFont;
                        label.Height = rowHeight;
                        label.Content = client != null ? client.name : string.Empty;
                        ClientGrid.Children.Add(label);

                        label = new Label();
                        label.HorizontalAlignment = HorizontalAlignment.Left;
                        label.VerticalAlignment = VerticalAlignment.Top;
                        label.Margin = new Thickness(6, 1 * rowHeight / 4, 0, 0);
                        label.Foreground = Brushes.White;
                        label.FontSize = FONT_SIZE_NORMAL * scaleFont;
                        label.Height = rowHeight;
                        label.Content = client != null ? string.Format("Телефон: {0}", client.phone) : string.Empty;
                        ClientGrid.Children.Add(label);

                        label = new Label();
                        label.HorizontalAlignment = HorizontalAlignment.Left;
                        label.VerticalAlignment = VerticalAlignment.Top;
                        label.Margin = new Thickness(6, 2 * rowHeight / 4, 0, 0);
                        label.Foreground = Brushes.White;
                        label.FontSize = FONT_SIZE_NORMAL * scaleFont;
                        label.Height = rowHeight;
                        label.Content = client != null ? client.getFirstAddress() : string.Empty;
                        ClientGrid.Children.Add(label);

                        label = new Label();
                        label.HorizontalAlignment = HorizontalAlignment.Left;
                        label.VerticalAlignment = VerticalAlignment.Top;
                        label.Margin = new Thickness(6, 3 * rowHeight / 4, 0, 0);
                        label.Foreground = Brushes.White;
                        label.FontSize = FONT_SIZE_NORMAL * scaleFont;
                        label.Height = rowHeight;
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
                        titleLabel.FontSize = FONT_SIZE_TITLE * scaleFont;
                        titleLabel.Content = "Состав";
                        CompositionGrid.Children.Add(titleLabel);
                    }
                    else
                    {
                        List<OrderItem> composition = l.orders.allOrderItems.Where(x => x.order_id == l.orders.orders[i].id).ToList();

                        for (int index=0; index < (composition.Count() < 4 ? composition.Count() : 4); index++)
                        {
                            Label label = new Label();
                            label.HorizontalAlignment = HorizontalAlignment.Left;
                            label.VerticalAlignment = VerticalAlignment.Top;
                            label.Margin = new Thickness(0, index * rowHeight / 4, 0, 0);
                            label.Foreground = Brushes.White;
                            label.FontSize = FONT_SIZE_NORMAL * scaleFont;
                            label.Height = rowHeight;
                            label.Content = composition[index].getFullName(l);
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
                        titleLabel.FontSize = FONT_SIZE_TITLE * scaleFont;
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
                        label.FontSize = FONT_SIZE_HIGHLIGHT * scaleFont;
                        label.Height = rowHeight;
                        label.Content = l.auth.usersFromDB.First(x=>x.id== l.orders.orders[i].id_user).name;
                        InfoGrid.Children.Add(label);

                        label = new Label();
                        label.HorizontalAlignment = HorizontalAlignment.Left;
                        label.VerticalAlignment = VerticalAlignment.Top;
                        label.Margin = new Thickness(6, 1 * rowHeight / 4, 0, 0);
                        label.Foreground = Brushes.White;
                        label.FontSize = FONT_SIZE_NORMAL * scaleFont;
                        label.Content = string.Format("Версия: {0}", l.orders.allOrderLogs.Where(x => x.id_order == l.orders.orders[i].id).Max(x => x.version));
                        InfoGrid.Children.Add(label);

                        label = new Label();
                        label.HorizontalAlignment = HorizontalAlignment.Left;
                        label.VerticalAlignment = VerticalAlignment.Top;
                        label.Margin = new Thickness(6, 2 * rowHeight / 4, 0, 0);
                        label.Foreground = Brushes.White;
                        label.Height = rowHeight;
                        label.FontSize = FONT_SIZE_NORMAL * scaleFont;
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

                    double windowWidth = si.VirtualScreen.Width;

                    try
                    {
                        if (this.ActualWidth <= double.MaxValue && this.ActualWidth >= double.MinValue)
                        windowWidth = this.ActualWidth;
                        
                    }
                    catch { };
                    mainColumn.Width = new GridLength(windowWidth * contentProcent);
                    additionalPanelsButtonsWidth = (windowWidth * (1 - contentProcent)) / 2;                  

                    additionalColumn1.Width = new GridLength(additionalPanelsButtonsWidth);
                    additionalColumn2.Width = new GridLength(additionalPanelsButtonsWidth);
                    newGrid.Children.Add(rect);
                    newGrid.Children.Add(OrderGrid);
                    newGrid.Children.Add(ClientGrid);
                    newGrid.Children.Add(CompositionGrid);
                    newGrid.Children.Add(InfoGrid);

                    tableOrder.Children.Add(newGrid);
                    Grid.SetRow(newGrid, i + 1);

                    if (i >= 0)
                    {
                        int margin = 10;
                        int index = i;

                        Grid newGrid2 = new Grid();
                        newGrid2.Background = l.orders.selectedOrder != null && l.orders.orders[i].id == l.orders.selectedOrder.id ? new SolidColorBrush(Color.FromArgb(155, 0, 0, 0)) : Brushes.Transparent;
                        newGrid2.MouseLeftButtonDown += (s, e) =>
                        {
                            Logic.Logic.execute(logic => l.orders.selectedOrder = logic.orders.orders[index]);
                        };

                        tableOrder.Children.Add(newGrid2);
                        Grid.SetRow(newGrid2, i + 1);
                    }
                }
                maxPage = (l.orders.allOrders.Where(x => !x.deleted).Count() / itemCount) + (l.orders.allOrders.Count() % itemCount > 0 ? 1 : 0);
                updatePaginatorInfo(l.orders.currentPage);
                updateArrows(l.orders);
                updateOrderInfo(l, rowHeight);
                updateFilterInfo(l, rowHeight);
            });
        }
        private int maxPage = 0;
        private void table_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Logic.Logic.execute(l => l.orders.currentPage = l.orders.currentPage);
        }

        private void updateArrows(Logic.Orders logic)
        {
            double margin = additionalPanelsButtonsWidth - ADDITIONAL_PANELS_BUTTONS_WIDTH - 50;

            animation1 = new ThicknessAnimation();
            animation1.FillBehavior = FillBehavior.HoldEnd;
            animation1.From = logic.isFilterHide != isFilterHide ? new Thickness(logic.isFilterHide ? 0 : margin, 0, logic.isFilterHide ? 0 : -margin, 0) : new Thickness(FilterGrid1.Margin.Left, 0, FilterGrid1.Margin.Right, 0);
            animation1.To = new Thickness(logic.isFilterHide ? margin : 0, 0, logic.isFilterHide ? -margin : 0, 0);
            animation1.Duration = TimeSpan.FromSeconds(logic.isFilterHide != isFilterHide ? 0.5 : 0.01);

            FilterGrid1.BeginAnimation(MarginProperty, animation1, HandoffBehavior.Compose);

            leftButtonAssist.Content = logic.isFilterHide ? "<" : ">";
            isFilterHide = logic.isFilterHide;


            animation2 = new ThicknessAnimation();
            animation2.FillBehavior = FillBehavior.HoldEnd;
            animation2.From = logic.isInfoHide != isInfoHide ? new Thickness(logic.isInfoHide ? 0 : -margin, 0, logic.isInfoHide ? 0 : margin, 0) : new Thickness(FilterGrid2.Margin.Left, 0, FilterGrid2.Margin.Right, 0);
            animation2.To = new Thickness(logic.isInfoHide ? -margin : 0, 0, logic.isInfoHide ? margin : 0, 0);
            animation2.Duration = TimeSpan.FromSeconds(logic.isInfoHide != isInfoHide ? 0.5 : 0.01);

            FilterGrid2.BeginAnimation(MarginProperty, animation2, HandoffBehavior.Compose);

            rightButtonAssist.Content = logic.isInfoHide ? ">" : "<";
            isInfoHide = logic.isInfoHide;
        }

        private void updateFilterInfo(Logic.Logic logic, double rowHeight)
        {
            rowHeight /= 4;

            DateLabel.Margin = new Thickness(0, rowHeight * 0, 0, 0);
            DateLabel.FontSize = FONT_SIZE_NORMAL * scaleFont;

            DateGrid.Margin = new Thickness(0, rowHeight * 1, 0, 0);
            DateFrom.FontSize = FONT_SIZE_NORMAL * scaleFont;
            DateTo.FontSize = FONT_SIZE_NORMAL * scaleFont;
            DateGrid.Height = rowHeight;

            TimeLabel.Margin = new Thickness(0, rowHeight * 2, 0, 0);
            TimeLabel.FontSize = FONT_SIZE_NORMAL * scaleFont;

            TimeGrid.Margin = new Thickness(0, rowHeight * 3, 0, 0);
            TimeFrom.FontSize = FONT_SIZE_NORMAL * scaleFont;
            TimeTo.FontSize = FONT_SIZE_NORMAL * scaleFont;
            TimeGrid.Height = rowHeight;

            NumberLabel.Margin = new Thickness(0, rowHeight * 4, 0, 0);
            NumberLabel.FontSize = FONT_SIZE_NORMAL * scaleFont;

            NumberGrid.Margin = new Thickness(0, rowHeight * 5, 0, 0);
            NumberFrom.FontSize = FONT_SIZE_NORMAL * scaleFont;
            NumberTo.FontSize = FONT_SIZE_NORMAL * scaleFont;
            NumberGrid.Height = rowHeight;

            PhoneLabel.Margin = new Thickness(0, rowHeight * 6, 0, 0);
            PhoneLabel.FontSize = FONT_SIZE_NORMAL * scaleFont;

            PhoneGrid.Margin = new Thickness(0, rowHeight * 7, 0, 0);
            PhoneText.FontSize = FONT_SIZE_NORMAL * scaleFont;
            PhoneGrid.Height = rowHeight;

            CategoryLabel.Margin = new Thickness(0, rowHeight * 8, 0, 0);
            CategoryLabel.FontSize = FONT_SIZE_NORMAL * scaleFont;

            CategoryGrid.Margin = new Thickness(0, rowHeight * 9, 0, 0);
            CategoryText.FontSize = FONT_SIZE_NORMAL * scaleFont;
            CategoryGrid.Height = rowHeight;

            ProductLabel.Margin = new Thickness(0, rowHeight * 10, 0, 0);
            ProductLabel.FontSize = FONT_SIZE_NORMAL * scaleFont;

            ProductGrid.Margin = new Thickness(0, rowHeight * 11, 0, 0);
            ProductText.FontSize = FONT_SIZE_NORMAL * scaleFont;
            ProductGrid.Height = rowHeight;

            DiscountLabel.Margin = new Thickness(0, rowHeight * 12, 0, 0);
            DiscountLabel.FontSize = FONT_SIZE_NORMAL * scaleFont;

            DiscountGrid.Margin = new Thickness(0, rowHeight * 13, 0, 0);
            DiscountFrom.FontSize = FONT_SIZE_NORMAL * scaleFont;
            DiscountTo.FontSize = FONT_SIZE_NORMAL * scaleFont;
            DiscountGrid.Height = rowHeight;

            DeletedGrid.Margin = new Thickness(0, rowHeight * 14, 0, 0);

            DeletedLabel.FontSize = FONT_SIZE_NORMAL * scaleFont;
        }

        private void updateOrderInfo(Logic.Logic logic, double rowHeight)
        {
            FullCompositionLabel.FontSize = FONT_SIZE_HIGHLIGHT * scaleFont;
            FullCompositionTextBlock.FontSize = FONT_SIZE_NORMAL * scaleFont;
            FullCompositionTextBlock.Text = string.Empty;
            HistoryLabel.FontSize = FONT_SIZE_HIGHLIGHT * scaleFont;
            HistoryTextBlock.FontSize = FONT_SIZE_NORMAL * scaleFont;

            if (logic.orders.selectedOrder != null)
            {
                List<OrderItem> composition = logic.orders.allOrderItems.Where(x => x.order_id == logic.orders.selectedOrder.id).ToList();
                for (int index = 0; index < composition.Count(); index++)
                {
                    string s = composition[index].getFullName(logic, true) + Environment.NewLine;
                    var parts = s.Split(new[] { "<b>", "</b>" }, StringSplitOptions.None);
                    bool isbold = false;
                    foreach (var part in parts)
                    {
                        if (isbold)
                            FullCompositionTextBlock.Inlines.Add(new Bold(new Run(part)));
                        else
                            FullCompositionTextBlock.Inlines.Add(new Run(part));

                        isbold = !isbold; 
                    }
                }

                HistoryTextBlock.Text = string.Empty;

                List<OrderLog> history = logic.orders.allOrderLogs.Where(x => x.id_order == logic.orders.selectedOrder.id).OrderByDescending(x=>x.version).ToList();
                
                for (int i=0;i<history.Count();i++)
                {
                    DateTime time = DateTime.Parse(history[i].moment);

                    HistoryTextBlock.Inlines.Add(new Run(string.Format("{0}.{1} {2}:{3}, ", time.Day, time.Month, time.Hour, time.Minute)));
                    HistoryTextBlock.Inlines.Add(new Run(string.Format("{0}({1})(вер.{2}), \n", history[i].host, logic.auth.usersFromDB.First(x => x.id == history[i].id_user).name, history[i].version)));
                    HistoryTextBlock.Inlines.Add(new Run(string.Format("{0}, \n", history[i].action)));
                    HistoryTextBlock.Inlines.Add(new Run(string.Format("{0}, \n{1}\n\n", history[i].value_was, history[i].value_now)));
                }
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

        private void mainForm_LayoutUpdated(object sender, EventArgs e)
        {
            var asd = 1;
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
