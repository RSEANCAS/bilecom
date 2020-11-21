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
    public class SedeBl : Conexion
    {
        public List<SedeBe> Listar(int empresaId, string nombre)
        {
            List<SedeBe> lSede = new List<SedeBe>();
            using (cn)
            {
                try
                {
                    cn.Open();
                    lSede = new SedeDa().fListar(cn, empresaId, nombre);
                    cn.Close();
                }
                catch (Exception)
                {

                    throw;
                }
            }

            return lSede;
        }
    }
}
