using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.be
{
    public class ComprobanteBe
    {
        public EmpresaBe Empresa { get; set; }
        public SerieBe Serie { get; set; }
        public int NroComprobante { get; set; }
        public DateTime FechaHoraEmision { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public MonedaBe Moneda { get; set; }
        public TipoOperacionVentaBe TipoOperacionVenta { get; set; }
        public ClienteBe Cliente { get; set; }
        public FormaPagoBe FormaPago { get; set; }
        public decimal TotalIgv { get; set; }
        public decimal ImporteTotal { get; set; }
        public bool FlagAnulado { get; set; }

    }
}
