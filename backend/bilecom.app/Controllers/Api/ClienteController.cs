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
        ClienteBl clienteBl = new ClienteBl();

        [HttpGet]
        [Route("buscar-cliente")]
        public DataPaginate<ClienteBe> BuscarCliente(int empresaId, string nroDocumentoIdentidad, string razonSocial, int draw, int start, int length, string columnaOrden = "ClienteId", string ordenMax = "ASC")
        {
            int totalRegistros = 0;
            var lista = clienteBl.BuscarCliente(empresaId, nroDocumentoIdentidad, razonSocial, start, length, columnaOrden, ordenMax, out totalRegistros);
            var respuesta = new DataPaginate<ClienteBe>
            {
                data = lista ?? new List<ClienteBe>(),
                draw = draw,
                recordsFiltered = totalRegistros,
                recordsTotal = totalRegistros
            };

            return respuesta;
        }
        
        [HttpGet]
        [Route("obtener-cliente")]
        public ClienteBe ObtenerCliente(int empresaId, int clienteId)
        {
            return clienteBl.Obtener(empresaId, clienteId);
        }


        [HttpPost]
        [Route("guardar-cliente")]
        public int GuardarCliente(ClienteBe registro)
        {
            int respuesta = clienteBl.GuardarCliente(registro);
            return respuesta;
        }

        [HttpPost]
        [Route("eliminar-cliente")]
        public bool EliminarCliente(int empresaId,int clienteId, string Usuario)
        {
            bool respuesta = clienteBl.EliminarCliente(empresaId,clienteId,Usuario);
            return respuesta;
        }
    }
}
