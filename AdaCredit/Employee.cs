namespace AdaCredit
{
    public class Employee
    {
        public string Name;
        public string Login;
        public string PasswordHash;
        public string PasswordSalt;
        public DateTime LastLogin;
        public bool Active;

        public bool CheckIdentity(string login, string password)
        {
            if (login == this.Login && password == this.Password)
                return true;
            return false;
        }
    }
}
