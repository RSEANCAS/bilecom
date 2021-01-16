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
    public class FacturaBl : Conexion
    {
        FacturaDa facturaDa = new FacturaDa();
        FacturaDetalleDa facturaDetalleDa = new FacturaDetalleDa();

        public List<FacturaBe> BuscarFactura(int empresaId, string nroDocumentoIdentidadCliente, string razonSocialCliente, DateTime fechaHoraEmisionDesde, DateTime fechaHoraEmisionHasta, int pagina, int cantidadRegistros, string columnaOrden, string ordenMax, out int totalRegistros)
        {
            totalRegistros = 0;
            List<FacturaBe> lista = null;
            try
            {
                cn.Open();
                lista = facturaDa.Buscar(empresaId, nroDocumentoIdentidadCliente, razonSocialCliente, fechaHoraEmisionDesde, fechaHoraEmisionHasta, pagina, cantidadRegistros, columnaOrden, ordenMax, cn, out totalRegistros);
                cn.Close();
            }
            catch (Exception) { lista = null; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return lista;
        }
    }
}
