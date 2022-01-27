using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.be
{
    public class FacturaCreditoDetalleBe
    {
        public int EmpresaId { get; set; }
        public int FacturaId { get; set; }
        public int FacturaCreditoDetalleId { get; set; }
        public decimal Monto { get; set; }
    }
}
