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
    public class ClienteBl : Conexion
    {
        public List<ClienteBe> Listar(int empresaId , string nroDocumentoIdentidad, string razonSocial, int pagina, int cantidadRegistros, string columnaOrden, string ordenMax, out int totalRegistros)
        {
            totalRegistros = 0;
            List<ClienteBe> lCliente = new List<ClienteBe>();
            using (cn)
            {
                try
                {
                    cn.Open();
                    lCliente = new ClienteDa().fListar(cn, empresaId, nroDocumentoIdentidad, razonSocial, pagina, cantidadRegistros, columnaOrden, ordenMax, out totalRegistros);
                    cn.Close();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return lCliente;
        }

        public bool ClienteGuardar(ClienteBe clienteBe)
        {
            bool respuesta = false;
            using (cn)
            {
                try
                {
                    cn.Open();
                    respuesta = new ClienteDa().ClienteGuardar(clienteBe, cn);
                    cn.Close();
                }
                catch (Exception ex)
                {
                    respuesta = false;
                }
                finally
                {
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            return respuesta;
        }
    }
}
