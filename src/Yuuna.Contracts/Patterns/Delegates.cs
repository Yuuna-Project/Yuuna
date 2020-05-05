// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

namespace Yuuna.Contracts.Patterns
{
    using Yuuna.Contracts.Evaluation;
    using Yuuna.Contracts.Interaction;

    public delegate Response Incomplete(Match m);

    public delegate Response Invoke(Match m);
}