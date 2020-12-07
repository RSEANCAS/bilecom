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
    [RoutePrefix("api/tipodocumentoidentidad")]
    public class TipoDocumentoIdentidadController : ApiController
    {
        TipoDocumentoIdentidadBl tipoDocumentoIdentidadBl = new TipoDocumentoIdentidadBl();

        [HttpGet]
        [Route("listar-tipodocumentoidentidad")]
        public List<TipoDocumentoIdentidadBe> ListarTipoDocumentoIdentidad()
        {
            return tipoDocumentoIdentidadBl.ListarTipoDocumentoIdentidad();
        }
    }
}
