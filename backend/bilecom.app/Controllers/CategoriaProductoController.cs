﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bilecom.app.Controllers
{
    public class CategoriaProductoController : Controller
    {
        // GET: CategoriaProducto
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Nuevo()
        {
            return View("MantenimientoCategoria");
        }
        public ActionResult Editar(int Id)
        {
            return View("MantenimientoCategoria");
        }
    }
}