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
    [RoutePrefix("api/cotizacion")]
    public class CotizacionController : ApiController
    {
        CotizacionBl cotizacionBl = new CotizacionBl();

        [HttpPost]
        [Route("guardar-cotizacion")]

        public bool GuardarCotizacion(CotizacionBe registro)
        {
            //Cada parámetro que tiene un "out int?" tiene que que inicializarse con nulo, porque el "int?" acepta valores nulos
            bool respuesta = cotizacionBl.GuardarCotizacion(registro);
            return respuesta;
        }

    }
}
