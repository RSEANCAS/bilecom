using bilecom.be;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bilecom.app.Controllers.Filters
{
    public class _BaseFilter : ActionFilterAttribute
    {
        protected dynamic Data
        {
            get
            {
                dynamic data = null;

                try
                {
                    HttpCookie cookie = HttpContext.Current.Request.Cookies.Get("ss");
                    if(cookie != null) data = JsonConvert.DeserializeObject<dynamic>(cookie.Value);
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
                dynamic data = null;

                try
                {
                    HttpCookie cookie = HttpContext.Current.Request.Cookies.Get("ss");
                    if (cookie != null) data = JsonConvert.DeserializeObject<dynamic>(cookie.Value);
                }
                catch (Exception ex)
                {
                    data = null;
                }

                bool existe = data != null;

                return existe;
            }
        }
    }
}