using bilecom.be;
using bilecom.da;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.bl
{
    public class KardexBl : Conexion
    {
        KardexDa kardexDa = new KardexDa();

        public List<KardexNivel1Be> BuscarNivel1(int empresaId, int almacenId, int productoId, DateTime fechaInicio, DateTime fechaFinal, int pagina, int cantidadRegistros, string columnaOrden, string ordenMax, out int totalRegistros)
        {
            totalRegistros = 0;
            List<KardexNivel1Be> lista = null;
            try
            {
                using (var cn = new SqlConnection(CadenaConexion))
                {
                    cn.Open();
                    lista = kardexDa.BuscarNivel1(empresaId, almacenId, productoId,fechaInicio,fechaFinal, pagina, cantidadRegistros, columnaOrden, ordenMax, cn, out totalRegistros);
                    cn.Close();
                }
            }
            catch (Exception) { lista = null; }
            //finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return lista;
        }
        public List<KardexNivel2Be> BuscarNivel2(int empresaId, int almacenId, int productoId, DateTime fechaInicio, DateTime fechaFinal, int pagina, int cantidadRegistros, string columnaOrden, string ordenMax, out int totalRegistros)
        {
            totalRegistros = 0;
            List<KardexNivel2Be> lista = null;
            try
            {
                using (var cn = new SqlConnection(CadenaConexion))
                {
                    cn.Open();
                    lista = kardexDa.BuscarNivel2(empresaId, almacenId, productoId, fechaInicio, fechaFinal, pagina, cantidadRegistros, columnaOrden, ordenMax, cn, out totalRegistros);
                    cn.Close();
                }
            }
            catch (Exception) { lista = null; }
            //finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return lista;
        }
    }
}
