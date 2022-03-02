using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CookieRunKingdom
{
    public partial class MainWindow
    {
        private void AddProcess()
        {
            cbProcessList.Items.Clear();
            List<Process> processesList = Util.GetProcessList("Nox");
            if (processesList.Count == 0)
            {
                MessageBox.Show("실행중인 프로세서가 없습니다.");
                Close();
                return;
            }

            for (int i = 0; i < processesList.Count; i++)
            {
                Process process = processesList[i];
                cbProcessList.Items.Add(process.MainWindowTitle);
            }

            cbProcessList.SelectedIndex = 0;
        }
    }
}
