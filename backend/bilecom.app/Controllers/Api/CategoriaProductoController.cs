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
        [HttpPost]
        [Route("guardar")]
        public bool Guardar(CategoriaProductoBe categoriaProducto)
        {
            bool respuesta = false;
            try
            {
                respuesta = new CategoriaProductoBl().CategoriaProductoGuardar(categoriaProducto);
            }
            catch(Exception ex)
            {
                respuesta = false;
            }
            return respuesta;
        }

        public bool Actualizar(CategoriaProductoBe categoriaProductoBe)
        {
            bool respuesta = false;
            try
            {
                respuesta = new CategoriaProductoBl().CategoriaProductoActualizar(categoriaProductoBe);
            }
            catch (Exception ex)
            {
                respuesta = false;
            }
            return respuesta;
        }
        [HttpGet]
        [Route("listar")]
        public DataPaginate<CategoriaProductoBe> Listar(int empresaId, string nombre, int draw, int start, int length, string columnaOrden = "CategoriaProductoId", string ordenMax = "ASC")
        {
            int totalRegistros = 0;
                var lista =  new CategoriaProductoBl().Listar(empresaId, nombre, start, length, columnaOrden, ordenMax, out totalRegistros);
            return new DataPaginate<CategoriaProductoBe>
            {
                data = lista,
                draw = draw,
                recordsFiltered = totalRegistros,
                recordsTotal = totalRegistros
            };
        }
    }
}
