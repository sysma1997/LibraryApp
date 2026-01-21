using Library.Core.Book.Domain;
using Library.Core.Book.Infrastructure;
using Library.Core.Client.Domain;
using Library.Core.Client.Infrastructure;
using Library.Core.Loan.Domain;
using Library.Core.Loan.Infrastructure;
using Library.Core.Shared.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private readonly Models.DatabaseContext context = new Models.DatabaseContext();
        private readonly BookRepository bookRepository;
        private readonly ClientRepository clientRepository;
        private readonly LoanRepository loanRepository;

        private MainWindowViews view = MainWindowViews.BOOKS;

        private List<Book.Dto> books = null;
        private List<Client.Dto> clients = null;
        private List<Loan.Dto> loans = null;

        public MainWindow()
        {
            InitializeComponent();

            bookRepository = new BookEFCRepository(context);
            clientRepository = new ClientEFCRepository(context);
            loanRepository = new LoanEFCRepository(context);
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
                List<Book.Dto> books = this.books.Where(b =>
                    b.name.ToUpper().Contains(search) ||
                    b.author.ToUpper().Contains(search)).ToList();
                dataGrid.ItemsSource = books;
            }
            else if (view == MainWindowViews.CLIENTS)
            {
                List<Client.Dto> clients = this.clients.Where(c =>
                    c.cardId.ToString().Contains(search) ||
                    c.name.ToUpper().Contains(search) ||
                    c.phone.Contains(search)).ToList();
                dataGrid.ItemsSource = clients;
            }
            else
            {
                List<Loan.Dto> loans = this.loans.Where(l =>
                    l.book.name.ToUpper().Contains(search) ||
                    l.client.name.ToUpper().Contains(search)).ToList();
                dataGrid.ItemsSource = loans;
            }
        }
        
        public void loadBooks()
        {
            clients = null;
            loans = null;

            books = bookRepository.getList()
                .Select(b => b.toDto()).ToList();
            DataGridModels.setItemBooks(dataGrid);
            dataGrid.ItemsSource = books;
        }
        public void loadClients()
        {
            books = null;
            loans = null;

            clients = clientRepository.getList()
                .Select(c => c.toDto()).ToList();
            DataGridModels.setItemClients(dataGrid);
            dataGrid.ItemsSource = clients;
        }
        public void loadLoans()
        {
            books = null;
            clients = null;

            loans = loanRepository.getList()
                .Select(l => l.toDto()).ToList();
            DataGridModels.setItemLoans(dataGrid);
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
                Book.Dto book = dataGrid.SelectedItem as Book.Dto;

                id = book.id;
                title = "Book";
                message = $"Remove this book {book.name}?";
            }
            else if (view == MainWindowViews.CLIENTS)
            {
                Client.Dto client = dataGrid.SelectedItem as Client.Dto;

                id = client.id;
                title = "Client";
                message = $"Remove this client {client.name}?";
            }
            else
            {
                Loan.Dto loan = dataGrid.SelectedItem as Loan.Dto;

                id = loan.id;
                title = "Loan";
                message = $"Did the client {loan.client.name} return the '{loan.book.name}' book?";
            }

            MessageBoxResult result = MessageBox.Show(message, title, MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;

            if (view == MainWindowViews.BOOKS)
            {
                bookRepository.delete(id);
                loadBooks();
            }
            else if (view == MainWindowViews.CLIENTS)
            {
                clientRepository.delete(id);
                loadClients();
            }
            else
            {
                loanRepository.returnBook(id);
                loadLoans();
            }
        }
        private void btnEditClick(object sender, RoutedEventArgs e)
        {
            if (view == MainWindowViews.BOOKS)
            {
                Book book = dataGrid.SelectedItem as Book;
                new Windows.AddEditBook(bookRepository, this, book).Show();
            }
            else if (view == MainWindowViews.CLIENTS)
            {
                Client client = dataGrid.SelectedItem as Client;
                new Windows.AddEditClient(clientRepository, this, client).Show();
            }
        }
        private void btnAddClick(object sender, RoutedEventArgs e)
        {
            if (view == MainWindowViews.BOOKS)
                new Windows.AddEditBook(bookRepository, this).Show();
            else if (view == MainWindowViews.CLIENTS)
                new Windows.AddEditClient(clientRepository, this).Show();
            else if (view == MainWindowViews.LOANS)
                new Windows.AddLoan(bookRepository, clientRepository, loanRepository, this).Show();
        }
    }
}
