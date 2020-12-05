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
    [RoutePrefix("api/tiposede")]
    public class TipoSedeController : ApiController
    {
        [HttpGet]
        [Route("listar")]
        public DataPaginate<TipoSedeBe> Listar(int empresaId, string nombre, int pagina = 1, int cantidadRegistros = 10, string columnaOrden = "TipoSedeId", string ordenMax = "ASC")
        {
            int totalRegistros = 0;
            var lista = new TipoSedeBl().Listar(empresaId, nombre, pagina, cantidadRegistros, columnaOrden, ordenMax, out totalRegistros);
            return new DataPaginate<TipoSedeBe>
            {
                data = lista,
                draw = 1,
                recordsFiltered = totalRegistros,
                recordsTotal = totalRegistros
            };
        }
    }
}
