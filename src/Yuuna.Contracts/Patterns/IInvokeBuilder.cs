// Author: Yuuna-Project@Orlys
// Github: github.com/Orlys
// Contact: orlys@yuuna-project.com

namespace Yuuna.Contracts.Patterns
{
    public interface IInvokeBuilder
    {
        void OnInvoke(Invoke invoke);

        //IIncompleteBuilder OnInvoke(Invoke invoke);
    }
}