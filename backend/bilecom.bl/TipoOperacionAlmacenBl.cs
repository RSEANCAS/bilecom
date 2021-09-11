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
    public class TipoOperacionAlmacenBl : Conexion
    {
        public List<TipoOperacionAlmacenBe> Listar()
        {
            List<TipoOperacionAlmacenBe> respuesta = null;
            try
            {
                using (var cn = new SqlConnection(CadenaConexion))
                {
                    cn.Open();
                    respuesta = new TipoOperacionAlmacenDa().Listar(cn);
                    cn.Close();
                }
            }
            catch (Exception ex) { respuesta = null; }
            //finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return respuesta;
        }
    }
}
