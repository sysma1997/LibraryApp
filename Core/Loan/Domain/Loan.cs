using Library.Core.Shared.Domain;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Loan.Domain
{
    public class Loan
    {
        public readonly Guid id;
        public readonly Guid idBook;
        public readonly Guid idClient;
        public readonly DateTime date;
        public readonly DateTime deadline;

        public readonly Book.Domain.Book book;
        public readonly Client.Domain.Client client;

        public Loan(Guid id, Guid idBook, Guid idClient, 
            DateTime date, DateTime deadline, 
            Book.Domain.Book book, 
            Client.Domain.Client client)
        {
            if (id == Guid.Empty || 
                idBook == Guid.Empty || 
                idClient == Guid.Empty || 
                book == null || 
                client == null)
            {
                ThrowMessage message = new ThrowMessage();

                if (id == Guid.Empty) message.add("Id is not valid.");
                if (idBook == Guid.Empty) message.add("Id book is not valid.");
                if (idClient == Guid.Empty) message.add("Id client is not valid.");
                if (book == null) message.add("Book is required.");
                if (client == null) message.add("Client is required.");

                throw new Exception(message.ToString());
            }

            this.id = id;
            this.idBook = idBook;
            this.idClient = idClient;
            this.date = date;
            this.deadline = deadline;
            this.book = book;
            this.client = client;
        }

        public Loan setId(Guid id)
        {
            return new Loan(id, idBook, idClient,
                date, deadline,
                book, client);
        }
        public Loan setDate(DateTime date)
        {
            return new Loan(id, idBook, idClient, 
                date, deadline, 
                book, client);
        }
        public Loan setDeadline(DateTime deadline)
        {
            return new Loan(id, idBook, idClient,
                date, deadline,
                book, client);
        }
        public Loan setBook(Book.Domain.Book book)
        {
            return new Loan(id, book.id, idClient,
                date, deadline,
                book, client);
        }
        public Loan setClient(Client.Domain.Client client)
        {
            return new Loan(id, idBook, client.id,
                date, deadline,
                book, client);
        }

        public Dto toDto()
        {
            return new Dto()
            {
                id = id,
                idBook = idBook,
                idClient = idClient,
                date = date,
                deadline = deadline,
                
                book = book.toDto(),
                client = client.toDto()
            };
        }

        public JObject toJson()
        {
            JObject json = new JObject()
            {
                ["id"] = id,
                ["idBook"] = idBook,
                ["idClient"] = idClient,
                ["date"] = date,
                ["deadline"] = deadline,

                ["book"] = book.toJson(),
                ["client"] = client.toJson()
            };

            return json;
        }
        public override string ToString()
        {
            return toJson().ToString();
        }

        public class Dto
        {
            public Guid id { get; set; }
            public Guid idBook { get; set; }
            public Guid idClient { get; set; }
            public DateTime date { get; set; } 
            public DateTime deadline { get; set; }

            public Book.Domain.Book.Dto book { get; set; }
            public Client.Domain.Client.Dto client { get; set; }
        }
    }
}
