using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.be
{
    public class TokenBe : Base
    {
        public int UsuarioId { get; set; }
        public int EmpresaId { get; set; }
        public string CodigoToken { get; set; }
        public int TipoTokenId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

    }
}
