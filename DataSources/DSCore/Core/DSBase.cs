using System;
using System.Collections;
using System.Linq;

namespace DSCore.Core
{
    public abstract class DSBase
    {
        protected readonly string ConnectionString;
        protected string CommandText;

        public DSBase(string connectionString, string commandText)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentNullException(nameof(connectionString));

            ConnectionString = connectionString;
            CommandText = commandText;
        }

        public abstract DataInfo GetDataInfo();

        public abstract IEnumerable GetData();

        protected string GetDataSourceName()
        {
            var builder = new System.Data.Common.DbConnectionStringBuilder();
            try
            {
                builder.ConnectionString = ConnectionString;
                var key = builder.Keys.Cast<string>().FirstOrDefault(x => new[] { "data source", "datasource", "ds" }.Contains(x));
                if (key != null)
                    return (string)builder[key];
            }
            catch { }

            return ConnectionString;
        }
    }
}
