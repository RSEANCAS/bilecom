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
    [RoutePrefix("api/sunat")]
    public class SunatController : ApiController
    {
        SunatBl sunatBl = new SunatBl();

        [HttpGet]
        [Route("obtener-padron-por-ruc")]
        public SunatPadronBe ObtenerPadronPorRuc(string ruc)
        {
            return sunatBl.ObtenerPadronPorRuc(ruc);
        }
    }
}
