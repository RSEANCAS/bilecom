﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bilecom.app.Controllers
{
    public class PersonalController : Controller
    {
        // GET: Personal
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Nuevo()
        {
            return View("Mantenimiento");
        }
        public ActionResult Editar(int Id)
        {
            return View("Mantenimiento");
        }
    }
}