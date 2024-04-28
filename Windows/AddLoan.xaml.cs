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
        private Models.DatabaseContext context;
        private MainWindow mainWindow;

        private List<Models.Loan> loans;

        public AddLoan(Models.DatabaseContext context, MainWindow mainWindow)
        {
            InitializeComponent();

            this.context = context;
            this.mainWindow = mainWindow;

            List<Models.Book> books = Core.Book.GetList.Init(context).ToList();
            List<Models.Client> clients = Core.Client.GetList.Init(context);
            loans = Core.Loan.GetList.Init(context);
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

            Core.Shared.DataGridModels.setItemBooks(dgBooks);
            dgBooks.ItemsSource = books;
            Core.Shared.DataGridModels.setItemClients(dgClients);
            dgClients.ItemsSource = clients;
        }

        private void btnAddClick(object sender, RoutedEventArgs e)
        {
            Models.Book book = dgBooks.SelectedItem as Models.Book;
            Models.Client client = dgClients.SelectedItem as Models.Client;
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
                foreach (Models.Loan _loan in loans)
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

            Models.Loan loan = new Models.Loan();
            loan.idBook = book.id;
            loan.idClient = client.id;
            loan.date = DateTime.Now.Date;
            loan.deadline = deadline.Date;

            Core.Loan.Add.Init(context, loan);
            this.Close();
            mainWindow.loadLoans();
        }
    }
}
