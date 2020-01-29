// Author: Orlys
// Github: https://github.com/Orlys

namespace Yuuna.Contracts.Modules
{
    using System;
    using System.Reflection;

    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ModuleMetadataAttribute : Attribute, IModuleMetadata
    {
        public ModuleMetadataAttribute(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }
        public string Author { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }


        internal static IModuleMetadata GetMetadata(Type type)
        {
            var meta = type.GetCustomAttribute<ModuleMetadataAttribute>();
            if (meta is null)
                meta = new ModuleMetadataAttribute(type.Name);
            else if (string.IsNullOrWhiteSpace(meta.Name))
                meta.Name = type.Name;
            return meta;
        }
    }
}