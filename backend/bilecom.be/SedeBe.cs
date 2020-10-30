using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.be
{
    public class SedeBe
    {
        public int SedeId { get; set; }
        public string Ruc { get; set; }
        public int Rubro { get; set; }
        public string Tipo { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Ubigeo { get; set; }
        public string CreadoPor { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string ModificadoPor { get; set; }
        public DateTime FechaModificacion { get; set; }
        
    }
}
