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
        ProveedorBl proveedorBl = new ProveedorBl();

        [HttpGet]
        [Route("buscar-proveedor")]
        public DataPaginate<ProveedorBe> BuscarProveedor(int empresaId, string nroDocumentoIdentidad, string razonSocial, int draw, int start, int length, string columnaOrden = "ProveedorId", string ordenMax = "ASC")
        {
            int totalRegistros = 0;
            var lista= proveedorBl.BuscarProveedor(empresaId, nroDocumentoIdentidad, razonSocial, start, length, columnaOrden, ordenMax, out totalRegistros);
            var respuesta = new DataPaginate<ProveedorBe>
            {
                data = lista ?? new List<ProveedorBe>(),
                draw = draw,
                recordsFiltered = totalRegistros,
                recordsTotal = totalRegistros
            };
            return respuesta;
        }

        [HttpGet]
        [Route("obtener-proveedor")]
        public ProveedorBe ObtenerProveedor(int empresaId, int proveedorId)
        {
            return proveedorBl.Obtener(empresaId, proveedorId);
        }


        [HttpPost]
        [Route("guardar-proveedor")]
        public bool GuardarProveedor(ProveedorBe registro)
        {
            bool respuesta = proveedorBl.ProveedorGuardar(registro);
            return respuesta;
        }

    }
}
