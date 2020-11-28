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
    [RoutePrefix("api/TipoSede")]
    public class TipoSedeController : ApiController
    {
        [HttpGet]
        [Route("Listar/{empresaId}/{nombre}")]
        public List<TipoSedeBe> Listar(int empresaId, string nombre)
        {
            return new TipoSedeBl().Listar(empresaId, nombre);
        }
    }
}
