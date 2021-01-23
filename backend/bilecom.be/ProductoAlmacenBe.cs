using System;   
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.be
{
    public class ProductoAlmacenBe
    {
        public int ProductoId { get; set; }
        public int AlmacenId { get; set; }
        public decimal Monto { get; set; }
        public int TipoMovimientoId { get; set; }
        public string Usuario { get; set; }
        
    }
}
