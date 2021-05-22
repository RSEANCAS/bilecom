using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.be
{
    public class FormatoBe
    {
        public int FormatoId { get; set; }
        public int TipoComprobanteId { get; set; }
        public TipoComprobanteBe TipoComprobante { get; set; }
        public string Nombre { get; set; }
        public string Html { get; set; }
    }
}
