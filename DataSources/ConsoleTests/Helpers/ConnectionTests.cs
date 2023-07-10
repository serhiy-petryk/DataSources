using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;

namespace ConsoleTests.Helpers
{
    public class ConnectionTests
    {
        public const string SqlConnectionString = @"Data Source=localhost;Initial Catalog=dbOneSAP_DW;Integrated Security=True;Encrypt=false";
        public const string SQLiteConnectionString = @"Data Source=E:\Apps\archive\Northwind\Northwind.sl3";
       public const string AccdbConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=E:\Apps\archive\Northwind\nwind.accdb;Persist Security Info=False";

       public static void Dummy()
       {
              Console.WriteLine($"Memory in Dummy: {Helpers.Misc.MemoryUsedInBytes() / 1024}");
       }
       public static void LoadLibraries()
       {

           Console.WriteLine($"Memory before open SqlServer: {Helpers.Misc.MemoryUsedInBytes() / 1024}");
           using (var conn = new SqlConnection(SqlConnectionString)) { }
           Console.WriteLine($"Memory before open SQLiteClient: {Helpers.Misc.MemoryUsedInBytes() / 1024}");
           using (var conn = new SqliteConnection(SQLiteConnectionString)) { }
           Console.WriteLine($"Memory before open AccDbClient: {Helpers.Misc.MemoryUsedInBytes() / 1024}");
           using (var conn = new OleDbConnection(AccdbConnectionString))

               Console.WriteLine($"Memory after open SqlServer: {Helpers.Misc.MemoryUsedInBytes() / 1024}");
       }
    }
}
