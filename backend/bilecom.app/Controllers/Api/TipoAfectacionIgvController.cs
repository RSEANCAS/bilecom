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
    [RoutePrefix("api/tipoafectacionigv")]
    public class TipoAfectacionIgvController : ApiController
    {
        TipoAfectacionIgvBl tipoAfectacionIgvBl = new TipoAfectacionIgvBl();

        [HttpGet]
        [Route("listar-tipoafectacionigv")]
        public List<TipoAfectacionIgvBe> Listar()
        {
            return tipoAfectacionIgvBl.Listar();
        }

    }
}
