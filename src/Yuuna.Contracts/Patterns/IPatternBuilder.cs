// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

namespace Yuuna.Contracts.Patterns
{
    using Yuuna.Contracts.Semantics;

    public interface IPatternBuilder
    {
        IInvokeBuilder Build(IGroup group, params IGroup[] rest);
    }
}