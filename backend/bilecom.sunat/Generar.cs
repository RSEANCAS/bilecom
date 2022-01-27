using bilecom.sunat.comprobante.creditnote;
using bilecom.sunat.comprobante.debitnote;
using bilecom.sunat.comprobante.invoice;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using static bilecom.enums.Enums;

namespace bilecom.sunat
{
    public class Generar
    {
        //public static string RetornarXmlFirmado(string xmlString, string rutaCertificado, string claveCertificado, out string hash)
        //{
        //    hash = null;

        //    XmlDocument documentXml = new XmlDocument();

        //    documentXml.PreserveWhitespace = true;
        //    documentXml.LoadXml(xmlString);
        //    var nodoExtension = documentXml.GetElementsByTagName("ExtensionContent", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2").Item(0);

        //    if (nodoExtension == null)
        //    {
        //        throw new InvalidOperationException("No se pudo encontrar el nodo ExtensionContent en el XML");
        //    }

        //    nodoExtension.RemoveAll();

        //    SignedXml firmado = new SignedXml(documentXml);

        //    var xmlSignature = firmado.Signature;

        //    byte[] certificadoByte = File.ReadAllBytes(rutaCertificado);
        //    X509Certificate2 certificado = new X509Certificate2();
        //    //certificado.Import(certificadoByte, claveCertificado, X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.Exportable);
        //    certificado.Import(certificadoByte, claveCertificado, X509KeyStorageFlags.Exportable);
        //    firmado.SigningKey = certificado.GetRSAPrivateKey();
        //    //firmado.SigningKey = (RSA)certificado.PrivateKey;
        //    //firmado.SigningKey = certificado.PrivateKey;

        //    //digest info agregada en la seccion firma
        //    var env = new XmlDsigEnvelopedSignatureTransform();
        //    Reference reference = new Reference();
        //    reference.AddTransform(env);

        //    reference.Uri = "";
        //    firmado.AddReference(reference);
        //    firmado.SignedInfo.SignatureMethod = "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256";

        //    reference.DigestMethod = "http://www.w3.org/2001/04/xmlenc#sha256";

        //    var keyInfoData = new KeyInfoX509Data(certificado);
        //    keyInfoData.AddSubjectName(certificado.Subject);

        //    // info para la llave publica 
        //    KeyInfo keyInfo = new KeyInfo();
        //    keyInfo.AddClause(keyInfoData);
        //    //keyInfo.sub

        //    xmlSignature.KeyInfo = keyInfo;
        //    xmlSignature.Id = "signatureKG";
        //    firmado.ComputeSignature();


        //    // Recuperamos el valor Hash de la firma para este documento.
        //    if (reference.DigestValue != null)
        //    {
        //        hash = Convert.ToBase64String(reference.DigestValue);
        //    }
            
        //    XmlNode xmlNodeFirmado = firmado.GetXml();
        //    xmlNodeFirmado.Prefix = "ds";

        //    //XmlNode xmlNodeContent = documentXml.CreateElement("ext", "ExtensionContent", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");
        //    //xmlNodeContent.AppendChild(xmlNodeFirmado);

        //    //XmlNode xmlNode = documentXml.CreateElement("ext", "UBLExtension", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");
        //    //xmlNode.AppendChild(xmlNodeContent);

        //    nodoExtension.AppendChild(xmlNodeFirmado);

        //    var settings = new XmlWriterSettings()
        //    {
        //        Encoding = Encoding.UTF8,
        //        Indent = true,
        //        IndentChars = "\t",
        //        NewLineChars = Environment.NewLine

        //    };

        //    string resultado = String.Empty;
        //    using (var memDoc = new MemoryStream())
        //    {

        //        using (var writer = XmlWriter.Create(memDoc, settings))
        //        {
        //            //XDocument xDocument = XDocument.Parse(documentXml.OuterXml);
        //            //xDocument.WriteTo(writer);
        //            documentXml.WriteTo(writer);
        //        }

        //        //resultado = Encoding.Unicode.GetString(memDoc.ToArray());
        //        //resultado = Encoding.GetEncoding("ISO-8859-1").GetString(memDoc.ToArray());
        //        //resultado = Convert.ToBase64String(memDoc.ToArray());
        //        resultado = Encoding.UTF8.GetString(memDoc.ToArray());

