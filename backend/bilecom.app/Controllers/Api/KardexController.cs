using bilecom.be;
using bilecom.bl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using bilecom.be.Custom;

namespace bilecom.app.Controllers.Api
{
    [RoutePrefix("api/kardex")]
    public class KardexController : ApiController
    {
        KardexBl kardexBl = new KardexBl();

        [HttpGet]
        [Route("buscarnivel1-kardex")]
        public DataPaginate<KardexNivel1Be> BuscarNivel1(int empresaId, int almacenId, int productoId, DateTime fechaInicio, DateTime fechaFinal, int draw, int start, int length, string columnaOrden = "FechaHoraEmision", string ordenMax = "ASC")
        {
            int totalRegistros = 0;
            var lista = kardexBl.BuscarNivel1(empresaId, almacenId, productoId,fechaInicio,fechaFinal, start, length, columnaOrden, ordenMax, out totalRegistros);
            var respuesta = new DataPaginate<KardexNivel1Be>
            {
                data = lista ?? new List<KardexNivel1Be>(),
                draw = draw,
                recordsFiltered = totalRegistros,
                recordsTotal = totalRegistros
            };
            return respuesta;
        }
        [HttpGet]
        [Route("buscarnivel2-kardex")]
        public DataPaginate<KardexNivel2Be> BuscarNivel2(int empresaId, int almacenId, int productoId, DateTime fechaInicio, DateTime fechaFinal, int draw, int start, int length, string columnaOrden = "FechaHoraEmision", string ordenMax = "ASC")
        {
            int totalRegistros = 0;
            var lista = kardexBl.BuscarNivel2(empresaId, almacenId, productoId, fechaInicio, fechaFinal, start, length, columnaOrden, ordenMax, out totalRegistros);
            var respuesta = new DataPaginate<KardexNivel2Be>
            {
                data = lista ?? new List<KardexNivel2Be>(),
                draw = draw,
                recordsFiltered = totalRegistros,
                recordsTotal = totalRegistros
            };
            return respuesta;
        }
    }
}
