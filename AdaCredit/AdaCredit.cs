using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AdaCredit
{

    public class AdaCreditApp
    {
        private DatabaseClient client;

        public AdaCreditApp(DatabaseClient client)
        {
            this.client = client;
        }

        public static void Main()
        {
            var client = new DatabaseClient();
            var app = new AdaCreditApp(client);
            app.login();
        }

        public void login()
        {
            Console.WriteLine("Type your login and password");
            Console.Write("Login: ");
            string login = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            Console.WriteLine($"{login}, {password}");

            this.login();
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
