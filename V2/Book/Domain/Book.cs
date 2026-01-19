using Library.V2.Shared.Domain;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Library.V2.Book.Domain
{
    class Book
    {
        public readonly Guid id;
        public readonly string name;
        public readonly string author;
        public readonly int numPages;
        public readonly int quantity;

        public Book(Guid id, 
            string name, string author, 
            int numPages, int quantity)
        {
            if (id == Guid.Empty || 
                (name == null || name == "") || 
                (author == null || author == "") || 
                numPages <= 0 || 
                quantity < 0)
            {
                ThrowMessage message = new ThrowMessage();

                if (id == Guid.Empty) message.add("Id not is valid.");
                if (name == null || name == "") message.add("Name is required.");
                if (author == null || author == "") message.add("Author is required.");
                if (numPages <= 0) message.add("The number of pages cannot be less than or equal to 0.");
                if (quantity < 0) message.add("The quantity cannot be a negative number.");

                throw new Exception(message.ToString());
            }

            this.id = id;
            this.name = name;
            this.author = author;
            this.numPages = numPages;
            this.quantity = quantity;
        }

        public Book setName(string name)
        {
            return new Book(id, name, author, numPages, quantity);
        }
        public Book setAuthor(string author)
        {
            return new Book(id, name, author, numPages, quantity);
        }
        public Book setNumPages(int numPages)
        {
            return new Book(id, name, author, numPages, quantity);
        }
        public Book setQuantity(int quantity)
        {
            return new Book(id, name, author, numPages, quantity);
        }
        public Book setLoans(Loan.Domain.Loan loan)
        {
            return new Book(id, name, author, numPages, quantity);
        }

        public JObject toJson()
        {
            return new JObject()
            {
                ["id"] = id,
                ["name"] = name,
                ["author"] = author,
                ["numPages"] = numPages,
                ["quantity"] = quantity
            };
        }
        public override string ToString()
        {
            return toJson().ToString();
        }
    }
}
