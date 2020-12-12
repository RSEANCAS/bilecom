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
    [RoutePrefix("api/producto")]
    public class ProductoController : ApiController
    {
        ProductoBl productoBl = new ProductoBl();

        [HttpPost]
        [Route("guardar-producto")]
        public bool GuardarProducto(ProductoBe registro)
        {
            bool respuesta = productoBl.ProductoGuardar(registro);
            return respuesta;
        }
        [HttpGet]
        [Route("buscar-producto")]
        public DataPaginate<ProductoBe> BuscarProducto(int empresaId, string nombre, string categoriaNombre, int draw, int start, int length, string columnaOrden = "ProductoId", string ordenMax = "ASC")
        {
            int totalRegistros = 0;
            var lista = productoBl.BuscarProducto(categoriaNombre, nombre, empresaId, start, length, columnaOrden, ordenMax, out totalRegistros);
            var respuesta = new DataPaginate<ProductoBe>
            {
                data = lista,
                draw = draw,
                recordsFiltered = totalRegistros,
                recordsTotal = totalRegistros
            };
            return respuesta;

        }
    }
}
