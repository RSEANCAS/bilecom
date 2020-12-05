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
    [RoutePrefix("api/proveedor")]
    public class ProveedorController : ApiController
    {
        [HttpGet]
        [Route("listar")]
        public DataPaginate<ProveedorBe> Listar(int empresaId, string nroDocumentoIdentidad, string razonSocial, int draw, int start, int length, string columnaOrden = "ProveedorId", string ordenMax = "ASC")
        {
            int totalRegistros = 0;
            var lista= new ProveedorBl().Listar(empresaId, nroDocumentoIdentidad, razonSocial, start, length, columnaOrden, ordenMax, out totalRegistros);
            return new DataPaginate<ProveedorBe>
            {
                data = lista,
                draw = draw,
                recordsFiltered = totalRegistros,
                recordsTotal = totalRegistros
            };
        }
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
