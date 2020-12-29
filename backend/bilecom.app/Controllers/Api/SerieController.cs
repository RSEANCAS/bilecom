using bilecom.be;
using bilecom.be.Custom;
using bilecom.bl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace bilecom.app.Controllers.Api
{
    [RoutePrefix("api/serie")]
    public class SerieController : ApiController
    {
        SerieBl serieBl = new SerieBl();

        [HttpGet]
        [Route("listar-serie-por-tipocomprobante")]
        public List<SerieBe> ListarSeriePorTipoComprobante(int tipoComprobanteId)
        {
            return serieBl.ListarSeriePorTipoComprobante(tipoComprobanteId);
        }
    }
}
