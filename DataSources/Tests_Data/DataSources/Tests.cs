using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DSCore.Core;
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
    }
}
