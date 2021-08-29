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
    public class TipoMovimientoBl:Conexion
    {
        TipoMovimientoDa tipoMovimientoDa = new TipoMovimientoDa();
        public List<TipoMovimientoBe> Listar()
        {
            List<TipoMovimientoBe> resultado = null;
            try
            {
                using (var cn = new SqlConnection(CadenaConexion))
                {
                    cn.Open();
                    resultado = tipoMovimientoDa.Listar(cn);
                    cn.Close();
                }
            }
            catch(Exception ex)
            {
                resultado = null;
            }
            //finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return resultado;
        }
    }
}
