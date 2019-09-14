namespace Yuuna
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    internal partial class VoiceAssistant //: ApplicationContext
    {
        [Conditional("UNSUPPORTED_AUTO_UPDATE")]
        private static void Update()
        {
        }

        static VoiceAssistant()
        { 
            Update(); 
        }

        private static void Main(string[] args)
        {
            var yuuna = new VoiceAssistant();
            yuuna.Run();
        }
    }

}
