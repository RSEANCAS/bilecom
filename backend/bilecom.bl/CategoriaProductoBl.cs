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
        public List<CategoriaProductoBe> Listar(int empresaId, string nombre)
        {
            List<CategoriaProductoBe> lCategoriaProducto = new List<CategoriaProductoBe>();
            using (cn)
            {
                try
                {
                    cn.Open();
                    lCategoriaProducto = new CategoriaProductoDa().fListar(cn, empresaId, nombre);
                    cn.Close();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return lCategoriaProducto;
        }

        public bool CategoriaProductoGuardar(CategoriaProductoBe categoriaProductoBe)
        {
            bool respuesta = false;
            using (cn)
            {
                try
                {
                    cn.Open();
                    respuesta = new CategoriaProductoDa().CategoriaProductoGuardar(categoriaProductoBe, cn);
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
        public bool CategoriaProductoActualizar(CategoriaProductoBe categoriaProductoBe)
        {
            bool respuesta = false;
            using (cn)
            {
                try
                {
                    cn.Open();
                    respuesta = new CategoriaProductoDa().CategoriaProductoActualizar(categoriaProductoBe, cn);
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
