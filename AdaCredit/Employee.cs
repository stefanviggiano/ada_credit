namespace AdaCredit
{
    public class Employee
    {
        public string Name;
        public string Login;
        public string Password;
        public DateTime LastLogin;
        public bool Active;

        public Employee(string name, string login, string password)
        {
            this.Name = name;
            this.Login = login;
            this.Password = password;
        }

        public bool CheckIdentity(string login, string password)
        {
            if (login == this.Login && password == this.Password)
                return true;
            return false;
        }
    }
}
