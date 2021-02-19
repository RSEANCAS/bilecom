using bilecom.be;
using bilecom.da;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace bilecom.bl
{
    public class ProductoBl : Conexion
    {
        ProductoDa productoDa = new ProductoDa();

        public List<ProductoBe> BuscarProducto(string categoriaNombre, string nombre, int empresaId, int pagina, int cantidadRegistros, string columnaOrden, string ordenMax, out int totalRegistros)
        {
            totalRegistros = 0;
            List<ProductoBe> lista = null;

            try
            {
                cn.Open();
                lista = productoDa.Buscar(categoriaNombre, nombre, empresaId, pagina, cantidadRegistros, columnaOrden, ordenMax, cn, out totalRegistros);
                cn.Close();
            }
            catch (Exception ex){lista = null;}
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return lista;
        }

        public List<ProductoBe> BuscarProductoPorCodigo(int? tipoProductoId, string codigo, int empresaId, int sedeAlmacenId = 0)
        {
            List<ProductoBe> lista = null;

            try
            {
                cn.Open();
                lista = productoDa.BuscarPorCodigo(tipoProductoId, codigo, empresaId,  cn, sedeAlmacenId);
                cn.Close();
            }
            catch (Exception ex) { lista = null; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return lista;
        }

        public List<ProductoBe> BuscarProductoPorNombre(int? tipoProductoId, string nombre, int empresaId, int sedeAlmacenId = 0)
        {
            List<ProductoBe> lista = null;

            try
            {
                cn.Open();
                lista = productoDa.BuscarPorNombre(tipoProductoId, nombre, empresaId, cn, sedeAlmacenId);
                cn.Close();
            }
            catch (Exception ex) { lista = null; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return lista;
        }

        public ProductoBe Obtener(int EmpresaId, int productoId)
        {
            ProductoBe respuesta = null;
            try
            {
                cn.Open();
                respuesta = new ProductoDa().Obtener(EmpresaId, productoId, cn);
                cn.Close();
            }
            catch (Exception ex) { respuesta = null; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return respuesta;
        }
        public bool ProductoGuardar(ProductoBe registro)
        {
            bool seGuardo = false;
            try
            {
                cn.Open();
                seGuardo = productoDa.Guardar(registro, cn);
                cn.Close();
            }
            catch (Exception ex) { seGuardo = false; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return seGuardo;
        }

        public bool EliminarProducto(int empresaId, int productoId, string Usuario)
        {
            bool seGuardo = false;
            try
            {
                cn.Open();
                seGuardo = productoDa.Eliminar(empresaId, productoId, Usuario, cn);
                cn.Close();
            }
            catch (Exception ex) { seGuardo = false; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return seGuardo;
        }
    }
}
