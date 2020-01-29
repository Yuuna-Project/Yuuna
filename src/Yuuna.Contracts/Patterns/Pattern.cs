// Author: Orlys
// Github: https://github.com/Orlys

namespace Yuuna.Contracts.Patterns
{
    using System;
    using System.Collections.Immutable;
    using System.Diagnostics;
    using System.Linq;
    using Yuuna.Contracts.Modules;
    using Yuuna.Contracts.Semantics;

    public sealed class Pattern : IPattern
    {
        private readonly ImmutableArray<IGroup>.Builder _groupBuilder;
        private readonly ImmutableArray<string>.Builder _keyBuilder;
        private IImmutableList<IGroup> _groups;

        public int Count
        {
            get
            {
                Debug.Assert(this._groups != null);
                return this._groups.Count;
            }
        }

        public IImmutableList<string> SequentialKeys { get; private set; }

        public Guid Owner { get; }

        internal Pattern(ModuleBase owner)
        {
            this._keyBuilder = ImmutableArray.CreateBuilder<string>();
            this._groupBuilder = ImmutableArray.CreateBuilder<IGroup>();
            this.Owner = owner.Id;
        }

        public bool Equals(IPattern other)
        {
            Debug.Assert(this.SequentialKeys != null);
            return this.SequentialKeys.SequenceEqual(other.SequentialKeys);
        }

        public IImmutableList<IGroup> ToImmutable()
        {
            Debug.Assert(this._groups != null);
            return this._groups;
        }

        internal void Add(IGroup g)
        {
            if (g == null)
                return;
            this._keyBuilder.Add(g.Key);
            this._groupBuilder.Add(g);
        }

        internal void Immute()
        {
            this.SequentialKeys = this._keyBuilder.ToImmutable();
            this._groups = this._groupBuilder.ToImmutable();
        }

    }
}