        //    }
        //    return resultado;
        //}

        public static string RetornarXmlFirmado(string prefijoComprobanteBusqueda, string tnsString, string xmlString, string rutaCertificado, string claveCertificado, out string hash)
        {
            hash = null;

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.PreserveWhitespace = true;
            xmlDocument.LoadXml(xmlString);

            X509Certificate2 certificado = new X509Certificate2(rutaCertificado, claveCertificado, X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.Exportable);

            XmlNamespaceManager nsMgr = new XmlNamespaceManager(xmlDocument.NameTable);
            nsMgr.AddNamespace("tns", tnsString);
            nsMgr.AddNamespace("ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");

            XmlElement elem = xmlDocument.CreateElement("ext:ExtensionContent", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");

            xmlDocument.SelectSingleNode($"{prefijoComprobanteBusqueda}/ext:UBLExtensions/ext:UBLExtension", nsMgr).AppendChild(elem);

            SignedXml signedXml = new SignedXml(xmlDocument);

            signedXml.SigningKey = certificado.GetRSAPrivateKey();
            KeyInfo KeyInfo = new KeyInfo();

            Reference Reference = new Reference();
            Reference.Uri = "";

            Reference.AddTransform(new XmlDsigEnvelopedSignatureTransform());
            signedXml.AddReference(Reference);

            X509Chain X509Chain = new X509Chain();
            X509Chain.Build(certificado);

            X509ChainElement local_element = X509Chain.ChainElements[0];
            KeyInfoX509Data x509Data = new KeyInfoX509Data(local_element.Certificate);

            string subjectName = local_element.Certificate.Subject;

            x509Data.AddSubjectName(subjectName);
            KeyInfo.AddClause(x509Data);
            signedXml.Signature.Id = "signatureKG";
            signedXml.KeyInfo = KeyInfo;
            signedXml.ComputeSignature();
            XmlElement signature = signedXml.GetXml();
            XmlNode dg = signature.GetElementsByTagName("DigestValue", "http://www.w3.org/2000/09/xmldsig#")[0];
            XmlNode sg = signature.GetElementsByTagName("SignatureValue", "http://www.w3.org/2000/09/xmldsig#")[0];
            hash = dg.InnerText;
            //SignatureValue = sg.InnerText;

            signature.Prefix = "ds";
            //SetPrefix("ds", signature);

            elem.AppendChild(signature);

            MemoryStream msXMLFirmado = new MemoryStream();

            xmlDocument.Save(msXMLFirmado);

            //msXMLFirmado.Position = 1;

            return Encoding.UTF8.GetString(msXMLFirmado.ToArray()).Substring(1);
        }

        public static byte[] RetornarXmlComprimido(string xmlFirmadoString, string nombreArchivoXml)
        {
            byte[] archivoZip = null;


            using (var memoryStream = new MemoryStream())
            {
                using (var zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    var demoFile = zipArchive.CreateEntry(nombreArchivoXml);

                    using (var entryStream = demoFile.Open())
                    {
                        using (var streamWriter = new StreamWriter(entryStream))
                        {
                            streamWriter.Write(xmlFirmadoString);
                        }
                    }
                }

                archivoZip = memoryStream.ToArray();
            }

            return archivoZip;
        }

        public static byte[] RetornarXmlComprimido(byte[] xmlFirmadoBytes, string nombreArchivoXml)
        {
            byte[] archivoZip = null;


            using (var memoryStream = new MemoryStream())
            {
                using (var zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    var demoFile = zipArchive.CreateEntry(nombreArchivoXml);

                    using (var entryStream = demoFile.Open())
                    {
                        entryStream.Write(xmlFirmadoBytes, 0, xmlFirmadoBytes.Length);
                    }
                }

                archivoZip = memoryStream.ToArray();
            }

            return archivoZip;
        }

        public static void EvaluarXmlCdrDescomprimido(byte[] cdrZipBytes, string nombreArchivoCdr, out string codigoCdr, out string descripcionCdr, out EstadoCdr? estadoCdr)
        {
            codigoCdr = null;
            descripcionCdr = null;
            estadoCdr = null;
            string contenidoXmlCdr = null;

            using (var memoryStream = new MemoryStream(cdrZipBytes))
            {
                using (var zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Read))
                {
                    var demoFile = zipArchive.GetEntry(nombreArchivoCdr);

                    var fileStream = demoFile.Open();

                    byte[] fileBytes = null;

                    using (var ms = new MemoryStream())
                    {
                        fileStream.CopyTo(ms);

                        fileBytes = ms.ToArray();
                    }

                    contenidoXmlCdr = Encoding.UTF8.GetString(fileBytes);

                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.LoadXml(contenidoXmlCdr);
                    try
                    {
                        codigoCdr = xmlDocument.DocumentElement.GetElementsByTagName("cac:DocumentResponse").Item(0).ChildNodes[0].ChildNodes[1].InnerXml;
                        descripcionCdr = xmlDocument.DocumentElement.GetElementsByTagName("cac:DocumentResponse").Item(0).ChildNodes[0].ChildNodes[2].InnerXml;
                        estadoCdr = codigoCdr == "0" ? EstadoCdr.Aceptado : EstadoCdr.Rechazado;
                    }
                    catch (Exception ex) { }
                }
            }
        }

