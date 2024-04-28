using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Book
{
    public static class GetList
    {
        public static List<Models.Book> Init(Models.DatabaseContext db)
        {
            return db.Books
                .OrderBy(b => b.name)
                .OrderBy(b => b.author)
                .AsNoTracking()
                .ToList();
        }
    }
}
