using bilecom.be;
using bilecom.da;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.bl
{
    public class ClienteBl : Conexion
    {
        public List<Cliente> Listar(int empresaId)
        {
            List<Cliente> lCliente = new List<Cliente>();
            using (cn)
            {
                try
                {
                    cn.Open();
                    lCliente = new ClienteDa().fListar(cn, empresaId);
                    cn.Close();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return lCliente;
        }
    }
}
