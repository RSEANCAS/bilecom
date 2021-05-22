using bilecom.be;
using bilecom.da;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.bl
{
    public class FormatoCorreoBl : Conexion
    {
        FormatoCorreoDa formatoCorreoDa = new FormatoCorreoDa();
        public FormatoCorreoBe ObtenerFormatoCorreo(int tipoFormatoCorreoId)
        {
            FormatoCorreoBe formato = null;
            try
            {
                cn.Open();
                formato = formatoCorreoDa.Obtener( tipoFormatoCorreoId, cn);
            }
            catch (Exception)
            {

                throw;
            }
            finally { if (cn.State == System.Data.ConnectionState.Open) cn.Close(); }
            return formato;
        }

    }
}
