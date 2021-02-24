using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.be
{
    public class MovimientoBe:Base
    {
        public int MovimientoId { get; set; }
        public int EmpresaId { get; set; }
        public int SedeId { get; set; }
        public int SedeAlmacenId { get; set; }
        public int TipoMovimientoId { get; set; }
        public TipoMovimientoBe TipoMovimiento { get; set; }
        public int TipoOperacionAlmacenId { get; set; }
        public TipoOperacionAlmacenBe TipoOperacionAlmacen { get; set; }
        public string ReferenciaTipo { get; set; }
        public string ReferenciaSerie { get; set; }
        public string ReferenciaNumero { get; set; }
        public DateTime FechaHoraEmision { get; set; }
        public int SerieId { get; set; }
        public SerieBe Serie { get; set; }
        public int NroMovimiento { get; set; }
        public string Tipo { get; set; }
        public int ProveedorId { get; set; }
        public ProveedorBe Proveedor { get; set; }
        public int ClienteId { get; set; }
        public ClienteBe Cliente { get; set; }
        public int PersonalId { get; set; }
        public PersonalBe Personal { get; set; }
        public int MonedaId { get; set; }
        public MonedaBe Moneda { get; set; }
        public decimal TotalImporte { get; set; }
        public bool FlagAnulado { get; set; }
        public List<MovimientoDetalleBe> ListaMovimientoDetalle { get; set; }
        public int[] ListaMovimientoDetalleEliminados { get; set; }
    }
}
