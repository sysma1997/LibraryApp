using Microsoft.EntityFrameworkCore;
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

namespace Library
{
    enum MainWindowViews { BOOKS, CLIENTS, LOANS }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Models.DatabaseContext db = new Models.DatabaseContext();

        private MainWindowViews view = MainWindowViews.BOOKS;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void setView(MainWindowViews view)
        {
            this.view = view;
            gDashboard.Visibility = Visibility.Visible;

            if (view == MainWindowViews.BOOKS)
            {
                lblSearch.Content = "Search Books:";
                loadBooks();
            }
            else if (view == MainWindowViews.CLIENTS)
            {
                lblSearch.Content = "Search Clients:";
            }
            else
            {
                lblSearch.Content = "Search Loans:";
            }
        }
        private void loadBooks()
        {
            List<Models.Book> books = Core.Book.GetList.Init(db);
            dataGrid.Columns.Clear();
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Id", 
                Binding = new Binding("id"), 
                Visibility = Visibility.Collapsed
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Name", 
                Binding = new Binding("name")
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Author",
                Binding = new Binding("author")
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Number pages",
                Binding = new Binding("numPages")
            });
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Quantity",
                Binding = new Binding("quantity"), 
                Width = new DataGridLength(1, DataGridLengthUnitType.Star)
            });
            dataGrid.ItemsSource = books;
        }

        private void btnBooksShow(object sender, RoutedEventArgs e)
        {
            setView(MainWindowViews.BOOKS);
        }
        private void btnClientsShow(object sender, RoutedEventArgs e)
        {
            setView(MainWindowViews.CLIENTS);
        }
        private void btnLoansShow(object sender, RoutedEventArgs e)
        {
            setView(MainWindowViews.LOANS);
        }

        private void tbEnterKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (view == MainWindowViews.BOOKS)
                {
                    //
                }
            }
        }
        private void btnSearchClick(object sender, RoutedEventArgs e)
        {
            if (view == MainWindowViews.BOOKS)
            {
                //
            }
        }
        private void btnAddClick(object sender, RoutedEventArgs e)
        {
            if (view == MainWindowViews.BOOKS)
            {
                //
            }
        }
    }
}
