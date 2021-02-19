using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.be
{
    public class EmpresaTipoAfectacionIgvBe : Base
    {
        public int EmpresaId { get; set; }
        public EmpresaBe Empresa { get; set; }
        public int TipoAfectacionIgvId { get; set; }
        public TipoAfectacionIgvBe TipoAfectacionIgv { get; set; }
        public bool FlagActivo { get; set; }
    }
}
