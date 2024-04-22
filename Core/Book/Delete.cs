using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Library.Core.Book
{
    public static class Delete
    {
        public static void Init(Models.DatabaseContext context, Guid id)
        {
            Models.Book book = context.Books.Where(b =>
                b.id == id).FirstOrDefault();
            if (book == null)
            {
                MessageBox.Show("Book not found or not exists", "Book");
                return;
            }

            context.Books.Remove(book);
            context.SaveChanges();
        }
    }
}
