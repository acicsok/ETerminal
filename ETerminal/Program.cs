using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ETerminal
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                if (args.Length == 1 && args[0] == "/gocollect")
                {
                    var controller = new TerminalController();
                    controller.StartCollectingLogs();
                }
                else if (args.Length == 1 && args[0] == "/gocollectall")
                {
                    var controller = new TerminalController();
                    controller.StartCollectingLogs(true);
                }
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
        }
    }
}
