using System;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using DSCore.Helpers;
// using CubeCore.Utils;

namespace DSCore.Core
{
    public class DSInMemory : DSBase
    {
        private readonly MethodInfo _miGetData;
        public Type ReturnType => _miGetData?.ReturnType;
        public Type ElementType
        {
            get
            {
                var returnType = _miGetData?.ReturnType;
                if (returnType != null && ReturnType.GetElementType() != null)
                    return ReturnType.GetElementType();
                if (returnType != null && returnType.GenericTypeArguments.Length == 1)
                    return ReturnType.GenericTypeArguments[0];
                if (returnType != null && returnType.GetGenericArguments().Length > 0)
                    return null;


                var t = Helpers.Types.GetItemProperty(returnType);
                var a = GetData();
                var a2 = GetData().Cast<object>().ToArray();
                if (returnType.Name.Contains("ArraySegment"))
                {

                }
                Debug.Print($"No element type: {returnType}");

                return null;
                // return _miGetData?.ReturnType.GetElementType();
            }
        }

        public DSInMemory(string connectionString) : base(connectionString, null)
        {
            // var x1 = Encoding.GetEncodings();
            var methodName = GetDataSourceName();
            var n1 = Assemblies.GetAllAssemblies().SelectMany(a => a.GetTypes()).Where(t=>t.IsPublic).ToArray();
            var n2 = Assemblies.GetAllAssemblies().SelectMany(a => a.GetTypes()).Distinct().ToArray();
            var mm = Assemblies.GetAllAssemblies().SelectMany(a => a.GetTypes()).Where(t => t.IsPublic)
                .SelectMany(t => t.GetMethods(BindingFlags.Static | BindingFlags.Public)).Where(m =>
                    $"{m.ReflectedType.FullName}.{m.Name}" == methodName && m.GetParameters().Length == 0 &&
                    typeof(IEnumerable).IsAssignableFrom(m.ReturnType) &&
                    !typeof(string).IsAssignableFrom(m.ReturnType)).ToArray();

            if (mm.Length == 1)
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
            try
            {
                return (IEnumerable) _miGetData.Invoke(null, null);
            }
            catch
            {
                return null;
            }
        }

    }
}
