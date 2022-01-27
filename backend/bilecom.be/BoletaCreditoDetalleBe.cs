using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.be
{
    public class BoletaCreditoDetalleBe
    {
        public int EmpresaId { get; set; }
        public int BoletaId { get; set; }
        public int BoletaCreditoDetalleId { get; set; }
        public decimal Monto { get; set; }
    }
}
