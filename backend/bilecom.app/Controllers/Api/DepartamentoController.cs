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
    [RoutePrefix("api/departamento")]
    public class DepartamentoController : ApiController
    {
        DepartamentoBl departamentoBl = new DepartamentoBl();

        [HttpGet]
        [Route("listar-departamento")]
        public List<DepartamentoBe> ListarDepartamento()
        {
            return departamentoBl.ListarDepartamento();
        }

    }
}
