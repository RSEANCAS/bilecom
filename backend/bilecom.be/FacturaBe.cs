﻿using bilecom.enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static bilecom.enums.Enums;

namespace bilecom.be
{
    public class FacturaBe : Base
    {
        public int EmpresaId { get; set; }
        public EmpresaBe Empresa { get; set; }
        public int FacturaId { get; set; }
        public int AmbienteSunatId { get; set; }
        public AmbienteSunatBe AmbienteSunat { get; set; }
        public int SedeId { get; set; }
        public SedeBe Sede { get; set; }
        public int SerieId { get; set; }
        public SerieBe Serie { get; set; }
        public int NroComprobante { get; set; }
        public DateTime FechaHoraEmision { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public int MonedaId { get; set; }
        public MonedaBe Moneda { get; set; }
        public int TipoOperacionVentaId { get; set; }
        public TipoOperacionVentaBe TipoOperacionVenta { get; set; }
        public int ClienteId { get; set; }
        public ClienteBe Cliente { get; set; }
        public int? FormaPagoId { get; set; }
        public FormaPagoBe FormaPago { get; set; }
        public bool FlagExportacion { get; set; }
        public bool FlagGratuito { get; set; }
        public bool FlagEmisorItinerante { get; set; }
        public bool FlagAnticipo { get; set; }
        public bool FlagISC { get; set; }
        public bool FlagOtrosCargos { get; set; }
        public bool FlagOtrosTributos { get; set; }
        public decimal TotalGravado { get; set; }
        public decimal TotalExonerado { get; set; }
        public int? TipoTributoIdExonerado { get; set; }
        public TipoTributoBe TipoTributoExonerado { get; set; }
        public decimal TotalInafecto { get; set; }
        public int? TipoTributoIdInafecto { get; set; }
        public TipoTributoBe TipoTributoInafecto { get; set; }
        public decimal TotalExportacion { get; set; }
        public int? TipoTributoIdExportacion { get; set; }
        public TipoTributoBe TipoTributoExportacion { get; set; }
        public decimal TotalGratuito { get; set; }
        public int? TipoTributoIdGratuito { get; set; }
        public TipoTributoBe TipoTributoGratuito { get; set; }
        public decimal TotalVentaArrozPilado { get; set; }
        public decimal TotalIgv { get; set; }
        public int? TipoTributoIdIgv { get; set; }
        public TipoTributoBe TipoTributoIgv { get; set; }
        public decimal TotalIsc { get; set; }
        public int? TipoTributoIdIsc { get; set; }
        public TipoTributoBe TipoTributoIsc { get; set; }
        public decimal TotalOtrosTributos { get; set; }
        public int? TipoTributoIdOtrosTributos { get; set; }
        public TipoTributoBe TipoTributoOtrosTributos { get; set; }
        public decimal TotalBaseImponible { get; set; }
        public decimal TotalOtrosCargos { get; set; }
        public decimal TotalDescuentos { get; set; }
        public decimal PorcentajeOtrosCargosGlobal { get; set; }
        public decimal TotalOtrosCargosGlobal { get; set; }
        public decimal PorcentajeDescuentosGlobal { get; set; }
        public decimal TotalDescuentosGlobal { get; set; }
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

        public int[] ListaFacturaGuiaRemisionEliminados { get; set; }
        public List<FacturaGuiaRemisionBe> ListaFacturaGuiaRemision { get; set; }

        public List<KeyValuePair<int, string>> ListaFacturaDocumentoEliminados { get; set; }
        public List<FacturaDocumentoBe> ListaFacturaDocumento { get; set; }
        public string ListaFacturaOrdenCompraStr
        {
            get
            {
                if (ListaFacturaDocumento == null) return null;
                if (ListaFacturaDocumento.Count == 0) return null;

                return string.Join("", ListaFacturaDocumento.Select(x => x.SerieNroComprobante).ToArray());
            }
        }

        public int[] ListaFacturaDetalleEliminados { get; set; }
        public List<FacturaDetalleBe> ListaFacturaDetalle { get; set; }
    }
}
