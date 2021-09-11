using bilecom.app.Controllers.Api.Jwt;
using bilecom.be;
using bilecom.be.Custom;
using bilecom.bl;
using bilecom.ut;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Http;
using static bilecom.enums.Enums;

namespace bilecom.app.Controllers.Api
{

    [RoutePrefix("api/usuario")]
    public class UsuarioController : ApiController
    {
        EmpresaBl empresaBl = new EmpresaBl();
        UsuarioBl usuarioBl = new UsuarioBl();
        PersonalBl personalBl = new PersonalBl();
        TokenBl tokenBl = new TokenBl();
        FormatoCorreoBl formatoCorreoBl = new FormatoCorreoBl();
        Correo correo = new Correo();

        [HttpGet]
        [Route("validar-usuario")]
        public IHttpActionResult ValidarUsuario(string ruc, string usuario)
        {
            BootStrapValidator.Remote item = new BootStrapValidator.Remote();

            if (string.IsNullOrEmpty((ruc ?? "").Trim()))
                return Ok(item);

            if (string.IsNullOrEmpty((usuario ?? "").Trim()))
                return Ok(item);

            var empresa = empresaBl.ObtenerEmpresaPorRuc(ruc);

            if (empresa == null)
                return Ok(item);

            var user = usuarioBl.ObtenerUsuarioPorNombre(usuario, empresa.EmpresaId);

            if (user == null)
                return Ok(item);

            item.valid = true;

            return Ok(item);
        }

        [HttpPost]
        [Route("autenticar-usuario")]
        public HttpResponseMessage AutenticarUsuario(string ruc, string usuario, string contraseña)
        {
            if (string.IsNullOrEmpty((ruc ?? "").Trim()))
            {
                var msg = new HttpResponseMessage(HttpStatusCode.BadRequest);
                msg.Content = new StringContent("El ruc está vacío.");
                return msg;
            }

            if (string.IsNullOrEmpty((usuario ?? "").Trim()))
            {
                var msg = new HttpResponseMessage(HttpStatusCode.BadRequest);
                msg.Content = new StringContent("El usuario está vacío.");
                return msg;
            }

            if (string.IsNullOrEmpty((contraseña ?? "").Trim()))
            {
                var msg = new HttpResponseMessage(HttpStatusCode.BadRequest);
                msg.Content = new StringContent("La contraseña está vacía.");
                return msg;
            }
            
            var empresa = empresaBl.ObtenerEmpresaPorRuc(ruc);

            if (empresa == null)
            {
                var msg = new HttpResponseMessage(HttpStatusCode.BadRequest);
                msg.Content = new StringContent($"El ruc {ruc} no se encuentra registrado.");
                return msg;
            }

            var user = usuarioBl.ObtenerUsuarioPorNombre(usuario, empresa.EmpresaId, loadListaPerfil: true, LoadListaSede: true);

            if (user == null)
            {
                var msg = new HttpResponseMessage(HttpStatusCode.BadRequest);
                msg.Content = new StringContent($"El usuario {usuario} no se encuentra registrado.");
                return msg;
            }
            else
            {
                if (user.PersonalId.HasValue) user.Personal = personalBl.ObtenerPersonal(empresa.EmpresaId, user.PersonalId.Value);
            }

            if(user.ListaPerfil == null)
            {
                var msg = new HttpResponseMessage(HttpStatusCode.BadRequest);
                msg.Content = new StringContent($"El usuario {usuario} no tiene ningún perfil asignado.");
                return msg;
            }

            if (user.ListaSede == null)
            {
                var msg = new HttpResponseMessage(HttpStatusCode.BadRequest);
                msg.Content = new StringContent($"El usuario {usuario} no tiene ninguna sede asignada.");
                return msg;
            }

            bool isCredentialValid = Seguridad.CompareMD5(contraseña, user.Contrasena);

            if (isCredentialValid)
            {
                var token = TokenGenerator.GenerateTokenJwt(user);

                var handler = new JwtSecurityTokenHandler();
                var payload = handler.ReadJwtToken(token).Payload;

                var response = new
                {
                    Token = token,
                    FechaExpiracion = DateTimeOffset.FromUnixTimeSeconds(payload.Exp.Value),
                    Usuario = new
                    {
                        Id = user.UsuarioId,
                        Nombre = user.Nombre,
                        Empresa = new
                        {
                            empresa.EmpresaId,
                            empresa.RazonSocial,
                            empresa.Ruc,
                            empresa.NombreComercial
                        },
                        Personal = user.Personal,
                        //ListaPerfil = user.ListaPerfil
                    },
                    PerfilActual = user.ListaPerfil[0],
                    SedeActual = user.ListaSede[0]
                };

                string responseString = JsonConvert.SerializeObject(response);

                HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.OK);
                message.Content = new StringContent(responseString);

                message.Headers.Add("ls.ss", responseString);

                return message;
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
        }

