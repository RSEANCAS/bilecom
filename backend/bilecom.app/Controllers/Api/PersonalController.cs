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
    [RoutePrefix("api/personal")]
    public class PersonalController : ApiController
    {
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
