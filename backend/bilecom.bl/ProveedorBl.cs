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
        ProveedorDa proveedorDa = new ProveedorDa();

        public List<ProveedorBe> BuscarProveedor(int empresaId, string nroDocumentoIdentidad, string razonSocial, int pagina, int cantidadRegistros, string columnaOrden, string ordenMax, out int totalRegistros)
        {
            totalRegistros = 0;
            List<ProveedorBe> lista = new List<ProveedorBe>();
            try
            {
                cn.Open();
                lista = proveedorDa.Buscar(empresaId, nroDocumentoIdentidad, razonSocial, pagina, cantidadRegistros, columnaOrden, ordenMax, cn, out totalRegistros);
                cn.Close();
            }
            catch (Exception ex) { lista = null; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return lista;
        }

        public ProveedorBe Obtener(int empresaId, int proveedorId)
        {
            ProveedorBe respuesta = null;
            try
            {
                cn.Open();
                respuesta = proveedorDa.Obtener(empresaId, proveedorId, cn);
                cn.Close();
            }
            catch (Exception ex) { respuesta = null; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return respuesta;
        }
        public bool ProveedorGuardar(ProveedorBe registro)
        {
            bool seGuardo = false;
            try
            {
                cn.Open();
                seGuardo = proveedorDa.Guardar(registro, cn);
                cn.Close();
            }
            catch (Exception ex) { seGuardo = false; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return seGuardo;
        }

        public bool EliminarProveedor(int empresaId, int proveedorId, string Usuario)
        {
            bool seGuardo = false;
            try
            {
                cn.Open();
                seGuardo = proveedorDa.Eliminar(empresaId, proveedorId, Usuario, cn);
                cn.Close();
            }
            catch (Exception ex) { seGuardo = false; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return seGuardo;
        }
    }
}
