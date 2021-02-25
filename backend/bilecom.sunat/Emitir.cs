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

            BasicHttpBinding binding = new BasicHttpBinding(BasicHttpSecurityMode.TransportWithMessageCredential);
            binding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
            binding.Security.Transport.ProxyCredentialType = HttpProxyCredentialType.None;
            binding.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.UserName;
            binding.Security.Message.AlgorithmSuite = System.ServiceModel.Security.SecurityAlgorithmSuite.Default;

            EndpointAddress remoteAddress;
            remoteAddress = new EndpointAddress(urlEnvio);

            ws.venta.billServiceClient billServiceClient = new ws.venta.billServiceClient(binding, remoteAddress);
            billServiceClient.ClientCredentials.UserName.UserName = $"{rucSOL}{usuarioSOL}";// "10096164144MODDATOS";//  '"20895626281PELM202" '- 'RUC + USUARIO
            billServiceClient.ClientCredentials.UserName.Password = claveSOL;// "moddatos"; // ' - Contraseña
            var elements = billServiceClient.Endpoint.Binding.CreateBindingElements();
            elements.Find<SecurityBindingElement>().EnableUnsecuredResponse = true;
            billServiceClient.Endpoint.Binding = new CustomBinding(elements);
            try
            {
                cdrBytes = billServiceClient.sendBill(nombreArchivo, contenidoArchivo, "");
                if(cdrBytes != null)
                {
                    string nombreArchivoCdr = $"R-{nombreArchivo.Replace(".zip", ".xml")}";
                    Generar.EvaluarXmlCdrDescomprimido(cdrBytes, nombreArchivoCdr, out codigoCdr, out descripcionCdr, out estadoCdr);
                }
            }
            catch (FaultException ex) { }
            catch (Exception ex) { }
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
