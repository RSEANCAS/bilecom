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
    [RoutePrefix("api/tipoproducto")]
    public class TipoProductoController : ApiController
    {
        TipoProductoBl tipoProductoBl = new TipoProductoBl();

        [HttpGet]
        [Route("listar-tipoproducto")]
        public List<TipoProductoBe> Listar()
        {
            return tipoProductoBl.Listar();
        }
    }
}
