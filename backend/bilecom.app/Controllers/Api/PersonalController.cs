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
    [RoutePrefix("api/personal")]
    //[Authorize]
    public class PersonalController : ApiController
    {
        [HttpGet]
        [Route("listar/{empresaId}/{nroDocumentoIdentidad}/{nombresCompletos}")]
        public List<PersonalBe> Listar (int empresaId, string nroDocumentoIdentidad, string nombresCompletos)
        {
            return new PersonalBl().Listar(empresaId, nroDocumentoIdentidad, nombresCompletos);
        }
    }
}
