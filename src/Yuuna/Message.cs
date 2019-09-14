
namespace Yuuna.ControlFlow
{
    using System;

    public class Message
    {
        internal Message(Guid owner, DateTime timeStamp, string message)
        {
            this.Owner = owner;
            this.TimeStamp = timeStamp;
            this.Content = message;
        }

        public Guid Owner { get; }
        public DateTime TimeStamp { get; }
        public string Content { get; }
    }


}