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
            Editar = 2
        }

        public enum TipoSede
        {
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
            Cotizacion = 6
        }
    }
}
