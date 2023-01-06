using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AdaCredit
{
    public class AdaCredit
    {
        public static void Main()
        {
            string desktopPath = Environment.GetFolderPath( Environment.SpecialFolder.Desktop);
            var baseExecDir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            string DatabaseDirPath = baseExecDir.Parent.Parent.Parent.FullName;

            string bankNumber = "777";
            var databaseClient = new DatabaseClient(
                    Path.Combine(DatabaseDirPath, "clients.csv"),
                    Path.Combine(DatabaseDirPath, "employees.csv"),
                    Path.Combine(desktopPath, "Transactions"),
                    bankNumber);

            var agencyNumber = "0001";
            var app = new App(databaseClient, agencyNumber);

            app.MainLoop();
            databaseClient.Save();
        }
    }
}
