using bilecom.be;
using bilecom.da;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.bl
{
    public class TableroConteo_x_DocumentoBl:Conexion
    {
        public TableroConteo_x_DocumentoBe Obtener(int EmpresaId, int Anyo, int Mes)
        {
            TableroConteo_x_DocumentoBe respuesta = null;
            try
            {
                cn.Open();
                respuesta = new TableroConteo_x_DocumentoDa().Obtener(EmpresaId,Anyo,Mes,cn);
                cn.Close();
            }
            catch(Exception ex)
            {
                respuesta = null;
            }
            finally
            {
                if (cn.State == System.Data.ConnectionState.Open) cn.Close();
            }
            return respuesta;
        }
    }
}
