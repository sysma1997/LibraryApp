using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Client
{
    public static class Add
    {
        public static Guid Init(Models.DatabaseContext context, Models.Client client)
        {
            if (client.id == Guid.Empty) client.id = Guid.NewGuid();

            context.Add(client);
            context.SaveChanges();
            return client.id;
        }
    }
}
