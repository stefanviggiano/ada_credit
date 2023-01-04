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
            var baseDir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            string DatabaseDirPath = baseDir.Parent.Parent.Parent.FullName;

            var databaseClient = new DatabaseClient(
                    Path.Combine(DatabaseDirPath, "clients.csv"),
                    Path.Combine(DatabaseDirPath, "employees.csv"));

            var agencyNumber = "0001";
            var app = new App(databaseClient, agencyNumber);

            app.MainLoop();
            databaseClient.Save();
        }
    }
}
