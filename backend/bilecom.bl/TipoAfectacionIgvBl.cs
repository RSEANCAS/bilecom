using bilecom.be;
using bilecom.da;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.bl
{
    public class TipoAfectacionIgvBl:Conexion
    {
        public List<TipoAfectacionIgvBe> Listar()
        {
            List<TipoAfectacionIgvBe> respuesta = null;
            try
            {
                cn.Open();
                respuesta = new TipoAfectacionIgvDa().Listar(cn);
                cn.Close();
            }
            catch(Exception ex)
            {
                respuesta = null;
            }
            finally
            {
                if (cn.State == ConnectionState.Open) cn.Close();
            }
            return respuesta;
        }
    }
}
