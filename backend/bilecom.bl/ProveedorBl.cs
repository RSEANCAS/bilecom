using bilecom.be;
using bilecom.da;
using System;
using System.Collections.Generic;
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
    }
}
