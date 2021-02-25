using bilecom.app.Helper;
using bilecom.be;
using bilecom.be.Custom;
using bilecom.bl;
using bilecom.enums;
using bilecom.sunat;
using bilecom.sunat.comprobante.invoice;
using bilecom.ut;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using static bilecom.enums.Enums;

namespace bilecom.app.Controllers.Api
{
    [RoutePrefix("api/factura")]
    public class FacturaController : ApiController
    {
        EmpresaBl empresaBl = new EmpresaBl();
        FacturaBl facturaBl = new FacturaBl();

        Emitir emitir = new Emitir();

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

            if (respuesta)
            {
                try
                {

                if (facturaId.HasValue) registro.FacturaId = facturaId.Value;
                if (fechaHoraEmision.HasValue) registro.FechaHoraEmision = fechaHoraEmision.Value;
                if (nroComprobante.HasValue) registro.NroComprobante = nroComprobante.Value;
                var cookieSS = Request.Headers.GetCookies("ss").FirstOrDefault();

                var user = JsonConvert.DeserializeObject<dynamic>(cookieSS["ss"].Value);

                int empresaId = user.Usuario.Empresa.EmpresaId;

                registro.Empresa = empresaBl.ObtenerEmpresa(empresaId, withUbigeo: true);

                string rucSOL = AppSettings.Get<string>("Sunat.RucSOL");
                string usuarioSOL = AppSettings.Get<string>("Sunat.UsuarioSOL");
                string claveSOL = AppSettings.Get<string>("Sunat.ClaveSOL");
                string rutaCertificado = AppSettings.Get<string>("Sunat.RutaCertificado").Replace(@"~\", AppDomain.CurrentDomain.BaseDirectory);
                string claveCertificado = AppSettings.Get<string>("Sunat.ClaveCertificado");

                InvoiceType invoiceType = ComprobanteSunat.ObtenerComprobante(registro, ComprobanteSunat.VersionUBL._2_1);

                string contenidoXml = Generar.GenerarXML(invoiceType);
                string hash = null;
                string contenidoXmlFirmado = Generar.RetornarXmlFirmado(contenidoXml, rutaCertificado, claveCertificado, out hash);
                byte[] contenidoXmlFirmadoBytes = Convert.FromBase64String(contenidoXmlFirmado);
                //byte[] contenidoXmlFirmadoBytes = Encoding.UTF8.GetBytes(contenidoXmlFirmado);
                string nombreArchivo = $"{registro.Empresa.Ruc}-01-{registro.Serie.Serial}-{registro.NroComprobante:00000000}";
                string nombreArchivoXml = $"{nombreArchivo}.xml";
                string nombreArchivoPdf = $"{nombreArchivo}.pdf";
                string nombreArchivoZip = $"{nombreArchivo}.zip";
                string nombreArchivoCdr = $"R-{nombreArchivo}.zip";
                byte[] contenidoZipBytes = Generar.RetornarXmlComprimido(contenidoXmlFirmadoBytes, nombreArchivoXml);
                //byte[] contenidoZipBytes = Generar.RetornarXmlComprimido(contenidoXmlFirmado, nombreArchivoXml);
                string codigoCdr = null, descripcionCdr = null;
                EstadoCdr? estadoCdr = null;
                byte[] cdrBytes = null;

                bool seEmitio = emitir.Venta("https://e-beta.sunat.gob.pe/ol-ti-itcpfegem-beta/billService", nombreArchivoZip, contenidoZipBytes, rucSOL, usuarioSOL, claveSOL, out cdrBytes, out codigoCdr, out descripcionCdr, out estadoCdr);

                string rutaCarpetaSunatComprobantesBase = AppSettings.Get<string>("Empresa.Almacenamiento.Sunat.Comprobantes");
                string rutaCarpetaSunatComprobantes = rutaCarpetaSunatComprobantesBase
                    .Replace(@"~\", AppDomain.CurrentDomain.BaseDirectory)
                    .Replace("{Ruc}", registro.Empresa.Ruc)
                    .Replace("{TipoComprobante}", TipoComprobante.Factura.GetAttributeOfType<DefaultValueAttribute>().Value.ToString())
                    .Replace("{Comprobante}", $"{registro.Serie.Serial}-{registro.NroComprobante:00000000}");
                string rutaArchivoXml = Path.Combine(rutaCarpetaSunatComprobantes, nombreArchivoXml);
                string rutaArchivoCdr = Path.Combine(rutaCarpetaSunatComprobantes, nombreArchivoCdr);
                string rutaArchivoPdf = Path.Combine(rutaCarpetaSunatComprobantes, nombreArchivoPdf);

                bool existeCarpeta = Directory.Exists(rutaCarpetaSunatComprobantes);

                if (!existeCarpeta) Directory.CreateDirectory(rutaCarpetaSunatComprobantes);
                File.WriteAllBytes(rutaArchivoXml, contenidoXmlFirmadoBytes);

                if (seEmitio)
                {

                    File.WriteAllBytes(rutaArchivoCdr, cdrBytes);
                    //File.WriteAllBytes(rutaArchivoPdf, contenidoXmlFirmadoBytes);

                    registro.CodigoRespuestaSunat = codigoCdr;
                    registro.DescripcionRespuestaSunat = descripcionCdr;
                    registro.EstadoIdRespuestaSunat = estadoCdr.HasValue ? (int?)estadoCdr.Value : null;
                    registro.RutaXml = rutaArchivoXml;
                    registro.RutaCdr = rutaArchivoCdr;

                    bool seGuardoRespuestaSunat = facturaBl.GuardarRespuestaSunatFactura(registro);
                    }
                }
                catch (Exception ex)
                {
                    string rutaLog = AppSettings.Get<string>("Log.Ruta")
                        .Replace("~", AppDomain.CurrentDomain.BaseDirectory)
                        .Replace("{Fecha}", DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss"));

                    string[] errors = new string[] { "Message: " + ex.Message, "StackTrace: " + ex.StackTrace };

                    File.WriteAllText(rutaLog, string.Join(Environment.NewLine, errors));

                }
            }
            return respuesta;
        }

        [HttpPut]
        [Route("anular-factura")]
        public bool AnularFactura(FacturaBe registro)
        {
            bool respuesta = facturaBl.AnularFactura(registro);
            return respuesta;
        }
    }
}