using bilecom.bl;
using bilecom.be;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using bilecom.be.Custom;

namespace bilecom.app.Controllers.Api
{
    [RoutePrefix("api/stockalmacen")]
    public class StockAlmacenController : ApiController
    {
        StockAlmacenBl stockAlmacenBl= new StockAlmacenBl();
        [HttpGet]
        [Route("buscar-stockalmacen")]
        public DataPaginate<StockAlmacenBe> BuscarPersonal(int empresaId, int almacenId, int filtro, int draw, int start, int length, string columnaOrden = "ProductoId", string ordenMax = "ASC")
        {
            int totalRegistros = 0;
            var lista = stockAlmacenBl.Buscar(empresaId, almacenId, filtro, start, length, columnaOrden, ordenMax, out totalRegistros);
            var respuesta = new DataPaginate<StockAlmacenBe>
            {
                data = lista ?? new List<StockAlmacenBe>(),
                draw = draw,
                recordsFiltered = totalRegistros,
                recordsTotal = totalRegistros
            };
            return respuesta;
        }
    }
}
