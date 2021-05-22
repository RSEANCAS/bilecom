using bilecom.bl;
using bilecom.be;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace bilecom.app.Controllers.Api
{
    [RoutePrefix("api/tablero")]
    public class TableroUltimosDocumentosController : ApiController
    {
        TableroUltimosDocumentosBl bl = new TableroUltimosDocumentosBl(); 

        [HttpGet]
        [Route("listar-tableroultimosdocumentos")]
        public List<TableroUltimosDocumentosBe> Listar(int empresaId,int cantidadRegistros = 10)
        {
            var lista = bl.Listar(empresaId,cantidadRegistros);
            return lista ?? new List<TableroUltimosDocumentosBe>();
        }
    }
}
