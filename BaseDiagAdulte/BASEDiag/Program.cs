using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace BASEDiagAdulte
{
    static class Program
    {

        public static FirstScreen MainForm;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm = new FirstScreen();
            Application.Run(MainForm);
        }
    }
}
