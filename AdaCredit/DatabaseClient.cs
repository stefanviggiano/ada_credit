using System;
using System.Globalization;
using System.IO;
using CsvHelper;


namespace AdaCredit
{
    public class DatabaseClient
    {
        private string ClientsFilePath;
        private string EmployeesFilePath;
        private string TransactionsDirPath;

        private string PendingTransactionsDirPath
        {
            get
            {
                return Path.Combine(this.TransactionsDirPath, "Pending");
            }
        }

        private string CompletedTransactionsDirPath
        {
            get
            {
                return Path.Combine(this.TransactionsDirPath, "Completed");
            }
        }

        private string FailedTransactionsDirPath
        {
            get
            {
                return Path.Combine(this.TransactionsDirPath, "Failed");
            }
        }


        private string pendingTransactions?;
        public string PendingTransactions
        {
            get
            {
                if (this.pendingTransactions == null)
                    this.pendingTransactions = this.LoadPendingTransactions();
                return this.pendingTransactions;
            }
        }


        private string completedTransactions?;
        public string CompletedTransactions
        {
            get
            {
                if (this.completedTransactions == null)
                    this.completedTransactions = this.LoadCompletedTransactions();
                return this.completedTransactions;
            }
        }

        private string failedTransactions?;
        public string FailedTransactions
        {
            get
            {
                if (this.failedTransactions == null)
                    this.failedTransactions = this.LoadFailedTransactions();
                return this.failedTransactions;
            }
        }
                

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

        public DatabaseClient(string clientsFilePath, string employeesFilePath,
                string transactionsDirPath)
        {
            this.ClientsFilePath = clientsFilePath;
            this.EmployeesFilePath = employeesFilePath;
            this.TransactionsDirPath = transactionsDirPath;
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

        private List<Transaction> LoadTransactions()
        {
        }

        private List<Transaction> LoadPendingTransactions()
        {

            var transactions = new List<Transaction>();

            var files = Directory.GetFiles(this.PendingTransactionsDirPath, "*.csv");

            foreach (string file in files)
            {
                using var reader new StreamReader(file);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                transactions.AddRange(csv.GetRecords<Transaction>());
            }

            return transactions;
        }


        private List<Transaction> LoadCompletedTransactions()
        {
            var transactions = new List<Transaction>();

            var files = Directory.GetFiles(this.CompleteTransactionsDirPath, "*.csv");

            foreach (string file in files)
            {
                using var reader new StreamReader(file);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                transactions.AddRange(csv.GetRecords<Transaction>());
            }

            return transactions;
        }


        private List<Transaction> LoadFailedTransactions()
        {
            var transactions = new List<Transaction>();

            var files = Directory.GetFiles(this.FailedTransactionsDirPath, "*.csv");

            foreach (string file in files)
            {
                using var reader new StreamReader(file);
                using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                transactions.AddRange(csv.GetRecords<Transaction>());
            }

            return transactions;
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
