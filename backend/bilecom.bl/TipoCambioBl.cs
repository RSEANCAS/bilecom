using bilecom.be;
using bilecom.da;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.bl
{
    public class TipoCambioBl : Conexion
    {
        TipoCambioDa tipoCambioDa = new TipoCambioDa();
        public TipoCambioBe ObtenerDolarTipoCambio()
        {
            TipoCambioBe oTipoCambio = new TipoCambioBe();
            try
            {
                oTipoCambio = new TipoCambioDa().ObtenerDolar();
            }
            catch (Exception ex)
            {
                oTipoCambio = null;
            }
            return oTipoCambio;
        }

        public bool GuardarTipoCambio(TipoCambioBe tipoCambioBe)
        {
            bool seGuardo = false;
            try
            {
                cn.Open();
                seGuardo = tipoCambioDa.Guardar(tipoCambioBe, cn);
                cn.Close();
            }
            catch (Exception ex)
            {
                seGuardo = false;
            }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return seGuardo;
        }
    }
}
