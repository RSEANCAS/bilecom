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
        public enum EstadoCdr
        {
            [Description("Aceptado")]
            Aceptado =1,
            [Description("En Proceso")]
            EnProceso =2,
            [Description("Rechazado")]
            Rechazado =3,
            [Description("No Emitido")]
            NoEmitido =4
        }
    }
}
