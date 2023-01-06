using System;
using System.Globalization;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;


namespace AdaCredit
{
    public class DatabaseClient
    {
        private string ClientsFilePath;
        private string EmployeesFilePath;
        private string TransactionsDirPath;
        public string BankNumber;

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


        private List<Transaction>? pendingTransactions;
        public List<Transaction> PendingTransactions
        {
            get
            {
                if (this.pendingTransactions == null)
                    this.pendingTransactions = this.LoadPendingTransactions();
                return this.pendingTransactions;
            }

            set
            {
                this.pendingTransactions = value;
            }
        }


        private List<Transaction>? completedTransactions;
        public List<Transaction> CompletedTransactions
        {
            get
            {
                if (this.completedTransactions == null)
                    this.completedTransactions = this.LoadCompletedTransactions();
                return this.completedTransactions;
            }
        }

        private List<Transaction>? failedTransactions;
        public List<Transaction> FailedTransactions
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
                string transactionsDirPath, string bankNumber)
        {
            this.ClientsFilePath = clientsFilePath;
            this.EmployeesFilePath = employeesFilePath;
            this.TransactionsDirPath = transactionsDirPath;
            this.BankNumber = bankNumber;
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


        private static List<Transaction> LoadTransactionsFile(string path)
        {
            
            string filename = Path.GetFileName(path).Split(".")[0];
            string bankName = filename.Split("-")[0];
            string stringDate = filename.Split("-")[1];


            var date = new DateTime(
                    int.Parse(stringDate.Substring(0, 4)),
                    int.Parse(stringDate.Substring(4, 2)),
                    int.Parse(stringDate.Substring(6,2)));


            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };
            using var reader = new StreamReader(path);
            List<Transaction> transactions = reader.ReadToEnd().Split().Where(x=>x!="").Select(x => new Transaction(x, bankName, date)).ToList();
            return transactions;
        }


        private List<Transaction> LoadPendingTransactions()
        {
            var transactions = new List<Transaction>();

            var files = Directory.GetFiles(this.PendingTransactionsDirPath, "*.csv");

            foreach (string file in files)
            {
                transactions.AddRange(LoadTransactionsFile(file));
                File.Delete(file);
            }

            return transactions;
        }


        private List<Transaction> LoadCompletedTransactions()
        {
            var transactions = new List<Transaction>();

            var files = Directory.GetFiles(this.CompletedTransactionsDirPath, "*.csv");

            foreach (string file in files)
                transactions.AddRange(LoadTransactionsFile(file));

            return transactions;
        }


        private List<Transaction> LoadFailedTransactions()
        {
            var transactions = new List<Transaction>();

            var files = Directory.GetFiles(this.FailedTransactionsDirPath, "*.csv");

            foreach (string file in files)
                transactions.AddRange(LoadTransactionsFile(file));

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


        public void SaveTransactions()
        {
            this.SaveCompletedTransactions();
            this.SaveFailedTransactions();
        }

        
        public void SaveCompletedTransactions()
        {
            if (this.completedTransactions == null)
            {
                return;
            }

            var transactions = this.CompletedTransactions;
            var dates = transactions.Select(x => x.date).Distinct().ToList();
            var bankNames = transactions.Select(x => x.bankName).Distinct().ToList();

            foreach (DateTime date in dates)
            {
                foreach (string name in bankNames)
                {
                    var trans = transactions.Where(x => x.date == date).Where(
                            x => x.bankName == name).ToList();

                    string path = Path.Combine(this.CompletedTransactionsDirPath, $"{name}-{date.Year}{date.Month}{date.Day}.csv");

                    SaveTransactionList(path, trans);
                }
            }
        }


        public void SaveFailedTransactions()
        {
            if (this.failedTransactions == null)
                return;

            var transactions = this.FailedTransactions;
            var dates = transactions.Select(x => x.date).Distinct().ToList();
            var bankNames = transactions.Select(x => x.bankName).Distinct().ToList();

            foreach (DateTime date in dates)
            {
                foreach (string name in bankNames)
                {
                    var trans = transactions.Where(x => x.date == date).Where(
                            x => x.bankName == name).ToList();

                    string path = Path.Combine(this.FailedTransactionsDirPath, $"{name}-{date.Year}{date.Month}{date.Day}.csv");

                    SaveTransactionList(path, trans);
                }
            }

        }


        public static void SaveTransactionList(string path, List<Transaction> transactions)
        {

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };
            using var writer = new StreamWriter(path);
            using var csv = new CsvWriter(writer, config);
            List<string> stringTransactions = transactions.Select(x => x.Serialize()).ToList();
            csv.WriteRecords(stringTransactions);
        }


        public void Save()
        {
            this.SaveClients();
            this.SaveEmployees();
            this.SaveTransactions();
        }
    }
}
