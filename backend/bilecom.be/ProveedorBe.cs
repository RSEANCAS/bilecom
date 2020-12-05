using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.be
{
    public class ProveedorBe
    {
        public int EmpresaId { get; set; }
        public int ProveedorId { get; set; }
        public int TipoDocumentoIdentidad { get; set; }
        public string NroDocumentoIdentidad { get; set; }
        public string RazonSocial { get; set; }
        public string Usuario { get; set; }
        public DateTime Fecha { get; set; }
    }
}
