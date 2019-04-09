using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DebuggerCheck
{
    class DebugCheck
    {
        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern bool CheckRemoteDebuggerPresent(IntPtr hProcess, ref bool isDebuggerPresent);

        public static void Debug()
        {
            bool isDebuggerPresent = false;
            CheckRemoteDebuggerPresent(Process.GetCurrentProcess().Handle, ref isDebuggerPresent);

            Console.WriteLine("Debugger Detected: " + isDebuggerPresent);
            Console.ReadLine();

            if (isDebuggerPresent == true)
            {
                // dou picantovich
            }
        }
    }
}
