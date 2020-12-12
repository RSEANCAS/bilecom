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
    public class CategoriaProductoBl : Conexion
    {
        CategoriaProductoDa categoriaProductoDa = new CategoriaProductoDa();

        public List<CategoriaProductoBe> BuscarCategoriaProducto(int empresaId, string nombre, int pagina, int cantidadRegistros, string columnaOrden, string ordenMax, out int totalRegistros)
        {
            totalRegistros = 0;
            List<CategoriaProductoBe> lista = null;
            try
            {
                cn.Open();
                lista = categoriaProductoDa.Buscar(empresaId, nombre, pagina, cantidadRegistros, columnaOrden, ordenMax, cn, out totalRegistros);
                cn.Close();
            }
            catch (Exception ex) { lista = null; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return lista;
        }

        public bool CategoriaProductoGuardar(CategoriaProductoBe registro)
        {
            bool seGuardo = false;
            try
            {
                cn.Open();
                seGuardo = categoriaProductoDa.Guardar(registro, cn);
                cn.Close();
            }
            catch (Exception ex) { seGuardo = false; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return seGuardo;
        }
    }
}
