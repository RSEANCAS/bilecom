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
        public int TipoProductoId { get; set; }
        public string Codigo { get; set; }
        public string CodigoSunat { get; set; }
        public string Nombre { get; set; }
        public int TipoAfectacionIgvId { get; set; }
        public string UnidadMedidaId { get; set; }
        public string TipoCalculo { get; set; }
        public decimal StockActual { get; set; }
        public decimal StockMinimo { get; set; }
        public decimal Monto { get; set; }
        public string Usuario { get; set; }
        public DateTime Fecha { get; set; }
        public CategoriaProductoBe categoriaProducto { get; set; }
    }
}
