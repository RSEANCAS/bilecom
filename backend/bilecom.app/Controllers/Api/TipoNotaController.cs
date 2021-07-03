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
    [RoutePrefix("api/tiponota")]
    public class TipoNotaController : ApiController
    {
        TipoNotaBl tipoNotaBl = new TipoNotaBl();

        [HttpGet]
        [Route("listar-tiponota-por-tipocomprobante")]
        public List<TipoNotaBe> ListarTipoNotaPorTipoComprobante(int tipoComprobanteId)
        {
            return tipoNotaBl.ListarPorTipoComprobante(tipoComprobanteId);
        }
    }
}
