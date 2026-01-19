using Library.Models;
using Library.V2.Client.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.V2.Client.Infrastructure
{
    class ClientEFCRepository : ClientRepository
    {
        private DatabaseContext context;

        public ClientEFCRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public void add(Domain.Client client)
        {
            Models.Client mClient = new Models.Client();
            mClient.id = client.id;
            mClient.cardId = client.cardId;
            mClient.name = client.name;
            mClient.phone = client.phone;

            context.Clients.Add(mClient);
        }
        public void edit(Domain.Client client)
        {
            Models.Client? mClient = context.Clients.Where(c =>
                c.id == client.id).FirstOrDefault();
            if (mClient == null)
                throw new Exception("Client not exists.");

            mClient.cardId = client.cardId;
            mClient.name = client.name;
            mClient.phone = client.phone;

            context.SaveChanges();
        }
        public void delete(Guid id)
        {
            Models.Client? mClient = context.Clients.Where(c =>
                c.id == id).FirstOrDefault();
            if (mClient == null)
                throw new Exception("Client not exists.");

            context.Clients.Remove(mClient);
            context.SaveChanges();
        }

        public List<Domain.Client> getList()
        {
            List<Domain.Client> clients = new List<Domain.Client>();
            List<Models.Client> mClients = context.Clients
                .OrderBy(c => c.cardId)
                .OrderBy(c => c.name)
                .AsNoTracking()
                .ToList();

            mClients.ForEach(c => clients.Add(new Domain.Client(c.id, 
                c.cardId, c.name, 
                c.phone)));

            return clients;
        }
    }
}
