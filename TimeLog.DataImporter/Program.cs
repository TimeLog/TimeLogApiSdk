using System;
using System.Windows.Forms;

namespace TimeLog.DataImporter
{
    static class Program
    {
        public static Login LoginForm;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            LoginForm ??= new Login();

            Application.Run(LoginForm);
        }
    }
}
