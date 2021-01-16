using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bilecom.app.Controllers
{
    public class LoginController : _BaseController
    {
        // GET: Login
        public ActionResult Index()
        {
            if (estaLogueado) return RedirectToAction("Index", "Inicio");
            return View();
        }
    }
}