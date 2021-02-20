using bilecom.sunat.comprobante.creditnote;
using bilecom.sunat.comprobante.debitnote;
using bilecom.sunat.comprobante.invoice;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace bilecom.sunat
{
    public class Generar
    {
        string RetornarXmlFirmado(string xmlString, string rutaCertificado, string claveCertificado, out string hash)
        {
            hash = null;

            XmlDocument documentXml = new XmlDocument();

            documentXml.PreserveWhitespace = true;
            documentXml.LoadXml(xmlString);
            var nodoExtension = documentXml.GetElementsByTagName("ExtensionContent", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2").Item(0);

            if (nodoExtension == null)
            {
                throw new InvalidOperationException("No se pudo encontrar el nodo ExtensionContent en el XML");
            }

            nodoExtension.RemoveAll();

            SignedXml firmado = new SignedXml(documentXml);

            var xmlSignature = firmado.Signature;

            byte[] certificadoByte = File.ReadAllBytes(rutaCertificado);
            X509Certificate2 certificado = new X509Certificate2();
            certificado.Import(certificadoByte, claveCertificado, X509KeyStorageFlags.Exportable);
            firmado.SigningKey = (RSA)certificado.PrivateKey;

            //digest info agregada en la seccion firma
            var env = new XmlDsigEnvelopedSignatureTransform();
            Reference reference = new Reference();
            reference.AddTransform(env);

            reference.Uri = "";
            firmado.AddReference(reference);
            firmado.SignedInfo.SignatureMethod = "http://www.w3.org/2000/09/xmldsig#rsa-sha1";

            reference.DigestMethod = "http://www.w3.org/2000/09/xmldsig#sha1";


            // info para la llave publica 
            KeyInfo keyInfo = new KeyInfo();
            keyInfo.AddClause(new KeyInfoX509Data(certificado));

            xmlSignature.KeyInfo = keyInfo;
            xmlSignature.Id = "SignatureErickOrlando";
            firmado.ComputeSignature();


            // Recuperamos el valor Hash de la firma para este documento.
            if (reference.DigestValue != null)
            {
                hash = Convert.ToBase64String(reference.DigestValue);
            }

            nodoExtension.AppendChild(firmado.GetXml());

            var settings = new XmlWriterSettings() { Encoding = Encoding.GetEncoding("ISO-8859-1") };
            string resultado = String.Empty;
            using (var memDoc = new MemoryStream())
            {

                using (var writer = XmlWriter.Create(memDoc, settings))
                {
                    documentXml.WriteTo(writer);
                }

                resultado = Convert.ToBase64String(memDoc.ToArray());

            }
            return resultado;
        }

        public static string GenerarXML(InvoiceType item)
        {
            //byte[] xmlByte = null;

            XmlSerializer nXmlSerializer = new XmlSerializer(typeof(InvoiceType));
            XmlWriter xtWriter;
            XmlWriterSettings setting = new XmlWriterSettings();
            setting.Indent = true;
            setting.IndentChars = "\t";
            //string RutaDocumento = Path.Combine(configuracionValor.RutaFacturacionElectronica, "BOLETAS");

            //string ArchivoXML = $"{item}"EmpresaRuc + "-" + TipoFactura + "-" + Fac_serie + "-" + NumFactura;
            //string file = Path.Combine(RutaDocumento, string.Format(@"{0}.xml", ArchivoXML));
            //xtWriter = XmlWriter.Create(file, setting);
            StringBuilder xmlString = new StringBuilder();
            xtWriter = XmlWriter.Create(xmlString, setting);
            xtWriter.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"ISO-8859-1\" standalone=\"no\"");
            nXmlSerializer.Serialize(xtWriter, item);
            xtWriter.Close();

            //string text = File.ReadAllText(file);

            //File.WriteAllText(file, text);
            //File.WriteAllText(file, text, System.Text.Encoding.GetEncoding("ISO-8859-1"));
            //return file;


            return xmlString.ToString();
        }

        public byte[] GenerarXML(CreditNoteType item)
        {
            byte[] xmlByte = null;

            return xmlByte;
        }

        public byte[] GenerarXML(DebitNoteType item)
        {
            byte[] xmlByte = null;

            return xmlByte;
        }
    }
}
