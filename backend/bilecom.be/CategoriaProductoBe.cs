using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.be
{
    public class CategoriaProductoBe : Base
    {
        public int EmpresaId { get; set; }
        public int CategoriaProductoId { get; set; }
        public string Nombre { get; set; }
        public bool FlagActivo { get; set; }
    }
}
