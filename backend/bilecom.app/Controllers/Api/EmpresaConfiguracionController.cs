using bilecom.be;
using bilecom.bl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace bilecom.app.Controllers.Api
{
    [RoutePrefix("api/empresaconfiguracion")]
    public class EmpresaConfiguracionController : ApiController
    {
        EmpresaConfiguracionBl empresaConfiguracionBl = new EmpresaConfiguracionBl();

        [HttpGet]
        [Route("obtener-empresaconfiguracion")]
        public EmpresaConfiguracionBe ObtenerEmpresaConfiguracion(int empresaId, bool withListaMoneda = false, bool withListaTipoAfectacionIgv = false, bool withListaTipoComprobanteTipoOperacionVenta = false, bool withListaTipoProducto = false, bool withListaUnidadMedida = false)
        {
            return empresaConfiguracionBl.ObtenerEmpresaConfiguracion(empresaId, withListaMoneda, withListaTipoAfectacionIgv, withListaTipoComprobanteTipoOperacionVenta, withListaTipoProducto, withListaUnidadMedida);
        }
    }
}
