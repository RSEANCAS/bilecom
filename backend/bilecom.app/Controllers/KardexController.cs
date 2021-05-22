using bilecom.app.Controllers.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bilecom.app.Controllers
{
    [RoutePrefix("Kardex")]
    [IsLogoutFilter]
    public class KardexController : Controller
    {
        // GET: Kardex
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }
    }
}