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
    [RoutePrefix("api/tipocomprobantetipooperacionventa")]
    public class TipoComprobanteTipoOperacionVentaController : ApiController
    {
        TipoComprobanteTipoOperacionVentaBl tipoComprobanteTipoOperacionVentaBl = new TipoComprobanteTipoOperacionVentaBl();

        [HttpGet]
        [Route("listar-tipocomprobantetipooperacionventa")]
        public List<TipoComprobanteTipoOperacionVentaBe> ListarTipoComprobanteTipoOperacionVenta(bool withTipoComprobante = false, bool withTipoOperacionVenta = false)
        {
            return tipoComprobanteTipoOperacionVentaBl.Listar(withTipoComprobante, withTipoOperacionVenta);
        }
    }
}
