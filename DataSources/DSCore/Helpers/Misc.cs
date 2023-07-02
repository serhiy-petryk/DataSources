using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSCore.Helpers
{
    public static class Misc
    {
        public static long MemoryUsedInBytes()
        {
            // clear memory
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
            //
            return GC.GetTotalMemory(true);
        }
    }
}
