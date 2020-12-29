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
    public class SerieBl : Conexion
    {
        SerieDa serieDa = new SerieDa();

        public List<SerieBe> ListarSeriePorTipoComprobante(int tipoComprobanteId)
        {
            List<SerieBe> lista = new List<SerieBe>();
            try
            {
                cn.Open();
                lista = serieDa.ListarPorTipoComprobante(tipoComprobanteId, cn);
                cn.Close();
            }
            catch (Exception) { lista = null; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return lista;
        }
    }
}
