using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.be
{
    public class TipoAfectacionIgvBe
    {
        public int TipoAfectacionIgvId { get; set; }
        public string Id { get; set; }
        public string Codigo { get; set; }
        public string Descripcion { get; set; }
        public bool FlagGravado { get; set; }
        public bool FlagExonerado { get; set; }
        public bool FlagInafecto { get; set; }
        public bool FlagExportacion { get; set; }
        public bool FlagGratuito { get; set; }
        public bool FlagVentaArrozPilado { get; set; }
    }
}
