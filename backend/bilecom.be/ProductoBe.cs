using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.be
{
    public class ProductoBe : Base
    {
        public int EmpresaId { get; set; }
        public int ProductoId { get; set; }
        public int CategoriaId { get; set; }
        public string Nombre { get; set; }
        public int TipoAfectacionIgvId { get; set; }
<<<<<<< HEAD
        public string UnidadMedidaId { get; set; }
        public string TipoCalculo { get; set; }
        public decimal StockMinimo { get; set; }
        public decimal Monto { get; set; }
        public string Usuario { get; set; }
        public DateTime Fecha { get; set; }
=======
        public int UnidadMedidaId { get; set; }
        public int Stock { get; set; }
>>>>>>> 324dc3857da6da3f15abe78312f2231f7c7adadd
        public CategoriaProductoBe categoriaProducto { get; set; }
    }
}
