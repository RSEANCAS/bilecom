using Microsoft.Web.Services3.Security.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using VisioForge.MediaFramework.ONVIF;

namespace bilecom.sunat
{
    public class PasswordDigestMessageInspector : IClientMessageInspector
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public PasswordDigestMessageInspector(string username, string password)
        {
            Username = username;
            Password = password;
        }

        #region IClientMessageInspector Members

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            return;
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            UsernameToken token = new UsernameToken(this.Username, this.Password, PasswordOption.SendPlainText);

            XmlElement securityToken = token.GetXml(new XmlDocument());

            // Modificamos el XML Generado.
            var nodo = securityToken.GetElementsByTagName("wsse:Nonce").Item(0);
            var fecha = securityToken.GetElementsByTagName("wsu:Created").Item(0);
            securityToken.RemoveChild(nodo);
            securityToken.RemoveChild(fecha);

            MessageHeader securityHeader = MessageHeader.CreateHeader("Security",
                "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd",
                securityToken, false);
            request.Headers.Add(securityHeader);

            return Convert.DBNull;
        }

        #endregion
    }
}
