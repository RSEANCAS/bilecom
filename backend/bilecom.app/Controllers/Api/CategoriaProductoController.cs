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
    [RoutePrefix("api/categoriaproducto")]
    public class CategoriaProductoController : ApiController
    {
        CategoriaProductoBl categoriaprodcutoBl = new CategoriaProductoBl();

        [HttpPost]
        [Route("guardar-categoriaproducto")]
        public bool GuardarCategoriaProducto(CategoriaProductoBe categoriaProducto)
        {
            bool respuesta = new CategoriaProductoBl().CategoriaProductoGuardar(categoriaProducto);
            return respuesta;
        }

        [HttpGet]
        [Route("listar-categoriaproducto")]
        public List<CategoriaProductoBe> ListarCategoriaProducto(int empresaId)
        {
            return new CategoriaProductoBl().ListarCategoriaProducto(empresaId);
        }

        [HttpGet]
        [Route("obtener-categoriaproducto")]
        public CategoriaProductoBe ObtenerCategoriaProducto(int empresaId,int categoriaproductoId)
        {
            return new CategoriaProductoBl().Obtener(empresaId, categoriaproductoId);
        }

        [HttpGet]
        [Route("buscar-categoriaproducto")]
        public DataPaginate<CategoriaProductoBe> BuscarCategoriaProducto(int empresaId, string nombre, int draw, int start, int length, string columnaOrden = "CategoriaProductoId", string ordenMax = "ASC")
        {
            int totalRegistros = 0;
            var lista = new CategoriaProductoBl().BuscarCategoriaProducto(empresaId, nombre, start, length, columnaOrden, ordenMax, out totalRegistros);
            var respuesta = new DataPaginate<CategoriaProductoBe>
            {
                data = lista ?? new List<CategoriaProductoBe>(),
                draw = draw,
                recordsFiltered = totalRegistros,
                recordsTotal = totalRegistros
            };
            return respuesta;
        }

        [HttpPost]
        [Route("eliminar-categoriaproducto")]
        public bool EliminarCategoriaProducto(int empresaId, int categoriaproductoId, string Usuario)
        {
            bool respuesta = categoriaprodcutoBl.EliminarCategoriaProducto(empresaId, categoriaproductoId, Usuario);
            return respuesta;
        }
    }
}