        //public static string GenerarXML(InvoiceType item)
        //{
        //    //byte[] xmlByte = null;

        //    XmlSerializerNamespaces ns = new XmlSerializerNamespaces();

        //    ns.Add("", "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2");
        //    ns.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");
        //    ns.Add("ds", "http://www.w3.org/2000/09/xmldsig#");
        //    ns.Add("qdt", "urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2");
        //    ns.Add("sac", "urn:sunat:names:specification:ubl:peru:schema:xsd:SunatAggregateComponents-1");
        //    ns.Add("ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");
        //    ns.Add("udt", "urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2");
        //    ns.Add("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
        //    ns.Add("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
        //    ns.Add("ccts", "urn:un:unece:uncefact:documentation:2");

        //    XmlSerializer nXmlSerializer = new XmlSerializer(typeof(InvoiceType));
        //    XmlWriter xtWriter;
        //    XmlWriterSettings setting = new XmlWriterSettings();
        //    setting.Indent = true;
        //    setting.IndentChars = "\t";
        //    StringBuilder xmlString = new StringBuilder();
        //    xtWriter = XmlWriter.Create(xmlString, setting);
        //    xtWriter.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"UTF-8\"");
        //    nXmlSerializer.Serialize(xtWriter, item, ns);
        //    xtWriter.Close();

        //    XmlDocument xmlDocument = new XmlDocument();
        //    xmlDocument.LoadXml(xmlString.ToString());
        //    XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
        //    xmlNamespaceManager.AddNamespace("ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");
        //    xmlNamespaceManager.AddNamespace("", "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2");

        //    var xmlNode = xmlDocument.DocumentElement.SelectNodes("ext:UBLExtensions/ext:UBLExtension", xmlNamespaceManager);
        //    xmlNode[0].AppendChild(xmlDocument.CreateNode(XmlNodeType.Element, "ext", "ExtensionContent", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2"));

        //    XDocument xDocument = XDocument.Parse(xmlDocument.OuterXml);

        //    return xDocument.ToString();
        //}

        public static string GenerarXML(InvoiceType item)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(InvoiceType));
            XmlSerializerNamespaces oxmlnames = new XmlSerializerNamespaces();
            oxmlnames.Add("", "urn:oasis:names:specification:ubl:schema:xsd:Invoice-2");
            oxmlnames.Add("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            oxmlnames.Add("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            oxmlnames.Add("ccts", "urn:un:unece:uncefact:documentation:2");
            oxmlnames.Add("ds", "http://www.w3.org/2000/09/xmldsig#");
            oxmlnames.Add("ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");
            oxmlnames.Add("qdt", "urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2");
            oxmlnames.Add("sac", "urn:sunat:names:specification:ubl:peru:schema:xsd:SunatAggregateComponents-1");
            oxmlnames.Add("udt", "urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2");
            oxmlnames.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");

            MemoryStream ms = new MemoryStream();
            serializer.Serialize(ms, item, oxmlnames);
            ms.Position = 0;

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.PreserveWhitespace = true;
            xmlDocument.Load(ms);

            //XmlNamespaceManager ns = new XmlNamespaceManager(xmlDocument.NameTable);
            //ns.AddNamespace("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");

            XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmlDocument.ReplaceChild(xmlDeclaration, xmlDocument.FirstChild);

            return xmlDocument.OuterXml;
        }

