namespace Yuuna
{
    using Config.Net;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using YamlDotNet;
    using YamlDotNet.Serialization;
    using Yuuna.ControlFlow;

    internal partial class VoiceAssistant
    {
        private void Run()
        {
            var kernel = new Kernel();
            var ti = new TextInput();
            kernel.Bind(ti);
            var fbs = new FeedbackSink();
            kernel.Bind(fbs);
            ti.Send("XXXX");


        } 
    }
}