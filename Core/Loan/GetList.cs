using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Loan
{
    public static class GetList
    {
        public static List<Models.Loan> Init(Models.DatabaseContext context)
        {
            return context.Loans
                .Select(l => new Models.Loan()
                {
                    id = l.id, 
                    idBook = l.idBook,
                    idClient = l.idClient,
                    date = l.date, 
                    deadline = l.deadline, 

                    Book = new Models.Book()
                    {
                        id = l.idBook, 
                        name = l.Book.name
                    }, 
                    Client = new Models.Client()
                    {
                        id = l.idClient, 
                        name = l.Client.name
                    }
                })
                .OrderBy(l => l.Book.name)
                .OrderBy(l => l.Client.name)
                .OrderByDescending(l => l.date)
                .OrderBy(l => l.deadline)
                .AsNoTracking()
                .ToList();
        }
    }
}
