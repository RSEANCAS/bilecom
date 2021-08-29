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
    public class StockAlmacenBl : Conexion
    {
        StockAlmacenDa stockAlmacenDa = new StockAlmacenDa();
        public List<StockAlmacenBe> Buscar(int empresaId, int almacenId, int filtro, int pagina, int cantidadRegistros, string columnaOrden, string ordenMax, out int totalRegistros)
        {
            totalRegistros = 0;
            List<StockAlmacenBe> lista = null;
            try
            {
                using (var cn = new SqlConnection(CadenaConexion))
                {
                    cn.Open();
                    lista = stockAlmacenDa.Buscar(empresaId, almacenId, filtro, pagina, cantidadRegistros, columnaOrden, ordenMax, cn, out totalRegistros);
                    cn.Close();
                }
            }
            catch (Exception) { lista = null; }
            //finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return lista;
        }
    }
}
