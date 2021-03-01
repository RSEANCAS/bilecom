using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.be
{
    public class EmpresaConfiguracionBe
    {
        public int EmpresaId { get; set; }
        public int AmbienteSunatId { get; set; }
        public EmpresaAmbienteSunatBe EmpresaAmbienteSunat { get; set; }
        public string RutaCertificado { get; set; }
        public string ClaveCertificado { get; set; }
        public string CuentaCorriente { get; set; }
        public string ComentarioLegal { get; set; }
    }
}
