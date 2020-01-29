
namespace Yuuna.Common.Configuration
{
    using System;

    [AttributeUsage(AttributeTargets.Property)]
    public sealed class FieldAttribute : Attribute
    {
        public FieldAttribute() : this(null)
        {
        }

        public FieldAttribute(string alias)
        {
            this.Alias = alias;
        }

        public string Alias { get; }
    }
}
