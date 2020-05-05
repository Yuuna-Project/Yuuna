// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

namespace Yuuna.Common.Configuration
{
    using System;

    [AttributeUsage(AttributeTargets.Property)]
    public sealed class FieldAttribute : Attribute
    {
        internal readonly string Alias;

        public FieldAttribute() : this(null)
        {
        }

        public FieldAttribute(string alias)
        {
            this.Alias = alias;
        }
    }
}