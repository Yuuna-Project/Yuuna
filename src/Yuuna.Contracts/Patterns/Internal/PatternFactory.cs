// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

namespace Yuuna.Contracts.Patterns
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Collections.Immutable;

    using Yuuna.Common.Artisan.Cascade;
    using Yuuna.Contracts.Modules;
    using Yuuna.Contracts.Semantics;

    internal sealed class PatternFactory : Initial<InvokeBuilder, IPattern>, IRule, IPatternBuilder
    {
        private readonly ConcurrentDictionary<IPattern, Bag> _list;
        private readonly ModuleBase _owner;

        internal PatternFactory(ModuleBase owner)
        {
            this._list = new ConcurrentDictionary<IPattern, Bag>();
            this._owner = owner;
        }

        public IInvokeBuilder Build(IGroup group, params IGroup[] rest)
        {
            if (group is null)
                throw new ArgumentNullException(nameof(group));

            var g = new Pattern(this._owner);
            g.Add(group);
            if (rest != null)
                foreach (var item in rest)
                    g.Add(item);
            g.Immute();

            return this.MoveNext(g);
        }

        public IImmutableList<IPattern> ToImmutable() => this._list.Keys.ToImmutableArray();

        public bool TryGet(IPattern pattern, out Bag invoke)
        {
            return this._list.TryGetValue(pattern, out invoke);
        }

        protected override void OnCompleted(Queue<object> session)
        {
            var ge = new Bag
            {
                Pattern = session.Dequeue() as IPattern,
                Invoke = session.Dequeue() as Invoke,
                //Incomplete = session.Dequeue() as Incomplete,
            };

            this._list.TryAdd(ge.Pattern, ge);
        }
    }
};