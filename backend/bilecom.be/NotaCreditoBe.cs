using bilecom.enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static bilecom.enums.Enums;

namespace bilecom.be
{
    public class NotaCreditoBe : Base
    {
        public int EmpresaId { get; set; }
        public EmpresaBe Empresa { get; set; }
        public int NotaCreditoId { get; set; }
        public int AmbienteSunatId { get; set; }
        public AmbienteSunatBe AmbienteSunat { get; set; }
        public int SedeId { get; set; }
        public SedeBe Sede { get; set; }
        public int SerieId { get; set; }
        public SerieBe Serie { get; set; }
        public int NroComprobante { get; set; }
        public DateTime FechaHoraEmision { get; set; }
        public int MonedaId { get; set; }
        public MonedaBe Moneda { get; set; }
        public int ClienteId { get; set; }
        public ClienteBe Cliente { get; set; }
        public int TipoComprobanteId { get; set; }
        public TipoComprobanteBe TipoComprobante { get; set; }
        public int ComprobanteId { get; set; }
        public decimal TotalGravado { get; set; }
        public decimal TotalExonerado { get; set; }
        public decimal TotalInafecto { get; set; }
        public decimal TotalExportacion { get; set; }
        public decimal TotalGratuito { get; set; }
        public decimal TotalVentaArrozPilado { get; set; }
        public decimal TotalIgv { get; set; }
        public decimal TotalIsc { get; set; }
        public decimal TotalOtrosTributos { get; set; }
        public decimal TotalOtrosCargos { get; set; }
        public decimal TotalBaseImponible { get; set; }
        public decimal TotalDescuentos { get; set; }
        public decimal ImporteTotal { get; set; }
        public string ImporteTotalEnLetras { get; set; }
        public string Observacion { get; set; }
        public string Hash { get; set; }

        public bool FlagAnulado { get; set; }
        public string CodigoRespuestaSunat { get; set; }
        public string DescripcionRespuestaSunat { get; set; }
        public int? EstadoIdRespuestaSunat { get; set; }
        public EstadoCdr EstadoRespuestaSunat
        {
            get
            {
                EstadoCdr value = EstadoCdr.NoEmitido;
                if (EstadoIdRespuestaSunat != null) value = (EstadoCdr)EstadoIdRespuestaSunat;

                return value;
            }
        }
        public string EstadoStrRespuestaSunat
        {
            get
            {
                return EstadoRespuestaSunat.GetAttributeOfType<DescriptionAttribute>().Description;
            }
        }
        public string EstadoColorRespuestaSunat
        {
            get
            {
                return EstadoRespuestaSunat.GetAttributeOfType<CategoryAttribute>().Category;
            }
        }
        public string RutaXml { get; set; }
        public string RutaPdf { get; set; }
        public string RutaCdr { get; set; }
        public int? FormatoId { get; set; }


        public string LogoFormatoBase64 { get; set; }
        public string QRBase64 { get; set; }

        public int[] ListaNotaCreditoDetalleEliminados { get; set; }
        public List<NotaCreditoDetalleBe> ListaNotaCreditoDetalle { get; set; }
    }
}
