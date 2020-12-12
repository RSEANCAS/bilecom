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
    [RoutePrefix("api/personal")]
    //[Authorize]
    public class PersonalController : ApiController
    {
        PersonalBl personalBl = new PersonalBl();

        [HttpGet]
        [Route("buscar-personal")]
        public DataPaginate<PersonalBe> BuscarPersonal(int empresaId, string nroDocumentoIdentidad, string nombresCompletos, int draw, int start, int length, string columnaOrden = "PersonalId", string ordenMax = "ASC")
        {
            int totalRegistros = 0;
            var lista = personalBl.BuscarPersonal(empresaId, nroDocumentoIdentidad, nombresCompletos, start, length, columnaOrden, ordenMax, out totalRegistros);
            var respuesta = new DataPaginate<PersonalBe>
            {
                data = lista,
                draw = draw,
                recordsFiltered = totalRegistros,
                recordsTotal = totalRegistros
            };
            return respuesta;
        }
        [HttpGet]
        [Route("obtener-personal")]
        public PersonalBe ObtenerPersonal(int EmpresaId, int PersonalId)
        {
            return personalBl.ObtenerPersonal(EmpresaId, PersonalId);
        }

        [HttpPost]
        [Route("guardar-personal")]
        public bool GuardarPersonal(PersonalBe registro)
        {
            bool respuesta = personalBl.GuardarPersonal(registro);
            return respuesta;
        }
    }
}
