using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AdaCredit
{
    public class App
    {
        private DatabaseClient DatabaseClient;
        private string AgencyNumber;

        public App(DatabaseClient databaseClient, string agencyNumber)
        {
            this.DatabaseClient = databaseClient;
            this.AgencyNumber = agencyNumber;
        }


        public void MainLoop()
        {
            string previous = "Login";
            string current = "Login";
            string next = default;

            while (true)
            {
                if (current == "Exit")
                    break;

                next = current switch
                {
                    "Login" => this.Login(),
                    "CreateEmployee" => this.CreateEmployee(),
                    "MainMenu" => this.MainMenu(),
                    "GoBack" => previous,
                    _ => "Exit"
                };

                if (next == "GoBack")
                    next = previous;

                previous = current;
                current = next;
            }
        }


        public string Login()
        {
            Console.WriteLine("LOGIN")
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            List<Employee> employees = this.DatabaseClient.Employees;

            if (employees.Count == 0 && username == "user"
                && password == "pass")
            {
                return "CreateEmployee";
            }

            foreach (Employee employee in employees)
            {
                if (employee.Login(username, password))
                    return "MainMenu";
            }

            Console.WriteLine("Incorrect credentials");
            return "Login";
        }


        public string CreateClient()
        {
            Console.WriteLine("CREATE CLIENT");

            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Phone number: ");
            string phoneNumber = Console.ReadLine();

            string accountNumber;
            while (true)
            {
                accountNumber = Utils.RandomString(6, "0123456789");
                if (this.DatabaseClient.Clients.FirstOrDefault(
                        x => x.AccountNumber == accountNumber) == null)
                    break;
            }

            var client = new Client(name, phoneNumber, accountNumber,
                    this.AgencyNumber);
            this.DatabaseClient.Clients.Add(client);
            return "GoBack";
        }


        public string CreateEmployee()
        {
            Console.WriteLine("CREATE EMPLOYEE");

            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Username: ");
            string username = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            var employee = new Employee(name, username, password);
            this.DatabaseClient.Employees.Add(employee);
            Console.WriteLine("Employee created");

            return "GoBack";
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
