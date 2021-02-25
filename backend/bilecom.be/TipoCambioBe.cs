using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.be
{
    public class TipoCambioBe
    {
        public DateTime Fecha { get; set; }
        public int MonedaId { get; set; }
        public decimal Compra { get; set; }
        public decimal Venta { get; set; }
        public string Usuario { get { return "usuarioBileCom"; } }
    }
}
