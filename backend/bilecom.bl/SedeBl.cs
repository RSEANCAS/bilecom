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
        public List<SedeBe> Listar(int empresaId)
        {
            List<SedeBe> lSede = new List<SedeBe>();
            using (cn)
            {
                try
                {
                    cn.Open();
                    lSede = new SedeDa().fListar(cn, empresaId);
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
