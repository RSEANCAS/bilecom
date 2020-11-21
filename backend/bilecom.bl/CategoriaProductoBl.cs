using bilecom.be;
using bilecom.da;
using System;
using System.Collections.Generic;
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
    }
}
