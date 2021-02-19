using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.be
{
    public class GuiaRemisionBe : Base
    {
        public int EmpresaId { get; set; }
        public int GuiaRemisionId { get; set; }
        public int SerieId { get; set; }
        public SerieBe Serie { get; set; }
        public int NroComprobante { get; set; }
        public int TipoComprobanteId { get; set; }
        public TipoComprobanteBe TipoComprobante { get; set; }
    }
}
