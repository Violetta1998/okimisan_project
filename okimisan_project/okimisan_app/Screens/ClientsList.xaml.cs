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
        int[] discounts = new int[] { 0, 5, 10, 20, 30, 50, 100 };
        const int itemCount = 20;
        const int headerHeight = 40;
        const double contentProcent = 0.64;
        Func<Data.Client, bool> phoneFilter = (x) => true;
        Func<Data.Client, bool> discountFilter = (x) => true;

        public ClientsList()
        {
            InitializeComponent();

            phoneFilter = (x) =>
            {
                if (phoneTextBox.Text.Equals(string.Empty))
                    return true;
                return x.phone.StartsWith(phoneTextBox.Text);
            };

            discountFilter = (x) =>
            {
                return x.discount >= discounts[discount.SelectedIndex];
            };

            table.RowDefinitions.Add(new RowDefinition { Height = new GridLength(headerHeight, GridUnitType.Pixel) });
            for (int i = 0; i < itemCount; i++)
            {
                table.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            }

            Logic.Logic.onLogicUpdate(l =>
            {
                table.Children.Clear();
                l.clients.clients = l.clients.allClients.Where(x => !x.deleted).Where(phoneFilter).Where(discountFilter).OrderByDescending(x=>x.id).Skip(itemCount * (l.clients.currentPage - 1)).Take(itemCount).ToArray();
                for (int i = -1; i < l.clients.clients.Count(); i++)
                {
                    double rowHeight = (table.RenderSize.Height - 40) / itemCount;
                    rowHeight = rowHeight > 10 ? rowHeight - 10 : rowHeight > 0 ? rowHeight : 30;
                    //PHONE
                    Label label = new Label();
                    label.HorizontalAlignment = HorizontalAlignment.Left;
                    label.VerticalAlignment = VerticalAlignment.Center;
                    label.Foreground = i >= 0 && l.clients.clients[i].blocked ? Brushes.Red : Brushes.White;
                    label.FontSize = i == -1 ? 18 : 16;
                    label.Content = i == -1 ? "Телефон" : l.clients.clients[i].phone;
                    Grid.SetColumn(label, 0);

                    //NAME
                    Label label2 = new Label();
                    label2.HorizontalAlignment = HorizontalAlignment.Left;
                    label2.VerticalAlignment = VerticalAlignment.Center;
                    label2.Foreground = i >= 0 && l.clients.clients[i].blocked ? Brushes.Red : Brushes.White;
                    label2.FontSize = i == -1 ? 18 : 16;
                    label2.Content = i == -1 ? "Имя" : l.clients.clients[i].name;
                    Grid.SetColumn(label2, 1);

                    //ADDRESS
                    Label label3 = new Label();
                    label3.HorizontalAlignment = HorizontalAlignment.Left;
                    label3.VerticalAlignment = VerticalAlignment.Center;
                    label3.Foreground = i >= 0 && l.clients.clients[i].blocked ? Brushes.Red : Brushes.White;
                    label3.FontSize = i == -1 ? 18 : 16;
                    label3.Content = i == -1 ? "Адрес" : l.clients.clients[i].street;
                    Grid.SetColumn(label3, 2);

                    //DISCOUNT
                    Label label4 = new Label();
                    label4.HorizontalAlignment = HorizontalAlignment.Right;
                    label4.VerticalAlignment = VerticalAlignment.Center;
                    label4.Foreground = i >= 0 && l.clients.clients[i].blocked ? Brushes.Red : Brushes.White;
                    label4.FontSize = i == -1 ? 18 : 16;
                    label4.Content = i == -1 ? "Скидка" : l.clients.clients[i].discount.ToString() + '%';
                    Grid.SetColumn(label4, 3);

                    //ORDERS
                    Label label5 = new Label();
                    label5.HorizontalAlignment = HorizontalAlignment.Right;
                    label5.VerticalAlignment = VerticalAlignment.Center;
                    label5.Foreground = i >= 0 && l.clients.clients[i].blocked ? Brushes.Red : Brushes.White;
                    label5.FontSize = i == -1 ? 18 : 16;
                    label5.Content = i == -1 ? "Кол-во З." : l.clients.clients[i].orders.ToString();
                    Grid.SetColumn(label5, 4);

                    //LastOrder
                    Label label6 = new Label();
                    label6.HorizontalAlignment = HorizontalAlignment.Left;
                    label6.VerticalAlignment = VerticalAlignment.Center;
                    label6.Foreground = i >= 0 && l.clients.clients[i].blocked ? Brushes.Red : Brushes.White;
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

                    if (i >= 0)
                    {
                        int margin = 10;
                        int index = i;

                        Ellipse ellipse1 = new Ellipse();
                        ellipse1.Width = rowHeight;
                        ellipse1.Height = rowHeight;
                        ellipse1.Stroke = Brushes.White;
                        ellipse1.StrokeThickness = 1;
                        ellipse1.Fill = Brushes.Black;

                        Label buttonLabel1 = new Label();
                        buttonLabel1.VerticalAlignment = VerticalAlignment.Center;
                        buttonLabel1.HorizontalAlignment = HorizontalAlignment.Center;
                        buttonLabel1.Content = "П";
                        buttonLabel1.Foreground = Brushes.White;
                        buttonLabel1.IsHitTestVisible = false;
                        buttonLabel1.FontSize = 14;

                        Grid buttonGrid1 = new Grid();
                        buttonGrid1.Children.Add(ellipse1);
                        buttonGrid1.Children.Add(buttonLabel1);

                        Button button1 = new Button();
                        button1.Background = Brushes.Transparent;
                        button1.BorderBrush = Brushes.Transparent;
                        button1.Content = buttonGrid1;
                        button1.Margin = new Thickness(0, 0, rowHeight * 2 + margin * 3, 0);
                        button1.HorizontalAlignment = HorizontalAlignment.Right;
                        button1.Visibility = l.clients.selectedClient != null && l.clients.clients[i].id == l.clients.selectedClient.id ? Visibility.Visible : Visibility.Collapsed;
                        button1.Click += (s, e) =>
                        {
                            Logic.Logic.execute(logic =>
                            {
                                logic.clients.selectedClient.blocked = !logic.clients.selectedClient.blocked;
                                DataBaseManager.getInstance().saveClient(logic, logic.clients.selectedClient);
                            });
                        };

                        Ellipse ellipse2 = new Ellipse();
                        ellipse2.Width = rowHeight;
                        ellipse2.Height = rowHeight;
                        ellipse2.Stroke = Brushes.White;
                        ellipse2.StrokeThickness = 1;
                        ellipse2.Fill = Brushes.Black;

                        Label buttonLabel2 = new Label();
                        buttonLabel2.VerticalAlignment = VerticalAlignment.Center;
                        buttonLabel2.HorizontalAlignment = HorizontalAlignment.Center;
                        buttonLabel2.Content = "Р";
                        buttonLabel2.Foreground = Brushes.White;
                        buttonLabel2.IsHitTestVisible = false;
                        buttonLabel2.FontSize = 14;

                        Grid buttonGrid2 = new Grid();
                        buttonGrid2.Children.Add(ellipse2);
                        buttonGrid2.Children.Add(buttonLabel2);

                        Button button2 = new Button();
                        button2.Background = Brushes.Transparent;
                        button2.BorderBrush = Brushes.Transparent;
                        button2.Content = buttonGrid2;
                        button2.Margin = new Thickness(0, 0, rowHeight * 1 + margin * 2, 0);
                        button2.HorizontalAlignment = HorizontalAlignment.Right;
                        button2.Visibility = l.clients.selectedClient != null && l.clients.clients[i].id == l.clients.selectedClient.id ? Visibility.Visible : Visibility.Collapsed;
                        button2.Click += (s, e) =>
                        {
                            Logic.Logic.execute(logic =>
                            {
                                logic.clients.editMode = true;
                                logic.general.currentModalPage = Logic.General.MODAL_PAGES.ClientEdit;
                            });
                        };

                        Ellipse ellipse3 = new Ellipse();
                        ellipse3.Width = rowHeight;
                        ellipse3.Height = rowHeight;
                        ellipse3.Stroke = Brushes.White;
                        ellipse3.StrokeThickness = 1;
                        ellipse3.Fill = Brushes.Black;

                        Label buttonLabel3 = new Label();
                        buttonLabel3.VerticalAlignment = VerticalAlignment.Center;
                        buttonLabel3.HorizontalAlignment = HorizontalAlignment.Center;
                        buttonLabel3.Content = "У";
                        buttonLabel3.Foreground = Brushes.White;
                        buttonLabel3.IsHitTestVisible = false;
                        buttonLabel3.FontSize = 14;

                        Grid buttonGrid3 = new Grid();
                        buttonGrid3.Children.Add(ellipse3);
                        buttonGrid3.Children.Add(buttonLabel3);

                        Button button3 = new Button();
                        button3.Background = Brushes.Transparent;
                        button3.BorderBrush = Brushes.Transparent;
                        button3.Content = buttonGrid3;
                        button3.Margin = new Thickness(0, 0, rowHeight * 0 + margin * 1, 0);
                        button3.HorizontalAlignment = HorizontalAlignment.Right;
                        button3.Visibility = l.clients.selectedClient != null && l.clients.clients[i].id == l.clients.selectedClient.id ? Visibility.Visible : Visibility.Collapsed;
                        button3.Click += (s, e) =>
                        {
                            Logic.Logic.execute(logic =>
                            {
                                logic.clients.selectedClient.deleted = true;
                                DataBaseManager.getInstance().saveClient(logic, logic.clients.selectedClient);
                            });
                        };

                        Grid newGrid2 = new Grid();
                        newGrid2.MaxWidth = si.VirtualScreen.Width * contentProcent;
                        newGrid2.Children.Add(button1);
                        newGrid2.Children.Add(button2);
                        newGrid2.Children.Add(button3);
                        newGrid2.Background = l.clients.selectedClient != null && l.clients.clients[i].id == l.clients.selectedClient.id ? new SolidColorBrush(Color.FromArgb(155, 0, 0, 0)) : Brushes.Transparent;
                        newGrid2.MouseLeftButtonDown += (s, e) =>
                        {
                            Logic.Logic.execute(logic => l.clients.selectedClient = logic.clients.clients[index]);
                        };

                        table.Children.Add(newGrid2);
                        Grid.SetRow(newGrid2, i + 1);
                    }                   

                }
                maxPage = (l.clients.allClients.Where(x=>!x.deleted).Where(phoneFilter).Where(discountFilter).Count() / itemCount) + (l.clients.allClients.Where(phoneFilter).Where(discountFilter).Count() % itemCount > 0 ? 1 : 0);
                updatePaginatorInfo(l.clients.currentPage);
            });
        }

        private void phoneTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            phonePlaceHolder.Visibility = phoneTextBox.Text.Equals(string.Empty) ? Visibility.Visible : Visibility.Collapsed;

            Logic.Logic.execute(l =>
            {
                maxPage = (l.clients.allClients.Where(x => !x.deleted).Where(phoneFilter).Where(discountFilter).Count() / itemCount) + (l.clients.allClients.Where(phoneFilter).Where(discountFilter).Count() % itemCount > 0 ? 1 : 0);
                updatePaginatorInfo(l.clients.currentPage);
            });
        }
        
        private int maxPage = 0;

        private void table_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Logic.Logic.execute(l => l.clients.currentPage = l.clients.currentPage);
        }

        private void UpdatePage(int page)
        {          
            Logic.Logic.execute(l =>
            {
                l.clients.currentPage = page;
            });            
        }

        private void RightArrow_Click(object sender, RoutedEventArgs e)
        {
            Logic.Logic.execute(l =>
            {
                if (l.clients.currentPage + 1 <= maxPage)
                {
                    l.clients.currentPage++;
                }
            });
        }

        private void LeftArrow_Click(object sender, RoutedEventArgs e)
        {
            Logic.Logic.execute(l =>
            {
                if (l.clients.currentPage - 1 > 0)
                {
                    l.clients.currentPage--;
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
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(160, GridUnitType.Pixel) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(90, GridUnitType.Pixel) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(90, GridUnitType.Pixel) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(90, GridUnitType.Pixel) });
        }

        private void discount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Logic.Logic.execute(l =>
            {
                maxPage = (l.clients.allClients.Where(x => !x.deleted).Where(phoneFilter).Where(discountFilter).Count() / itemCount) + (l.clients.allClients.Where(phoneFilter).Where(discountFilter).Count() % itemCount > 0 ? 1 : 0);
                updatePaginatorInfo(l.clients.currentPage);
            });
            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            phoneTextBox.TextChanged += phoneTextBox_TextChanged;
            discount.SelectionChanged += discount_SelectionChanged;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Logic.Logic.execute(logic =>
            {
                logic.clients.selectedClient = null;
                logic.clients.editMode = false;
                logic.general.currentModalPage = Logic.General.MODAL_PAGES.ClientEdit;
            });
        }
    }
}
