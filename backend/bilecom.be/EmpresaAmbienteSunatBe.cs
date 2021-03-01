using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.be
{
    public class EmpresaAmbienteSunatBe
    {
        public int EmpresaId { get; set; }
        public int AmbienteSunatId { get; set; }
        public AmbienteSunatBe AmbienteSunat { get; set; }
        public string RucSOL { get; set; }
        public string UsuarioSOL { get; set; }
        public string ClaveSOL { get; set; }
    }
}
