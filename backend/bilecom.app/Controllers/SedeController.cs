﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bilecom.app.Controllers
{
    public class SedeController : Controller
    {
        // GET: Sede
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Mantenimiento()
        {
            return View();
        }
    }
}