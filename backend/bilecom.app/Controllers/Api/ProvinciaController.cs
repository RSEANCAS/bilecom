﻿using bilecom.be;
using bilecom.bl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace bilecom.app.Controllers.Api
{
    [RoutePrefix("api/provincia")]
    public class ProvinciaController : ApiController
    {
        ProvinciaBl provinciaBl = new ProvinciaBl();

        [HttpGet]
        [Route("listar-provincia")]
        public List<ProvinciaBe> ListarProvincia()
        {
            return provinciaBl.ListarProvincia();
        }
    }
}
