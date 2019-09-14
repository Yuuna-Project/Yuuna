
namespace Yuuna.ControlFlow
{
    public interface IFeedbackSink
    {
        void Received(object sender, StatusEventArgs e);
    }


}