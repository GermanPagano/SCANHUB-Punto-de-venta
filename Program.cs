using SCANHUB___INVENTARIO_Y_CAJA.Database;
using System;
using System.Windows.Forms;

namespace SCANHUB___INVENTARIO_Y_CAJA
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            DatabaseConfig.InitializeDatabase();  // Asegúrate de que esta llamada es correcta
            Application.Run(new LoginForm());
        }
    }
}
