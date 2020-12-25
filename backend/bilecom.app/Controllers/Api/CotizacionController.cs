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
    [RoutePrefix("api/cotizacion")]
    public class CotizacionController : ApiController
    {
        CotizacionBl cotizacionBl = new CotizacionBl();

        [HttpGet]
        [Route("buscar-cotizacion")]
        public DataPaginate<CotizacionBe> BuscarCotizacion(int empresaId, string nombresCompletosPersonal, string razonSocialCliente, DateTime fechaEmisionDesde, DateTime fechaEmisionHasta, int draw, int start, int length, string columnaOrden = "CotizacionId", string ordenMax = "ASC")
        {
            int totalRegistros = 0;
            var lista = cotizacionBl.BuscarCotizacion(empresaId, nombresCompletosPersonal, razonSocialCliente, fechaEmisionDesde, fechaEmisionHasta, start, length, columnaOrden, ordenMax, out totalRegistros);
            var respuesta = new DataPaginate<CotizacionBe>
            {
                data = lista ?? new List<CotizacionBe>(),
                draw = draw,
                recordsFiltered = totalRegistros,
                recordsTotal = totalRegistros
            };
            return respuesta;
        }


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
