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
        public int EmpresaId { get; set; }
        public int TipoSedeId { get; set; }
        public int DistritoId { get; set; }
        public string Ruc { get; set; }
        public int Rubro { get; set; }
        public string Tipo { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Ubigeo { get; set; }
        public bool FlagActivo { get; set; }
        public string Usuario { get; set; }
        public DateTime Fecha { get; set; }

        
    }
}
