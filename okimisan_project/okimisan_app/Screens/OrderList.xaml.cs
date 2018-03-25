﻿using System;
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
    /// Логика взаимодействия для OrderList.xaml
    /// </summary>
    public partial class OrderList : Page
    {
        const int itemCount = 10;
        const int headerHeight = 40;
        const double contentProcent = 0.64;

        public OrderList()
        {
            InitializeComponent();

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
                    Console.WriteLine(rowHeight);

                    //MOMENT
                    Label label = new Label();
                    label.HorizontalAlignment = HorizontalAlignment.Left;
                    label.VerticalAlignment = VerticalAlignment.Center;
                    label.Foreground = Brushes.White;
                    label.FontSize = i == -1 ? 18 : 16;
                    label.Content = i == -1 ? "Дата и время заказа" : l.orders.orders[i].moment;
                    Grid.SetColumn(label, 0);

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
                        for (int j = 0; j < l.clients.clients.Length; j++)
                        {

                            if (l.orders.orders[i].id_client == l.clients.clients[j].id)
                            {
                                name = l.clients.clients[j].name;
                            }
                        }
                    }
                   
                    label3.Content = i == -1 ? "Клиент" : name;
                    Grid.SetColumn(label3, 2);

                    //PHONE
                    Label label4 = new Label();
                    label3.HorizontalAlignment = HorizontalAlignment.Left;
                    label3.VerticalAlignment = VerticalAlignment.Center;
                    label3.Foreground = Brushes.White;
                    label3.FontSize = i == -1 ? 18 : 16;
                    string phone = "";
                    if (i > 0)
                    {
                        for (int j = 0; j < l.clients.clients.Length; j++)
                        {

                            if (l.orders.orders[i].id_client == l.clients.clients[j].id)
                            {
                                phone = l.clients.clients[j].phone;
                            }
                        }
                    }
                    
                    label3.Content = i == -1 ? "Номер телефона клиента" : phone;
                    Grid.SetColumn(label4, 3);

                    //SUMM
                    Label label5 = new Label();
                    label3.HorizontalAlignment = HorizontalAlignment.Left;
                    label3.VerticalAlignment = VerticalAlignment.Center;
                    label3.Foreground = Brushes.White;
                    label3.FontSize = i == -1 ? 18 : 16;
                    label3.Content = i == -1 ? "Сумма заказа" : l.orders.orders[i].total.ToString();
                    Grid.SetColumn(label5, 4);

                    //DISCOUNT
                    Label label6 = new Label();
                    label3.HorizontalAlignment = HorizontalAlignment.Left;
                    label3.VerticalAlignment = VerticalAlignment.Center;
                    label3.Foreground = Brushes.White;
                    label3.FontSize = i == -1 ? 18 : 16;
                    label3.Content = i == -1 ? "Скидка" : l.orders.orders[i].discount.ToString();
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

                    tableOrder.Children.Add(newGrid);
                    Grid.SetRow(newGrid, i + 1);
                }
                maxPage = (l.orders.allOrders.Where(x => !x.deleted).Count() / itemCount) + (l.orders.allOrders.Count() % itemCount > 0 ? 1 : 0);
                updatePaginatorInfo(l.orders.currentPage);
            });
        }
        private int maxPage = 0;
        private void table_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Logic.Logic.execute(l => l.orders.currentPage = l.orders.currentPage);
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
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(180, GridUnitType.Pixel) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50, GridUnitType.Pixel) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100, GridUnitType.Pixel) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100, GridUnitType.Pixel) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100, GridUnitType.Pixel) });
        }
    }
   }
