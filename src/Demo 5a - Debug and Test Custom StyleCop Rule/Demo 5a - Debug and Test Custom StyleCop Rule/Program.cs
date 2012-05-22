using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Demo_5a___Debug_and_Test_Custom_StyleCop_Rule
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
