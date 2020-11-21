using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.be
{
    public class ProductoBe
    {
        public int ProductoId { get; set; }
        public int CategoriaId { get; set; }
        public string Nombre { get; set; }
        public string Usuario { get; set; }
        public DateTime Fecha { get; set; }
        public CategoriaProductoBe categoriaProducto { get; set; }
    }
}
