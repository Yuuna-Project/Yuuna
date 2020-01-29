// Author: Orlys
// Github: https://github.com/Orlys

namespace Yuuna.Contracts.Patterns
{
    using Yuuna.Common.Linq;

    public interface IPatternSet : IImmutable<IPattern>
    {
        bool TryGet(IPattern pattern, out Bag bag);
    }
}