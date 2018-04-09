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

namespace okimisan_app.Screens
{
    /// <summary>
    /// Логика взаимодействия для Categories.xaml
    /// </summary>
    public partial class Categories : Page
    {
        const int headerHeight = 40;
       
        public Categories()
        {
            InitializeComponent();

            Logic.Logic.onLogicUpdate(l =>
            {
                table.Children.Clear();
                l.categories.category = l.categories.allCategory.ToArray();
                int count = 0;
                for (int i = 0; i < l.categories.category.Count(); i++)
                {
                    Grid newGrid = new Grid();

                    for (int j = 0; j < l.categories.category.Count(); j++)
                    {
                        if (l.categories.category[i].id_category == l.categories.category[j].id)
                        {
                            count++;
                        } 
                    }
                    if (count == 0)
                    {
                        Label label = new Label();
                        label.HorizontalAlignment = HorizontalAlignment.Left;
                        label.VerticalAlignment = VerticalAlignment.Center;
                        label.Foreground = Brushes.White;
                        label.FontSize = 16;
                        label.Content = l.categories.category[i].name;
                        Grid.SetColumn(label, 0);
                        newGrid.Children.Add(label);
                    }
                    count = 0;
                    AddRowDedinitions(newGrid);
                    table.Children.Add(newGrid);
                    Grid.SetRow(newGrid, i + 1);
                }
            });  
    }

        private void AddRowDedinitions(Grid grid)
        {
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50, GridUnitType.Pixel) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50, GridUnitType.Pixel) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50, GridUnitType.Pixel) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50, GridUnitType.Pixel) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50, GridUnitType.Pixel) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50, GridUnitType.Pixel) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50, GridUnitType.Pixel) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50, GridUnitType.Pixel) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(50, GridUnitType.Pixel) });
        }

        private void table_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Logic.Logic.execute(l => l.categories.currentPage = l.categories.currentPage);
        }

        private void UpdatePage(int page)
        {
            Logic.Logic.execute(l =>
            {
                l.categories.currentPage = page;
            });
        }
    }
 }
