using bilecom.ut;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace bilecom.procesos
{
    public partial class ServicioTarea : ServiceBase
    {
        Timer timer = new Timer();

        public ServicioTarea()
        {
            InitializeComponent();
            double intervalo = AppSettings.Get<double>("");

            timer = new Timer();
            timer.Interval = intervalo;
            timer.Elapsed += Timer_Elapsed;

        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void OnStart(string[] args)
        {
            timer.Start();
        }

        protected override void OnStop()
        {
            timer.Stop();
        }
    }
}
