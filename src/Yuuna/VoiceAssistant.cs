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
            var interactive = new Kernel();
            var ti = new TextInput();
            interactive.Bind(ti);
            var fbs = new FeedbackSink();
            interactive.Bind(fbs);
            ti.Send("XXXX");


        } 
    }
}