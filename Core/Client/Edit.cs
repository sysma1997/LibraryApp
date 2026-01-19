using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Library.Core.Client
{
    public static class Edit
    {
        public static void Init(Models.DatabaseContext context, Models.Client clientEdit)
        {
            Models.Client client = context.Clients.Where(c => 
                c.id == clientEdit.id).FirstOrDefault();
            if (client == null)
            {
                MessageBox.Show("Client not found or not exists.", "Warning");
                return;
            }

            client.cardId = clientEdit.cardId;
            client.name = clientEdit.name;
            client.phone = clientEdit.phone;

            context.SaveChanges();
        }
    }
}
