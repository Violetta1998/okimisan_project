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
    /// Логика взаимодействия для UserList.xaml
    /// </summary>
    public partial class UserList : Page
    {
        const int itemCount = 20;
        const double contentProcent = 0.64;

        public UserList()
        {
            InitializeComponent();

            for (int i = 0; i < itemCount; i++)
            {
                table.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            }

            maxPage = (DataBaseManager.getInstance().clients.Count() / itemCount) + (DataBaseManager.getInstance().clients.Count() % itemCount > 0 ? 1 : 0);
            updatePaginatorInfo();
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
           
            var currentTable = DataBaseManager.getInstance().clients.Where(x => true).Skip(itemCount * (page - 1)).Take(itemCount).ToList();

            for(int i=0; i < currentTable.Count(); i++)
            {
                //PHONE
                Label label = new Label();
                label.HorizontalAlignment = HorizontalAlignment.Left;
                label.VerticalAlignment = VerticalAlignment.Center;
                label.Foreground = Brushes.White;
                label.FontSize = 18;
                label.Content = currentTable[i].phone;
                Grid.SetColumn(label, 0);

                //NAME
                Label label2 = new Label();
                label2.HorizontalAlignment = HorizontalAlignment.Left;
                label2.VerticalAlignment = VerticalAlignment.Center;
                label2.Foreground = Brushes.White;
                label2.FontSize = 18;
                label2.Content = currentTable[i].name;
                Grid.SetColumn(label2, 1);

                //ADDRESS
                Label label3 = new Label();
                label3.HorizontalAlignment = HorizontalAlignment.Left;
                label3.VerticalAlignment = VerticalAlignment.Center;
                label3.Foreground = Brushes.White;
                label3.FontSize = 18;
                label3.Content = currentTable[i].street;
                Grid.SetColumn(label3, 2);

                //DISCOUNT
                Label label4 = new Label();
                label4.HorizontalAlignment = HorizontalAlignment.Right;
                label4.VerticalAlignment = VerticalAlignment.Center;
                label4.Foreground = Brushes.White;
                label4.FontSize = 18;
                label4.Content = currentTable[i].discount.ToString()+'%';
                Grid.SetColumn(label4, 3);

                //ORDERS
                Label label5 = new Label();
                label5.HorizontalAlignment = HorizontalAlignment.Right;
                label5.VerticalAlignment = VerticalAlignment.Center;
                label5.Foreground = Brushes.White;
                label5.FontSize = 18;
                label5.Content = currentTable[i].orders;
                Grid.SetColumn(label5, 4);

                //LastOrder
                Label label6 = new Label();
                label6.HorizontalAlignment = HorizontalAlignment.Left;
                label6.VerticalAlignment = VerticalAlignment.Center;
                label6.Foreground = Brushes.White;
                label6.FontSize = 18;
                label6.Content = currentTable[i].last_order;
                Grid.SetColumn(label6, 5);

                //ButtonsGrid
                Grid grid = new Grid();
                grid.HorizontalAlignment = HorizontalAlignment.Right;
                Grid.SetColumn(grid, 6);

                //RECTANGLE
                Rectangle rect = new Rectangle();
                rect.StrokeThickness = 1;
                rect.Stroke = Brushes.White;
                Grid.SetColumnSpan(rect, 7);

                Grid newGrid = new Grid();
                newGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(150, GridUnitType.Pixel) });
                newGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(160, GridUnitType.Pixel) });
                newGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(600, GridUnitType.Pixel) });
                newGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(80, GridUnitType.Pixel) });
                newGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(60, GridUnitType.Pixel) });
                newGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100, GridUnitType.Pixel) });
                newGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                newGrid.MaxWidth = si.VirtualScreen.Width * contentProcent;
                newGrid.Children.Add(rect);
                newGrid.Children.Add(label);
                newGrid.Children.Add(label2);
                newGrid.Children.Add(label3);
                newGrid.Children.Add(label4);
                newGrid.Children.Add(label5);
                newGrid.Children.Add(label6);
                newGrid.Children.Add(grid);
                table.Children.Add(newGrid);
                Grid.SetRow(newGrid, i);
            }
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
    }
}
