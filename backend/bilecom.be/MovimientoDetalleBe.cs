using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.be
{
    public class MovimientoDetalleBe: Base
    {
        public int EmpresaId { get; set; }
        public int MovimientoId { get; set; }
        public int MovimientoDetalleId { get; set; }
        public int ProductoId { get; set; }
        public string Descripcion { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal TotalImporte { get; set; }
        public string Usuario { get; set; }
        public DateTime FechaVencimiento{ get; set; }
    }
}
