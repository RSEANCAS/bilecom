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
    public class NotaCreditoBl : Conexion
    {
        NotaCreditoDa notaCreditoDa = new NotaCreditoDa();
        NotaCreditoDetalleDa notaCreditoDetalleDa = new NotaCreditoDetalleDa();

        public List<NotaCreditoBe> BuscarNotaCredito(int empresaId, int ambienteSunatId, string nroDocumentoIdentidadCliente, string razonSocialCliente, DateTime fechaHoraEmisionDesde, DateTime fechaHoraEmisionHasta, int pagina, int cantidadRegistros, string columnaOrden, string ordenMax, out int totalRegistros)
        {
            totalRegistros = 0;
            List<NotaCreditoBe> lista = null;
            try
            {
                cn.Open();
                lista = notaCreditoDa.Buscar(empresaId, ambienteSunatId, nroDocumentoIdentidadCliente, razonSocialCliente, fechaHoraEmisionDesde, fechaHoraEmisionHasta, pagina, cantidadRegistros, columnaOrden, ordenMax, cn, out totalRegistros);
                cn.Close();
            }
            catch (Exception ex) { lista = null; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return lista;
        }

        public bool GuardarNotaCredito(NotaCreditoBe registro, out int? notaCreditoId, out int? nroComprobante, out DateTime? fechaHoraEmision, out string totalImporteEnLetras)
        {
            notaCreditoId = null;
            nroComprobante = null;
            fechaHoraEmision = null;
            totalImporteEnLetras = null;
            bool seGuardo = false;
            {
                try
                {
                    using (TransactionScope scope = new TransactionScope())
                    {
                        cn.Open();
                        seGuardo = notaCreditoDa.Guardar(registro, cn, out notaCreditoId, out nroComprobante, out fechaHoraEmision, out totalImporteEnLetras);
                        if (seGuardo)
                        {
                            // Si la Lista de Detalle es diferente de Null
                            if (registro.ListaNotaCreditoDetalle != null)
                            {
                                //Entonces recorro la misma Lista de detalle con el Item
                                foreach (var item in registro.ListaNotaCreditoDetalle)
                                {
                                    int? notaCreditoDetalleId = null;

                                    item.NotaCreditoId = (int)notaCreditoId;
                                    item.EmpresaId = registro.EmpresaId;
                                    item.Usuario = registro.Usuario;
                                    seGuardo = notaCreditoDetalleDa.Guardar(item, cn, out notaCreditoDetalleId);
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

        public bool AnularNotaCredito(NotaCreditoBe registro)
        {
            bool seGuardo = false;
            {
                try
                {
                    cn.Open();
                    seGuardo = notaCreditoDa.Anular(registro, cn);
                    cn.Close();
                }
                catch (Exception ex) { seGuardo = false; }
                finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            }
            return seGuardo;
        }

        public bool GuardarRespuestaSunatNotaCredito(NotaCreditoBe registro)
        {
            bool seGuardo = false;
            {
                try
                {
                    cn.Open();
                    seGuardo = notaCreditoDa.GuardarRespuestaSunat(registro, cn);
                    cn.Close();
                }
                catch (Exception ex) { seGuardo = false; }
                finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            }
            return seGuardo;
        }
    }
}
