using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Models
{
    public class Loan
    {
        public Guid id { get; set; }
        public Guid idLibrary { get; set; }
        public Guid idClient { get; set; }
        public DateTime date { get; set; }
        public DateTime deadline { get; set; }

        public virtual Library Library { get; set; }
        public virtual Client Client { get; set; }
    }
}
