using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bilecom.app.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Nuevo()
        {
            return View("Mantenimiento");
        }
        public ActionResult Editar(int id)
        {
            return View("Mantenimiento");
        }
        public ActionResult MantenimientoCategoria()
        {
            return View();
        }
    }
}