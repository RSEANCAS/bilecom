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
    [RoutePrefix("api/tipooperacionalmacen")]
    public class TipoOperacionAlmacenController : ApiController
    {
        TipoOperacionAlmacenBl tipoOperacionAlmacenBl = new TipoOperacionAlmacenBl();

        [HttpGet]
        [Route("listar-tipooperacionalmacen")]
        public List<TipoOperacionAlmacenBe> Listar()
        {
            return tipoOperacionAlmacenBl.Listar();
        }
    }
}
