using bilecom.ut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

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
