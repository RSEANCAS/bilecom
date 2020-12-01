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
    public class ProveedorBl : Conexion
    {
        public List<ProveedorBe> Listar(int empresaId, string nroDocumentoIdentidad, string razonSocial)
        {
            List<ProveedorBe> lProveedor = new List<ProveedorBe>();
            using (cn)
            {
                try
                {
                    cn.Open();
                    lProveedor = new ProveedorDa().fListar(cn, empresaId, nroDocumentoIdentidad, razonSocial);
                    cn.Close();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return lProveedor;
        }

        public bool ProveedorGuardar(ProveedorBe proveedorBe)
        {
            bool respuesta = false;
            using (cn)
            {
                try
                {
                    cn.Open();
                    respuesta = new ProveedorDa().ProveedorGuardar(proveedorBe, cn);
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
