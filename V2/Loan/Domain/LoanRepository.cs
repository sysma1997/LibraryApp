using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.V2.Loan.Domain
{
    interface LoanRepository
    {
        void add(Loan loan);
        void returnBook(Guid id);

        List<Loan> getList();
    }
}
