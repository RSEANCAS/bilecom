using bilecom.be;
using bilecom.be.Custom;
using bilecom.bl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace bilecom.app.Controllers.Api
{
    [RoutePrefix("api/sede")]
    public class SedeController : ApiController
    {
        [HttpGet]
        [Route("listar")]
        public DataPaginate<SedeBe> Listar(int empresaId, string nombre, int draw, int start, int length, string columnaOrden = "SedeId", string ordenMax = "ASC")
        {
            int totalRegistros = 0;
            var lista = new SedeBl().Listar(empresaId, nombre, start, length, columnaOrden, ordenMax, out totalRegistros);
            return new DataPaginate<SedeBe>
            {
                data = lista,
                draw = draw,
                recordsFiltered = totalRegistros,
                recordsTotal = totalRegistros
            };
        }
    }
}
