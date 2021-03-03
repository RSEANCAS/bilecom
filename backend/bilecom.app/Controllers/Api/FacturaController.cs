using bilecom.app.Helper;
using bilecom.be;
using bilecom.be.Custom;
using bilecom.bl;
using bilecom.enums;
using bilecom.sunat;
using bilecom.sunat.comprobante.invoice;
using bilecom.ut;
using Newtonsoft.Json;
using SelectPdf;
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
        EmpresaImagenBl empresaImagenBl = new EmpresaImagenBl();
        FacturaBl facturaBl = new FacturaBl();
        FormatoBl formatoBl = new FormatoBl();

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
            string totalImporteEnLetras = null;
            bool respuesta = facturaBl.GuardarFactura(registro, out facturaId, out nroComprobante, out fechaHoraEmision, out totalImporteEnLetras);

            if (respuesta)
            {
                try
                {

                    if (facturaId.HasValue) registro.FacturaId = facturaId.Value;
                    if (fechaHoraEmision.HasValue) registro.FechaHoraEmision = fechaHoraEmision.Value;
                    if (nroComprobante.HasValue) registro.NroComprobante = nroComprobante.Value;
                    if (totalImporteEnLetras != null) registro.ImporteTotalEnLetras = totalImporteEnLetras;
                    var cookieSS = Request.Headers.GetCookies("ss").FirstOrDefault();

                    var user = JsonConvert.DeserializeObject<dynamic>(cookieSS["ss"].Value);

                    int empresaId = user.Usuario.Empresa.EmpresaId;

                    registro.Empresa = empresaBl.ObtenerEmpresa(empresaId, withUbigeo: true, withConfiguracion: true);

                    List<ColumnasEmpresaImagen> columnasEmpresaImgen = new List<ColumnasEmpresaImagen> { ColumnasEmpresaImagen.LogoFormatoTipoContenido, ColumnasEmpresaImagen.LogoFormato };

                    var empresaImagen = empresaImagenBl.ObtenerDinamico(empresaId, columnasEmpresaImgen);

                    registro.LogoFormatoBase64 = $"data:${empresaImagen.LogoFormatoTipoContenido};base64,{Convert.ToBase64String(empresaImagen.LogoFormato)}";

                    string rucSOL = registro.Empresa.EmpresaConfiguracion.EmpresaAmbienteSunat.RucSOL;
                    string usuarioSOL = registro.Empresa.EmpresaConfiguracion.EmpresaAmbienteSunat.UsuarioSOL;
                    string claveSOL = registro.Empresa.EmpresaConfiguracion.EmpresaAmbienteSunat.ClaveSOL;
                    string rutaCertificado = registro.Empresa.EmpresaConfiguracion.RutaCertificado.Replace(@"~\", AppDomain.CurrentDomain.BaseDirectory);
                    string claveCertificado = registro.Empresa.EmpresaConfiguracion.ClaveCertificado;

                    //string rucSOL = AppSettings.Get<string>("Sunat.RucSOL");
                    //string usuarioSOL = AppSettings.Get<string>("Sunat.UsuarioSOL");
                    //string claveSOL = AppSettings.Get<string>("Sunat.ClaveSOL");
                    //string rutaCertificado = AppSettings.Get<string>("Sunat.RutaCertificado").Replace(@"~\", AppDomain.CurrentDomain.BaseDirectory);
                    //string claveCertificado = AppSettings.Get<string>("Sunat.ClaveCertificado");

                    InvoiceType invoiceType = ComprobanteSunat.ObtenerComprobante(registro, ComprobanteSunat.VersionUBL._2_1);

                    string contenidoXml = Generar.GenerarXML(invoiceType);
                    string hash = null;
                    string contenidoXmlFirmado = Generar.RetornarXmlFirmado(contenidoXml, rutaCertificado, claveCertificado, out hash);
                    registro.Hash = hash;

                    var qr = Generar.GenerarQR(registro.Empresa.Ruc, TipoComprobante.Factura.GetAttributeOfType<DefaultValueAttribute>().Value.ToString(), registro.Serie.Serial, registro.NroComprobante, registro.FechaHoraEmision, registro.Cliente.TipoDocumentoIdentidad.Codigo, registro.Cliente.NroDocumentoIdentidad, registro.TotalIgv, registro.ImporteTotal, registro.Hash);
                    registro.QRBase64 = $"data:image/png;base64,{Convert.ToBase64String(qr)}";

                    var formato = formatoBl.Obtener(registro.FormatoId.Value);

                    string registroJsonString = JsonConvert.SerializeObject(registro);

                    string html = formato.Html
                        .Replace("[LOGO]", registro.LogoFormatoBase64)
                        .Replace("[RUC_EMPRESA]", registro.Empresa.Ruc)
                        .Replace("[SERIE]", registro.Serie.Serial)
                        .Replace("[NUMERO]", registro.NroComprobante.ToString("00000000"))
                        .Replace("[RAZON_SOCIAL]", registro.Cliente.RazonSocial)
                        .Replace("[FECHA_EMISION]", registro.FechaHoraEmision.ToString("dd/MM/yyyy"))
                        .Replace("[RUC]", registro.Cliente.NroDocumentoIdentidad)
                        .Replace("[DIRECCION]", registro.Cliente.Direccion)
                        .Replace("[FECHA_VENCIMIENTO]", registro.FechaVencimiento == null ? "" : registro.FechaVencimiento.Value.ToString("dd/MM/yyyy"))
                        .Replace("[CONDICION_PAGO]", registro.FormaPago.Descripcion)
                        .Replace("[ORDEN_COMPRA]", registro.ListaFacturaOrdenCompraStr)
                        .Replace("[MONEDA]", registro.Moneda.Nombre)
                        .Replace("[DETALLE]", string.Join(Environment.NewLine, registro.ListaFacturaDetalle.Select(x => $"<li class='data row'><span class='text-center'>{x.Fila}</span><span class='text-right'>{x.Cantidad}</span><span>{x.UnidadMedida.Descripcion}</span><span>{x.Descripcion}</span><span class='text-right'>{x.ValorUnitario:0.00}</span><span class='text-right'>{x.Descuento:0.00}</span><span class='text-right'>{x.ValorVenta:0.00}</span><span class='text-right'>{x.PrecioUnitario:0.00}</span><span class='text-right'>{x.PrecioVenta:0.00}</span></li>").ToArray()))
                        .Replace("[SIMBOLO_MONEDA]", registro.Moneda.Simbolo)
                        .Replace("[TOTAL_GRAVADO]", registro.TotalGravado.ToString("0.00"))
                        .Replace("[TOTAL_INAFECTO]", registro.TotalInafecto.ToString("0.00"))
                        .Replace("[TOTAL_EXONERADO]", registro.TotalExonerado.ToString("0.00"))
                        .Replace("[TOTAL_GRATUITO]", registro.TotalGratuito.ToString("0.00"))
                        .Replace("[TOTAL_DESCUENTO]", registro.TotalDescuentos.ToString("0.00"))
                        .Replace("[TOTAL_IGV]", registro.TotalIgv.ToString("0.00"))
                        .Replace("[TOTAL_ISC]", registro.TotalIsc.ToString("0.00"))
                        .Replace("[TOTAL_OTROSCARGOS]", registro.TotalOtrosCargos.ToString("0.00"))
                        .Replace("[TOTAL_OTROSTRIBUTOS]", registro.TotalOtrosTributos.ToString("0.00"))
                        .Replace("[TOTAL_IMPORTETOTAL]", registro.ImporteTotal.ToString("0.00"))
                        .Replace("[BENEFICIARIO]", registro.Empresa.RazonSocial)
                        .Replace("[CUENTA_CORRIENTE]", registro.Empresa.EmpresaConfiguracion.CuentaCorriente)
                        .Replace("[COMENTARIO_LEGAL]", registro.Empresa.EmpresaConfiguracion.ComentarioLegal)
                        .Replace("[OBSERVACIONES]", registro.Observacion)
                        .Replace("[LETRAS_MONTOAPAGAR]", registro.ImporteTotalEnLetras)
                        .Replace("[TOTAL_MONTOAPAGAR]", registro.ImporteTotal.ToString("0.00"))
                        .Replace("[QR]", registro.QRBase64);
                    //.Replace("[DATA]", registroJsonString);
                    byte[] contenidoPdfBytes = ut.HtmlToPdf.GetSelectPdf(html, PdfPageSize.A4);
                    //byte[] contenidoPdfBytes = ut.HtmlToPdf.GenerarPDF(html);

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

                    bool seEmitio = emitir.Venta(registro.Empresa.EmpresaConfiguracion.EmpresaAmbienteSunat.AmbienteSunat.ServicioWebUrlVenta, nombreArchivoZip, contenidoZipBytes, rucSOL, usuarioSOL, claveSOL, out cdrBytes, out codigoCdr, out descripcionCdr, out estadoCdr);

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
                    File.WriteAllBytes(rutaArchivoPdf, contenidoPdfBytes);

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