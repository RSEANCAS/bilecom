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
    [RoutePrefix("api/formapago")]
    public class FormaPagoController : ApiController
    {
        FormaPagoBl formaPagoBl = new FormaPagoBl();

        [HttpGet]
        [Route("listar-formapago")]
        public List<FormaPagoBe> ListarFormaPago()
        {
            return formaPagoBl.ListarFormaPago();
        }
    }
}
