// Author: Orlys
// Github: https://github.com/Orlys

namespace Yuuna.Contracts.Patterns
{
    using Yuuna.Contracts.Interaction;
    using Yuuna.Contracts.Optimization;

    public delegate Response Incomplete(Match m);

    public delegate Response Invoke(Match m);
}