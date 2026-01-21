using Library.Core.Book.Domain;
using Library.Core.Client.Domain;
using Library.Core.Loan.Domain;
using Library.Core.Shared.Infrastructure;
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
using System.Windows.Shapes;

namespace Library.Windows
{
    /// <summary>
    /// Interaction logic for AddLoan.xaml
    /// </summary>
    public partial class AddLoan : Window
    {
        private readonly BookRepository bookRepository;
        private readonly ClientRepository clientRepository;
        private readonly LoanRepository loanRepository;
        private MainWindow mainWindow;

        private List<Book.Dto> books;
        private List<Client.Dto> clients;
        private List<Loan.Dto> loans;

        public AddLoan(BookRepository bookRepository, 
            ClientRepository clientRepository, 
            LoanRepository loanRepository, 
            MainWindow mainWindow)
        {
            InitializeComponent();

            this.bookRepository = bookRepository;
            this.clientRepository = clientRepository;
            this.loanRepository = loanRepository;
            this.mainWindow = mainWindow;

            books = bookRepository.getList().Select(b => b.toDto()).ToList();
            clients = clientRepository.getList().Select(c => c.toDto()).ToList();
            loans = loanRepository.getList().Select(l => l.toDto()).ToList();
            if (loans.Count > 0) loans.ForEach(loan =>
            {
                for (int i = 0; i < books.Count; i++)
                {
                    if (loan.idBook == books[i].id)
                    {
                        books[i].quantity--;

                        if (books[i].quantity < 1)
                        {
                            books.RemoveAt(i);
                            i--;
                        }
                    }
                }
            });

            DataGridModels.setItemBooks(dgBooks);
            dgBooks.ItemsSource = books;
            DataGridModels.setItemClients(dgClients);
            dgClients.ItemsSource = clients;
        }

        private void txtBookFilterKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) btnSearchBookClick(null, null);
        }
        private void btnSearchBookClick(object sender, RoutedEventArgs e)
        {
            if (txtBookFilter.Text == "")
            {
                dgBooks.ItemsSource = this.books;
                return;
            }

            string search = txtBookFilter.Text.ToUpper();
            List<Book.Dto> books = this.books.Where(b =>
                b.name.ToUpper().Contains(search) ||
                b.author.ToUpper().Contains(search)).ToList();
            dgBooks.ItemsSource = books;
        }
        private void txtClientFilterKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) btnSearchClientClick(null, null);
        }
        private void btnSearchClientClick(object sender, RoutedEventArgs e)
        {
            if (txtClientFilter.Text == "")
            {
                dgClients.ItemsSource = this.clients;
                return;
            }

            string search = txtClientFilter.Text.ToUpper();
            List<Client.Dto> clients = this.clients.Where(c =>
                c.cardId.ToString().Contains(search) ||
                c.name.ToUpper().Contains(search)).ToList();
            dgClients.ItemsSource = clients;
        }
        private void btnAddClick(object sender, RoutedEventArgs e)
        {
            Book.Dto book = dgBooks.SelectedItem as Book.Dto;
            Client.Dto client = dgClients.SelectedItem as Client.Dto;
            if (book == null || 
                client == null)
            {
                string message = "";

                if (book == null) message += "Book";
                if (client == null) message += (book == null) ? " and client" : "Client";

                MessageBox.Show($"Not select {message}", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (dpDeadline.SelectedDate == null)
            {
                MessageBox.Show($"Not select deadline", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            DateTime deadline = dpDeadline.SelectedDate.GetValueOrDefault();
            if (loans.Count > 0)
            {
                foreach (Loan.Dto _loan in loans)
                {
                    if (_loan.idBook == book.id &&
                        _loan.idClient == client.id)
                    {
                        MessageBox.Show("This book has already been loaned to this client.", "Warning");
                        return;
                    }
                };
            }

            MessageBoxResult result = MessageBox.Show($"Lend book '{book.name}' to client {client.name}?\n\n" + 
                $"Deadline: {deadline.ToString("dd/MM/yyyy")}", "Lend",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result != MessageBoxResult.Yes) return;

            Book cBook = new Book(book.id, book.name, book.author, book.numPages, book.quantity);
            Client cClient = new Client(client.id, client.cardId, client.name, client.phone);
            Loan loan = new Loan(Guid.NewGuid(), book.id, client.id, 
                DateTime.Now.Date, deadline.Date, 
                cBook, cClient);

            try
            {
                loanRepository.add(loan);
                this.Close();
                mainWindow.loadLoans();
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning");
            }
        }
    }
}
