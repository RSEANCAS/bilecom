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
    public class TipoComprobanteTipoOperacionVentaBl : Conexion
    {
        TipoComprobanteTipoOperacionVentaDa tipoComprobanteTipoOperacionVentaDa = new TipoComprobanteTipoOperacionVentaDa();
        TipoComprobanteDa tipoComprobanteDa = new TipoComprobanteDa();
        TipoOperacionVentaDa tipoOperacionVentaDa = new TipoOperacionVentaDa();

        public List<TipoComprobanteTipoOperacionVentaBe> Listar(bool withTipoComprobante = false, bool withTipoOperacionVenta = false)
        {
            List<TipoComprobanteTipoOperacionVentaBe> lista = null;

            try
            {
                cn.Open();
                lista = tipoComprobanteTipoOperacionVentaDa.Listar(cn);
                if(lista != null && (withTipoComprobante || withTipoOperacionVenta))
                {
                    foreach (var item in lista)
                    {
                        if (withTipoComprobante) item.TipoComprobante = tipoComprobanteDa.Obtener(item.TipoComprobanteId, cn);
                        if (withTipoOperacionVenta) item.TipoOperacionVenta = tipoOperacionVentaDa.Obtener(item.TipoOperacionVentaId, cn);
                    }
                }
                cn.Close();
            }
            catch (Exception ex) { lista = null; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return lista;
        }
    }
}
