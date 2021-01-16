using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.be
{
    public class FacturaDetalleBe
    {
        public int EmpresaId { get; set; }
        public int FacturaId { get; set; }
        public int FacturaDetalleId { get; set; }
        public int TipoProductoId { get; set; }
        public TipoProductoBe TipoProductoBe { get; set; }
        public decimal Cantidad { get; set; }
        public int UnidadMedidaId { get; set; }
        public UnidadMedidaBe UnidadMedida { get; set; }
        public int ProductoId { get; set; }
        public ProductoBe Producto { get; set; }
        public string Descripcion { get; set; }
        public bool FlagAplicaICPBER { get; set; }
        public int TipoAfectacionIgvId { get; set; }
        public TipoAfectacionIgvBe TipoAfectacionIgv { get; set; }
        public decimal Descuento { get; set; }
        public decimal ISC { get; set; }
        public decimal PorcentajeIGV { get; set; }
        public decimal IGV { get; set; }
        public decimal ICPBER { get; set; }
        public decimal PorcentajeICPBER { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal ValorVenta { get; set; }
        public decimal PrecioVenta { get; set; }
        public decimal ImporteTotal { get; set; }
    }
}