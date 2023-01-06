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



    }
}
