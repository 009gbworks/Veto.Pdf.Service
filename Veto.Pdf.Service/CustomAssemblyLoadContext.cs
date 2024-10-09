using System.Reflection;
using System.Runtime.Loader;

namespace Veto.Pdf.Service
{
    public class CustomAssemblyLoadContext : AssemblyLoadContext
    {
        public IntPtr LoadDinkToPdfUnmanagedLibrary()
        {
            //var solutionDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
            return LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), "Libs/libwkhtmltox.dll"));
        }

        public IntPtr LoadUnmanagedLibrary(string absolutePath)
        {
            return LoadUnmanagedDll(absolutePath);
        }
        protected override IntPtr LoadUnmanagedDll(String unmanagedDllName)
        {
            return LoadUnmanagedDllFromPath(unmanagedDllName);
        }

        protected override Assembly Load(AssemblyName assemblyName)
        {
            throw new NotImplementedException();
        }
    }
}
