using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookieRunKingdom
{
    public class Util
    {
        public static List<Process> GetProcessList(string processName)
        {
            Process[] processList = Process.GetProcesses();
            List<Process> list = new List<Process>();

            foreach (Process process in processList)
            {
                if (process.ProcessName.Equals(processName))
                {
                    list.Add(process);
                }
            }

            return list;
        }

        public static Rectangle GetWindowRect(IntPtr handle)
        {
            Rectangle rect;
            Dll.GetWindowRect(handle, out rect);
            rect = new Rectangle(rect.X, rect.Y, (rect.Width - rect.X), (rect.Height - rect.Y));
            return rect;
        }
    }
}
