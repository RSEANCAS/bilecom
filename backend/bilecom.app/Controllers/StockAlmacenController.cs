using bilecom.app.Controllers.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bilecom.app.Controllers
{
    [RoutePrefix("StockAlmacenes")]
    [IsLogoutFilter]
    public class StockAlmacenController : Controller
    {
        // GET: StockAlmacen
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }
    }
}