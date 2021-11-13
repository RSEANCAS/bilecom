using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bilecom.enums;
using static bilecom.enums.Enums;

namespace bilecom.be
{
    public class TableroUltimosDocumentosBe
    {
        public int TipoComprobanteId { get; set; }
        public string TipoComprobanteCodigoSunat { get; set; }
        public string TipoComprobanteNombre { get; set; }
        public string TipoDocumentoDescripcion { get; set; }
        public string ClienteRazonSocial { get; set; }
        public string Serie { get; set; }
        public string Numero { get; set; }
        public DateTime FechaEmision { get; set; }
        public decimal Total { get; set; }
        public string SimboloMoneda { get; set; }
        public bool FlagAnulado { get; set; }
        public int? EstadoSunatId { get; set; }
        public string EstadoSunatDescripcion 
        {
            get
            {
                string text = null;
                if (!EstadoSunatId.HasValue) return text;
                text = ((EstadoCdr)EstadoSunatId.Value).GetAttributeOfType<DescriptionAttribute>().Description;
                return text;
            }
        }

    }
}
