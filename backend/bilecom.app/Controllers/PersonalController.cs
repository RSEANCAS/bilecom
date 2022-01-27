using bilecom.app.Controllers.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bilecom.app.Controllers
{
    [RoutePrefix("Personal")]
    [IsLogoutFilter]
    public class PersonalController : Controller
    {
        // GET: Personal
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }
        [Route("Nuevo")]
        public ActionResult Nuevo()
        {
            TempData["Id"] = 0;
            ViewBag.Titulo = "Nuevo Personal";
            return View("Mantenimiento");
        }
        [Route("Editar")]
        public ActionResult Editar(int Id)
        {
            TempData["Id"] = Id;
            ViewBag.Titulo = "Editar Personal";
            return View("Mantenimiento");
        }
    }
}