using bilecom.be;
using bilecom.da;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.bl
{
    public class EmpresaConfiguracionBl : Conexion
    {
        AmbienteSunatDa ambienteSunatDa = new AmbienteSunatDa();
        EmpresaConfiguracionDa empresaConfiguracionDa = new EmpresaConfiguracionDa();
        EmpresaAmbienteSunatDa empresaAmbienteSunatDa = new EmpresaAmbienteSunatDa();
        MonedaDa monedaDa = new MonedaDa();
        TipoAfectacionIgvDa tipoAfectacionIgvDa = new TipoAfectacionIgvDa();
        TipoComprobanteTipoOperacionVentaDa tipoComprobanteTipoOperacionVentaDa = new TipoComprobanteTipoOperacionVentaDa();
        TipoProductoDa tipoProductoDa = new TipoProductoDa();
        UnidadMedidaDa unidadMedidaDa = new UnidadMedidaDa();

        public EmpresaConfiguracionBe ObtenerEmpresaConfiguracion(int empresaId, bool withListaMoneda = false, bool withListaTipoAfectacionIgv = false, bool withListaTipoComprobanteTipoOperacionVenta = false, bool withListaTipoProducto = false, bool withListaUnidadMedida = false)
        {
            EmpresaConfiguracionBe item = null;

            try
            {
                cn.Open();

                item = empresaConfiguracionDa.Obtener(empresaId, cn);
                if(item != null)
                {
                    item.EmpresaAmbienteSunat = empresaAmbienteSunatDa.Obtener(item.EmpresaId, item.AmbienteSunatId, cn);
                    if (item.EmpresaAmbienteSunat != null) item.EmpresaAmbienteSunat.AmbienteSunat = ambienteSunatDa.Obtener(item.AmbienteSunatId, cn);

                    if (withListaMoneda || withListaTipoAfectacionIgv || withListaTipoComprobanteTipoOperacionVenta || withListaTipoProducto || withListaUnidadMedida)
                    {
                        if (withListaMoneda) item.ListaMoneda = monedaDa.ListarPorEmpresa(empresaId, cn);
                        if (withListaTipoAfectacionIgv) item.ListaTipoAfectacionIgv = tipoAfectacionIgvDa.ListarPorEmpresa(empresaId, cn);
                        if (withListaTipoComprobanteTipoOperacionVenta) item.ListaTipoComprobanteTipoOperacionVenta = tipoComprobanteTipoOperacionVentaDa.ListarPorEmpresa(empresaId, cn);
                        if (withListaTipoProducto) item.ListaTipoProducto = tipoProductoDa.ListarPorEmpresa(empresaId, cn);
                        if (withListaUnidadMedida) item.ListaUnidadMedida = unidadMedidaDa.ListarPorEmpresa(empresaId, cn);
                    }
                }
            }
            catch (Exception ex) { throw ex; }
            finally { if (cn.State == System.Data.ConnectionState.Open) cn.Close(); }

            return item;
        }
    }
}
