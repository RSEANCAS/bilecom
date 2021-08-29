using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.be
{
    public class EmpresaConfiguracionBe : Base
    {
        public int EmpresaId { get; set; }
        public EmpresaBe Empresa { get; set; }
        public int AmbienteSunatId { get; set; }
        public EmpresaAmbienteSunatBe EmpresaAmbienteSunat { get; set; }
        public string RutaCertificado { get; set; }
        public string ClaveCertificado { get; set; }
        public string CuentaCorriente { get; set; }
        public string ComentarioLegal { get; set; }
        public string ComentarioLegalDetraccion { get; set; }
        public int CantidadDecimalGeneral { get; set; }
        public int CantidadDecimalDetallado { get; set; }
        public string FormatoIds { get; set; }
        public string[] ListaFormatoId
        {
            get { return string.IsNullOrEmpty(FormatoIds) ? null : FormatoIds.Split(',').ToArray(); }
        }
        //public FormatoBe Formato { get; set; }
        public int? MonedaIdPorDefecto { get; set; }
        public int? TipoAfectacionIgvIdPorDefecto { get; set; }
        public string TipoComprobanteTipoOperacionVentaIdsPorDefecto { get; set; }
        public string[] ListaTipoComprobanteTipoOperacionVentaIdPorDefecto
        {
            get { return string.IsNullOrEmpty(TipoComprobanteTipoOperacionVentaIdsPorDefecto) ? null : TipoComprobanteTipoOperacionVentaIdsPorDefecto.Split(',').ToArray(); }
        }
        public int? TipoProductoIdPorDefecto { get; set; }
        public int? UnidadMedidaIdPorDefecto { get; set; }

        public List<MonedaBe> ListaMoneda { get; set; }
        public List<TipoAfectacionIgvBe> ListaTipoAfectacionIgv { get; set; }
        public List<TipoComprobanteTipoOperacionVentaBe> ListaTipoComprobanteTipoOperacionVenta { get; set; }
        public List<TipoProductoBe> ListaTipoProducto { get; set; }
        public List<UnidadMedidaBe> ListaUnidadMedida { get; set; }

        public List<MonedaBe> ListaMonedaPorDefecto { get; set; }
        public List<TipoAfectacionIgvBe> ListaTipoAfectacionIgvPorDefecto { get; set; }
        public List<TipoComprobanteTipoOperacionVentaBe> ListaTipoComprobanteTipoOperacionVentaPorDefecto { get; set; }
        public List<TipoProductoBe> ListaTipoProductoPorDefecto { get; set; }
        public List<UnidadMedidaBe> ListaUnidadMedidaPorDefecto { get; set; }
    }
}
