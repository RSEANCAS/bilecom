using bilecom.app.Controllers.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bilecom.app.Controllers
{
    [RoutePrefix("CategoriasProducto")]
    [IsLogoutFilter]
    public class CategoriaProductoController : Controller
    {
        // GET: CategoriaProducto
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }
        [Route("Nuevo")]
        public ActionResult Nuevo()
        {
            TempData["Id"] = 0;
            ViewBag.Titulo = "Nueva Categoría de Producto";
            return View("MantenimientoCategoria");
        }
        [Route("Editar")]
        public ActionResult Editar(int Id)
        {
            TempData["Id"] = Id;
            ViewBag.Titulo = "Editar Categoría de Producto";
            return View("MantenimientoCategoria");
        }
    }
}