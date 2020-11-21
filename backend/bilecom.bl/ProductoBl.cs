using bilecom.be;
using bilecom.da;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.bl
{
    public class ProductoBl : Conexion
    {
        public List<ProductoBe> Listar(int productoId, string nombre, int empresaId)
        {
            List<ProductoBe> lProducto = new List<ProductoBe>();

            using (cn)
            {
                try
                {
                    cn.Open();
                    lProducto = new ProductoDa().fListar(cn, productoId, nombre, empresaId);
                    cn.Close();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return lProducto;
        }
    }
}