        public static string GenerarXML(CreditNoteType item)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(CreditNoteType));
            XmlSerializerNamespaces oxmlnames = new XmlSerializerNamespaces();
            oxmlnames.Add("", "urn:oasis:names:specification:ubl:schema:xsd:CreditNote-2");
            oxmlnames.Add("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            oxmlnames.Add("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            oxmlnames.Add("ccts", "urn:un:unece:uncefact:documentation:2");
            oxmlnames.Add("ds", "http://www.w3.org/2000/09/xmldsig#");
            oxmlnames.Add("ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");
            oxmlnames.Add("qdt", "urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2");
            oxmlnames.Add("sac", "urn:sunat:names:specification:ubl:peru:schema:xsd:SunatAggregateComponents-1");
            oxmlnames.Add("udt", "urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2");
            oxmlnames.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");

            MemoryStream ms = new MemoryStream();
            serializer.Serialize(ms, item, oxmlnames);
            ms.Position = 0;

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.PreserveWhitespace = true;
            xmlDocument.Load(ms);

            XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmlDocument.ReplaceChild(xmlDeclaration, xmlDocument.FirstChild);

            return xmlDocument.OuterXml;
        }

        public static string GenerarXML(DebitNoteType item)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(DebitNoteType));
            XmlSerializerNamespaces oxmlnames = new XmlSerializerNamespaces();
            oxmlnames.Add("", "urn:oasis:names:specification:ubl:schema:xsd:DebitNote-2");
            oxmlnames.Add("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
            oxmlnames.Add("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
            oxmlnames.Add("ccts", "urn:un:unece:uncefact:documentation:2");
            oxmlnames.Add("ds", "http://www.w3.org/2000/09/xmldsig#");
            oxmlnames.Add("ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");
            oxmlnames.Add("qdt", "urn:oasis:names:specification:ubl:schema:xsd:QualifiedDatatypes-2");
            oxmlnames.Add("sac", "urn:sunat:names:specification:ubl:peru:schema:xsd:SunatAggregateComponents-1");
            oxmlnames.Add("udt", "urn:un:unece:uncefact:data:specification:UnqualifiedDataTypesSchemaModule:2");
            oxmlnames.Add("xsi", "http://www.w3.org/2001/XMLSchema-instance");

            MemoryStream ms = new MemoryStream();
            serializer.Serialize(ms, item, oxmlnames);
            ms.Position = 0;

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.PreserveWhitespace = true;
            xmlDocument.Load(ms);

            XmlDeclaration xmlDeclaration = xmlDocument.CreateXmlDeclaration("1.0", "UTF-8", null);
            xmlDocument.ReplaceChild(xmlDeclaration, xmlDocument.FirstChild);

            return xmlDocument.OuterXml;
        }

        public static byte[] GenerarQR(string rucEmpresa, string codigoSunatTipoComprobante, string serie, int nroComprobante, DateTime fechaEmision, string codigoSunatTipoDocumentoIdentidad, string nroDocumentoIdentidadCliente, decimal totalIGV, decimal totalImporte, string hash)
        {
            byte[] imagen = null;

            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            QrCode qrCode = new QrCode();

            qrEncoder.TryEncode($"{rucEmpresa}|{codigoSunatTipoComprobante}|{serie}-{nroComprobante}|{totalIGV}|{totalImporte}|{fechaEmision:dd/MM/yyyy)}|{codigoSunatTipoDocumentoIdentidad}|{nroDocumentoIdentidadCliente}|{hash}", out qrCode);

            GraphicsRenderer renderer = new GraphicsRenderer(new FixedCodeSize(400, QuietZoneModules.Zero), Brushes.Black, Brushes.White);

            MemoryStream ms = new MemoryStream();

            renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, ms);

            imagen = ms.ToArray();

            return imagen;
        }
    }
}
