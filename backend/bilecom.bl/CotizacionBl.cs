using bilecom.be;
using bilecom.da;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace bilecom.bl
{
    public class CotizacionBl : Conexion
    {
        CotizacionDa cotizacionDa = new CotizacionDa();
        CotizacionDetalleDa cotizacionDetalleDa = new CotizacionDetalleDa();
        SerieDa serieDa = new SerieDa();
        MonedaDa monedaDa = new MonedaDa();
        ClienteDa clienteDa = new ClienteDa();
        PersonalDa personalDa = new PersonalDa();

        //Como ultimo paso definir las Reglas de negocio (listar, insertar, eliminar)
        public List<CotizacionBe> BuscarCotizacion(int empresaId, string nombresCompletosPersonal, string razonSocialCliente, DateTime fechaHoraEmisionDesde, DateTime fechaHoraEmisionHasta, int pagina, int cantidadRegistros, string columnaOrden, string ordenMax, out int totalRegistros)
        {
            totalRegistros = 0;
            List<CotizacionBe> lista = null;
            try
            {
                cn.Open();
                lista = cotizacionDa.Buscar(empresaId, nombresCompletosPersonal, razonSocialCliente, fechaHoraEmisionDesde, fechaHoraEmisionHasta, pagina, cantidadRegistros, columnaOrden, ordenMax, cn, out totalRegistros);
                cn.Close();
            }
            catch (Exception) { lista = null; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return lista;
        }

        public CotizacionBe ObtenerCotizacion(int empresaId, int cotizacionId, bool conSerie = false, bool conMoneda = false, bool conCliente = false, bool conPersonal = false, bool conListaDetalleCotizacion = false)
        {
            CotizacionBe item = null;
            try
            {
                cn.Open();
                item = cotizacionDa.Obtener(empresaId, cotizacionId, cn);
                if(item != null)
                {
                    if (conSerie) item.Serie = serieDa.Obtener(empresaId, item.SerieId, cn);
                    if (conMoneda) item.Moneda = monedaDa.Obtener(item.MonedaId, cn);
                    if (conCliente) item.Cliente = clienteDa.Obtener(empresaId, item.ClienteId, cn);
                    if (conPersonal) item.Personal = personalDa.Obtener(empresaId, item.PersonalId, cn);
                    if (conListaDetalleCotizacion) item.ListaCotizacionDetalle = cotizacionDetalleDa.Listar(empresaId, item.CotizacionId, cn);
                }
                cn.Close();
            }
            catch (Exception) { item = null; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return item;
        }

        public bool GuardarCotizacion(CotizacionBe registro)
        {
            int? cotizacionId = null;
            bool seGuardo = false;
            {
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        cn.Open();
                        seGuardo = cotizacionDa.Guardar(registro, cn, out cotizacionId);
                        // Si seGuardo es True entonces 
                        if (seGuardo)
                        {
                            if(registro.ListaCotizacionDetalleEliminados != null)
                            {
                                foreach (int cotizacionDetalleId in registro.ListaCotizacionDetalleEliminados)
                                {
                                    CotizacionDetalleBe registroDetalleEliminar = new CotizacionDetalleBe();
                                    registroDetalleEliminar.EmpresaId = registro.EmpresaId;
                                    registroDetalleEliminar.CotizacionId = (int)cotizacionId;
                                    registroDetalleEliminar.CotizacionDetalleId = cotizacionDetalleId;
                                    registroDetalleEliminar.Usuario = registro.Usuario;
                                    seGuardo = cotizacionDetalleDa.Eliminar(registroDetalleEliminar, cn);

                                    if (!seGuardo) break;
                                }
                            }

                            // Si la Lista de Detalle es diferente de Null
                            if (registro.ListaCotizacionDetalle != null)
                            {
                                //Entonces recorro la misma Lista de detalle con el Item
                                foreach (var item in registro.ListaCotizacionDetalle)
                                {
                                    item.CotizacionId = (int)cotizacionId;
                                    item.EmpresaId = registro.EmpresaId;
                                    item.Usuario = registro.Usuario;
                                    seGuardo = cotizacionDetalleDa.Guardar(item, cn);
                                    //seGuardo = new 
                                    if (!seGuardo) break;
                                }
                            }
                        }

                        if (seGuardo) scope.Complete();
                        cn.Close();
                    }
                }
                catch (Exception ex) { seGuardo = false; }
                finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            }
            return seGuardo;
        }

        public bool AnularCotizacion(CotizacionBe registro)
        {
            bool seGuardo = false;
            {
                try
                {
                    cn.Open();
                    seGuardo = cotizacionDa.Anular(registro, cn);
                    cn.Close();
                }
                catch (Exception ex) { seGuardo = false; }
                finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            }
            return seGuardo;
        }
    }
}
