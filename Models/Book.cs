using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Book
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string author { get; set; }
        public int numPages { get; set; }
        public int quantity { get; set; }

        public virtual List<Loan> Loans { get; } = new();
    }
}
