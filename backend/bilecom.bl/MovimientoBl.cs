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
    public class MovimientoBl : Conexion
    {
        MovimientoDa movimientoDa = new MovimientoDa();
        MovimientoDetalleDa movimientoDetalleDa = new MovimientoDetalleDa();
        SerieDa serieDa = new SerieDa();
        MonedaDa monedaDa = new MonedaDa();
        ClienteDa clienteDa = new ClienteDa();
        ProveedorDa proveedorDa = new ProveedorDa();
        PersonalDa personalDa = new PersonalDa();
        ProductoAlmacenDa productoAlmacenDa = new ProductoAlmacenDa();
        public List<MovimientoBe> Buscar(int empresaId, string nombresCompletosPersonal, string razonSocial, DateTime fechaHoraEmisionDesde, DateTime fechaHoraEmisionHasta, int pagina, int cantidadRegistros, string columnaOrden, string ordenMax, out int totalRegistros)
        {
            totalRegistros = 0;
            List<MovimientoBe> lista = null;
            try
            {
                cn.Open();
                lista = movimientoDa.Buscar(empresaId, nombresCompletosPersonal, razonSocial, fechaHoraEmisionDesde, fechaHoraEmisionHasta, pagina, cantidadRegistros, columnaOrden, ordenMax, cn, out totalRegistros);
                cn.Close();
            }
            catch (Exception ex) { lista = null; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return lista;
        }

        public MovimientoBe Obtener(int empresaId, int movimientoId, bool conSerie = false, bool conMoneda = false, bool conCliente = false, bool conPersonal = false, bool conListaDetalleMovimiento = false,bool conProveedor =false)
        {
            MovimientoBe item = null;
            try
            {
                cn.Open();
                item = movimientoDa.Obtener(empresaId, movimientoId, cn);
                if (item != null)
                {
                    if (conSerie) item.Serie = serieDa.Obtener(empresaId, item.SerieId, cn);
                    if (conMoneda) item.Moneda = monedaDa.Obtener(item.MonedaId, cn);
                    if (conCliente) item.Cliente = clienteDa.Obtener(empresaId, item.ClienteId, cn);
                    if (conPersonal) item.Personal = personalDa.Obtener(empresaId, item.PersonalId, cn);
                    if (conProveedor) item.Proveedor = proveedorDa.Obtener(empresaId, item.ProveedorId, cn);
                    if (conListaDetalleMovimiento) item.ListaMovimientoDetalle = movimientoDetalleDa.Listar(empresaId, item.MovimientoId, cn);
                }
                cn.Close();
            }
            catch (Exception) { item = null; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return item;
        }
        public bool Guardar(MovimientoBe registro)
        {
            int? movimientoId = null;
            bool seGuardo = false;
            {
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        cn.Open();
                        seGuardo = movimientoDa.Guardar(registro, cn, out movimientoId);
                        // Si seGuardo es True entonces 
                        if (seGuardo)
                        {
                            if (registro.ListaMovimientoDetalleEliminados != null)
                            {
                                foreach (int movimientoDetalleId in registro.ListaMovimientoDetalleEliminados)
                                {
                                    MovimientoDetalleBe registroDetalleEliminar = new MovimientoDetalleBe();
                                    registroDetalleEliminar.EmpresaId = registro.EmpresaId;
                                    registroDetalleEliminar.MovimientoId = (int)movimientoId;
                                    registroDetalleEliminar.MovimientoDetalleId= movimientoDetalleId;
                                    registroDetalleEliminar.Usuario = registro.Usuario;
                                    seGuardo = movimientoDetalleDa.Eliminar(registroDetalleEliminar, cn);

                                    if (!seGuardo) break;
                                }
                            }

                            // Si la Lista de Detalle es diferente de Null
                            if (registro.ListaMovimientoDetalle != null)
                            {
                                //Entonces recorro la misma Lista de detalle con el Item
                                foreach (var item in registro.ListaMovimientoDetalle)
                                {
                                    item.MovimientoId = (int)movimientoId;
                                    item.EmpresaId = registro.EmpresaId;
                                    item.Usuario = registro.Usuario;
                                    seGuardo = movimientoDetalleDa.Guardar(item, cn);
                                    //seGuardo = new 
                                    if (!seGuardo) break;

                                    ProductoAlmacenBe paBe = new ProductoAlmacenBe();
                                    paBe.EmpresaId = registro.EmpresaId;
                                    paBe.AlmacenId = registro.SedeAlmacenId;
                                    paBe.ProductoId = item.ProductoId;
                                    paBe.Monto = item.Cantidad;
                                    paBe.TipoMovimientoId = registro.TipoMovimientoId;
                                    paBe.Usuario = registro.Usuario;

                                    seGuardo = productoAlmacenDa.Guardar(paBe, cn);
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
    }
}
