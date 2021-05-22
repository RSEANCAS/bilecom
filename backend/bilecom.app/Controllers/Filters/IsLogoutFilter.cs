using bilecom.bl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace bilecom.app.Controllers.Filters
{
    public class IsLogoutFilter : _BaseFilter
    {
        PerfilBl perfilBl = new PerfilBl();
        EmpresaBl empresaBl = new EmpresaBl();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!IsLogin)
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { Controller = "Login", Action = "Index" }));
            }
            else
            {
                int empresaId = Data.Usuario.Empresa.EmpresaId;
                int usuarioId = Data.Usuario.Id;

                var empresa = empresaBl.ObtenerEmpresa(empresaId, withUbigeo: true, withConfiguracion: true, columnasEmpresaImagen: new List<enums.Enums.ColumnasEmpresaImagen> { enums.Enums.ColumnasEmpresaImagen.Logo, enums.Enums.ColumnasEmpresaImagen.LogoTipoContenido });

                filterContext.Controller.ViewBag.Empresa = empresa;
                filterContext.Controller.ViewBag.Data = Data;
                filterContext.Controller.ViewBag.ListaPerfil = perfilBl.ListarPerfilPorUsuario(empresaId, usuarioId, loadListaOpcion: true);
            }

            base.OnActionExecuting(filterContext);
        }
    }
}