using bilecom.be;
using bilecom.da;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.bl
{
    public class TipoOperacionVentaBl : Conexion
    {
        TipoOperacionVentaDa tipoOperacionVentaDa = new TipoOperacionVentaDa();

        public List<TipoOperacionVentaBe> ListarTipoOperacionVentaPorEmpresaTipoComprobante(int empresaId, int tipoComprobanteId)
        {
            List<TipoOperacionVentaBe> lista = null;

            try
            {
                using (var cn = new SqlConnection(CadenaConexion))
                {
                    cn.Open();
                    lista = tipoOperacionVentaDa.ListarPorEmpresaTipoComprobante(empresaId, tipoComprobanteId, cn);
                    cn.Close();
                }
            }
            catch (Exception ex) { lista = null; }
            //finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return lista;
        }
    }
}
