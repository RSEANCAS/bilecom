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
    [RoutePrefix("api/unidadmedida")]
    public class UnidadMedidaController : ApiController
    {
        UnidadMedidaBl unidadMedidaBl = new UnidadMedidaBl(); 

        [HttpGet]
        [Route("listar-unidadmedida")]
        public List<UnidadMedidaBe> Listar()
        {
            return unidadMedidaBl.Listar();
        }
    }
}
