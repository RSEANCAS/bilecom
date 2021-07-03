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
    public class TipoNotaBl : Conexion
    {
        TipoNotaDa tipoNotaDa = new TipoNotaDa();

        public List<TipoNotaBe> ListarPorTipoComprobante(int tipoComprobanteId)
        {
            List<TipoNotaBe> lista = new List<TipoNotaBe>();
            try
            {
                cn.Open();
                lista = tipoNotaDa.ListarPorTipoComprobante(tipoComprobanteId, cn);
                cn.Close();
            }
            catch (SqlException ex) { lista = null; }
            catch (Exception ex) { lista = null; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return lista;
        }
    }
}
