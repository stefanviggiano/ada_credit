using System.Globalization;
using CsvHelper;


namespace AdaCredit
{
    public class DatabaseClient
    {
        private string ClientsFilePath;
        private string EmployeesFilePath;

        private List<Client>? clients;
        public List<Client> Clients
        {
            get
            {
                if (this.clients == null)
                    this.clients = this.LoadClients();
                return this.clients;
            }
        }

        private List<Employee>? employees;
        public List<Employee> Employees
        {
            get
            {
                if (this.employees == null)
                    this.employees = this.LoadEmployees();
                return this.employees;
            }
        }

        public DatabaseClient(string clientsFilePath, string employeesFilePath)
        {
            this.ClientsFilePath = clientsFilePath;
            this.EmployeesFilePath = employeesFilePath;
        }

        private List<Client> LoadClients()
        {
            if (!File.Exists(this.ClientsFilePath))
                return new List<Client>();

            using var reader = new StreamReader(ClientsFilePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csv.GetRecords<Client>().ToList();
        }

        private List<Employee> LoadEmployees()
        {
            if (!File.Exists(this.EmployeesFilePath))
                return new List<Employee>();

            using var reader = new StreamReader(EmployeesFilePath);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csv.GetRecords<Employee>().ToList();
        }

        public void SaveClients()
        {
            if (this.clients != null)
            {
                using var writer = new StreamWriter(this.ClientsFilePath);
                using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
                csv.WriteRecords(this.Clients);
            }
        }

        public void SaveEmployees()
        {
            if (this.employees != null)
            {
                using var writer = new StreamWriter(this.EmployeesFilePath);
                using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
                csv.WriteRecords(this.Employees);
            }
        }

        public void Save()
        {
            this.SaveClients();
            this.SaveEmployees();
        }
    }
}
