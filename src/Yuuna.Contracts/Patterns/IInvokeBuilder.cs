// Author: Orlys
// Github: https://github.com/Orlys
namespace Yuuna.Contracts.Patterns
{
    public interface IInvokeBuilder
    {
        void OnInvoke(Invoke invoke);
        //IIncompleteBuilder OnInvoke(Invoke invoke);
    }
}