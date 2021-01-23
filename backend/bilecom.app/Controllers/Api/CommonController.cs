using bilecom.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static bilecom.enums.Enums;

namespace bilecom.app.Controllers.Api
{
    [RoutePrefix("api/common")]
    public class CommonController : ApiController
    {
        [HttpGet]
        [Route("listar-enum-tipo-sede")]
        public List<dynamic> ListarEnumTipoSede()
        {
            List<dynamic> respuesta = Enum<TipoSede>.GetCollection().ToList();

            return respuesta;
        }
    }
}
