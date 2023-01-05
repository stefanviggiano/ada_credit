namespace AdaCredit
{
    public class Transaction
    {
        [Index(0)]
        public string OriginBank { get; set; }
        [Index(1)]
        public string OriginAgency { get; set; }
        [Index(2)]
        public string OriginAccount { get; set; }

        [Index(3)]
        public string DestinationBank { get; set; }
        [Index(4)]
        public string DestinationAgency { get; set; }
        [Index(5)]
        public string DestinationAccount { get; set; }

        [Index(6)]
        public string Type { get; set; }
        [Index(7)]
        public decimal Value { get; set; }

        public Transaction(string OriginBank, string OriginAgency, 
                string OriginAccount, string DestinationBank,
                string DestinationAgency, string DestinationAccount,
                string Type, decimal Value)
        {
            this.OriginBank = OriginBank;
            this.OriginAgency = OriginAgency;
            this.OriginAccount = OriginAccount;

            this.DestinationBank = DestinationBank;
            this.DestinationAccount = DestinationAccount;
            this.DestinationAgency = DestinationAgency;

            this.Type = Type;
            this.Value = Value;
        }
    }
