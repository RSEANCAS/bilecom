using Pechkin;
using Pechkin.Synchronized;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.ut
{
    public class HtmlToPdf
    {

        public static byte[] GetSelectPdf(string htmlString, PdfPageSize pdfPageSize, string Titulo = "", string Autor = "", string Asunto = "", string PalabraClave = "")
        {
            byte[] PDFBytes = null;
            try
            {
                SelectPdf.HtmlToPdf htmlToPdf = new SelectPdf.HtmlToPdf();

                htmlToPdf.Options.PdfPageSize = pdfPageSize;
                htmlToPdf.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
                htmlToPdf.Options.AutoFitWidth = HtmlToPdfPageFitMode.ShrinkOnly;
                htmlToPdf.Options.WebPageWidth = 1000;
                htmlToPdf.Options.WebPageHeight = 0;
                htmlToPdf.Options.MarginLeft = 0;
                htmlToPdf.Options.MarginRight = 0;
                htmlToPdf.Options.MarginTop = 0;
                htmlToPdf.Options.MarginBottom = 0;

                htmlToPdf.Options.PdfDocumentInformation.Title = Titulo;
                htmlToPdf.Options.PdfDocumentInformation.Author = Autor;
                htmlToPdf.Options.PdfDocumentInformation.CreationDate = DateTime.Now;
                htmlToPdf.Options.PdfDocumentInformation.Subject = Asunto;
                htmlToPdf.Options.PdfDocumentInformation.Keywords = PalabraClave;
                //htmlToPdf.Options.JavaScriptEnabled = true;
                htmlToPdf.Options.RenderingEngine = RenderingEngine.WebKitRestricted;
                //htmlToPdf.Options.RenderPageOnTimeout = true;
                //htmlToPdf.Options.MinPageLoadTime = 2;
                //htmlToPdf.Options.CssMediaType = HtmlToPdfCssMediaType.Screen;
                SelectPdf.PdfDocument pdf = htmlToPdf.ConvertHtmlString(htmlString);

                MemoryStream ms = new MemoryStream();
                pdf.Save(ms);
                pdf.Close();

                PDFBytes = ms.ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return PDFBytes;
        }

        public static byte[] GenerarPDF(string html)
        {
            byte[] pdfBuffer = new SimplePechkin(new GlobalConfig()).Convert(new Pechkin.ObjectConfig().SetScreenMediaType(true), html);

            return pdfBuffer;

        }
        //public void prueba()
        //{
        //    string archivo = File.ReadAllText(@"C:/Users/Usuario01/Downloads/templateFactura.html");
        //    byte[] array = null;

        //    array = GetSelectPdf(archivo);
        //    Directory.CreateDirectory($"D:\\prueba");
        //    File.WriteAllBytes($"D:\\prueba\\prueba.pdf", array);
        //}

    }
}
