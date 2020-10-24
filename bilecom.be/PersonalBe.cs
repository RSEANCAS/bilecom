using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.be
{
    public class PersonalBe
    {
        public int PersonalId { get; set; }
        public int TipoDocumentoIdentidadId { get; set; }
        public string NroDocumentoIdentidad { get; set; }
        public string Nombres { get; set; }
        public string Correo { get; set; }
        public bool FlagActivo { get; set; }
        public string CreadoPor { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
