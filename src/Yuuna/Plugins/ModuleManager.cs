// Author: Orlys
// Github: https://github.com/Orlys

namespace Yuuna
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Loader;
    using System.IO;
    using System.Linq.Expressions;
    using System.Reflection;
     
    public sealed class ModuleManager
    {
        private static volatile ModuleManager s_inst;
        private static object s_lock = new object();
        public static ModuleManager Instance
        {
            get
            {
                if (s_inst is null)
                {
                    lock (s_lock)
                    {
                        if (s_inst is null)
                        {
                            s_inst = new ModuleManager();
                        }
                    }
                }
                return s_inst;

            }
        }

        private const string PATH = "./modules";

        private readonly List<ModuleCoupler> _modules;
        private readonly DirectoryInfo _modulesFolder;

        private ModuleManager()
        {
            this._modules = new List<ModuleCoupler>();
            this._modulesFolder = new DirectoryInfo(PATH);
            if (!this._modulesFolder.Exists)
            {
                this._modulesFolder.Create();
            }
            else
            {
                this.Scan();
            }
        }

        public IReadOnlyList<ModuleCoupler> Modules => this._modules;

        private void Scan()
        {
            foreach (var moduleFolder in this._modulesFolder.EnumerateDirectories())
            {
                var n = new ModuleCoupler(moduleFolder.EnumerateFiles("*.deps.json"), moduleFolder.EnumerateFiles("*.dll"));
                this._modules.Add(n);
            }
        }

        public void UnloadAll()
        {
            foreach (var m in this._modules)
            {
                m.Dispose();
            }
            GC.Collect();
            GC.WaitForPendingFinalizers();
            this._modules.Clear();
        }

        public void ReloadAll()
        {
            this.UnloadAll();
            this.Scan();
        }
    }
}