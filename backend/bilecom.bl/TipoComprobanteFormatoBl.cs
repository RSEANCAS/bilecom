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
    public class TipoComprobanteFormatoBl : Conexion
    {
        TipoComprobanteFormatoDa tipoComprobanteFormatoDa = new TipoComprobanteFormatoDa();

        public List<TipoComprobanteFormatoBe> ListarTipoComprobanteFormatoPorTipoComprobante(int tipoComprobanteId)
        {
            List<TipoComprobanteFormatoBe> lista = null;
            try
            {
                cn.Open();
                lista = tipoComprobanteFormatoDa.ListarPorTipoComprobante(tipoComprobanteId, cn);
                cn.Close();
            }
            catch (Exception ex) { lista = null; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return lista;
        }
    }
}
