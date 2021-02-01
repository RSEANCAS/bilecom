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
    [RoutePrefix("api/tipooperacionventa")]
    public class TipoOperacionVentaController : ApiController
    {
        TipoOperacionVentaBl tipoOperacionVentaBl = new TipoOperacionVentaBl();

        [HttpGet]
        [Route("listar-tipooperacionventa-por-empresa")]
        public List<TipoOperacionVentaBe> ListarTipoOperacionVentaPorEmpresa(int empresaId)
        {
            return tipoOperacionVentaBl.ListarTipoOperacionVentaPorEmpresa(empresaId);
        }
    }
}
