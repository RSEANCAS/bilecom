using bilecom.app.Controllers.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static bilecom.enums.Enums;

namespace bilecom.app.Controllers
{
    [RoutePrefix("Boletas")]
    [IsLogoutFilter]
    public class BoletaController : Controller
    {
        [Route("")]
        // GET: Boleta
        public ActionResult Index()
        {
            return View();
        }

        [Route("Nuevo")]
        public ActionResult Nuevo()
        {
            ViewBag.Titulo = "Nueva Boleta";
            ViewBag.Accion = (int)Accion.Nuevo;
            return View("Mantenimiento");
        }
    }
}