using Library.V2.Shared.Domain;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.V2.Loan.Domain
{
    class Loan
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

        public JObject toJson()
        {
            JObject json = new JObject()
            {
                ["id"] = id,
                ["idBook"] = idBook,
                ["idClient"] = idClient,
                ["date"] = date,
                ["deadline"] = deadline
            };

            if (book != null) json["book"] = book.toJson();
            if (client != null) json["client"] = client.toJson();

            return json;
        }
        public override string ToString()
        {
            return toJson().ToString();
        }
    }
}
