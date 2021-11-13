using bilecom.app.Controllers.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bilecom.app.Controllers
{
    [RoutePrefix("Reporte")]
    [IsLogoutFilter]
    public class ReporteController : Controller
    {
        // GET: Reporte
        //public ActionResult Index()
        //{
        //    return View();
        //}

        #region Facturación
        [Route("Facturacion/Ventas")]
        public ActionResult FacturacionVentas()
        {
            return View();
        }
        #endregion
    }
}