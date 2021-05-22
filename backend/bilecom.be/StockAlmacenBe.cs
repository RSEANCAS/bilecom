using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.be
{
    public class StockAlmacenBe
    {
        public int Fila { get; set; }
        public int ProductoId { get; set; }
        public string Codigo { get; set; }
        public string CodigoSunat { get; set; }
        public string Descripcion { get; set; }
        public string UnidadMedidaDescripcion{ get; set; }
        public decimal StockMinimo { get; set; }
        public decimal StockActual { get; set; }
        public string TipoCalculo { get; set; }
        public decimal Monto { get; set; }
    }
}
