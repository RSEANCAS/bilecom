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
    [RoutePrefix("api/tipotributo")]
    public class TipoTributoController : ApiController
    {
        TipoTributoBl tipoTributoBl = new TipoTributoBl();

        [HttpGet]
        [Route("listar-tipoTributo")]
        public List<TipoTributoBe> ListarTipoTributo()
        {
            return tipoTributoBl.ListarTipoTributo();
        }
    }
}
