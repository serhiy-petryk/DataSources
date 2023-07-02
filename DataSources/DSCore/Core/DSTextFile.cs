using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace DSCore.Core
{
    public class DSTextFile : DSBase
    {
        private readonly string _fileName;
        // private string _codePage;

        public DSTextFile(string connectionString) : base(connectionString, null)
        {
            _fileName = GetDataSourceName();
        }

        public override DataInfo GetDataInfo()
        {
            var dataInfo = new DataInfo
            {
                Columns = new List<ColumnInfo>
                {
                    new ColumnInfo {Name = "LineNo", Type = typeof(int)},
                    new ColumnInfo {Name = "LineContent", Type = typeof(string)}
                }
            };
            return dataInfo;
        }

        public override IEnumerable GetData()
        {
            using (var rdr = new StreamReader(_fileName))
            {
                var lineNo = 0;
                string context;
                while ((context = rdr.ReadLine()) != null)
                    yield return new Element { LineNo = lineNo++, Context = context };
            }
        }

        public class Element
        {
            public int LineNo;
            public string Context;
        }
    }
}
