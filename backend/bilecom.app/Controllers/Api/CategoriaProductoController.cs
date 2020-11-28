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
    [RoutePrefix("api/categoriaproducto")]
    public class CategoriaProductoController : ApiController
    {
        public bool Guardar(CategoriaProductoBe categoriaProductoBe)
        {
            bool respuesta = false;
            try
            {
                respuesta = new CategoriaProductoBl().CategoriaProductoGuardar(categoriaProductoBe);
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
        [Route("Listar/{empresaId}/{nombre}")]
        public List<CategoriaProductoBe> Listar(int empresaId, string nombre)
        {
            return new CategoriaProductoBl().Listar(empresaId, nombre);
        }
    }
}
