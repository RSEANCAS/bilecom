using bilecom.app.Controllers.Filters;
using bilecom.be;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace bilecom.app.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [IsLoginFilter]
        public ActionResult Index()
        {
            //if (estaLogueado) return RedirectToAction("Index", "Inicio");
            return View();
        }

        public ActionResult CerrarSession()
        {
            Response.Cookies.Remove("ls.us");

            return RedirectToAction("Index", "Login");
        }
    }
}