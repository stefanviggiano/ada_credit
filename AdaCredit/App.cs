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
                    "MainMenu" => this.MainMenu(),
                    "ClientsMenu" => this.ClientsMenu(),
                    "EmployeesMenu" => this.EmployeesMenu(),
                    "ReportsMenu" => this.ReportsMenu(),
                    "CreateClient" => this.CreateClient(),
                    "ConsultClient" => this.ConsultClient(),
                    "EditClient" => this.EditClient(),
                    "DeactivateClient" => this.DeactivateClient(),
                    "CreateEmployee" => this.CreateEmployee(),
                    "EditEmployeesPassword" => this.EditEmployeesPassword(),
                    "DeactivateEmployee" => this.DeactivateEmployee(),
                    "ReportActiveClients" => this.ReportActiveClients(),
                    "ReportInactiveClients" => this.ReportInactiveClients(),
                    "ReportActiveEmployees" => this.ReportActiveEmployees(),
                    "ReportFailedTransactions" => this.ReportFailedTransactions(),
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
            Console.WriteLine("LOGIN");
            Console.Write("Username: ");
            string username = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            List<Employee> employees = this.DatabaseClient.Employees.Where(
                    employee => employee.Active).ToList();
            foreach (var e in employees)
                Console.WriteLine($"{e.Name}, {e.Active}");

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


        public string MainMenu()
        {
            Console.WriteLine("MAIN MENU");
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


        public string ClientsMenu()
        {
            Console.WriteLine("CLIENTS MENU");
            Console.WriteLine("1 - Create client");
            Console.WriteLine("2 - Consult client");
            Console.WriteLine("3 - Edit client");
            Console.WriteLine("4 - Deactivate client");
            Console.WriteLine("5 - Main menu");
            string option = Console.ReadLine();
            string newWindow = option switch
            {
                "1" => "CreateClient",
                "2" => "ConsultClient",
                "3" => "EditClient",
                "4" => "DeactivateClient",
                "5" => "MainMenu",
                _ => "ClientsMenu"
            };
            return newWindow;
        }


        public string EmployeesMenu()
        {
            Console.WriteLine("EMPLOYEES MENU");
            Console.WriteLine("1 - Create employee");
            Console.WriteLine("2 - Edit employee's password");
            Console.WriteLine("3 - Deactivate employee");
            Console.WriteLine("4 - Main menu");
            string option = Console.ReadLine();
            string newWindow = option switch
            {
                "1" => "CreateEmployee",
                "2" => "EditEmployeesPassword",
                "3" => "DeactivateEmployee",
                "4" => "MainMenu",
                _ => "ClientsMenu"
            };
            return newWindow;
        }


        public string ReportsMenu()
        {
            Console.WriteLine("REPORTS MENU");
            Console.WriteLine("1 - Active clients");
            Console.WriteLine("2 - Inactive clients");
            Console.WriteLine("3 - Active employees");
            Console.WriteLine("4 - Failed transactions");
            Console.WriteLine("5 - Main menu");
            string option = Console.ReadLine();
            string newWindow = option switch
            {
                "1" => "ReportActiveClients",
                "2" => "ReportInactiveClients",
                "3" => "ReportActiveEmployees",
                "4" => "ReportFailedTransactions",
                "5" => "MainMenu",
                _ => "ReportsMenu"
            };
            return newWindow;
        }


        public string CreateClient()
        {
            Console.WriteLine("CREATE CLIENT");

            Console.Write("Name: ");
            string name = Console.ReadLine();

            Console.Write("Phone number: ");
            string phoneNumber = Console.ReadLine();

            string accountNumber = Utils.GenerateNewAccountNumber(this.DatabaseClient);

            var client = new Client(name, phoneNumber, accountNumber,
                    this.AgencyNumber);

            if (this.DatabaseClient.Clients.FirstOrDefault(x => x.Name == name) != null)
                Console.WriteLine("Client with the specified name already exists");
            else
                this.DatabaseClient.Clients.Add(client);
            return "GoBack";
        }


        public string ConsultClient()
        {
            Console.WriteLine("CONSULT CLIENT");
            Console.Write("Client's name: ");
            string name = Console.ReadLine();

            Client client = this.DatabaseClient.Clients.FirstOrDefault(x => x.Name == name);
            if (client == null)
                Console.WriteLine($"No client found with name {name}");
            else
                Console.WriteLine(client);
            return "GoBack";
        }


        public string EditClient()
        {
            Console.WriteLine("EDIT CLIENT");
            Console.Write("Client's name: ");
            string name = Console.ReadLine();

            Client client = this.DatabaseClient.Clients.FirstOrDefault(x => x.Name == name);
            if (client == null)
            {
                Console.WriteLine($"No client found with name {name}");
            }
            else
            {
                Console.Write(client);
                Console.Write("New name: ");
                string newName = Console.ReadLine();

                Console.Write("New phone number: ");
                string newPhoneNumber = Console.ReadLine();

                if (this.DatabaseClient.Clients.FirstOrDefault(x => x.Name == newName) != null)
                    Console.WriteLine("Client with the specified name already exists");
                else
                {
                    client.Name = newName;
                    client.PhoneNumber = newPhoneNumber;
                    Console.WriteLine("Client edited");
                }
            }
            return "GoBack";
        }


        public string DeactivateClient()
        {
            Console.WriteLine("DEACTIVATE CLIENT");
            Console.Write("Client's name: ");
            string name = Console.ReadLine();

            Client client = this.DatabaseClient.Clients.FirstOrDefault(x => x.Name == name);
            if (client == null)
            {
                Console.WriteLine($"No client found with name {name}");
            }
            else
            {
                client.Active = false;
                Console.Write("Client deactivated");
            }
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
            if (this.DatabaseClient.Employees.FirstOrDefault(
                        x => x.Username == username) != null)
            {
                Console.WriteLine($"There is already an employee with the username {username}");
            }
            else
            {
                this.DatabaseClient.Employees.Add(employee);
                Console.WriteLine("Employee created");
            }
            return "GoBack";
        }


        public string EditEmployeesPassword()
        {
            Console.WriteLine("EDIT EMPLOYEES'S PASSWORD");
            Console.Write("Employee's username: ");
            string username = Console.ReadLine();

            Employee employee = this.DatabaseClient.Employees.FirstOrDefault(
                    x => x.Username == username);
            if (employee == null)
            {
                Console.WriteLine("No employee with username {username}");
            }
            else
            {
                Console.Write("New password: ");
                string password = Console.ReadLine();
                employee.Password = password;
            }
            return "GoBack";
        }


        public string DeactivateEmployee()
        {
            Console.WriteLine("DEACTIVATE EMPLOYEE");
            Console.Write("Employee's name: ");
            string name = Console.ReadLine();

            Employee employee = this.DatabaseClient.Employees.Where(
                    x => x.Active).FirstOrDefault(
                    x => x.Name == name);
            if (employee == null)
            {
                Console.WriteLine("No active employee with name {name}");
            }
            else
            {
                employee.Active = false;
            }
            return "GoBack";
        }


        public string ReportActiveClients()
        {
            var clients = this.DatabaseClient.Clients.Where(
                    client => client.Active);
            foreach (Client client in clients)
                Console.WriteLine(client);
            return "GoBack";
        }


        public string ReportInactiveClients()
        {
            var clients = this.DatabaseClient.Clients.Where(
                    client => !client.Active);
            foreach (Client client in clients)
                Console.WriteLine(client);
            return "GoBack";
        }


        public string ReportActiveEmployees()
        {
            var employees = this.DatabaseClient.Employees.Where(
                    employee => employee.Active);
            foreach (Employee employee in employees)
                Console.WriteLine(employee);
            return "GoBack";
        }


        public string ReportFailedTransactions()
        {
            return "GoBack";
        }
    }
}
