using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Loan
{
    public static class Add
    {
        public static Guid Init(Models.DatabaseContext context, Models.Loan loan)
        {
            if (loan.id == Guid.Empty) loan.id = Guid.NewGuid();

            context.Loans.Add(loan);
            context.SaveChanges();
            return loan.id;
        }
    }
}
