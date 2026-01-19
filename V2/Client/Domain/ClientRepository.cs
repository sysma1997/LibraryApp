using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.V2.Client.Domain
{
    interface ClientRepository
    {
        void add(Client client);
        void edit(Client client);
        void delete(Guid id);

        List<Client> getList();
    }
}
