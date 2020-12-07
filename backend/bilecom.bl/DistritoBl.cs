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
        DistritoDa distritoDa = new DistritoDa();

        public List<DistritoBe> ListarDistrito()
        {
            List<DistritoBe> lista = null;
            try
            {
                cn.Open();
                lista = distritoDa.Listar(cn);
                cn.Close();
            }
            catch (Exception ex) { lista = null; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return lista;
        }
    }
}
