// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

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
        private readonly ImmutableArray<string>.Builder _namesBuilder;
        private IImmutableList<IGroup> _groups;

        public int Count
        {
            get
            {
                Debug.Assert(this._groups != null);
                return this._groups.Count;
            }
        }

        public IImmutableList<string> Groups { get; private set; }

        public Guid Owner { get; }

        internal Pattern(ModuleBase owner)
        {
            this._namesBuilder = ImmutableArray.CreateBuilder<string>();
            this._groupBuilder = ImmutableArray.CreateBuilder<IGroup>();
            this.Owner = owner.Id;
        }

        public bool Equals(IPattern other)
        {
            Debug.Assert(this.Groups != null);
            return this.Groups.SequenceEqual(other.Groups);
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
            this._namesBuilder.Add(g.Name);
            this._groupBuilder.Add(g);
        }

        internal void Immute()
        {
            this.Groups = this._namesBuilder.ToImmutable();
            this._groups = this._groupBuilder.ToImmutable();
        }
    }
}