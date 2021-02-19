using bilecom.app.Controllers.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bilecom.app.Controllers
{
    [RoutePrefix("Configuracion")]
    [IsLogoutFilter]
    public class ConfiguracionController : Controller
    {
        [Route("")]
        // GET: Configuracion
        public ActionResult Index()
        {
            return View();
        }
    }
}