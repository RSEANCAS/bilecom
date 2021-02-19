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
    public class FacturaBl : Conexion
    {
        FacturaDa facturaDa = new FacturaDa();
        FacturaDetalleDa facturaDetalleDa = new FacturaDetalleDa();

        public List<FacturaBe> BuscarFactura(int empresaId, string nroDocumentoIdentidadCliente, string razonSocialCliente, DateTime fechaHoraEmisionDesde, DateTime fechaHoraEmisionHasta, int pagina, int cantidadRegistros, string columnaOrden, string ordenMax, out int totalRegistros)
        {
            totalRegistros = 0;
            List<FacturaBe> lista = null;
            try
            {
                cn.Open();
                lista = facturaDa.Buscar(empresaId, nroDocumentoIdentidadCliente, razonSocialCliente, fechaHoraEmisionDesde, fechaHoraEmisionHasta, pagina, cantidadRegistros, columnaOrden, ordenMax, cn, out totalRegistros);
                cn.Close();
            }
            catch (Exception) { lista = null; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return lista;
        }

        public bool GuardarFactura(FacturaBe registro, out int? facturaId, out int? nroComprobante, out DateTime? fechaHoraEmision)
        {
            facturaId = null;
            nroComprobante = null;
            fechaHoraEmision = null;
            bool seGuardo = false;
            {
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        cn.Open();
                        seGuardo = facturaDa.Guardar(registro, cn, out facturaId, out nroComprobante, out fechaHoraEmision);
                        // Si seGuardo es True entonces 
                        if (seGuardo)
                        {
                            //if (registro.ListaFacturaDetalleEliminados != null)
                            //{
                            //    foreach (int facturaDetalleId in registro.ListaFacturaDetalleEliminados)
                            //    {
                            //        FacturaDetalleBe registroDetalleEliminar = new FacturaDetalleBe();
                            //        registroDetalleEliminar.EmpresaId = registro.EmpresaId;
                            //        registroDetalleEliminar.FacturaId = (int)facturaId;
                            //        registroDetalleEliminar.FacturaDetalleId = facturaDetalleId;
                            //        registroDetalleEliminar.Usuario = registro.Usuario;
                            //        seGuardo = facturaDetalleDa.Eliminar(registroDetalleEliminar, cn);

                            //        if (!seGuardo) break;
                            //    }
                            //}

                            // Si la Lista de Detalle es diferente de Null
                            if (registro.ListaFacturaDetalle != null)
                            {
                                //Entonces recorro la misma Lista de detalle con el Item
                                foreach (var item in registro.ListaFacturaDetalle)
                                {
                                    int? facturaDetalleId = null;

                                    item.FacturaId = (int)facturaId;
                                    item.EmpresaId = registro.EmpresaId;
                                    item.Usuario = registro.Usuario;
                                    seGuardo = facturaDetalleDa.Guardar(item, cn, out facturaDetalleId);
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
    }
}