        [HttpPost]
        [Route("recuperar-contrasena")]
        public HttpResponseMessage RecuperarContrasena(string ruc, string usuario)
        {
            bool seProceso = false;
            if (string.IsNullOrEmpty((ruc ?? "").Trim()))
            {
                var msg = new HttpResponseMessage(HttpStatusCode.BadRequest);
                msg.Content = new StringContent("El ruc está vacío.");
                return msg;
            }
            if (string.IsNullOrEmpty((usuario ?? "").Trim()))
            {
                var msg = new HttpResponseMessage(HttpStatusCode.BadRequest);
                msg.Content = new StringContent("El usuario está vacío.");
                return msg;
            }
            var empresa = empresaBl.ObtenerEmpresaPorRuc(ruc);
            if (empresa == null)
            {
                var msg = new HttpResponseMessage(HttpStatusCode.BadRequest);
                msg.Content = new StringContent($"El ruc {ruc} no se encuentra registrado.");
                return msg;
            }
            var user = usuarioBl.ObtenerUsuarioPorNombre(usuario, empresa.EmpresaId, loadListaPerfil: true);

            if (user == null)
            {
                var msg = new HttpResponseMessage(HttpStatusCode.BadRequest);
                msg.Content = new StringContent($"El usuario {usuario} no se encuentra registrado.");
                return msg;
            }
            string codigoToken = null;
            int tiempoExpiracion = AppSettings.Get<int>("RecuperarContrasena.TiempoExpiracion");
            TokenBe token = new TokenBe();
            token.EmpresaId = user.EmpresaId ?? 0;
            token.UsuarioId = user.UsuarioId;
            //token.CodigoToken = codigoToken;
            token.TipoTokenId = (int)TipoCodigo.CodigoToken;
            token.FechaInicio = DateTime.Now;
            token.FechaFin = token.FechaInicio.AddMinutes(tiempoExpiracion);
            token.Usuario = usuario;
            token.Fecha = DateTime.Now;

            bool seGuardoToken = tokenBl.GuardarToken(token, out codigoToken);
            if (seGuardoToken)
            {
                var formatoCorreo = formatoCorreoBl.ObtenerFormatoCorreo((int)TipoFormatoCorreo.RecuperacionContrasena);
                string url = AppSettings.Get<string>("Url.RecuperarContrasena");
                string dataHtml = formatoCorreo.Html;
                string usuarioIdStr = Convert.ToBase64String(Encoding.UTF8.GetBytes(token.UsuarioId.ToString()));
                string empresaIdStr = Convert.ToBase64String(Encoding.UTF8.GetBytes(token.EmpresaId.ToString()));
                string tipoTokenIdStr = Convert.ToBase64String(Encoding.UTF8.GetBytes(token.TipoTokenId.ToString()));
                dataHtml = dataHtml.Replace("[Usuario]", user.Nombre).Replace("[Codigo]", codigoToken).Replace("[link]", url+"Acceso/RecuperarContrasena?token="+codigoToken+"|"+usuarioIdStr+"|"+empresaIdStr+"|"+tipoTokenIdStr);
                bool seEnvioCorreo = correo.EnviarCorreo(user.Correo, "Recuperación de Contraseña", dataHtml);
                if (seEnvioCorreo) seProceso = true;
            }
                   
            HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.OK, seProceso);

            return message;
        }
    }
}