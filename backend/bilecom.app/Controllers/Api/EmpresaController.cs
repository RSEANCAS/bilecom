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
    [RoutePrefix("api/empresa")]
    public class EmpresaController : ApiController
    {
        EmpresaBl empresaBl = new EmpresaBl();

        [HttpGet]
        [Route("validar-empresa-por-ruc")]
        public IHttpActionResult ValidarEmpresaPorRuc(string ruc)
        {
            BootStrapValidator.Remote item = new BootStrapValidator.Remote();

            if (string.IsNullOrEmpty((ruc ?? "").Trim()))
                return Ok(item);

            var empresa = empresaBl.ObtenerEmpresaPorRuc(ruc);

            if (empresa == null)
                return Ok(item);

            item.valid = true;

            return Ok(item);
        }
    }
}
