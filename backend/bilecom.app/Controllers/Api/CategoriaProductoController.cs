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
    public class CategoriaProductoController : ApiController
    {
        public bool CategoriaProductoGuardar(CategoriaProductoBe categoriaProductoBe)
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

        public bool CategoriaProductoActualizar(CategoriaProductoBe categoriaProductoBe)
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
    }
}
