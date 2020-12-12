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
    public class SedeBl : Conexion
    {
        SedeDa sedeDa = new SedeDa();

        public List<SedeBe> BuscarSede(int empresaId, string nombre, int pagina, int cantidadRegistros, string columnaOrden, string ordenMax, out int totalRegistros)
        {
            totalRegistros = 0;
            List<SedeBe> lista = null;
            try
            {
                cn.Open();
                lista = sedeDa.Buscar(empresaId, nombre, pagina, cantidadRegistros, columnaOrden, ordenMax, cn, out totalRegistros);
                cn.Close();
            }
            catch (Exception) { lista = null; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }

            return lista;
        }
    }
}
