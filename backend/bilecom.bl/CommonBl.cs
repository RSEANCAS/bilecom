using bilecom.be.Custom;
using bilecom.da;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.bl
{
    public class CommonBl : Conexion
    {
        CommonDa commonDa = new CommonDa();

        public List<ComprobanteCustom> BuscarComprobanteVenta(int empresaId, int ambienteSunatId, int tipoComprobanteId, int serieId, string nroComprobante/*, string clienteRazonSocial*/)
        {
            List<ComprobanteCustom> lista = null;
            try
            {
                cn.Open();
                lista = commonDa.BuscarComprobanteVenta(empresaId, ambienteSunatId, tipoComprobanteId, serieId, nroComprobante/*, clienteRazonSocial*/, cn);
                cn.Close();
            }
            catch (Exception ex) { lista = null; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return lista;
        }
    }
}
