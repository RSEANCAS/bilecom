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
    [RoutePrefix("api/cliente")]
    public class ClienteController : ApiController
    {
        [HttpGet]
        [Route("listar")]
        public DataPaginate<ClienteBe> Listar(int empresaId, string nroDocumentoIdentidad, string razonSocial, int draw, int start, int length, string columnaOrden = "ClienteId", string ordenMax = "ASC")
        {
            int totalRegistros = 0;
            var lista = new ClienteBl().Listar(empresaId, nroDocumentoIdentidad, razonSocial, start, length, columnaOrden, ordenMax, out totalRegistros);
            return new DataPaginate<ClienteBe>
            {
                data = lista,
                draw = draw,
                recordsFiltered = totalRegistros,
                recordsTotal = totalRegistros
            };

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
