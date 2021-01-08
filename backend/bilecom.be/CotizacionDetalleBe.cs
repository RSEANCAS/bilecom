using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.be
{
    public class CotizacionDetalleBe : Base
    {
        public int EmpresaId { get; set; }
        public int CotizacionId { get; set; }
        public int CotizacionDetalleId { get; set; }
        public int ProductoId { get; set; }
        public string Descripcion { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal TotalImporte { get; set; }
    }
}
