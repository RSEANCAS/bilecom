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
    [RoutePrefix("api/tipocomprobante")]
    public class TipoComprobanteController : ApiController
    {
        TipoComprobanteBl  tipoComprobanteBl = new TipoComprobanteBl();

        [HttpGet]
        [Route("listar-tipocomprobante")]
        public List<TipoComprobanteBe> ListarTipoComprobante()
        {
            return tipoComprobanteBl.ListarTipoComprobante();
        }
    }
}
