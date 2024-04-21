using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Book
{
    public static class Add
    {
        public static Guid Init(Models.DatabaseContext db, Models.Book book)
        {
            if (book.id == Guid.Empty) book.id = Guid.NewGuid();

            db.Books.Add(book);
            db.SaveChanges();
            return book.id;
        }
    }
}
