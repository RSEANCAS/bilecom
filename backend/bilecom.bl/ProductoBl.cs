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

    }
}
