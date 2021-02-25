using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.enums
{
    public class Enums
    {
        public enum Accion
        {
            Nuevo = 1,
            Editar = 2,
            Lectura = 3
        }

        public enum TipoSede
        {
            [Description("Almacén")]
            Almacen = 1
        }

        public enum TipoComprobante
        {
            [DefaultValue("01")]
            [Description("Factura")]
            Factura = 1,
            [DefaultValue("03")]
            [Description("Boleta")]
            Boleta = 2,
            [DefaultValue("07")]
            [Description("Nota de Crédito")]
            NotaCredito = 3,
            [DefaultValue("08")]
            [Description("Nota de Débito")]
            NotaDebito = 4,
            [DefaultValue("09")]
            [Description("Guía de Remisión Remitente")]
            GuiaRemisionRemitente = 5,
            [DefaultValue("CT")]
            [Description("Cotizacion")]
            Cotizacion = 6,
            [DefaultValue("MV")]
            [Description("Movimiento")]
            Movimiento = 7

        }
        public enum TipoDocumentoIdentidad
        {
            [DefaultValue("1")]
            DNI = 1,
            [DefaultValue("6")]
            RUC = 2,
            [DefaultValue("3")]
            CarnetExtranjeria = 3,
            [DefaultValue("4")]
            Pasaporte = 4
        }

        public enum TipoOperacionVenta
        {
            VentaInterna = 1,
            Exportacion = 2,
            NoDomiciliados = 3,
            VentaInternaAnticipos = 4,
            VentaItinerante = 5,
            FacturaGuia = 6,
            VentaArrozPilado = 7,
            FacturaComprobantePercepcion = 8,
            FacturaGuiaRemitente = 9,
            FacturaGuiaTransportista = 10,
        }

<<<<<<< HEAD
        public enum Moneda
        {
            Dolares = 2
=======
        public enum TipoTributo
        {
            Igv = 1,
            Isc = 2,
            Exportacion = 3,
            Gratuito = 4,
            Exonerado = 5,
            Inafecto = 6,
            OtrosConceptosPago = 7,
            ImpuestoVentaArrozPilado = 8
        }

        public enum EstadoCdr
        {
            [Description("Aceptado")]
            [Category("success")]
            Aceptado = 1,
            [Description("En Proceso")]
            [Category("warning")]
            EnProceso = 2,
            [Description("Rechazado")]
            [Category("danger")]
            Rechazado = 3,
            [Description("No Emitido")]
            [Category("default")]
            NoEmitido = 4
>>>>>>> baafaacc9a7e5e2654dca384fec9211bd1488804
        }
    }
}