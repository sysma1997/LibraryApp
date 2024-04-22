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
using System.Windows.Shapes;

namespace Library.Windows
{
    /// <summary>
    /// Interaction logic for AddBookWindow.xaml
    /// </summary>
    public partial class AddEditBook : Window
    {
        private Models.DatabaseContext db;
        private MainWindow mainWindow;
        private Models.Book book;

        public AddEditBook(Models.DatabaseContext db, 
            MainWindow mainWindow, 
            Models.Book book = null)
        {
            InitializeComponent();

            this.db = db;
            this.mainWindow = mainWindow;
            this.book = book;

            if (book != null)
            {
                txtName.Text = book.name;
                txtAuthor.Text = book.author;
                txtNumPages.Text = book.numPages.ToString();
                txtQuantity.Text = book.quantity.ToString();

                btnAddEdit.Content = "Edit";
            }
        }
        private void setMessage(string message)
        {
            lblMessage.Foreground = Brushes.Red;
            lblMessage.Content = message;
        }
        private void clearMessage()
        {
            lblMessage.Foreground = Brushes.Black;
            lblMessage.Content = "";
        }

        private void btnAddClick(object sender, RoutedEventArgs e)
        {
            if (txtName.Text == "" || 
                txtAuthor.Text == "" || 
                txtNumPages.Text == "" || 
                txtQuantity.Text == "")
            {
                string message = "";
                int comma = 0;

                if (txtName.Text == "")
                {
                    message += "Name";
                    comma++;
                }
                if (txtAuthor.Text == "") message += (comma++ > 0) ? ", author" : "Author";
                if (txtNumPages.Text == "") message += (comma++ > 0) ? ", Number pages" : "Number pages";
                if (txtQuantity.Text == "") message += (comma > 0) ? ", quantity" : "Quantity";

                setMessage(message + " cannot be cleared");
                return;
            }
            clearMessage();

            string name = txtName.Text;
            string author = txtAuthor.Text;
            int numPages = 0;
            try { numPages = Convert.ToInt32(txtNumPages.Text); }
            catch 
            { 
                setMessage("Number the pages is not valid value");
                return;
            }
            int quantity = 0;
            try { quantity = Convert.ToInt32(txtQuantity.Text); }
            catch 
            { 
                setMessage("Quantity is not valid value");
                return;
            }

            Models.Book book = new Models.Book();
            book.name = name;
            book.author = author;
            book.numPages = numPages;
            book.quantity = quantity;
            
            if (this.book == null)
                Core.Book.Add.Init(db, book);
            else
            {
                book.id = this.book.id;
                Core.Book.Edit.Init(db, book);
            }

            mainWindow.loadBooks();
            this.Close();
        }
    }
}
