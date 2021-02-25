using bilecom.be;
using bilecom.bl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.procesos
{
    public class TareaTipoCambio
    {
        public static void GuardarTipoCambio()
        {
            TipoCambioBl tipoCambioBl = new TipoCambioBl();

            TipoCambioBe tipoCambio = tipoCambioBl.ObtenerDolarTipoCambio();
            _ = tipoCambioBl.GuardarTipoCambio(tipoCambio);
        }
    }
}
