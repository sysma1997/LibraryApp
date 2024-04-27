using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Library.Core.Client
{
    public static class Delete
    {
        public static void Init(Models.DatabaseContext context, Guid id)
        {
            Models.Client client = context.Clients.Where(c =>
                c.id == id).FirstOrDefault();
            if (client == null)
            {
                MessageBox.Show("Client not found or not exists.", "Warning");
                return;
            }

            context.Clients.Remove(client);
            context.SaveChanges();
        }
    }
}
