
namespace Yuuna.ControlFlow
{
    using System;

    public abstract class FeedbackSink : IFeedbackSink, IBindable
    {
        void IFeedbackSink.Received(object sender, StatusEventArgs e)
        {
            if (this._token is null)
                throw new Exception("尚未將此物件綁定至交互服務");
            this.OnReceived(this, e);
        }

        protected abstract void OnReceived(object sender, StatusEventArgs e);

        private Guid? _token;
        void IBindable.BindTo(ITokenizable tokenizable)
        {
            this._token = tokenizable.Token;
        }
    }


}