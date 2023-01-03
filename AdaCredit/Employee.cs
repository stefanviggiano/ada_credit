namespace AdaCredit
{
    public class Employee
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get ; set; }
        public DateTime LastLogin { get; set; }
        public bool Active { get; set; }

        public Employee(string Name, string Login, string Password)
        {
            this.Name = Name;
            this.Login = Login;
            this.Password = Password;
            this.Active = true;
        }

        public bool Login(string login, string password)
        {
            if (login == this.Login && password == this.Password)
            {
                this.LastLogin = DateTime.Now;
                return true;
            }
            return false;
        }
    }
}
