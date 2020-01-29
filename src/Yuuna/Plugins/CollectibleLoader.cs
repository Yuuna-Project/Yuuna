// Author: Orlys
// Github: https://github.com/Orlys

namespace Yuuna
{
    using System.Runtime.Loader;
    using System.Reflection;

    public sealed class CollectibleLoader : AssemblyLoadContext
    {
        private AssemblyDependencyResolver _resolver;

        // https://docs.microsoft.com/zh-tw/dotnet/standard/assembly/unloadability

        internal CollectibleLoader(string mainAssemblyToLoadPath) : base(true)
        { 
            this._resolver = new AssemblyDependencyResolver(mainAssemblyToLoadPath);
        }

        protected override Assembly Load(AssemblyName name)
        {
            string assemblyPath = this._resolver.ResolveAssemblyToPath(name);
            if (assemblyPath != null)
            {
                return this.LoadFromAssemblyPath(assemblyPath);
            }

            return null;
        }
    }
}