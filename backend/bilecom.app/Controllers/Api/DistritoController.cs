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
    [RoutePrefix("api/distrito")]
    public class DistritoController : ApiController
    {
        [HttpGet]
        [Route("listar")]
        public List<DistritoBe> DistritoListar()
        {
            return new DistritoBl().DistritoListar();
        }
    }
}
