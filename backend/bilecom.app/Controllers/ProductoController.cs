using bilecom.app.Controllers.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bilecom.app.Controllers
{
    [RoutePrefix("Productos")]
    [IsLogoutFilter]
    public class ProductoController : Controller
    {
        // GET: Producto
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }
        [Route("Nuevo")]
        public ActionResult Nuevo()
        {
            TempData["Id"] = 0;
            return View("Mantenimiento");
        }
        [Route("Editar")]
        public ActionResult Editar(int id)
        {
            TempData["Id"] = id;
            return View("Mantenimiento");
        }

    }
}