namespace AdaCredit
{
    public class Employee
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get ; set; }
        public DateTime LastLogin { get; set; }
        public bool Active { get; set; }

        public Employee(string Name, string Username, string Password)
        {
            this.Name = Name;
            this.Username = Username;
            this.Password = Password;
            this.Active = true;
        }

        public Employee(string Name, string Username, string Password,
                DateTime LastLogin, bool Active)
        {
            this.Name = Name;
            this.Username = Username;
            this.Password = Password;
            this.LastLogin = LastLogin;
            this.Active = Active;
        }

        public bool Login(string username, string password)
        {
            if (username == this.Username && password == this.Password)
            {
                this.LastLogin = DateTime.Now;
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"Employee(Name={this.Name}, Username={this.Username}, Password={this.Password}, LastLogin={this.LastLogin}, Active={this.Active})";
        }
    }
}
