using System;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using DebuggerCheck;
[assembly:SuppressIldasmAttribute]

namespace eternity.us
{
    static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Login());
        }
    }
}
