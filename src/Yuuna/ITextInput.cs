
namespace Yuuna.ControlFlow
{
    using System;

    public interface ITextInput
    {
        event EventHandler<MessageEventArgs> OnSend;
    }


}