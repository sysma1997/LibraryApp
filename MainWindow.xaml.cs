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

            btnDelete.Visibility = Visibility.Collapsed;
            btnEdit.Visibility = Visibility.Collapsed;

            if (view == MainWindowViews.BOOKS)
            {
                lblSearch.Content = "Search Books:";
                loadBooks();
            }
            else if (view == MainWindowViews.CLIENTS)
            {
                lblSearch.Content = "Search Clients:";
                loadClients();
            }
            else
            {
                lblSearch.Content = "Search Loans:";
                loadLoans();
            }
        }
        private void search()
        {
            if (txtSearch.Text == "")
            {
                if (view == MainWindowViews.BOOKS) loadBooks();
                else if (view == MainWindowViews.CLIENTS) loadClients();
                else loadLoans();

                return;
            }

            string search = txtSearch.Text.ToUpper();
            if (view == MainWindowViews.BOOKS)
            {
                List<Models.Book> books = this.books.Where(b =>
                    b.name.ToUpper().Contains(search) ||
                    b.author.ToUpper().Contains(search)).ToList();
                dataGrid.ItemsSource = books;
            }
            else if (view == MainWindowViews.CLIENTS)
            {
                List<Models.Client> clients = this.clients.Where(c =>
                    c.cardNumber.ToString().Contains(search) ||
                    c.name.ToUpper().Contains(search) ||
                    c.phone.Contains(search)).ToList();
                dataGrid.ItemsSource = clients;
            }
        }
        
        public void loadBooks()
        {
            clients = null;
            loans = null;

            books = Core.Book.GetList.Init(db);
            Core.Shared.DataGridModels.setItemBooks(dataGrid);
            dataGrid.ItemsSource = books;
        }
        public void loadClients()
        {
            books = null;
            loans = null;

            clients = Core.Client.GetList.Init(db);
            Core.Shared.DataGridModels.setItemClients(dataGrid);
            dataGrid.ItemsSource = clients;
        }
        public void loadLoans()
        {
            books = null;
            clients = null;

            loans = Core.Loan.GetList.Init(db);
            Core.Shared.DataGridModels.setItemLoans(dataGrid);
            dataGrid.ItemsSource = loans;
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

            if (view == MainWindowViews.LOANS)
            {
                btnDelete.Visibility = Visibility.Visible;
                btnDelete.Content = "Return";
                btnEdit.Visibility = Visibility.Collapsed;
                return;
            }

            btnDelete.Visibility = Visibility.Visible;
            btnDelete.Content = "Delete";
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
                message = $"Remove this book {book.name}?";
            }
            else if (view == MainWindowViews.CLIENTS)
            {
                Models.Client client = dataGrid.SelectedItem as Models.Client;

                id = client.id;
                title = "Client";
                message = $"Remove this client {client.name}?";
            }
            else
            {
                Models.Loan loan = dataGrid.SelectedItem as Models.Loan;

                id = loan.id;
                title = "Loan";
                message = $"Did the client {loan.Client.name} return the '{loan.Book.name}' book?";
            }

            MessageBoxResult result = MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;

            if (view == MainWindowViews.BOOKS)
            {
                Core.Book.Delete.Init(db, id);
                loadBooks();
            }
            else if (view == MainWindowViews.CLIENTS)
            {
                Core.Client.Delete.Init(db, id);
                loadClients();
            }
            else
            {
                Core.Loan.Return.Init(db, id);
                loadLoans();
            }
        }
        private void btnEditClick(object sender, RoutedEventArgs e)
        {
            if (view == MainWindowViews.BOOKS)
            {
                Models.Book book = dataGrid.SelectedItem as Models.Book;
                new Windows.AddEditBook(db, this, book).Show();
            }
            else if (view == MainWindowViews.CLIENTS)
            {
                Models.Client client = dataGrid.SelectedItem as Models.Client;
                new Windows.AddEditClient(db, this, client).Show();
            }
        }
        private void btnAddClick(object sender, RoutedEventArgs e)
        {
            if (view == MainWindowViews.BOOKS)
                new Windows.AddEditBook(db, this).Show();
            else if (view == MainWindowViews.CLIENTS)
                new Windows.AddEditClient(db, this).Show();
            else if (view == MainWindowViews.LOANS)
                new Windows.AddLoan(db, this).Show();
        }
    }
}
