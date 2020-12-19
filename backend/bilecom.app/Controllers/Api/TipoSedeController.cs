using bilecom.be;
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
    [RoutePrefix("api/tiposede")]
    public class TipoSedeController : ApiController
    {
        TipoSedeBl tipoSedeBl = new TipoSedeBl();

        [HttpGet]
        [Route("buscar-tiposede")]
        public DataPaginate<TipoSedeBe> BuscarTipoSede(int empresaId, string nombre, int draw, int start, int length, string columnaOrden = "TipoSedeId", string ordenMax = "ASC")
        {
            int totalRegistros = 0;
            var lista = tipoSedeBl.BuscarTipoSede(empresaId, nombre, start, length, columnaOrden, ordenMax, out totalRegistros);
            var respuesta = new DataPaginate<TipoSedeBe>
            {
                data = lista ?? new List<TipoSedeBe>(),
                draw = draw,
                recordsFiltered = totalRegistros,
                recordsTotal = totalRegistros
            };
            return respuesta;
        }

        [HttpGet]
        [Route("obtener-tiposede")]
        public TipoSedeBe ObtenerTipoSede(int empresaId, int tiposedeId)
        {
            return tipoSedeBl.Obtener(empresaId, tiposedeId);
        }

        [HttpGet]
        [Route("listar-tiposede")]
        public List<TipoSedeBe> ListarTipoSede(int empresaId)
        {
            return tipoSedeBl.ListarTipoSede(empresaId);
        }

        [HttpPost]
        [Route("guardar-tiposede")]
        public bool GuardarTipoSede(TipoSedeBe tipoSedeBe)
        {
            return tipoSedeBl.Guardar(tipoSedeBe);
    }
    }
}
