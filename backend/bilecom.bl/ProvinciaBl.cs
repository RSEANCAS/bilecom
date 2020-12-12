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
        ProvinciaDa provinciaDa = new ProvinciaDa();

        public List<ProvinciaBe> ListarProvincia()
        {
            List<ProvinciaBe> lista = null;
            try
            {
                cn.Open();
                lista = provinciaDa.Listar(cn);
                cn.Close();
            }
            catch (Exception ex){lista = null;}
            finally{if (cn.State == ConnectionState.Open) cn.Close();}
            return lista;
        }
    }
}
