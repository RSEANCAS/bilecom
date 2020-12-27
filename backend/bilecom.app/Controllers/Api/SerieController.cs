﻿using bilecom.be;
using bilecom.be.Custom;
using bilecom.bl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace bilecom.app.Controllers.Api
{
    [RoutePrefix("api/serie")]
    public class SerieController : ApiController
    {
        SerieBl serieBl = new SerieBl();
        [HttpGet]
        [Route("buscar-serie")]
        public DataPaginate<SerieBe> BuscarSerie(int empresaId, int? tipoComprobanteId, string serial, int draw, int start, int length, string columnaOrden = "SerieId", string ordenMax = "ASC")
        {
            int totalRegistros = 0;
            var lista = serieBl.BuscarSerie(empresaId, tipoComprobanteId,serial, start, length, columnaOrden, ordenMax, out totalRegistros);
            var respuesta = new DataPaginate<SerieBe>
            {
                data = lista ?? new List<SerieBe>(),
                draw = draw,
                recordsFiltered = totalRegistros,
                recordsTotal = totalRegistros
            };

            return respuesta;
        }
    }
}
