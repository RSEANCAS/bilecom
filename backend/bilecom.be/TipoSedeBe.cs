using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.be
{
    public class TipoSedeBe
    {
        public int TipoSedeId { get; set; }
        public int EmpresaId { get; set; }
        public string Nombre { get; set; }
        public int CodigoEnum { get; set; }
        public bool FlagActivo { get; set; }
        public string Usuario { get; set; }
        public DateTime Fecha { get; set; }

    }
}
