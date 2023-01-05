namespace AdaCredit
{
    public class Client
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string AccountNumber { get; set; }
        public string AgencyNumber { get; set; }
        public bool Active { get; set; }

        public Client(string Name, string PhoneNumber, string AccountNumber,
                string AgencyNumber)
        {
            this.Name = Name;
            this.PhoneNumber = PhoneNumber;
            this.AccountNumber = AccountNumber;
            this.AgencyNumber = AgencyNumber;
        }

        public Client(string Name, string PhoneNumber, string AccountNumber,
                string AgencyNumber, bool Active)
        {
            this.Name = Name;
            this.PhoneNumber = PhoneNumber;
            this.AccountNumber = AccountNumber;
            this.AgencyNumber = AgencyNumber;
            this.Active = Active;
        }

        public override string ToString()
        {
            return $"Client(Name={this.Name}, PhoneNumber={this.PhoneNumber}, AccountNumber={this.AccountNumber}, AgencyNumber={this.AgencyNumber})";
        }
    }
}
