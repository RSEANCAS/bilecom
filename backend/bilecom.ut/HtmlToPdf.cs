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
        public byte[] GetSelectPdf(string UrlString, string BaseUrl, bool Baja = false, string Titulo = "", string Autor = "", string Asunto = "", string PalabraClave = "")
        {
            byte[] PDFBytes = null;
            try
            {
                SelectPdf.HtmlToPdf htmlToPdf = new SelectPdf.HtmlToPdf();

                htmlToPdf.Options.PdfPageSize = SelectPdf.PdfPageSize.A4;
                htmlToPdf.Options.AutoFitWidth = SelectPdf.HtmlToPdfPageFitMode.AutoFit;
                htmlToPdf.Options.WebPageWidth = 0;
                htmlToPdf.Options.WebPageHeight = 0;
                htmlToPdf.Options.MarginLeft = 45;
                htmlToPdf.Options.MarginRight = 45;
                htmlToPdf.Options.MarginTop = 40;
                htmlToPdf.Options.MarginBottom = 40;

                htmlToPdf.Options.PdfDocumentInformation.Title = Titulo;
                htmlToPdf.Options.PdfDocumentInformation.Author = Autor;
                htmlToPdf.Options.PdfDocumentInformation.CreationDate = DateTime.Now;
                htmlToPdf.Options.PdfDocumentInformation.Subject = Asunto;
                htmlToPdf.Options.PdfDocumentInformation.Keywords = PalabraClave;

                SelectPdf.PdfDocument pdf = htmlToPdf.ConvertHtmlString(UrlString, BaseUrl);

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
        public void prueba()
        {
            string archivo = File.ReadAllText(@"C:/Users/Usuario01/Downloads/templateFactura.html");
            byte[] array = null;

            array = GetSelectPdf(archivo, "prueba");
            Directory.CreateDirectory($"D:\\prueba");
            File.WriteAllBytes($"D:\\prueba\\prueba.pdf", array);
        }

    }
}
