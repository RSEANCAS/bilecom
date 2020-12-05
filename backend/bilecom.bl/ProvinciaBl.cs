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
    public class ProvinciaBl:Conexion
    {
        public List<ProvinciaBe> ProvinciaListar()
        {
            List<ProvinciaBe> respuesta = null;
            try
            {
                cn.Open();
                respuesta = new ProvinciaDa().ProvinciaListar(cn);
                cn.Close();
            }
            catch (Exception ex)
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
