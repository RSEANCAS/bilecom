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
        [httpPost]
        [Route("guardar")]

        public bool CotizacionGuardar(CotizacionBe cotizacion, out int? cotizacionId)
        {
            //Cada parámetro que tiene un "out int?" tiene que que inicializarse con nulo, porque el "int?" acepta valores nulos
            cotizacionId = null;
            bool respuesta = false;
            try
            {
                respuesta = new CotizacionBl().CotizacionGuardar(cotizacion, out cotizacionId);
            }
            catch (Exception ex)
            {
                respuesta = false;
            }
            return respuesta;
        }

    }
}
