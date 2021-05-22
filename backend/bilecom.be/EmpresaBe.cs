using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.be
{
    public class EmpresaBe
    {
        public int EmpresaId { get; set; }
        public string Ruc { get; set; }
        public string RazonSocial { get; set; }
        public string NombreComercial { get; set; }
        public int DistritoId { get; set; }
        public DistritoBe Distrito { get; set; }
        public string Direccion { get; set; }
        public string CreadoPor { get; set; }

        public EmpresaImagenBe EmpresaImagen { get; set; }
        public EmpresaConfiguracionBe EmpresaConfiguracion { get; set; }
    }
}
