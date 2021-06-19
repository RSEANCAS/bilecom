using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.be
{
    public class SerieBe : Base
    {
        public int EmpresaId { get; set; }
        public int SerieId { get; set; }
        public int AmbienteSunatId { get; set; }
        public int TipoComprobanteId { get; set; }
        public int TipoComprobanteReferenciaId { get; set; }
        public TipoComprobanteBe TipoComprobante { get; set; }
        public TipoComprobanteBe TipoComprobanteReferencia { get; set; }
        public string Serial { get; set; }
        public int ValorInicial { get; set; }
        public int? ValorFinal { get; set; }
        public int ValorActual { get; set; }
        public bool FlagSinFinal { get; set; }  
    }
}
