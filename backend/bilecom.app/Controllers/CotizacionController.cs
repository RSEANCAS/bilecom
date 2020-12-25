using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bilecom.app.Controllers
{
    [RoutePrefix("Cotizaciones")]
    public class CotizacionController : Controller
    {
        [Route("")]
        // GET: Cotizacion
        public ActionResult Index()
        {
            return View();
        }

        [Route("Nuevo")]
        public ActionResult Nuevo()
        {
            ViewBag.Titulo = "Nueva Cotización";
            return View("Mantenimiento");
        }

        [Route("Editar")]
        public ActionResult Editar(int cotizacionId)
        {
            ViewBag.Titulo = "Editar Cotización";
            return View("Mantenimiento");
        }
    }

}