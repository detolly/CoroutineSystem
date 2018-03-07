using System;
using System.Collections.Generic;
using System.Collections;
using System.Windows.Forms;
using System.Threading;

namespace CoroutineSystem
{
    static class Program
    {
        internal static Form1 mainForm;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            CoroutineManager.Init();
            mainForm            = new Form1();
            mainForm.Show();
            while (true)
            {
                Application.DoEvents();
                CoroutineManager.Interval();
                if (mainForm.hasClosed) break;
                Thread.Sleep(1);
            }
        }

    }
}
