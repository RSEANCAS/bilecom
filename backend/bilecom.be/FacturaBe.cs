using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.be
{
    public class FacturaBe : Base
    {
        public int EmpresaId { get; set; }
        public int FacturaId { get; set; }
        public int SerieId { get; set; }
        public SerieBe Serie { get; set; }
        public int NroComprobante { get; set; }
        public DateTime FechaHoraEmision { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public int MonedaId { get; set; }
        public MonedaBe Moneda { get; set; }
        public int ClienteId { get; set; }
        public ClienteBe Cliente { get; set; }
        public bool FlagExportacion { get; set; }
        public bool FlagGratuito { get; set; }
        public bool FlagEmisorItinerante { get; set; }
        public bool FlagAnticipo { get; set; }
        public bool FlagISC { get; set; }
        public bool FlagOtrosCargos { get; set; }
        public bool FlagOtrosTributos { get; set; }
        public decimal TotalIgv { get; set; }
        public decimal TotalIsc { get; set; }
        public decimal TotalOtrosCargos { get; set; }
        public decimal TotalOtrosTributos { get; set; }
        public decimal TotalBaseImponible { get; set; }
        public decimal TotalDescuentos { get; set; }
        public decimal ImporteTotal { get; set; }
        public bool FlagAnulado { get; set; }
    }
}
