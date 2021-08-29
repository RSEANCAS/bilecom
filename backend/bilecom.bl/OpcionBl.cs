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
    public class OpcionBl : Conexion
    {
        OpcionDa opcionDa = new OpcionDa();

        public List<OpcionBe> ListarOpcionPorPerfil(int empresaId, int perfilId)
        {
            List<OpcionBe> lista = null;

            try
            {
                using (var cn = new SqlConnection(CadenaConexion))
                {
                    cn.Open();
                    lista = opcionDa.ListarPorPerfil(perfilId, empresaId, cn);
                    cn.Close();
                }
            }
            catch (Exception ex) { throw ex; }
            //finally { if (cn.State == ConnectionState.Open) cn.Close(); }

            return lista;
        }
    }
}
