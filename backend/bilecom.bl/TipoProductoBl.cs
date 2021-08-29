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
    public class TipoProductoBl : Conexion
    {
        TipoProductoDa tipoProductoDa = new TipoProductoDa();

        public List<TipoProductoBe> ListarTipoProducto()
        {
            List<TipoProductoBe> respuesta = null;
            try
            {
                using (var cn = new SqlConnection(CadenaConexion))
                {
                    cn.Open();
                    respuesta = tipoProductoDa.Listar(cn);
                    cn.Close();
                }
            }
            catch (Exception ex)
            {
                respuesta = null;
            }
            //finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return respuesta;
        }

        public List<TipoProductoBe> ListarTipoProductoPorEmpresa(int empresaId)
        {
            List<TipoProductoBe> respuesta = null;
            try
            {
                using (var cn = new SqlConnection(CadenaConexion))
                {
                    cn.Open();
                    respuesta = tipoProductoDa.ListarPorEmpresa(empresaId, cn);
                    cn.Close();
                }
            }
            catch (Exception ex)
            {
                respuesta = null;
            }
            //finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return respuesta;
        }
    }
}
