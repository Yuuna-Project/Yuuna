
namespace Yuuna.ControlFlow
{
    using System;

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
                input.OnSend += (sender, e) => Console.WriteLine(e.Message.Content);
                return;
            }
            throw new Exception();
        }
        
        private Action<ICallbackStatus> _onReceived;
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
    }


}