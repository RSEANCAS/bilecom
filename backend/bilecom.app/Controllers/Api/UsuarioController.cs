using bilecom.app.Controllers.Api.Jwt;
using bilecom.be;
using bilecom.bl;
using bilecom.ut;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace bilecom.app.Controllers.Api
{

    [RoutePrefix("api/usuario")]
    public class UsuarioController : ApiController
    {
        EmpresaBl empresaBl = new EmpresaBl();
        UsuarioBl usuarioBl = new UsuarioBl();

        [HttpPost]
        [Route("autenticar")]
        public IHttpActionResult Authenticate(string ruc, string usuario, string contraseña)
        {
            if (string.IsNullOrEmpty((ruc ?? "").Trim()))
                return BadRequest("El ruc está vacío.");

            if (string.IsNullOrEmpty((usuario ?? "").Trim()))
                return BadRequest("El usuario está vacío.");

            if (string.IsNullOrEmpty((contraseña ?? "").Trim()))
                return BadRequest("La contraseña está vacía.");

            var empresa = empresaBl.ObtenerPorRuc(ruc);

            if (empresa == null)
                return BadRequest($"El ruc {ruc} no se encuentra registrado.");

            var user = usuarioBl.ObtenerPorNombre(usuario, empresa.EmpresaId);

            if (user == null)
                return BadRequest($"El ruc {ruc} no se encuentra registrado.");

            bool isCredentialValid = Seguridad.CompareMD5(contraseña, user.Contrasena);

            if (isCredentialValid)
            {
                var token = TokenGenerator.GenerateTokenJwt(user);

                var response = new
                {
                    Token = token,
                    Usuario = new
                    {
                        Nombre = user.Nombre,
                        Empresa = new
                        {
                            empresa.RazonSocial,
                            empresa.Ruc,
                            empresa.NombreComercial
                        },
                    }
                };

                return Ok(response);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}