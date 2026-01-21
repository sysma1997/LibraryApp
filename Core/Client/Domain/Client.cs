using Library.Core.Shared.Domain;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Client.Domain
{
    public class Client
    {
        public readonly Guid id;
        public readonly long cardId;
        public readonly string name;
        public readonly string phone;

        public Client(Guid id,
            long cardId, string name,
            string phone)
        {
            if (id == Guid.Empty ||
                cardId <= 0 ||
                (name == null || name == "") ||
                (phone == null || phone == ""))
            {
                ThrowMessage message = new ThrowMessage();

                if (id == Guid.Empty) message.add("Id is not valid");
                if (cardId <= 0) message.add("Card id cannot be less than or equal to 0.");
                if (name == null || name == "") message.add("Name is required.");
                if (phone == null || phone == "") message.add("Phone is required.");

                throw new Exception(message.ToString());
            }

            this.id = id;
            this.cardId = cardId;
            this.name = name;
            this.phone = phone;
        }

        public Client setId(Guid id)
        {
            return new Client(id, cardId, name, phone);
        }
        public Client setCardId(long carId)
        {
            return new Client(id, cardId, name, phone);
        }
        public Client setName(string name)
        {
            return new Client(id, cardId, name, phone);
        }
        public Client setPhone(string phone)
        {
            return new Client(id, cardId, name, phone);
        }
        public Client setLoans(List<Loan.Domain.Loan> loans)
        {
            return new Client(id, cardId, name, phone);
        }

        public Dto toDto()
        {
            return new Dto()
            {
                id = id,
                cardId = cardId,
                name = name,
                phone = phone
            };
        }

        public JObject toJson()
        {
            return new JObject()
            {
                ["id"] = id,
                ["cardId"] = cardId,
                ["name"] = name,
                ["phone"] = phone
            };
        }
        public override string ToString()
        {
            return toJson().ToString();
        }

        public class Dto
        {
            public Guid id { get; set; }
            public long cardId { get; set; }
            public string name { get; set; }
            public string phone { get; set; }
        }
    }
}
