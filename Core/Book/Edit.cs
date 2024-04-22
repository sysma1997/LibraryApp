using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Library.Core.Book
{
    public static class Edit
    {
        public static void Init(Models.DatabaseContext context, Models.Book bookEdit)
        {
            Models.Book book = context.Books.Where(b =>
                b.id == bookEdit.id).FirstOrDefault();
            if (book == null)
            {
                MessageBox.Show("Book not found or not exists.", "Warning");
                return;
            }

            book.name = bookEdit.name;
            book.author = bookEdit.author;
            book.numPages = bookEdit.numPages;
            book.quantity = bookEdit.quantity;

            context.SaveChanges();
        }
    }
}
