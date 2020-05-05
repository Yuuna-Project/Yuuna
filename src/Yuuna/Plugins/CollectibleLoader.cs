// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

namespace Yuuna
{
    using System.Reflection;
    using System.Runtime.Loader;

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