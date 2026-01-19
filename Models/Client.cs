using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Client
    {
        public Guid id { get; set; }
        public long cardId { get; set; }
        public string name { get; set; }
        public string phone { get; set; }

        public virtual List<Loan> Loans { get; } = new();
    }
}
