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
        protected dynamic Data
        {
            get
            {
                dynamic data = null;

                try
                {
                    HttpCookie cookie = Request.Cookies.Get("ss");
                    if (cookie != null) data = JsonConvert.DeserializeObject<dynamic>(cookie.Value);
                }
                catch (Exception ex)
                {
                    data = null;
                }

                return data;
            }
        }

        protected bool IsLogin
        {
            get
            {
                bool existe = Data != null;

                return existe;
            }
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //token = Request == null ? null : Request.Cookies["ls.tk"] == null ? null : Request.Cookies["ls.tk"].Value;
            //estaLogueado = !string.IsNullOrEmpty(token);
            //string userString = Request == null ? null : Request.Cookies["ls.us"] == null ? null : Request.Cookies["ls.us"].Value;
            //if (!string.IsNullOrEmpty(userString))
            //{
            //    byte[] userByte = System.Convert.FromBase64String(userString);
            //    string user64Decoded = System.Text.ASCIIEncoding.ASCII.GetString(userByte);
            //    user = JsonConvert.DeserializeObject<dynamic>(user64Decoded);
            //}
            //else
            //{
            //    filterContext.Result = RedirectToAction("Index", "Login");
            //}

            base.OnActionExecuting(filterContext);
        }
    }
}