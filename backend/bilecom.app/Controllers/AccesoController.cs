using bilecom.bl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace bilecom.app.Controllers
{
    public class AccesoController : Controller
    {
        TokenBl tokenBl = new TokenBl();
        // GET: RecuperarContrasena
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult IniciarRecuperacion()
        {
            return View();
        }
        [HttpPost]
        public ActionResult IniciarRecuperacion(string ruc, string usuario)
        {
            return View();
        }
        public ActionResult RecuperarContrasena(string token)
        {
            string[] valores = token.Split('|');
            string codigoToken = valores[0];
            string usuarioIdStr = valores[1];
            string empresaIdStr = valores[2];
            string tipoTokenIdStr = valores[3];

            int usuarioId = int.Parse(Encoding.UTF8.GetString(Convert.FromBase64String(usuarioIdStr)));
            int empresaId = int.Parse(Encoding.UTF8.GetString(Convert.FromBase64String(empresaIdStr)));
            int tipoTokenId = int.Parse(Encoding.UTF8.GetString(Convert.FromBase64String(tipoTokenIdStr)));
            var tokenbe = tokenBl.ObtenerToken(usuarioId,empresaId,codigoToken,tipoTokenId);
            bool esValido = tokenbe != null;
            if (!esValido) 
            {
                return HttpNotFound();
            }
            if (!(DateTime.Now >= tokenbe.FechaInicio && DateTime.Now <= tokenbe.FechaFin))
            {
                return HttpNotFound();
            }
            return View();
        }
    }
}