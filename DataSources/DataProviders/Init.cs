using System.Diagnostics;

namespace DataProviders
{
    public class Init
    {
        static Init()
        {
            Debug.Print("AAAAAAAAA");
            // SQLitePCL.raw.SetProvider(new SQLitePCL.SQLite3Provider_e_sqlite3());
        }
    }
}
