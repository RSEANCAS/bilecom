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
    [RoutePrefix("api/tablero")]
    public class TableroConteo_x_DocumentoController : ApiController
    {
        TableroConteo_x_DocumentoBl tableroConteo_X_DocumentoBl = new TableroConteo_x_DocumentoBl();

        [HttpGet]
        [Route("conteo_x_documento-tablero")]
        public TableroConteo_x_DocumentoBe Obtener(int empresaId, int Anyo, int Mes)
        {
            return tableroConteo_X_DocumentoBl.Obtener(empresaId,Anyo,Mes);
        }
    }
}
