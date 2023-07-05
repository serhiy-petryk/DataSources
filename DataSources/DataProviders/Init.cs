using System.Collections.Generic;
using System.Collections.Specialized;
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

        public static NameValueCollection TestNameValueCollection()
        {
            var aa1 = new NameValueCollection() {{"AA", "BBB"}};
            foreach (var a1 in aa1)
            {

            }
            return aa1;
        }

        public static List<T> TestGenericList<T>()
        {
            return new List<T>();
        }

        public static List<string> TestStringList()
        {
            return new List<string>() {"AAA"};
        }

        public static List<object> TestObjectList()
        {
            return new List<object>() {"AAA"};
        }
    }
}
