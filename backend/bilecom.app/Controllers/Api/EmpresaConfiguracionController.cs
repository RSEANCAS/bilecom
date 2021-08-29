using bilecom.be;
using bilecom.bl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
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

        [HttpPost]
        [Route("guardar-empresaconfiguracion")]
        public bool GuardarEmpresaConfiguracion()
        {
            bool seGuardo = false;
            if (!Request.Content.IsMimeMultipartContent()) return seGuardo;
            MemoryStream msLogoFile = new MemoryStream(), msLogoFormatoFile = new MemoryStream();
            var logoFile = System.Web.HttpContext.Current.Request.Files["Empresa.EmpresaImagen.LogoFile"];
            var logoFileFormato = System.Web.HttpContext.Current.Request.Files["Empresa.EmpresaImagen.LogoFormatoFile"];
            logoFile.InputStream.CopyTo(msLogoFile);
            logoFileFormato.InputStream.CopyTo(msLogoFormatoFile);

            string listaMonedaPorDefectoStr = System.Web.HttpContext.Current.Request.Form["ListaMoneda"];
            var listaMonedaPorDefecto = string.IsNullOrEmpty(listaMonedaPorDefectoStr) ? null : listaMonedaPorDefectoStr.Split(',').Select(x => new MonedaBe { MonedaId = int.Parse(x) }).ToList();

            string listaTipoAfectacionIgvPorDefectoStr = System.Web.HttpContext.Current.Request.Form["ListaTipoAfectacionIgv"];
            var listaTipoAfectacionIgvPorDefecto = string.IsNullOrEmpty(listaTipoAfectacionIgvPorDefectoStr) ? null : listaTipoAfectacionIgvPorDefectoStr.Split(',').Select(x => new TipoAfectacionIgvBe { TipoAfectacionIgvId = int.Parse(x) }).ToList();

            string listaTipoOperacionVentaPorDefectoStr = System.Web.HttpContext.Current.Request.Form["ListaTipoComprobanteTipoOperacionVenta"];
            var listaTipoOperacionVentaPorDefecto = string.IsNullOrEmpty(listaTipoOperacionVentaPorDefectoStr) ? null : listaTipoOperacionVentaPorDefectoStr.Split(',').Select(x => { string[] values = x.Split('|'); return new TipoComprobanteTipoOperacionVentaBe { TipoComprobanteId = int.Parse(values[0]), TipoOperacionVentaId = int.Parse(values[1]) }; }).ToList();

            string listaTipoProductoPorDefectoStr = System.Web.HttpContext.Current.Request.Form["ListaTipoProducto"];
            var listaTipoProductoPorDefecto = string.IsNullOrEmpty(listaTipoProductoPorDefectoStr) ? null : listaTipoProductoPorDefectoStr.Split(',').Select(x => new TipoProductoBe { TipoProductoId = int.Parse(x) }).ToList();

            string listaUnidadMedidaPorDefectoStr = System.Web.HttpContext.Current.Request.Form["ListaUnidadMedida"];
            var listaUnidadMedidaPorDefecto = string.IsNullOrEmpty(listaUnidadMedidaPorDefectoStr) ? null : listaUnidadMedidaPorDefectoStr.Split(',').Select(x => new UnidadMedidaBe { UnidadMedidaId = int.Parse(x) }).ToList();

            var registro = new EmpresaConfiguracionBe();
            registro.EmpresaId = int.Parse(System.Web.HttpContext.Current.Request.Form["EmpresaId"]);
            registro.Empresa = new EmpresaBe();
            registro.Empresa.EmpresaId = registro.EmpresaId;
            registro.Empresa.NombreComercial = System.Web.HttpContext.Current.Request.Form["Empresa.NombreComercial"];
            registro.Empresa.EmpresaImagen = new EmpresaImagenBe();
            registro.Empresa.EmpresaImagen.Logo = msLogoFile.ToArray();
            registro.Empresa.EmpresaImagen.LogoTipoContenido = logoFile.ContentType;
            registro.Empresa.EmpresaImagen.LogoFormato = msLogoFormatoFile.ToArray();
            registro.Empresa.EmpresaImagen.LogoFormatoTipoContenido = logoFileFormato.ContentType;
            registro.ListaMonedaPorDefecto = listaMonedaPorDefecto;
            registro.MonedaIdPorDefecto = int.Parse(System.Web.HttpContext.Current.Request.Form["MonedaIdPorDefecto"]);
            registro.ListaTipoAfectacionIgvPorDefecto = listaTipoAfectacionIgvPorDefecto;
            registro.TipoAfectacionIgvIdPorDefecto = int.Parse(System.Web.HttpContext.Current.Request.Form["TipoAfectacionIgvIdPorDefecto"]);
            registro.ListaTipoComprobanteTipoOperacionVentaPorDefecto = listaTipoOperacionVentaPorDefecto;
            registro.TipoComprobanteTipoOperacionVentaIdsPorDefecto = System.Web.HttpContext.Current.Request.Form["TipoComprobanteTipoOperacionVentaIdsPorDefecto"];
            registro.ListaTipoProductoPorDefecto = listaTipoProductoPorDefecto;
            registro.TipoProductoIdPorDefecto = int.Parse(System.Web.HttpContext.Current.Request.Form["TipoProductoIdPorDefecto"]);
            registro.ListaUnidadMedidaPorDefecto = listaUnidadMedidaPorDefecto;
            registro.UnidadMedidaIdPorDefecto = int.Parse(System.Web.HttpContext.Current.Request.Form["UnidadMedidaIdPorDefecto"]);
            registro.CuentaCorriente = System.Web.HttpContext.Current.Request.Form["CuentaCorriente"];
            registro.ComentarioLegal = System.Web.HttpContext.Current.Request.Form["ComentarioLegal"];
            registro.ComentarioLegalDetraccion = System.Web.HttpContext.Current.Request.Form["ComentarioLegalDetraccion"];
            registro.FormatoIds = System.Web.HttpContext.Current.Request.Form["FormatoIds"];
            registro.CantidadDecimalGeneral = int.Parse(System.Web.HttpContext.Current.Request.Form["CantidadDecimalGeneral"]);
            registro.CantidadDecimalDetallado = int.Parse(System.Web.HttpContext.Current.Request.Form["CantidadDecimalDetallado"]);

            seGuardo = empresaConfiguracionBl.GuardarEmpresaConfiguracion(registro, true);

            return seGuardo;
        }
    }
}
