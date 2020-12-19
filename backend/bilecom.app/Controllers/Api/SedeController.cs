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
    [RoutePrefix("api/sede")]
    public class SedeController : ApiController
    {
        SedeBl sedeBl = new SedeBl();

        [HttpGet]
        [Route("buscar-sede")]
        public DataPaginate<SedeBe> BuscarSede(int empresaId, string nombre, int draw, int start, int length, string columnaOrden = "SedeId", string ordenMax = "ASC")
        {
            int totalRegistros = 0;
            var lista = sedeBl.BuscarSede(empresaId, nombre, start, length, columnaOrden, ordenMax, out totalRegistros);
            var respuesta = new DataPaginate<SedeBe>
            {
                data = lista ?? new List<SedeBe>(),
                draw = draw,
                recordsFiltered = totalRegistros,
                recordsTotal = totalRegistros
            };
            return respuesta;
        }
        [HttpGet]
        [Route("obtener-sede")]
        public SedeBe ObtenerSede(int empresaId, int sedeId)
        {
            return sedeBl.Obtener(empresaId, sedeId);
        }

        [HttpPost]
        [Route("guardar-sede")]
        public bool GuardarSede(SedeBe sedeBe)
        {
            return sedeBl.Guardar(sedeBe);
        }
    }
}
