using Library.Core.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Shared.Domain
{
    public class ThrowMessage
    {
        private StringBuilder message = new StringBuilder();

        public void add(string message) => this.message.Append(
            ((this.message.Length > 0) ? "\n" : "") + 
            message);
        public void clean() => message.Clear();

        public override string ToString() => message.ToString();
    }
}
