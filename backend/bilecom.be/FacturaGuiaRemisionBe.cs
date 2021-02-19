using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.be
{
    public class FacturaGuiaRemisionBe : Base
    {
        public int EmpresaId { get; set; }
        public int FacturaId { get; set; }
        //public FacturaBe Factura { get; set; }
        public int? GuiaRemisionId { get; set; }
        public GuiaRemisionBe GuiaRemision { get; set; }
        public int TipoComprobanteId { get; set; }
        public TipoComprobanteBe TipoComprobante { get; set; }
        public string SerieNroComprobante { get; set; }
        public bool FlagEliminado { get; set; }
    }
}
