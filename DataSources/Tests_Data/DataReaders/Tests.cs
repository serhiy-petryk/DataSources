using System.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests_Data.DataReaders
{
    [TestClass]
    public class Tests
    {
        private const int Records = 10000;

        [TestMethod]
        public void SqlServerReaderTest()
        {

            using (var conn = new SqlConnection(Settings.SqlConnectionString))
            using (var cmd = new SqlCommand($"SELECT TOP {Records} * from gldocline", conn))
            {
                conn.Open();
                var rdr = cmd.ExecuteReader();
                var sw = new Stopwatch();
                sw.Start();
                while (rdr.Read())
                {
                    var aa = new object[100];
                    rdr.GetValues(aa);
                }
                sw.Stop();
                Debug.Print($"Sql read select: {sw.ElapsedMilliseconds:N0} msecs");
            }
        }

        [TestMethod]
        public void SQLiteReaderTest()
        {

            using (var conn = new SqliteConnection(Settings.SQLiteConnectionString))
            using (var cmd = new SqliteCommand($"SELECT * from Customers", conn))
            {
                conn.Open();
                var rdr = cmd.ExecuteReader();
                var sw = new Stopwatch();
                sw.Start();
                while (rdr.Read())
                {
                    var aa = new object[100];
                    rdr.GetValues(aa);
                }
                sw.Stop();
                Debug.Print($"Sqlite read select: {sw.ElapsedMilliseconds:N0} msecs");
            }
        }
    }
}
