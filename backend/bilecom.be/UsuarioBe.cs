using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.be
{
    public class UsuarioBe
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public byte[] Contrasena { get; set; }
        public bool FlagCambiarContrasena { get; set; }
        public bool FlagActivo { get; set; }
        public string CreadoPor { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
