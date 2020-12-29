namespace bilecom.be
{
    public class SerieBe : Base
    {
        public int EmpresaId { get; set; }
        public int SerieId { get; set; }
        public int TipoComprobanteId { get; set; }
        public string Serial { get; set; }
        public int ValorInicial { get; set; }
        public int? ValorFinal { get; set; }
        public bool FlagSinFinal { get; set; }
        public int ValorActual { get; set; }
    }
}