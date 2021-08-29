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
    public class TableroConteo_x_DocumentoBl : Conexion
    {
        public TableroConteo_x_DocumentoBe Obtener(int empresaId, int anio, int mes)
        {
            TableroConteo_x_DocumentoBe respuesta = null;
            try
            {
                using (var cn = new SqlConnection(CadenaConexion))
                {
                    cn.Open();
                    respuesta = new TableroConteo_x_DocumentoDa().Obtener(empresaId, anio, mes, cn);
                    cn.Close();
                }
            }
            catch (Exception ex)
            {
                respuesta = null;
            }
            //finally { if (cn.State == System.Data.ConnectionState.Open) cn.Close(); }
            return respuesta;
        }
    }
}
