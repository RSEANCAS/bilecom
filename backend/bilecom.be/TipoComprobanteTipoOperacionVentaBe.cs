using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.be
{
    public class TipoComprobanteTipoOperacionVentaBe : Base
    {
        public int TipoComprobanteId { get; set; }
        public TipoComprobanteBe TipoComprobante { get; set; }
        public int TipoOperacionVentaId { get; set; }
        public TipoOperacionVentaBe TipoOperacionVenta { get; set; }
        public bool FlagActivo { get; set; }
    }
}
