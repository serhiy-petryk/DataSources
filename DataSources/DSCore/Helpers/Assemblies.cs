using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DSCore.Helpers
{
    public static class Assemblies
    {
        private const string MicrosoftCompanyName = "Microsoft Corporation";

        private static Dictionary<string, Assembly> _assemblyLocations;
        //==============================================
        public static IEnumerable<Type> GetInheritedTypes(Type baseType) => GetAllAssemblies().SelectMany(a => a.GetTypes()).Where(t => !t.IsAbstract && t.IsSubclassOf(baseType));
        public static IEnumerable<Assembly> GetNotMicrosoftAssemblies() => GetAllAssemblies().Where(c => !Equals(c.CompanyName(), MicrosoftCompanyName));

        public static IEnumerable<Assembly> GetAllAssemblies()
        { // ~700 msecs in Toshiba 
            if (_assemblyLocations == null)
            {
                _assemblyLocations = new Dictionary<string, Assembly>();
                GetAllAssemblies(GetInitialAssembly()).ToList();
                var dllFileNames = Directory.GetFiles(Environment.CurrentDirectory, "*.dll").Where(fn => !_assemblyLocations.ContainsKey(fn)).ToList();
                dllFileNames.ForEach(fn =>
                {
                    try
                    {
                        var a = Assembly.LoadFrom(fn);
                        GetAllAssemblies(a).ToList();
                    }
                    catch (Exception ex) { }
                });
            }

            return _assemblyLocations.Values;
        }

        public static string CompanyName(this Assembly assembly)
        {
            var a = assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
            return a.Length > 0 ? ((AssemblyCompanyAttribute)a[0]).Company : null;
        }

        //based on: https://stackoverflow.com/questions/851248/c-sharp-reflection-get-all-active-assemblies-in-a-solution
        private static IEnumerable<Assembly> GetAllAssemblies(Assembly initialAssembly)
        {
            var list = new List<string>();
            var stack = new Stack<Assembly>();

            stack.Push(initialAssembly);

            do
            {
                var asm = stack.Pop();
                if (!_assemblyLocations.ContainsKey(asm.Location))
                    _assemblyLocations.Add(asm.Location, asm);

                yield return asm;

                foreach (var reference in asm.GetReferencedAssemblies())
                    if (!list.Contains(reference.FullName))
                    {
                        var a = Assembly.Load(reference);
                        if (!list.Contains(a.FullName))
                        {
                            stack.Push(a);
                            list.Add(a.FullName);
                        }
                    }

            }
            while (stack.Count > 0);
        }

        // see https://stackoverflow.com/questions/11014280/c-sharp-getting-parent-assembly-name-of-calling-assembly
        // this method hasn't issue in test environment
        private static Assembly GetInitialAssembly()
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            var callerAssemblies = new StackTrace().GetFrames().Select(x => x.GetMethod().ReflectedType.Assembly).Distinct()
              .Where(x => x.GetReferencedAssemblies().Any(y => y.FullName == currentAssembly.FullName));
            // return callerAssemblies.Last();
            try { return callerAssemblies.Last(); }
            catch { return currentAssembly; }
        }

    }
}
