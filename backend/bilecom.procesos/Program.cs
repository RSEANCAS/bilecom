using bilecom.ut;
using System.ServiceProcess;

namespace bilecom.procesos
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        static void Main()
        {
            bool isDebugger = AppSettings.Get<bool>("debug");

            if (isDebugger)
            {
                TareaTipoCambio.GuardarTipoCambio();
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
