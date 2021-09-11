using bilecom.be;
using bilecom.da;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.bl
{
    public class ClienteBl : Conexion
    {
        ClienteDa clienteDa = new ClienteDa();

        public List<ClienteBe> BuscarCliente(int empresaId, string nroDocumentoIdentidad, string razonSocial, int pagina, int cantidadRegistros, string columnaOrden, string ordenMax, out int totalRegistros)
        {
            totalRegistros = 0;
            List<ClienteBe> lista = null;
            try
            {
                using (var cn = new SqlConnection(CadenaConexion))
                {
                    cn.Open();
                    lista = clienteDa.Buscar(empresaId, nroDocumentoIdentidad, razonSocial, pagina, cantidadRegistros, columnaOrden, ordenMax, cn, out totalRegistros);
                    cn.Close();
                }
            }
            catch (Exception ex) { lista = null; }
            //finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return lista;
        }

        public ClienteBe Obtener(int empresaId, int clienteId)
        {
            ClienteBe respuesta = null;
            try
            {
                using (var cn = new SqlConnection(CadenaConexion))
                {
                    cn.Open();
                    respuesta = clienteDa.Obtener(empresaId, clienteId, cn);
                    cn.Close();
                }
            }
            catch (Exception ex) { respuesta = null; }
            //finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return respuesta;
        }

        public int GuardarCliente(ClienteBe registro)
        {
            int seGuardo = 0;
            try
            {
                using (var cn = new SqlConnection(CadenaConexion))
                {
                    cn.Open();
                    seGuardo = clienteDa.Guardar(registro, cn);
                    cn.Close();
                }
            }
            catch (Exception ex) { seGuardo = 0; }
            //finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return seGuardo;
        }

        public bool EliminarCliente(int empresaId,int clienteId, string Usuario)
        {
            bool seGuardo = false;
            try
            {
                using (var cn = new SqlConnection(CadenaConexion))
                {
                    cn.Open();
                    seGuardo = clienteDa.Eliminar(empresaId, clienteId, Usuario, cn);
                    cn.Close();
                }
            }
            catch (Exception ex) { seGuardo = false; }
            //finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return seGuardo;
        }
    }
}
