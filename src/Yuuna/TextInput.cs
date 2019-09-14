
namespace Yuuna.ControlFlow
{
    using System;

    public class TextInput : ITextInput, IBindable
    {
        public Guid Owner => this._token.Value;

        private Guid? _token;

        public event EventHandler<MessageEventArgs> OnSend;

        public void Send(string message)
        {
            if(this._token is null)
                throw new Exception("尚未將此物件綁定至交互服務");

            var now = DateTime.Now;
            var payload = new Message(this._token.Value, now, message);
            var e = new MessageEventArgs(payload);
            this.OnSend?.Invoke(this, e);
        }
        
        void IBindable.BindTo(ITokenizable tokenizable)
        {
            this._token = tokenizable.Token;
        }
    }


}