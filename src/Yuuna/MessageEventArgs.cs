
namespace Yuuna.ControlFlow
{
    using System;

    public sealed class MessageEventArgs : EventArgs
    {
        internal MessageEventArgs(Message message)
        {
            this.Message = message;
        }
        public Message Message { get; }
    }


}