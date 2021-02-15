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
        [Route("obtener-producto")]
        public ProductoBe ObtenerProducto(int empresaId, int productoId)
        {
            return new ProductoBl().Obtener(empresaId, productoId);
        }

        [HttpGet]
        [Route("buscar-producto")]
        public DataPaginate<ProductoBe> BuscarProducto(int empresaId, string nombre, string categoriaNombre, int draw, int start, int length, string columnaOrden = "ProductoId", string ordenMax = "ASC")
        {
            int totalRegistros = 0;
            var lista = productoBl.BuscarProducto(categoriaNombre, nombre, empresaId, start, length, columnaOrden, ordenMax, out totalRegistros);
            var respuesta = new DataPaginate<ProductoBe>
            {
                data = lista ?? new List<ProductoBe>(),
                draw = draw,
                recordsFiltered = totalRegistros,
                recordsTotal = totalRegistros
            };
            return respuesta;

        }

        [HttpGet]
        [Route("buscar-producto-por-codigo")]
        public List<ProductoBe> BuscarProductoPorCodigo(int empresaId, int? tipoProductoId, string codigo, int sedeAlmacenId = 0)
        {
            var respuesta = productoBl.BuscarProductoPorCodigo(tipoProductoId, codigo, empresaId, sedeAlmacenId);
            return respuesta;
        }

        [HttpGet]
        [Route("buscar-producto-por-nombre")]
        public List<ProductoBe> BuscarProductoPorNombre(int empresaId, int? tipoProductoId, string nombre, int sedeAlmacenId=0)
        {
            var respuesta = productoBl.BuscarProductoPorNombre(tipoProductoId, nombre, empresaId, sedeAlmacenId);
            return respuesta;
        }

        [HttpPost]
        [Route("eliminar-producto")]
        public bool EliminarProducto(int empresaId, int productoId, string Usuario)
        {
            bool respuesta = productoBl.EliminarProducto(empresaId, productoId, Usuario);
            return respuesta;
        }
    }
}
