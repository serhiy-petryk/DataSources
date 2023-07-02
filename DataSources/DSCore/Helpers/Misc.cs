using System;

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
