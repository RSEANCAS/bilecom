using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static bilecom.enums.Enums;

namespace bilecom.app.Controllers
{
    [RoutePrefix("Movimientos")]
    public class MovimientoController : Controller
    {
        // GET: Movimiento
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("Nuevo")]
        public ActionResult Nuevo()
        {
            ViewBag.Titulo = "Nuevo Movimiento";
            ViewBag.Accion = (int)Accion.Nuevo;
            return View("Mantenimiento");
        }

        [Route("Editar")]
        public ActionResult Editar(int id)
        {
            ViewBag.Titulo = "Editar Movimiento";
            ViewBag.Accion = (int)Accion.Editar;
            return View("Mantenimiento");
        }
    }
}