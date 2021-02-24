using bilecom.be;
using bilecom.da;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.bl
{
    public class TipoCambioBl : Conexion
    {
        public TipoCambioBe Obtener(string fecha)
        {
            TipoCambioBe oTipoCambio = new TipoCambioBe();
            try
            {
                oTipoCambio = new TipoCambioDa().Obtener(fecha);
            }
            catch (Exception ex)
            {
                oTipoCambio = null;
            }
            return oTipoCambio;
        }
    }
}
