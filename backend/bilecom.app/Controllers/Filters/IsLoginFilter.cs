using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bilecom.app.Controllers.Filters
{
    public class IsLoginFilter : _BaseFilter
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (IsLogin)
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { Controller = "Inicio", Action = "Index" }));
            }

            base.OnActionExecuting(filterContext);
        }
    }
}