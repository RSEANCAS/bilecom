using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.be
{
    public class PersonalBe : Base
    {
        // Definir los atributos que tiene la clase
        public int PersonalId { get; set; }
        public int EmpresaId { get; set; }
        public int TipoDocumentoIdentidadId { get; set; }
        public TipoDocumentoIdentidadBe TipoDocumentoIdentidad { get; set; }
        public string NroDocumentoIdentidad { get; set; }
        public string NombresCompletos { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public bool FlagActivo { get; set; }
        //public string Usuario { get; set; }
        //public DateTime Fecha { get; set; }
        public int DistritoId { get; set; }
    }
}
