using FileHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.procesos.dto
{
    [DelimitedRecord("|")]
    [IgnoreFirst()]
    public class SunatPadronDto
    {
        [FieldOrder(0)]
        public string Ruc { get; set; }
        [FieldOrder(1)]
        public string RazonSocial { get; set; }
        [FieldOrder(2)]
        public string EstadoContribuyente { get; set; }
        [FieldOrder(3)]
        public string CondicionDomiciliaria { get; set; }
        [FieldOrder(4)]
        public string Ubigeo { get; set; }
        [FieldOrder(5)]
        public string TipoVia { get; set; }
        [FieldOrder(6)]
        public string NombreVia { get; set; }
        [FieldOrder(7)]
        public string CodigoZona { get; set; }
        [FieldOrder(8)]
        public string TipoZona { get; set; }
        [FieldOrder(9)]
        public string Numero { get; set; }
        [FieldOrder(10)]
        public string Interior { get; set; }
        [FieldOrder(11)]
        public string Lote { get; set; }
        [FieldOrder(12)]
        public string Departamento { get; set; }
        [FieldOrder(13)]
        public string Manzana { get; set; }
        [FieldOrder(14)]
        public string Kilometro { get; set; }
        [FieldOrder(15)]
        public string EmptyColumn { get; set; }
        [FieldOrder(16)]
        [FieldOptional]
        public string EmptyColumn2 { get; set; }
    }
}
