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
    /// Interaction logic for AddEditClient.xaml
    /// </summary>
    public partial class AddEditClient : Window
    {
        private Models.DatabaseContext context;
        private MainWindow mainWindow;
        private Models.Client client;

        public AddEditClient(Models.DatabaseContext context, 
            MainWindow mainWindow, 
            Models.Client client = null)
        {
            InitializeComponent();

            this.context = context;
            this.mainWindow = mainWindow;
            this.client = client;

            if (client != null)
            {
                txtCardNumber.Text = client.cardNumber.ToString();
                txtName.Text = client.name;
                txtPhone.Text = client.phone;

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
            if (txtCardNumber.Text == "" || 
                txtName.Text == "" || 
                txtPhone.Text == "")
            {
                string message = "";
                int comma = 0;

                if (txtCardNumber.Text == "")
                {
                    message += "Card number";
                    comma++;
                }
                if (txtName.Text == "") message += (comma++ > 0) ? ", name" : "Name";
                if (txtPhone.Text == "") message += (comma > 0) ? ", phone" : "Phone";

                setMessage(message + " cannot be cleared");
                return;
            }
            clearMessage();

            long cardNumber = 0;
            try { cardNumber = Convert.ToInt64(txtCardNumber.Text); }
            catch 
            {
                setMessage("Card number is not valid value");
                return;
            }
            string name = txtName.Text;
            string phone = txtPhone.Text;

            Models.Client client = new Models.Client();
            client.cardNumber = cardNumber;
            client.name = name;
            client.phone = phone;

            if (this.client == null)
                Core.Client.Add.Init(context, client);
            else
            {
                client.id = this.client.id;
                Core.Client.Edit.Init(context, client);
            }

            mainWindow.loadClients();
            this.Close();
        }
    }
}
