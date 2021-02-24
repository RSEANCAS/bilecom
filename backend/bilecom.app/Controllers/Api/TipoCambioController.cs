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
    [RoutePrefix("api/tipocambio")]
    public class TipoCambioController : ApiController
    {
        TipoCambioBl tipoCambioBl = new TipoCambioBl();

        [HttpGet]
        [Route("obtener-tipocambio")]
        public TipoCambioBe ObtenerTipoCambioSunat(string fecha)
        {
            return tipoCambioBl.Obtener(fecha);
        }

    }
}
