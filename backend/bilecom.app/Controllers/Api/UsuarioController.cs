using bilecom.app.Controllers.Api.Jwt;
using bilecom.be;
using bilecom.be.Custom;
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

        [HttpGet]
        [Route("validar-usuario")]
        public IHttpActionResult ValidarUsuario(string ruc, string usuario)
        {
            BootStrapValidator.Remote item = new BootStrapValidator.Remote();

            if (string.IsNullOrEmpty((ruc ?? "").Trim()))
                return Ok(item);

            if (string.IsNullOrEmpty((usuario ?? "").Trim()))
                return Ok(item);

            var empresa = empresaBl.ObtenerPorRuc(ruc);

            if (empresa == null)
                return Ok(item);

            var user = usuarioBl.ObtenerPorNombre(usuario, empresa.EmpresaId);

            if (user == null)
                return Ok(item);

            item.valid = true;

            return Ok(item);
        }

        [HttpPost]
        [Route("autenticar")]
        public IHttpActionResult Autenticar(string ruc, string usuario, string contraseña)
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
                return BadRequest($"El usuario {usuario} no se encuentra registrado.");

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
                            empresa.EmpresaId,
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