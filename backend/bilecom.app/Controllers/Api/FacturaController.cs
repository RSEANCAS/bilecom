using bilecom.app.Helper;
using bilecom.be;
using bilecom.be.Custom;
using bilecom.bl;
using bilecom.sunat;
using bilecom.sunat.comprobante.invoice;
using bilecom.ut;
using Newtonsoft.Json;
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
        EmpresaBl empresaBl = new EmpresaBl();
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
            int? facturaId = null, nroComprobante = null;
            DateTime? fechaHoraEmision = null;
            bool respuesta = facturaBl.GuardarFactura(registro, out facturaId, out nroComprobante, out fechaHoraEmision);

            //if (respuesta)
            //{
            //    var cookieSS = Request.Headers.GetCookies("ss").FirstOrDefault();

            //    var user = JsonConvert.DeserializeObject<dynamic>(cookieSS["ss"].Value);

            //    int empresaId = user.Usuario.Empresa.EmpresaId;

            //    registro.Empresa = empresaBl.ObtenerEmpresa(empresaId, withUbigeo: true);

            //    string rucSOL = AppSettings.Get<string>("Sunat.RucSOL");
            //    string usuarioSOL = AppSettings.Get<string>("Sunat.UsuarioSOL");
            //    string claveSOL = AppSettings.Get<string>("Sunat.ClaveSOL");
            //    string rutaCertificado = AppSettings.Get<string>("Sunat.RutaCertificado");
            //    string claveCertificado = AppSettings.Get<string>("Sunat.ClaveCertificado");

            //    InvoiceType invoiceType = ComprobanteSunat.ObtenerComprobante(registro, ComprobanteSunat.VersionUBL._2_1);

            //    string contenidoXML = Generar.GenerarXML(invoiceType);
            //}
            return respuesta;
        }
    }
}
