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
        public List<ClienteBe> Listar(int empresaId , string nroDocumentoIdentidad, string razonSocial)
        {
            List<ClienteBe> lCliente = new List<ClienteBe>();
            using (cn)
            {
                try
                {
                    cn.Open();
                    lCliente = new ClienteDa().fListar(cn, empresaId, nroDocumentoIdentidad, razonSocial);
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
