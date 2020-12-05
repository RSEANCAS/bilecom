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
        [HttpPost]
        [Route("guardar")]
        public bool ProductoGuardar(ProductoBe productoBe)
        {
            bool respuesta = false;
            try
            {
                respuesta = new ProductoBl().ProductoGuardar(productoBe);
            }
            catch (Exception ex)
            {
                respuesta = false;
            }
            return respuesta;
        }
        public bool ProductoActualizar(ProductoBe productoBe)
        {
            //ProductoBl p = new ProductoBl();
            bool respuesta = false;
            try
            {
                respuesta = new ProductoBl().ProductoActualizar(productoBe);
            }
            catch (Exception ex)
            {
                respuesta = false;
            }
            return respuesta;
        }
        [HttpGet]
        [Route("listar")]
        public DataPaginate<ProductoBe> Listar(int empresaId, string nombre, string categoriaNombre, int draw, int start, int length, string columnaOrden = "ProductoId", string ordenMax = "ASC")
        {
            int totalRegistros = 0;
            var lista = new ProductoBl().Listar(categoriaNombre, nombre, empresaId, start, length, columnaOrden, ordenMax, out totalRegistros);
            return new DataPaginate<ProductoBe>{
                data = lista,
                draw = draw,
                recordsFiltered = totalRegistros,
                recordsTotal = totalRegistros
            };

        }
    }
}
