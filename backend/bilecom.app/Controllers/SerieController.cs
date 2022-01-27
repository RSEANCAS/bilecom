using bilecom.app.Controllers.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bilecom.app.Controllers
{
    [RoutePrefix("Series")]
    [IsLogoutFilter]
    public class SerieController : Controller
    {
        // GET: Serie
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("Nuevo")]
        public ActionResult Nuevo()
        {
            TempData["Id"] = 0;
            ViewBag.Titulo = "Nueva Serie";
            return View("Mantenimiento");
        }

        [Route("Editar")]
        public ActionResult Editar(int Id)
        {
            TempData["Id"] = Id;
            ViewBag.Titulo = "Editar Serie";
            return View("Mantenimiento");
        }
    }
}