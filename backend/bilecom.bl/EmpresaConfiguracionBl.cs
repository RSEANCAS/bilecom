using bilecom.be;
using bilecom.da;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace bilecom.bl
{
    public class EmpresaConfiguracionBl : Conexion
    {
        AmbienteSunatDa ambienteSunatDa = new AmbienteSunatDa();
        EmpresaDa empresaDa = new EmpresaDa();
        EmpresaConfiguracionDa empresaConfiguracionDa = new EmpresaConfiguracionDa();
        EmpresaAmbienteSunatDa empresaAmbienteSunatDa = new EmpresaAmbienteSunatDa();
        EmpresaMonedaDa empresaMonedaDa = new EmpresaMonedaDa();
        EmpresaTipoAfectacionIgvDa empresaTipoAfectacionIgvDa = new EmpresaTipoAfectacionIgvDa();
        EmpresaTipoComprobanteTipoOperacionVentaDa empresaTipoComprobanteTipoOperacionVentaDa = new EmpresaTipoComprobanteTipoOperacionVentaDa();
        EmpresaTipoProductoDa empresaTipoProductoDa = new EmpresaTipoProductoDa();
        EmpresaUnidadMedidaDa empresaUnidadMedidaDa = new EmpresaUnidadMedidaDa();
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
                using (var cn = new SqlConnection(CadenaConexion))
                {
                    cn.Open();

                    item = empresaConfiguracionDa.Obtener(empresaId, cn);
                    if (item != null)
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
            }
            catch (Exception ex) { throw ex; }
            //finally { if (cn.State == System.Data.ConnectionState.Open) cn.Close(); }

            return item;
        }

        public bool GuardarEmpresaConfiguracion(EmpresaConfiguracionBe registro, bool saveEmpresa = false)
        {
            bool seGuardo = false;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (var cn = new SqlConnection(CadenaConexion))
                    {
                        cn.Open();
                        seGuardo = empresaConfiguracionDa.Guardar(registro, cn);
                        if (seGuardo && saveEmpresa)
                        {
                            seGuardo = empresaDa.Guardar(registro.Empresa, cn, out int? empresaId);
                        }

                        if (seGuardo && saveEmpresa && registro.ListaMonedaPorDefecto != null)
                        {
                            seGuardo = empresaMonedaDa.EliminarPorEmpresa(registro.EmpresaId, cn);

                            if (seGuardo)
                            {
                                foreach (var item in registro.ListaMonedaPorDefecto)
                                {
                                    seGuardo = empresaMonedaDa.Guardar(registro.EmpresaId, item.MonedaId, cn);
                                    if (!seGuardo) break;
                                }
                            }
                        }

                        if (seGuardo && saveEmpresa && registro.ListaTipoAfectacionIgvPorDefecto != null)
                        {
                            seGuardo = empresaTipoAfectacionIgvDa.EliminarPorEmpresa(registro.EmpresaId, cn);

                            if (seGuardo)
                            {
                                foreach (var item in registro.ListaTipoAfectacionIgvPorDefecto)
                                {
                                    seGuardo = empresaTipoAfectacionIgvDa.Guardar(registro.EmpresaId, item.TipoAfectacionIgvId, cn);
                                    if (!seGuardo) break;
                                }
                            }
                        }

                        if (seGuardo && saveEmpresa && registro.ListaTipoComprobanteTipoOperacionVentaPorDefecto != null)
                        {
                            seGuardo = empresaTipoComprobanteTipoOperacionVentaDa.EliminarPorEmpresa(registro.EmpresaId, cn);

                            if (seGuardo)
                            {
                                foreach (var item in registro.ListaTipoComprobanteTipoOperacionVentaPorDefecto)
                                {
                                    seGuardo = empresaTipoComprobanteTipoOperacionVentaDa.Guardar(registro.EmpresaId, item.TipoComprobanteId, item.TipoOperacionVentaId, cn);
                                    if (!seGuardo) break;
                                }
                            }
                        }

                        if (seGuardo && saveEmpresa && registro.ListaTipoProductoPorDefecto != null)
                        {
                            seGuardo = empresaTipoProductoDa.EliminarPorEmpresa(registro.EmpresaId, cn);

                            if (seGuardo)
                            {
                                foreach (var item in registro.ListaTipoProductoPorDefecto)
                                {
                                    seGuardo = empresaTipoProductoDa.Guardar(registro.EmpresaId, item.TipoProductoId, cn);
                                    if (!seGuardo) break;
                                }
                            }
                        }

                        if (seGuardo && saveEmpresa && registro.ListaUnidadMedidaPorDefecto != null)
                        {
                            seGuardo = empresaUnidadMedidaDa.EliminarPorEmpresa(registro.EmpresaId, cn);

                            if (seGuardo)
                            {
                                foreach (var item in registro.ListaUnidadMedidaPorDefecto)
                                {
                                    seGuardo = empresaUnidadMedidaDa.Guardar(registro.EmpresaId, item.UnidadMedidaId, cn);
                                    if (!seGuardo) break;
                                }
                            }
                        }
                        if (seGuardo) scope.Complete();
                        cn.Close();
                    }
                }
            }
            catch (Exception ex) { seGuardo = false; }
            //finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return seGuardo;
        }
    }
}
