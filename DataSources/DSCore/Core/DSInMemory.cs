using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using DSCore.Helpers;
// using CubeCore.Utils;

namespace DSCore.Core
{
    public class DSInMemory : DSBase
    {
        private readonly MethodInfo _miGetData;
        public Type ElementType => _miGetData?.ReturnType.GetElementType();

        public DSInMemory(string connectionString) : base(connectionString, null)
        {
            // var x1 = Encoding.GetEncodings();
            var methodName = GetDataSourceName();
            var mm = Assemblies.GetAllAssemblies().SelectMany(a => a.GetTypes())
                .SelectMany(t => t.GetMethods(BindingFlags.Static | BindingFlags.Public))
                .Where(m => $"{m.ReflectedType.FullName}.{m.Name}" == methodName).ToList();

            if (mm.Count == 1)
                _miGetData = mm[0];
            else
                throw new Exception("Invalid method reference in connection string: " + connectionString);
        }

        public override DataInfo GetDataInfo()
        {
            // var x2 = GetData(commandText);
            // var elementType1 = x2.GetType().GetElementType();
            // var elementType2 = ListBindingHelper.GetListItemType(x2);

            /*var columns = typeof(EncodingInfo).GetProperties()
              .Select(p => new ColumnInfo {Name = p.Name, Type = p.PropertyType});

            var dataInfo = new DataInfo
            {
              Columns = columns.ToList()
            };
            return dataInfo;*/
            return null;
        }

        public override IEnumerable GetData()
        {
            return (IEnumerable)_miGetData.Invoke(null, null);
        }

    }
}
