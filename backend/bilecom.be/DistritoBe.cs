using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.be
{
    public class DistritoBe
    {
        public int DistritoId { get; set; }
        public int ProvinciaId { get; set; }
        public ProvinciaBe Provincia { get; set; }
        public string CodigoUbigeo { get; set; }
        public string Nombre { get; set; }
    }
}
