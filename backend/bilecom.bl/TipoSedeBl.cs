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
    public class TipoSedeBl : Conexion
    {
        TipoSedeDa tipoSedeDa = new TipoSedeDa();

        public List<TipoSedeBe> BuscarTipoSede(int empresaId, string nombre, int pagina, int cantidadRegistros, string columnaOrden, string ordenMax, out int totalRegistros)
        {
            totalRegistros = 0;
            List<TipoSedeBe> lista = new List<TipoSedeBe>();
            try
            {
                cn.Open();
                lista = tipoSedeDa.Buscar(empresaId, nombre, pagina, cantidadRegistros, columnaOrden, ordenMax, cn, out totalRegistros);
                cn.Close();
            }
            catch (Exception ex) { lista = null; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return lista;
        }
    }
}
