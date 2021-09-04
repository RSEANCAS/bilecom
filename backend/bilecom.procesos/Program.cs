using bilecom.procesos.manager;
using bilecom.ut;
using System.ServiceProcess;

namespace bilecom.procesos
{
    static class Program
    //class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        static void Main()
        {
            bool isDebugger = AppSettings.Get<bool>("debug");

            if (isDebugger)
            {
                SunatManager.ProcesarPadronSunat();
                //TareaTipoCambio.GuardarTipoCambio();
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                new ServicioTarea()
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
