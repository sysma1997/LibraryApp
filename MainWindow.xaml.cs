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

        private List<Models.Book> books = null;
        private List<Models.Client> clients = null;
        private List<Models.Loan> loans = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void setView(MainWindowViews view)
        {
            this.view = view;
            
            if (lblDefaultOption.Visibility == Visibility.Visible)
                lblDefaultOption.Visibility = Visibility.Collapsed;
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
        private void search()
        {
            if (txtSearch.Text == "")
            {
                if (view == MainWindowViews.BOOKS) loadBooks();

                return;
            }

            if (view == MainWindowViews.BOOKS)
            {
                List<Models.Book> books = this.books.Where(b =>
                    b.name.Contains(txtSearch.Text) ||
                    b.author.Contains(txtSearch.Text)).ToList();
                dataGrid.ItemsSource = books;
            }
        }
        
        public void loadBooks()
        {
            clients = null;
            loans = null;

            books = Core.Book.GetList.Init(db);
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
            if (e.Key == Key.Enter) search();
        }
        private void btnSearchClick(object sender, RoutedEventArgs e)
        {
            search();
        }
        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid.SelectedItem == null) return;

            btnDelete.Visibility = Visibility.Visible;
            btnEdit.Visibility = Visibility.Visible;
        }
        private void btnDeleteClick(object sender, RoutedEventArgs e)
        {
            Guid id = Guid.Empty;
            string title = "";
            string message = "";

            if (view == MainWindowViews.BOOKS)
            {
                Models.Book book = dataGrid.SelectedItem as Models.Book;

                id = book.id;
                title = "Book";
                message = $"¿Remove this book {book.name}?";
            }

            MessageBoxResult result = MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;

            if (view == MainWindowViews.BOOKS)
            {
                Core.Book.Delete.Init(db, id);
                loadBooks();
            }
        }
        private void btnEditClick(object sender, RoutedEventArgs e)
        {
            if (view == MainWindowViews.BOOKS)
            {
                Models.Book book = dataGrid.SelectedItem as Models.Book;
                new Windows.AddEditBook(db, this, book).Show();
            }
        }
        private void btnAddClick(object sender, RoutedEventArgs e)
        {
            if (view == MainWindowViews.BOOKS)
                new Windows.AddEditBook(db, this).Show();
        }
    }
}
