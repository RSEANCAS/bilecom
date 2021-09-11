using bilecom.be;
using bilecom.be.Custom;
using bilecom.bl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using static bilecom.enums.Enums;

namespace bilecom.app.Controllers.Api
{
    [RoutePrefix("api/empresa")]
    public class EmpresaController : ApiController
    {
        EmpresaBl empresaBl = new EmpresaBl();
        EmpresaConfiguracionBl empresaConfiguracionBl = new EmpresaConfiguracionBl();
        DistritoBl distritoBl = new DistritoBl();
        ProvinciaBl provinciaBl = new ProvinciaBl();
        DepartamentoBl departamentoBl = new DepartamentoBl();
        PaisBl paisBl = new PaisBl();

        [HttpGet]
        [Route("validar-empresa-por-ruc")]
        public IHttpActionResult ValidarEmpresaPorRuc(string ruc)
        {
            BootStrapValidator.Remote item = new BootStrapValidator.Remote();

            if (string.IsNullOrEmpty((ruc ?? "").Trim()))
                return Ok(item);

            var empresa = empresaBl.ObtenerEmpresaPorRuc(ruc);

            if (empresa == null)
                return Ok(item);

            item.valid = true;

            return Ok(item);
        }

        [HttpGet]
        [Route("obtener-empresa")]
        public EmpresaBe ObtenerEmpresa(int empresaId, bool withUbigeo = false, bool withEmpresaConfiguracion = false, bool withListaMoneda = false, bool withListaTipoAfectacionIgv = false, bool withListaTipoComprobanteTipoOperacionVenta = false, bool withListaTipoProducto = false, bool withListaUnidadMedida = false, bool withLogo = false, bool withLogoFormato = false)
        {
            List<ColumnasEmpresaImagen> columnasEmpresaImagen = new List<ColumnasEmpresaImagen>();
            if (withLogo)
            {
                columnasEmpresaImagen.Add(ColumnasEmpresaImagen.LogoNombre);
                columnasEmpresaImagen.Add(ColumnasEmpresaImagen.LogoTipoContenido);
                columnasEmpresaImagen.Add(ColumnasEmpresaImagen.Logo);
            }

            if (withLogoFormato)
            {
                columnasEmpresaImagen.Add(ColumnasEmpresaImagen.LogoFormatoNombre);
                columnasEmpresaImagen.Add(ColumnasEmpresaImagen.LogoFormatoTipoContenido);
                columnasEmpresaImagen.Add(ColumnasEmpresaImagen.LogoFormato);
            }

            var empresa = empresaBl.ObtenerEmpresa(empresaId, withUbigeo: withUbigeo, withConfiguracion: withEmpresaConfiguracion, withListaMoneda: withListaMoneda, withListaTipoAfectacionIgv: withListaTipoAfectacionIgv, withListaTipoComprobanteTipoOperacionVenta: withListaTipoComprobanteTipoOperacionVenta, withListaTipoProducto: withListaTipoProducto, withListaUnidadMedida: withListaUnidadMedida, columnasEmpresaImagen: columnasEmpresaImagen);

            //if(empresa != null && (withEmpresaConfiguracion || withUbigeo))
            //{
            //    if (withUbigeo) empresa.Distrito = distritoBl.ObtenerDistrito(empresa.DistritoId, withUbigeo);
            //    if (withEmpresaConfiguracion) empresa.EmpresaConfiguracion = empresaConfiguracionBl.ObtenerEmpresaConfiguracion(empresaId, withListaMoneda, withListaTipoAfectacionIgv, withListaTipoComprobanteTipoOperacionVenta, withListaTipoProducto, withListaUnidadMedida);
            //}

            return empresa;
        }
    }
}
