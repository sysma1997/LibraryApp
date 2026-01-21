using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Book.Domain
{
    public interface BookRepository
    {
        void add(Book book);
        void edit(Book book);
        void delete(Guid id);

        List<Book> getList();
    }
}
