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
        public List<ProductoBe> Listar(string categoriaNombre, string nombre, int empresaId)
        {
            List<ProductoBe> lProducto = new List<ProductoBe>();

            using (cn)
            {
                try
                {
                    cn.Open();
                    lProducto = new ProductoDa().fListar(cn, categoriaNombre, nombre, empresaId);
                    cn.Close();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return lProducto;
        }

        public bool ProductoGuardar(ProductoBe productoBe)
        {
            bool respuesta = false;
            using (cn)
            {
                try
                {
                    cn.Open();
                    respuesta = new ProductoDa().ProductoGuardar(productoBe, cn);
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
        public bool ProductoActualizar(ProductoBe productoBe)
        {
            bool respuesta = false;
            using (cn)
            {
                try
                {
                    cn.Open();
                    respuesta = new ProductoDa().ProductoActualizar(productoBe, cn);
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
