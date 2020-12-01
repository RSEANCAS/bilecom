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
    [RoutePrefix("api/proveedor")]
    public class ProveedorController : ApiController
    {
        [HttpPost]
        [Route("guardar")]
        public bool Guardar(ProveedorBe proveedorBe)
        {
            bool respuesta = false;
            try
            {
                respuesta = new ProveedorBl().ProveedorGuardar(proveedorBe);
            }
            catch (Exception ex)
            {
                respuesta = false;
            }
            return respuesta;
        }

    }
}
