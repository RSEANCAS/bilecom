using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using bilecom.enums;
using static bilecom.enums.Enums;

namespace bilecom.be
{
    public class TipoSedeBe
    {
        public int TipoSedeId { get; set; }
        public int EmpresaId { get; set; }
        public string Nombre { get; set; }
        public int? CodigoPlantilla { get; set; }
        public string CodigoPlantillaStr { 
            get
            {
                string text = null;
                if (!CodigoPlantilla.HasValue) return text;
                text = ((TipoSede)CodigoPlantilla.Value).GetAttributeOfType<DescriptionAttribute>().Description;
                return text;
            } 
        }
        public bool FlagEditable { get; set; }
        public bool FlagActivo { get; set; }
        public string Usuario { get; set; }
        public DateTime Fecha { get; set; }

    }
}
