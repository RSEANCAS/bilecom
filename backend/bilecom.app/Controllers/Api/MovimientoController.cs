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
    [RoutePrefix("api/movimiento")]
    public class MovimientoController : ApiController
    {
        MovimientoBl movimientoBl= new MovimientoBl();
        [HttpGet]
        [Route("buscar-movimiento")]
        public DataPaginate<MovimientoBe> Buscar(int empresaId, string nombresCompletosPersonal, string razonSocialCliente, DateTime fechaEmisionDesde, DateTime fechaEmisionHasta, int draw, int start, int length, string columnaOrden = "MovimientoId", string ordenMax = "ASC")
        {
            int totalRegistros = 0;
            var lista = movimientoBl.Buscar(empresaId, nombresCompletosPersonal, razonSocialCliente, fechaEmisionDesde, fechaEmisionHasta, start, length, columnaOrden, ordenMax, out totalRegistros);
            var respuesta = new DataPaginate<MovimientoBe>
            {
                data = lista ?? new List<MovimientoBe>(),
                draw = draw,
                recordsFiltered = totalRegistros,
                recordsTotal = totalRegistros
            };
            return respuesta;
        }

        [HttpGet]
        [Route("obtener-movimiento")] 
        public MovimientoBe ObtenerCotizacion(int empresaId, int movimientoId)
        {
            return movimientoBl.Obtener(empresaId, movimientoId, conCliente: true, conPersonal: true, conListaDetalleMovimiento: true, conProveedor: true);
        }

        [HttpPost]
        [Route("guardar-movimiento")]
        public bool Guardar(MovimientoBe registro)
        {
            //Cada parámetro que tiene un "out int?" tiene que que inicializarse con nulo, porque el "int?" acepta valores nulos
            bool respuesta = movimientoBl.Guardar(registro);
            return respuesta;
        }
    }
}
