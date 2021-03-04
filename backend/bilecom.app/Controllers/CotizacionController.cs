using bilecom.app.Controllers.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static bilecom.enums.Enums;

namespace bilecom.app.Controllers
{
    [RoutePrefix("Cotizaciones")]
    [IsLogoutFilter]
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
            ViewBag.Accion = (int)Accion.Nuevo;
            return View("Mantenimiento");
        }

        [Route("Editar")]
        public ActionResult Editar(int id)
        {
            ViewBag.Titulo = "Editar Cotización";
            ViewBag.Accion = (int)Accion.Editar;
            return View("Mantenimiento");
        }
    }

}