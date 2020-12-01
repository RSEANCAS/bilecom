﻿using bilecom.be;
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
        [Route("Listar/{empresaId}/{nroDocumentoIdentidad}/{nombresCompletos}")]
        public List<ProductoBe> Listar(int empresaId, string nombre, string categoriaNombre)
        {
            return new ProductoBl().Listar(categoriaNombre, nombre, empresaId);
        }
    }
}