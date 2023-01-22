using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TokoSepatuApp.View;
using TokoSepatuApp.View.FormCustomers;
using TokoSepatuApp.View.FormProducts;

namespace TokoSepatuApp
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var formLogin = new FormLogin();

            if (formLogin.ShowDialog() == DialogResult.OK)
                Application.Run(new MainForm());
            else
                Application.Exit();
        }
    }
}
