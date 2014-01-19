using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WorkLogForm
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            login login = new login();
            if (login.ShowDialog() == DialogResult.OK)
            {
                main mainForm = new main();
                mainForm.User = login.User;
                Application.Run(mainForm);
            }
        }
    }
}
