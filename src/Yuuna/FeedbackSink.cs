
namespace Yuuna.ControlFlow
{
    using System;

    public class FeedbackSink : IFeedbackSink, IBindable
    {
        void IFeedbackSink.Received(ICallbackStatus status)
        {
            if (this._token is null)
                throw new Exception("尚未將此物件綁定至交互服務");
            this.OnReceived(status);
        }

        protected virtual void OnReceived(ICallbackStatus status)
        {
            Console.WriteLine(status.Message.Content);
        }

        private Guid? _token;
        void IBindable.BindTo(ITokenizable tokenizable)
        {
            this._token = tokenizable.Token;
        }
    }


}