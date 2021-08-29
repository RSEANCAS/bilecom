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
    public class TableroUltimosDocumentosBl : Conexion
    {
        public List<TableroUltimosDocumentosBe> Listar(int EmpresaId, int CantidadRegistros)
        {
            List<TableroUltimosDocumentosBe> respuesta = null;
            try
            {
                using (var cn = new SqlConnection(CadenaConexion))
                {
                    cn.Open();
                    respuesta = new TableroUltimosDocumentosDa().Listar(EmpresaId, CantidadRegistros, cn);
                    cn.Close();
                }
            }
            catch(Exception ex)
            {
                respuesta = null;
            }
            //finally { if (cn.State == System.Data.ConnectionState.Open) cn.Close(); }
            return respuesta;
        }
    }
}
