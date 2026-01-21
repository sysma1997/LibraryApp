using Library.Core.Client.Domain;
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
        private readonly ClientRepository clientRepository;
        private MainWindow mainWindow;
        private Client client;

        public AddEditClient(ClientRepository clientRepository, 
            MainWindow mainWindow, 
            Client client = null)
        {
            InitializeComponent();

            this.clientRepository = clientRepository;
            this.mainWindow = mainWindow;
            this.client = client;

            if (client != null)
            {
                txtCardId.Text = client.cardId.ToString();
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
            if (txtCardId.Text == "" || 
                txtName.Text == "" || 
                txtPhone.Text == "")
            {
                string message = "";
                int comma = 0;

                if (txtCardId.Text == "")
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

            long cardId = 0;
            try { cardId = Convert.ToInt64(txtCardId.Text); }
            catch 
            {
                setMessage("Card number is not valid value");
                return;
            }
            string name = txtName.Text;
            string phone = txtPhone.Text;

            Client client = new Client(Guid.NewGuid(), 
                cardId, name, 
                phone);

            try
            {
                if (this.client == null) 
                    clientRepository.add(client);
                else
                {
                    client = client.setId(this.client.id);
                    clientRepository.edit(client);
                }
            } catch (Exception ex)
            {
                setMessage(ex.Message);
                return;
            }

            mainWindow.loadClients();
            this.Close();
        }
    }
}
