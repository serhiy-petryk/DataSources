using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using DSCore.Core;
using DSCore.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests_Data.DataSources
{
    [TestClass]
    public class Tests
    {
        private const int Records = 10000;

        [TestMethod]
        public void DSInMemoryTest()
        {
            var a1 = DSCore.Helpers.Misc.MemoryUsedInBytes();
            Debug.Print($"A1: {a1:N0}");
            var x1 = new DSInMemory("System.Text.Encoding.GetEncodings");
            var x2 = x1.GetData();
            var x21 = x1.GetData().Cast<object>().Where(a=> true == false).ToList();
            var elementType1 = x2.GetType().GetElementType();
            // var elementType2 = ListBindingHelper.GetListItemType(x2);
            var a2 = DSCore.Helpers.Misc.MemoryUsedInBytes();
            Debug.Print($"A2: {a2:N0}");
        }

        [TestMethod]
        public void GetAllMethods()
        {
            var mm = Assemblies.GetAllAssemblies().SelectMany(a => a.GetTypes()).Where(t => t.IsPublic)
                .SelectMany(t => t.GetMethods(BindingFlags.Static | BindingFlags.Public)).Where(a =>
                    a.GetParameters().Length == 0 && typeof(IEnumerable).IsAssignableFrom(a.ReturnType) &&
                    !typeof(string).IsAssignableFrom(a.ReturnType) && a.ReturnType.IsPublic).ToArray();
            foreach (var m in mm)
            {
                var name = $"{m.ReflectedType.FullName}.{m.Name}";
                var x1 = new DSInMemory(name);
                if (x1.ElementType == null)
                {
//                    Debug.Print($"No element type: {x1.ReturnType}");
                }
                var x2 = x1.GetData();

            }
        }

    }
}
