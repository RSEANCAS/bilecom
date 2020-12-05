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
        [HttpGet]
        [Route("listar")]
        public DataPaginate<PersonalBe> Listar (int empresaId, string nroDocumentoIdentidad, string nombresCompletos, int draw, int start, int length, string columnaOrden="PersonalId", string ordenMax="ASC")
        {
            int totalRegistros = 0;
                var lista = new PersonalBl().Listar(empresaId, nroDocumentoIdentidad, nombresCompletos, start, length, columnaOrden, ordenMax, out totalRegistros);
            return new DataPaginate<PersonalBe>
            {
                data = lista,
                draw = draw,
                recordsFiltered = totalRegistros,
                recordsTotal = totalRegistros
            };
        }
        [HttpPost]
        [Route("guardar")]
        public bool Guardar(PersonalBe personalBe)
        {
            bool respuesta = false;
            try
            {
                respuesta = new PersonalBl().PersonalGuardar(personalBe);
            }
            catch (Exception ex)
            {
                respuesta = false;
            }
            return respuesta;
        }
    }
}
