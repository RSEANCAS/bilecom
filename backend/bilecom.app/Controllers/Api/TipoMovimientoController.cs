using bilecom.be;
using bilecom.bl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace bilecom.app.Controllers.Api
{

    [RoutePrefix("api/tipomovimiento")]
    public class TipoMovimientoController : ApiController
    {
        TipoMovimientoBl tipoMovimientoBl = new TipoMovimientoBl();

        [HttpGet]
        [Route("listar-tipomovimiento")]
        public List<TipoMovimientoBe> Listar()
        {
            return tipoMovimientoBl.Listar();
        }
    }
}
