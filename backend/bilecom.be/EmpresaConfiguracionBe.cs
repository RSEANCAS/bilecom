using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.be
{
    public class EmpresaConfiguracionBe
    {
        public int EmpresaId { get; set; }
        public int AmbienteSunatId { get; set; }
        public EmpresaAmbienteSunatBe EmpresaAmbienteSunat { get; set; }
        public string RutaCertificado { get; set; }
        public string ClaveCertificado { get; set; }
        public string CuentaCorriente { get; set; }
        public string ComentarioLegal { get; set; }
        public string ComentarioLegalDetraccion { get; set; }
        public int CantidadDecimalGeneral { get; set; }
        public int CantidadDecimalDetallado { get; set; }
        public int FormatoId { get; set; }
        public FormatoBe Formato { get; set; }
        public int? MonedaIdPorDefecto { get; set; }
        public int? TipoAfectacionIgvIdPorDefecto { get; set; }
        public string TipoComprobanteTipoOperacionVentaIdPorDefecto { get; set; }
        public int? TipoProductoIdPorDefecto { get; set; }
        public int? UnidadMedidaIdPorDefecto { get; set; }

        public List<MonedaBe> ListaMoneda { get; set; }
        public List<TipoAfectacionIgvBe> ListaTipoAfectacionIgv { get; set; }
        public List<TipoComprobanteTipoOperacionVentaBe> ListaTipoComprobanteTipoOperacionVenta { get; set; }
        public List<TipoProductoBe> ListaTipoProducto { get; set; }
        public List<UnidadMedidaBe> ListaUnidadMedida { get; set; }
    }
}
