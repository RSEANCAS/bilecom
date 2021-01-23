using Newtonsoft.Json;
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
        protected string token;
        protected dynamic user;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            token = Request == null ? null : Request.Cookies["ls.tk"] == null ? null : Request.Cookies["ls.tk"].Value;
            estaLogueado = !string.IsNullOrEmpty(token);
            string userString = Request == null ? null : Request.Cookies["ls.us"] == null ? null : Request.Cookies["ls.us"].Value;
            if (!string.IsNullOrEmpty(userString))
            {
                byte[] userByte = System.Convert.FromBase64String(userString);
                string user64Decoded = System.Text.ASCIIEncoding.ASCII.GetString(userByte);
                user = JsonConvert.DeserializeObject<dynamic>(user64Decoded);
            }

            base.OnActionExecuting(filterContext);
        }
    }
}