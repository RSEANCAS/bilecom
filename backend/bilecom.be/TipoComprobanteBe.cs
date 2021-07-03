using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.be
{
    public class TipoComprobanteBe
    {
        public int TipoComprobanteId { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string IdentificadorSerie { get; set; }
    }
}
