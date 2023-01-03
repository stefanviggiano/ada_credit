using System;
using System.IO;
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
            var baseDir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            string DatabaseDirPath = baseDir.Parent.Parent.Parent.FullName;

            var DatabaseClient = new DatabaseClient(
                    Path.Combine(DatabaseDirPath, "clients.csv"),
                    Path.Combine(DatabaseDirPath, "employees.csv"));

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
            this.DatabaseClient.SaveEmployees();
            Console.WriteLine("Employee created");

            return "Login";
        }

        public string MainMenu()
        {
            Console.WriteLine("MAIN MENU");
            Console.WriteLine("Choose a sub menu:");
            Console.WriteLine("1 - Clients");
            Console.WriteLine("2 - Employees");
            Console.WriteLine("3 - Transactions");
            Console.WriteLine("4 - Reports");
            Console.WriteLine("5 - Exit");
            string option = Console.ReadLine();
            string nextWindow = option switch
            {
                "1" => "ClientsMenu",
                "2" => "EmployeesMenu",
                "3" => "TransactionsMenu",
                "4" => "ReportsMenu",
                "5" => "Exit",
                _ => "MainMenu"
            };
            return nextWindow;
        }
    }
}
