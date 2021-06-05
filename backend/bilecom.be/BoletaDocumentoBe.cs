using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.be
{
    public class BoletaDocumentoBe : Base
    {
        public int EmpresaId { get; set; }
        public int BoletaId { get; set; }
        public int TipoComprobanteId { get; set; }
        public TipoComprobanteBe TipoComprobante { get; set; }
        public string SerieNroComprobante { get; set; }
        public bool FlagEliminado { get; set; }
    }
}
