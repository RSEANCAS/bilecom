﻿using bilecom.app.Controllers.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static bilecom.enums.Enums;

namespace bilecom.app.Controllers
{
    [RoutePrefix("Facturas")]
    [IsLogoutFilter]
    public class FacturaController : Controller
    {
        [Route("")]
        // GET: Factura
        public ActionResult Index()
        {
            return View();
        }

        [Route("Nuevo")]
        public ActionResult Nuevo()
        {
            ViewBag.Titulo = "Nueva Factura";
            ViewBag.Accion = (int)Accion.Nuevo;
            return View("Mantenimiento");
        }
    }
}