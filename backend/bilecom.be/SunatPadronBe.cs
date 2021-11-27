using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.be
{
    public class SunatPadronBe
    {
        public string Ruc { get; set; }
        public string RazonSocial { get; set; }
        public string EstadoContribuyente { get; set; }
        public string CondicionDomiciliaria { get; set; }
        public string Ubigeo { get; set; }
        public int? DistritoId { get; set; }
        public int? ProvinciaId { get; set; }
        public int? DepartamentoId { get; set; }
        public int? PaisId { get; set; }
        public string Direccion { get; set; }
    }
}
