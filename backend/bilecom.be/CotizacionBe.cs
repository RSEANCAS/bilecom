using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.be
{
    public class CotizacionBe:Base
    {
        // Definir los atributos que tiene la clase o tabla
        public int CotizacionId { get; set; }
        public int EmpresaId { get; set; }
        public DateTime FechaHoraEmision { get; set; }
        public int SerieId { get; set; }
        public int NroComprobante { get; set; }
        public string NroPedido { get; set; }
        public int ClienteId { get; set; }
        public ClienteBe Cliente { get; set; }
        public int PersonalId { get; set; }
        public PersonalBe Personal { get; set; }
        public int MonedaId { get; set; }
        public float TotalImporte { get; set; }
        public bool FlagEliminado { get; set; }
        public string CreadoPor { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime FechaModificacion { get; set; }
        public List<CotizacionDetalleBe> ListaCotizacionDetalle { get; set; }
    }
    
}
