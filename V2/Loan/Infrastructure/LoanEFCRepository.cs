using Library.Models;
using Library.V2.Loan.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.V2.Loan.Infrastructure
{
    class LoanEFCRepository : LoanRepository
    {
        private DatabaseContext context;

        public LoanEFCRepository(DatabaseContext context)
        {
            this.context = context;
        }

        public void add(Domain.Loan loan)
        {
            Models.Loan mLoan = new Models.Loan();
            mLoan.id = loan.id;
            mLoan.idBook = loan.idBook;
            mLoan.idClient = loan.idClient;
            mLoan.date = loan.date;
            mLoan.deadline = loan.deadline;

            context.Loans.Add(mLoan);
            context.SaveChanges();
        }
        public void returnBook(Guid id)
        {
            Models.Loan? loan = context.Loans.Where(l =>
                l.id == id).FirstOrDefault();
            if (loan == null)
                throw new Exception("Loan not exists.");

            context.Loans.Remove(loan);
            context.SaveChanges();
        }

        public List<Domain.Loan> getList()
        {
            List<Domain.Loan> loans = new List<Domain.Loan>();
            List<Models.Loan> mLoans = context.Loans
                .OrderBy(l => l.Book.name)
                .OrderBy(l => l.Client.name)
                .OrderByDescending(l => l.date)
                .OrderBy(l => l.deadline)
                .AsNoTracking()
                .ToList();

            mLoans.ForEach(l => loans.Add(new Domain.Loan(l.id, l.idBook, l.idClient,
                l.date, l.deadline,
                new Book.Domain.Book(l.Book.id, l.Book.name, l.Book.author, l.Book.numPages, l.Book.quantity),
                new Client.Domain.Client(l.Client.id, l.Client.cardId, l.Client.name, l.Client.phone))));

            return loans;
        }
    }
}
