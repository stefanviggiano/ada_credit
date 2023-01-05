namespace AdaCredit
{
    public class Client
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string AccountNumber { get; set; }
        public string AgencyNumber { get; set; }
        public bool Active { get; set; }
        public decimal Balance { get; set; }

        public Client(string Name, string PhoneNumber, string AccountNumber,
                string AgencyNumber)
        {
            this.Name = Name;
            this.PhoneNumber = PhoneNumber;
            this.AccountNumber = AccountNumber;
            this.AgencyNumber = AgencyNumber;
            this.Active = true;
            this.Balance = 0;
        }

        public Client(string Name, string PhoneNumber, string AccountNumber,
                string AgencyNumber, bool Active, decimal Balance);
        {
            this.Name = Name;
            this.PhoneNumber = PhoneNumber;
            this.AccountNumber = AccountNumber;
            this.AgencyNumber = AgencyNumber;
            this.Active = Active;
            this.Balance = Balance;
        }

        public override string ToString()
        {
            return $"Client(Name={this.Name}, PhoneNumber={this.PhoneNumber}, AccountNumber={this.AccountNumber}, AgencyNumber={this.AgencyNumber})";
        }
    }
}
