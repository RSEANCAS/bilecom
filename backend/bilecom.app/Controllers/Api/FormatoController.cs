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
    [RoutePrefix("api/formato")]
    public class FormatoController : ApiController
    {
        FormatoBl formatoBl = new FormatoBl();

        [HttpGet]
        [Route("listar-formato-por-tipocomprobante")]
        public List<FormatoBe> ListarFormatoPorTipoComprobante(int tipoComprobanteId)
        {
            return formatoBl.ListarFormatoPorTipoComprobante(tipoComprobanteId);
        }
    }
}
