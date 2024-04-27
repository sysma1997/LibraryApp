using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Client
{
    public static class GetList
    {
        public static List<Models.Client> Init(Models.DatabaseContext context)
        {
            return context.Clients.ToList();
        }
    }
}
