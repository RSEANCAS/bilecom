using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.be
{
    public class TipoNotaBe
    {
        public int TipoNotaId { get; set; }
        public int TipoComprobanteId { get; set; }
        public string Descripcion { get; set; }
        public string CodigoSunat { get; set; }
    }
}
