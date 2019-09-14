
namespace Yuuna.ControlFlow
{
    using System;

    public class FeedbackSink : IFeedbackSink, IBindable
    {
        public Guid Owner => this._token.Value;
        void IFeedbackSink.Received(object sender, StatusEventArgs e)
        {
            if (this._token is null)
                throw new Exception("尚未將此物件綁定至交互服務");
            this.OnReceived(this, e);
        }

        protected virtual void OnReceived(object sender, StatusEventArgs e)
        {
            Console.WriteLine(e.Status.Message.Content);
        }

        private Guid? _token;
        void IBindable.BindTo(ITokenizable tokenizable)
        {
            this._token = tokenizable.Token;
        }
    }


}