using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AdaCredit
{
    public class AdaCreditApp
    {
        private DatabaseClient DatabaseClient;

        public AdaCreditApp(DatabaseClient DatabaseClient)
        {
            this.DatabaseClient = DatabaseClient;
        }

        public static void Main()
        {
            var DatabaseClient = new DatabaseClient();
            var app = new AdaCreditApp(DatabaseClient);
            app.MainLoop();
        }

        public void MainLoop()
        {
            string next = this.Login();
            while (true)
            {
                if (next == "Exit")
                    break;

                next = next switch
                {
                    "Login" => this.Login(),
                    "CreateEmployee" => this.CreateEmployee(),
                    "MainMenu" => this.MainMenu(),
                    _ => next = "Exit"
                };
            }
        }

        public string Login()
        {
            Console.WriteLine("Type your login and password");
            Console.Write("Login: ");
            string login = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            List<Employee> employees = this.DatabaseClient.Employees;

            Console.WriteLine(login, password);
            if (employees.Count == 0 && login == "user"
                && password == "pass")
            {
                return "CreateEmployee";
            }

            foreach (Employee employee in employees)
            {
                if (employee.CheckIdentity(login, password))
                    return "MainMenu";
            }

            Console.WriteLine("Incorrect login and password");
            return "Login";
        }

        public string CreateEmployee()
        {
            Console.WriteLine("CREATE EMPLOYEE");

            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Login: ");
            string login = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            var employee = new Employee(name, login, password);
            this.DatabaseClient.Employees.Add(employee);
            Console.WriteLine("Employee created");

            return "Login";
        }

        public string MainMenu()
        {
            Console.WriteLine("MAIN MENU");
            Console.WriteLine("Choose one of the options:");
            Console.WriteLine("1 - Clients");
            Console.WriteLine("2 - Employees");
            Console.WriteLine("3 - Transactions");
            Console.WriteLine("4 - Reports");
            string option = Console.ReadLine();
            string next_window = option switch
            {
                "1" => "ClientsMenu",
                "2" => "EmployeesMenu",
                "3" => "TransactionsMenu",
                "4" => "ReportsMenu",
                _ => "MainMenu"
            };
            return next_window;
        }
    }
}
