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
    [RoutePrefix("api/Proveedor")]
    public class ProveedorController : ApiController
    {
        [HttpGet]
        [Route("Listar/{empresaId}/{nroDocumentoIdentidad}/{razonSococial}")]
        public List<ProveedorBe> Listar(int empresaId, string nroDocumentoIdentidad, string razonSocial)
        {
            return new ProveedorBl().Listar(empresaId, nroDocumentoIdentidad, razonSocial);
        }
    }
}
