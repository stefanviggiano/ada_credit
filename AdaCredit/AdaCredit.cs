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
}
