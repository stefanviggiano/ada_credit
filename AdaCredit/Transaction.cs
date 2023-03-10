namespace AdaCredit
{
    public class Transaction
    {
        public string OriginBank { get; set; }
        public string OriginAgency { get; set; }
        public string OriginAccount { get; set; }

        public string DestinationBank { get; set; }
        public string DestinationAgency { get; set; }
        public string DestinationAccount { get; set; }

        public string Type { get; set; }
        public decimal Value { get; set; }

        public string bankName;
        public DateTime date;

        public Transaction(string OriginBank, string OriginAgency, 
                string OriginAccount, string DestinationBank,
                string DestinationAgency, string DestinationAccount,
                string Type, decimal Value, string bankName, DateTime date)
        {
            this.OriginBank = OriginBank;
            this.OriginAgency = OriginAgency;
            this.OriginAccount = OriginAccount;

            this.DestinationBank = DestinationBank;
            this.DestinationAccount = DestinationAccount;
            this.DestinationAgency = DestinationAgency;

            this.Type = Type;
            this.Value = Value;

            this.bankName = bankName;
            this.date = date;
        }

        public Transaction(string transactionString, string bankName, DateTime date)
        {
            var arguments = transactionString.Split(",").ToList();

            this.OriginBank = arguments[0];
            this.OriginAgency = arguments[1];
            this.OriginAccount = arguments[2];
            this.DestinationBank = arguments[3];
            this.DestinationAccount = arguments[4];
            this.DestinationAgency = arguments[5];
            this.Type = arguments[6];
            decimal.TryParse(arguments[7], out decimal value_);
            this.Value = value_;

            this.bankName = bankName;
            this.date = date;
        }

        public string Serialize()
        {
            return $"{this.OriginBank},{this.OriginAgency},{OriginAccount},{this.DestinationBank},{this.DestinationAgency},{DestinationAccount},{this.Type},{this.Value}";
        }


        public bool Process(DatabaseClient databaseClient)
        {
            if (this.Type == "TEF")
                return this.ProcessTEF(databaseClient);
            else if (this.Type == "DOC")
                return this.ProcessDOC(databaseClient);
            else if (this.Type == "TED")
                return this.ProcessTED(databaseClient);
            return false;
        }

        public bool ProcessTEF(DatabaseClient databaseClient)
        {
            if (this.OriginBank != this.DestinationBank)
                return false;

            bool debit = this.ProcessTEFDebit(databaseClient);
            bool credit = this.ProcessTEFCredit(databaseClient);

            return (debit && credit);
        }


        public bool ProcessTEFDebit(DatabaseClient databaseClient)
        {
            if (this.OriginBank != databaseClient.BankNumber)
                return true;

            Client client = databaseClient.Clients.FirstOrDefault(x =>
                    x.AccountNumber == this.OriginAccount);

            if (client == null)
                return false;

            if (client.Balance < this.Value)
                return false;

            client.Balance -= this.Value;
            return true;
        }


        public bool ProcessTEFCredit(DatabaseClient databaseClient)
        {
            if (this.DestinationAccount != databaseClient.BankNumber)
                return true;

            Client client = databaseClient.Clients.FirstOrDefault(x =>
                    x.AccountNumber == this.DestinationAccount);

            if (client == null)
                return false;

            client.Balance += this.Value;
            return true;
        }


        public bool ProcessDOC(DatabaseClient databaseClient)
        {
            bool debit = this.ProcessDOCDebit(databaseClient);
            bool credit = this.ProcessDOCCredit(databaseClient);

            return (debit && credit);
        }


        public bool ProcessDOCDebit(DatabaseClient databaseClient)
        {
            if (this.OriginBank != databaseClient.BankNumber)
                return true;

            Client client = databaseClient.Clients.FirstOrDefault(x =>
                    x.AccountNumber == this.OriginAccount);

            if (client == null)
                return false;

            decimal tax;
            if (this.date < new DateTime(2022, 11, 30))
            {
                tax = 0;
            }
            else
            {
                tax = this.Value * 0.01M;
                if (tax > 5)
                    tax = 5;
                tax += 1;
            }

            decimal value_ = this.Value + tax;
            if (client.Balance < value_)
                return false;

            client.Balance -= value_;
            return true;
        }


        public bool ProcessDOCCredit(DatabaseClient databaseClient)
        {
            if (this.DestinationAccount != databaseClient.BankNumber)
                return true;

            Client client = databaseClient.Clients.FirstOrDefault(x =>
                    x.AccountNumber == this.DestinationAccount);

            if (client == null)
                return false;

            client.Balance += this.Value;
            return true;
        }


        public bool ProcessTED(DatabaseClient databaseClient)
        {
            bool debit = this.ProcessTEDDebit(databaseClient);
            bool credit = this.ProcessTEDCredit(databaseClient);

            return (debit && credit);
        }


        public bool ProcessTEDDebit(DatabaseClient databaseClient)
        {
            if (this.OriginBank != databaseClient.BankNumber)
                return true;

            Client client = databaseClient.Clients.FirstOrDefault(x =>
                    x.AccountNumber == this.OriginAccount);

            if (client == null)
                return false;

            decimal tax;
            if (this.date < new DateTime(2022, 11, 30))
                tax = 0;
            else
                tax = 5;

            decimal value_ = this.Value + tax;
            if (client.Balance < value_)
                return false;

            client.Balance -= value_;
            return true;
        }


        public bool ProcessTEDCredit(DatabaseClient databaseClient)
        {
            if (this.DestinationAccount != databaseClient.BankNumber)
                return true;

            Client client = databaseClient.Clients.FirstOrDefault(x =>
                    x.AccountNumber == this.DestinationAccount);

            if (client == null)
                return false;

            client.Balance += this.Value;
            return true;
        }

    }
}
