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
    [RoutePrefix("api/factura")]
    public class FacturaController : ApiController
    {
        FacturaBl facturaBl = new FacturaBl();

        [HttpGet]
        [Route("buscar-factura")]
        public DataPaginate<FacturaBe> BuscarFactura(int empresaId, string nroDocumentoIdentidadCliente, string razonSocialCliente, DateTime fechaEmisionDesde, DateTime fechaEmisionHasta, int draw, int start, int length, string columnaOrden = "FacturaId", string ordenMax = "ASC")
        {
            int totalRegistros = 0;
            var lista = facturaBl.BuscarFactura(empresaId, nroDocumentoIdentidadCliente, razonSocialCliente, fechaEmisionDesde, fechaEmisionHasta, start, length, columnaOrden, ordenMax, out totalRegistros);
            var respuesta = new DataPaginate<FacturaBe>
            {
                data = lista ?? new List<FacturaBe>(),
                draw = draw,
                recordsFiltered = totalRegistros,
                recordsTotal = totalRegistros
            };
            return respuesta;
        }


        [HttpPost]
        [Route("guardar-factura")]
        public bool GuardarFactura(FacturaBe registro)
        {
            //Cada parámetro que tiene un "out int?" tiene que que inicializarse con nulo, porque el "int?" acepta valores nulos
            bool respuesta = facturaBl.GuardarFactura(registro);
            return respuesta;
        }
    }
}
