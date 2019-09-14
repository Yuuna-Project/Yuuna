
namespace Yuuna.ControlFlow
{
    using System;


    public sealed class StatusEventArgs : EventArgs
    {
        public StatusEventArgs(ICallbackStatus status)
        {
            this.Status = status;
        }

        public ICallbackStatus Status { get; }
    }

    public class Kernel : ITokenizable
    {
        public Guid Token { get; }

        public Kernel() : this(Guid.NewGuid())
        {

        }

        public Kernel(Guid token)
        {
            this.Token = token;
        }

        public void Bind(ITextInput input)
        {
            if (input is IBindable b)
            {
                b.BindTo(this);
                input.OnSend += this.Process;
                return;
            }
            throw new Exception();
        }
        
        private EventHandler<StatusEventArgs> _onReceived;
        public void Bind(IFeedbackSink sink)
        {
            if(sink is IBindable b)
            {
                b.BindTo(this);
                this._onReceived += sink.Received;
                return;
            }
            throw new Exception();
        }

        

        private void Process(object sender, MessageEventArgs e)
        {
            var result = CallbackStatusFactory.Success(e.Message, MoodKinds.Mad);

            this._onReceived.Invoke(this, new StatusEventArgs(result));
        }
    }


}