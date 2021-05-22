using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using static bilecom.enums.Enums;

namespace bilecom.sunat
{
    public class Emitir
    {
        public bool Venta(string urlEnvio, string nombreArchivo, byte[] contenidoArchivo, string rucSOL, string usuarioSOL, string claveSOL, out byte[] cdrBytes, out string codigoCdr, out string descripcionCdr, out EstadoCdr? estadoCdr)
        {
            cdrBytes = null;
            codigoCdr = null;
            descripcionCdr = null;
            estadoCdr = null;

            bool seEmitio = false;

            System.Net.ServicePointManager.UseNagleAlgorithm = true;
            System.Net.ServicePointManager.Expect100Continue = false;
            System.Net.ServicePointManager.CheckCertificateRevocationList = true;

            BasicHttpBinding binding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);

            EndpointAddress remoteAddress;
            remoteAddress = new EndpointAddress(urlEnvio);

            ws.venta.billServiceClient billServiceClient = new ws.venta.billServiceClient(binding, remoteAddress);
            var behavior = new PasswordDigestBehavior($"{rucSOL}{usuarioSOL}", claveSOL);
            billServiceClient.Endpoint.EndpointBehaviors.Add(behavior);
            try
            {
                billServiceClient.Open();
                cdrBytes = billServiceClient.sendBill(nombreArchivo, contenidoArchivo, "");
                billServiceClient.Close();
                if(cdrBytes != null)
                {
                    string nombreArchivoCdr = $"R-{nombreArchivo.Replace(".zip", ".xml")}";
                    Generar.EvaluarXmlCdrDescomprimido(cdrBytes, nombreArchivoCdr, out codigoCdr, out descripcionCdr, out estadoCdr);
                }
            }
            catch (FaultException ex) {
                descripcionCdr = ex.Message;
                codigoCdr = ex.Code.Name.ToString();
            }
            catch (Exception ex) { descripcionCdr = ex.Message; }
            seEmitio = cdrBytes != null;

            return seEmitio;
        }

        public bool VentaBaja(string urlEnvio, string nombreArchivo, byte[] contenidoArchivo, string rucSOL, string usuarioSOL, string claveSOL, out byte[] cdrBytes, out string codigoCdr, out string descripcionCdr, out EstadoCdr? estadoCdr)
        {
            cdrBytes = null;
            codigoCdr = null;
            descripcionCdr = null;
            estadoCdr = null;

            bool seEmitio = false;

            System.Net.ServicePointManager.UseNagleAlgorithm = true;
            System.Net.ServicePointManager.Expect100Continue = false;
            System.Net.ServicePointManager.CheckCertificateRevocationList = true;

            BasicHttpBinding binding = new BasicHttpBinding(BasicHttpSecurityMode.Transport);

            EndpointAddress remoteAddress;
            remoteAddress = new EndpointAddress(urlEnvio);

            ws.venta.billServiceClient billServiceClient = new ws.venta.billServiceClient(binding, remoteAddress);
            var behavior = new PasswordDigestBehavior($"{rucSOL}{usuarioSOL}", claveSOL);
            billServiceClient.Endpoint.EndpointBehaviors.Add(behavior);
            string ticket = null;
            try
            {
                billServiceClient.Open();
                ticket = billServiceClient.sendSummary(nombreArchivo, contenidoArchivo, "");
                var status = billServiceClient.getStatus(ticket);
                billServiceClient.Close();
                if (status != null)
                {
                    codigoCdr = status.statusCode;
                    cdrBytes = status.content;
                    if (cdrBytes != null)
                    {
                        string nombreArchivoCdr = $"R-{nombreArchivo.Replace(".zip", ".xml")}";
                        Generar.EvaluarXmlCdrDescomprimido(cdrBytes, nombreArchivoCdr, out codigoCdr, out descripcionCdr, out estadoCdr);
                    }
                }
            }
            catch (FaultException ex)
            {
                descripcionCdr = ex.Message;
                codigoCdr = ex.Code.Name.ToString();
            }
            catch (Exception ex) { descripcionCdr = ex.Message; }
            seEmitio = cdrBytes != null;

            return seEmitio;
        }

        public bool Guia(bool esPrueba = true)
        {
            bool seEmitio = false;

            return seEmitio;
        }

        public bool Otros(bool esPrueba = true)
        {
            bool seEmitio = false;

            return seEmitio;
        }
    }
}
