// Author: Orlys
// Github: https://github.com/Orlys

namespace Yuuna.Contracts.Patterns
{
    using Yuuna.Contracts.Semantics;

    public interface IPatternBuilder
    {
        IInvokeBuilder Build(IGroup group, params IGroup[] rest);
    }
}