using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.be
{
    public class KardexNivel1Be : Base
    {
        public int ProductoId { get; set; }
        public string Codigo { get; set; }
        public string CodigoSunat { get; set; }
        public string Nombre { get; set; }
        public string UnidadMedidaDescripcion{ get; set; }
        public decimal StockActual { get; set; }
        public List<KardexNivel2Be> Detalle { get; set; }
    }
    public class KardexNivel2Be : Base
    {
        public DateTime FechaHoraEmision { get; set; }
        public int ProductoId { get; set; }
        public decimal Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal TotalImporte { get; set; }
        public int TipoMovimientoId { get; set; }
        public string TipoMovimientoDescripcion { get; set; }
        public decimal Stock { get; set; }

    }
}

