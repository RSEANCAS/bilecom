using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.be
{
    public class ClienteBe
    {
        // Definir los atributos que tiene la clase o tabla
        public int ClienteId { get; set; }
        public int EmpresaId { get; set; }
        public string TipoDocumento { get; set; }
        public string NroDocumentoIdentidad { get; set; }
        public string RazonSocial { get; set; }
        public string NombreComercial { get; set; }
        public int DistritoId { get; set; }
        public string Direccion { get; set; }
        public string Correo { get; set; }
        public bool FlagActivo { get; set; }
        public string Usuario { get; set; }

    }
}
