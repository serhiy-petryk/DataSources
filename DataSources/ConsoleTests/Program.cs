using System;
using System.Diagnostics;

namespace ConsoleTests
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine($"Memory at start: {Helpers.Misc.MemoryUsedInBytes()/1024}");

            Helpers.ConnectionTests.Dummy();
            // Helpers.ConnectionTests.LoadLibraries();

            Console.WriteLine("Hello World!");
            Console.WriteLine($"Memory at end: {Helpers.Misc.MemoryUsedInBytes() / 1024}");
            Console.WriteLine($"Memory at end2: {Helpers.Misc.MemoryUsedInBytes() / 1024}");
        }
    }
}
