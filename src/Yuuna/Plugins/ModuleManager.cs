// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

namespace Yuuna
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    public sealed class ModuleManager
    {
        private const string PATH = "./modules";
        private static volatile ModuleManager s_inst;
        private static object s_lock = new object();
        private readonly List<ModuleCoupler> _modules;

        private readonly DirectoryInfo _modulesFolder;

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

        public IReadOnlyList<ModuleCoupler> Modules => this._modules;

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

        public void ReloadAll()
        {
            this.UnloadAll();
            this.Scan();
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

        private void Scan()
        {
            foreach (var moduleFolder in this._modulesFolder.EnumerateDirectories())
            {
                var n = new ModuleCoupler(moduleFolder.EnumerateFiles("*.deps.json"), moduleFolder.EnumerateFiles("*.dll"));
                this._modules.Add(n);
            }
        }
    }
}