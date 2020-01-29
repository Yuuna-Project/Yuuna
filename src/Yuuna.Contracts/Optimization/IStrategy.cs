// Author: Orlys
// Github: https://github.com/Orlys


namespace Yuuna.Contracts.Optimization
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using Yuuna.Contracts.Modules;
    using Yuuna.Contracts.Patterns;

    public interface IStrategy
    {
   
        Alternative FindBest(IEnumerable<IPatternSet> patternSetCollection, IImmutableList<string> feed);
        //Alternative FindBest(IEnumerable<ModuleBase> plugins, IImmutableList<string> feed);
    }
}