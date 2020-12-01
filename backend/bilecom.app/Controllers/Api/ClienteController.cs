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
    [RoutePrefix("api/Cliente")]
    public class ClienteController : ApiController
    {
        [HttpGet]
        [Route("Listar/{empresaId}/{nroDocumentoIdentidad}/{razonSocial}")]
        public List<ClienteBe> Listar(int empresaId, string nroDocumentoIdentidad, string razonSocial)
        {
            return new ClienteBl().Listar(empresaId, nroDocumentoIdentidad, razonSocial);
        }
        [HttpPost]
        [Route("guardar")]
        public bool Guardar(ClienteBe clienteBe)
        {
            bool respuesta = false;
            try
            {
                respuesta = new ClienteBl().ClienteGuardar(clienteBe);
            }
            catch (Exception ex)
            {
                respuesta = false;
            }
            return respuesta;
        }
    }
}
