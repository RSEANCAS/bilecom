using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace bilecom.be
{
    public class EmpresaImagenBe
    {
        public int EmpresaId { get; set; }
        public string LogoTipoContenido { get; set; }
        public byte[] Logo { get; set; }
        public string LogoFormatoTipoContenido { get; set; }
        public byte[] LogoFormato { get; set; }
    }
}
