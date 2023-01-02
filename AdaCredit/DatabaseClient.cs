namespace AdaCredit
{
    public class DatabaseClient
    {
        private List<Client>? clients;
        public List<Client> Clients
        {
            get
            {
                if (this.clients == null)
                    this.clients = this.GetClientsFromCSV();
                return this.clients;
            }
        }

        private List<Employee>? employees;
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
