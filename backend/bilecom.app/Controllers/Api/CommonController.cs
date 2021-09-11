using bilecom.be.Custom;
using bilecom.bl;
using bilecom.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static bilecom.enums.Enums;

namespace bilecom.app.Controllers.Api
{
    [RoutePrefix("api/common")]
    public class CommonController : ApiController
    {
        CommonBl commonBl = new CommonBl();
        FacturaBl facturaBl = new FacturaBl();
        BoletaBl boletaBl = new BoletaBl();

        [HttpGet]
        [Route("listar-enum-tipo-sede")]
        public List<dynamic> ListarEnumTipoSede()
        {
            List<dynamic> respuesta = Enum<TipoSede>.GetCollection().ToList();

            return respuesta;
        }

        [HttpGet]
        [Route("buscar-comprobante-venta")]
        public List<ComprobanteCustom> BuscarComprobanteVenta(int empresaId, int ambienteSunatId, int tipoComprobanteId, int serieId, string nroComprobante/*, string clienteRazonSocial = null*/)
        {
            List<ComprobanteCustom> respuesta = commonBl.BuscarComprobanteVenta(empresaId, ambienteSunatId, tipoComprobanteId, serieId, nroComprobante/*, clienteRazonSocial*/);

            return respuesta;
        }

        [HttpGet]
        [Route("obtener-comprobante-venta")]
        public dynamic ObtenerComprobanteVenta(int empresaId, int tipoComprobanteId, int comprobanteId)
        {
            object data = null;

            switch ((TipoComprobante)tipoComprobanteId)
            {
                case TipoComprobante.Boleta:
                    data = boletaBl.ObtenerBoleta(empresaId, comprobanteId, conCliente: true, conDetalle: true);
                    break;
                case TipoComprobante.Factura:
                    data = facturaBl.ObtenerFactura(empresaId, comprobanteId, conCliente: true, conDetalle: true);
                    break;
            }

            return data;
        }
    }
}
