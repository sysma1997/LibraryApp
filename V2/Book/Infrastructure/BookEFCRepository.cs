using Library.Models;
using Library.V2.Book.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.V2.Book.Infrastructure
{
    class BookEFCRepository : BookRepository
    {
        private Models.DatabaseContext context;

        public BookEFCRepository(Models.DatabaseContext context)
        {
            this.context = context;
        }

        public void add(Domain.Book book)
        {
            Models.Book mBook = new Models.Book();
            mBook.id = book.id;
            mBook.name = book.name;
            mBook.author = book.author;
            mBook.numPages = book.numPages;
            mBook.quantity = book.quantity;

            context.Books.Add(mBook);
            context.SaveChanges();
        }
        public void edit(Domain.Book book)
        {
            Models.Book? mBook = context.Books.Where(b => 
                b.id == book.id).FirstOrDefault();
            if (mBook == null)
                throw new Exception("Book not exists.");

            mBook.name = book.name;
            mBook.author = book.author;
            mBook.numPages = book.numPages;
            mBook.quantity = book.quantity;

            context.SaveChanges();
        }
        public void delete(Guid id)
        {
            Models.Book? mBook = context.Books.Where(b =>
                b.id == id).FirstOrDefault();
            if (mBook == null)
                throw new Exception("Book not exists.");

            context.Books.Remove(mBook);
            context.SaveChanges();
        }

        public List<Domain.Book> getList()
        {
            List<Domain.Book> books = new List<Domain.Book>();
            List<Models.Book> mBooks = context.Books
                .OrderBy(b => b.name)
                .OrderBy(b => b.author)
                .AsNoTracking()
                .ToList();

            mBooks.ForEach(b => books.Add(new Domain.Book(b.id, 
                b.name, b.author, 
                b.numPages, b.quantity)));

            return books;
        }
    }
}
