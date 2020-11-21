using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.be
{
    public class CategoriaProductoBe
    {
        public int EmpresaId { get; set; }
        public int CategoriaProductoId { get; set; }
        public string Nombre { get; set; }
        public string Usuario { get; set; }
        public DateTime Fecha { get; set; }
    }
}
