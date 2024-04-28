using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Library.Core.Loan
{
    public static class Return
    {
        public static void Init(Models.DatabaseContext context, Guid id)
        {
            Models.Loan loan = context.Loans.Where(l =>
                l.id == id).FirstOrDefault();
            if (loan == null)
            {
                MessageBox.Show("Loan not found.", "Warning");
                return;
            }

            context.Loans.Remove(loan);
            context.SaveChanges();
        }
    }
}
