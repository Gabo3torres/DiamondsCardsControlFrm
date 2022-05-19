using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiamondsCardsControlFrm
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
            //DiamondsClient client = new DiamondsClient()
            //{
            //    Nombre = "Prueba Nombre",
            //    Descripcion = "decripcion",
            //    Coordinador = "Prueba Coordinador",
            //    NumeroSala = 1,
            //    FechaIngreso = DateTime.Now,
            //    DocumentoHabilitante = "147852369",
            //    Vuelo = "123"
            //};
            //ClientDiamondsRepository insertedProof = new ClientDiamondsRepository();
            //insertedProof.Insert(client);
            LogginFrm main = new LogginFrm();
            main.FormClosed += MainForm_Closed; // agrega esto aquí
            main.Show();
            Application.Run();
        }

        private static void MainForm_Closed(object sender, FormClosedEventArgs e)
        {
            ((Form)sender).FormClosed -= MainForm_Closed;

            if (Application.OpenForms.Count == 0)
            {
                Application.ExitThread();
            }
            else
            {
                Application.OpenForms[0].FormClosed += MainForm_Closed;
            }
        }
    }


}
