// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

namespace Yuuna.Contracts.Modules
{
    using System;
    using System.Reflection;

    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ModuleMetadataAttribute : Attribute, IModuleMetadata
    {
        public string Author { get; set; }

        public string Description { get; set; }

        public string Name { get; private set; }

        public string Url { get; set; }

        public ModuleMetadataAttribute(string name)
        {
            this.Name = name;
        }

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