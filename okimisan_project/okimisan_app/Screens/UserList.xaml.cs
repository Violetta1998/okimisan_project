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
using System.Windows.Forms;

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
                Rectangle rect = new Rectangle();
                rect.StrokeThickness = 1;
                rect.Stroke = Brushes.White;
                Grid newGrid = new Grid();
                newGrid.MaxWidth = SystemInformation.VirtualScreen.Width * contentProcent;
                newGrid.Children.Add(rect);
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
