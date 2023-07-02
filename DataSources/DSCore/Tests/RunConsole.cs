using System;
using System.Data;
using System.Data.Common;
using System.Linq;

namespace DSCore.Tests
{
    public class RunConsole
    {
        public static void ExeConsoleApp()
        {
            var m = Helpers.Misc.MemoryUsedInBytes();
            var x1 = Helpers.Assemblies.GetAllAssemblies().Distinct().ToList();
            var dbType = Helpers.Assemblies.GetInheritedTypes(typeof(DbConnection)).ToList();
            var t1 = x1.SelectMany(a => a.GetTypes()).Where(t => !t.IsAbstract && t.GetInterfaces().Contains(typeof(IDbConnection)))
              .ToList();
            var m1 = Helpers.Misc.MemoryUsedInBytes();
            var ole = dbType.Where(t => t.Name.ToLower().Contains("oledb")).ToList()[0];
            // var sqlCe = dbType.Where(t => t.Name.ToLower().Contains("sqlce")).ToList()[0];
            var sqLite = dbType.Where(t => t.Name.ToLower().Contains("sqlite")).ToList()[0];
            using (var conn = (DbConnection)Activator.CreateInstance(sqLite))
            {
                // x86 conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\Apps\archive\Northwind\nwind.mdb";
                // bad x86 conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=D:\Apps\archive\Northwind\nwind.accdb;Persist Security Info=False";
                // x86 conn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Apps\archive\Northwind\nwind.mdb;Persist Security Info=False";
                // x86 conn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Apps\archive\Northwind\nwind.accdb;Persist Security Info=False";
                // x86/x64 "Persist Security Info = False; Data Source = 'D:\Apps\archive\Northwind\Northwind.001.sdf';File Mode = 'shared read'; Max Database Size = 256; Max Buffer Size = 1024";
                // x86/x64 "Persist Security Info = False; Data Source=D:\Apps\archive\Northwind\Northwind.sl3;";
                conn.ConnectionString = @"Data Source=E:\Apps\archive\Northwind\Northwind.sl3;";
                // conn.ConnectionString = @"Persist Security Info = False; Data Source=E:\Apps\archive\Northwind\Northwind.sl3;";
                conn.Open();
                // var x = conn.GetSchema();
                // x.Dispose();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * from Customers";
                    using (var rdr = cmd.ExecuteReader())
                    {
                        var oo = new object[11];
                        while (rdr.Read())
                        {
                            var xx = rdr.GetValues(oo);
                        }
                    }
                }
            }
            Console.WriteLine($"m1: {m1 / 1024}. End!");
            Console.ReadLine();
        }
    }

}
