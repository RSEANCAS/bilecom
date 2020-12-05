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
    public class DistritoBl:Conexion
    {
        public List<DistritoBe> DistritoListar()
        {
            List<DistritoBe> respuesta = null;
            try
            {
                cn.Open();
                respuesta = new DistritoDa().DistritoListar(cn);
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
