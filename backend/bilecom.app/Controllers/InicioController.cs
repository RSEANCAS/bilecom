using bilecom.app.Controllers.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bilecom.app.Controllers
{
    public class InicioController : Controller
    {
        // GET: Inicio
        [IsLogoutFilter]
        public ActionResult Index()
        {
            return View();
        }
    }
}