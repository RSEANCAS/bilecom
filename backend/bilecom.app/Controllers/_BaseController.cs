using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bilecom.app.Controllers
{
    public class _BaseController : Controller
    {
        protected bool estaLogueado;
        protected int empresaId;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string empresaIdString = Request == null ? null : Request.Cookies["ep.ky"] == null ? null : Request.Cookies["ep.ky"].Value;
            estaLogueado = int.TryParse(empresaIdString, out empresaId);
            base.OnActionExecuting(filterContext);
        }

        // GET: _Base
        public ActionResult Index()
        {
            return View();
        }
    }
}