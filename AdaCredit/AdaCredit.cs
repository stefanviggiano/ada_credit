using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AdaCredit
{
    public class AdaCreditApp
    {
        private DatabaseClient database_client;

        public AdaCreditApp(DatabaseClient database_client)
        {
            this.database_client = database_client;
        }

        public static void Main()
        {
            var database_client = new DatabaseClient();
            var app = new AdaCreditApp(database_client);
            app.Start();
        }

        public void Start()
        {
            this.Login();
        }

        public void Login()
        {
            Console.WriteLine("Type your login and password");
            Console.Write("Login: ");
            string login = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            List<Employee> employees = this.database_client.Employees;

            if (employees.Length == 0 && login == "user"
                && password == "login")
            {
                this.CreatePassword();
                return;
            }

            foreach (Employee employee in employees)
            {
                if (employee.CheckIdentity(login, password))
                {
                    this.MainScreen();
                    return;
                }
            }

            Console.WriteLine("Incorrect login and password");
            this.Login();
            }
        }

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


    public class Client
    {
        public string Name;
        public int AccountNumber;
        public int AgencyNumber;

    }


    public class DatabaseClient
    {
        private List<Client> clients;
        public List<Client> Clients
        {
            get
            {
                if (this.clients == null)
                    this.clients = this.GetClientsFromCSV();
                return this.clients;
            }
        }

        private List<Employee> employees;
        public List<Employee> Employees
        {
            get
            {
                if (this.employees == null)
                    this.employees = this.GetEmployessFromCSV();
                return this.employees;
            }
        }

        private List<Client> GetClientsFromCSV()
        {
            return default;
        }

        private List<Employee> GetEmployessFromCSV()
        {
            return default;
        }
    }
}
