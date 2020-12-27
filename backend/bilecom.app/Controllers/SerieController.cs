using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bilecom.app.Controllers
{
    [RoutePrefix("Series")]
    public class SerieController : Controller
    {
        // GET: Serie
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }
    }
}