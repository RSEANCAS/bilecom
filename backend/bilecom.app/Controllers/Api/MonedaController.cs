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
    [RoutePrefix("api/moneda")]
    public class MonedaController : ApiController
    {
        MonedaBl monedaBl = new MonedaBl();

        [HttpGet]
        [Route("listar-moneda-por-empresa")]
        public List<MonedaBe> ListarMonedaPorEmpresa(int empresaId)
        {
            return monedaBl.ListarMonedaPorEmpresa(empresaId);
        }
    }
}
