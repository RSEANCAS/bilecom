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
    public class NotaDebitoBl : Conexion
    {
        NotaDebitoDa notaDebitoDa = new NotaDebitoDa();
        NotaDebitoDetalleDa notaDebitoDetalleDa = new NotaDebitoDetalleDa();

        public List<NotaDebitoBe> BuscarNotaDebito(int empresaId, int ambienteSunatId, string nroDocumentoIdentidadCliente, string razonSocialCliente, DateTime fechaHoraEmisionDesde, DateTime fechaHoraEmisionHasta, int pagina, int cantidadRegistros, string columnaOrden, string ordenMax, out int totalRegistros)
        {
            totalRegistros = 0;
            List<NotaDebitoBe> lista = null;
            try
            {
                cn.Open();
                lista = notaDebitoDa.Buscar(empresaId, ambienteSunatId, nroDocumentoIdentidadCliente, razonSocialCliente, fechaHoraEmisionDesde, fechaHoraEmisionHasta, pagina, cantidadRegistros, columnaOrden, ordenMax, cn, out totalRegistros);
                cn.Close();
            }
            catch (Exception ex) { lista = null; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return lista;
        }

        public bool GuardarNotaDebito(NotaDebitoBe registro, out int? notaDebitoId, out int? nroComprobante, out DateTime? fechaHoraEmision, out string totalImporteEnLetras)
        {
            notaDebitoId = null;
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
                        seGuardo = notaDebitoDa.Guardar(registro, cn, out notaDebitoId, out nroComprobante, out fechaHoraEmision, out totalImporteEnLetras);
                        if (seGuardo)
                        {
                            // Si la Lista de Detalle es diferente de Null
                            if (registro.ListaNotaDebitoDetalle != null)
                            {
                                //Entonces recorro la misma Lista de detalle con el Item
                                foreach (var item in registro.ListaNotaDebitoDetalle)
                                {
                                    int? notaDebitoDetalleId = null;

                                    item.NotaDebitoId = (int)notaDebitoId;
                                    item.EmpresaId = registro.EmpresaId;
                                    item.Usuario = registro.Usuario;
                                    seGuardo = notaDebitoDetalleDa.Guardar(item, cn, out notaDebitoDetalleId);
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

        public bool AnularNotaDebito(NotaDebitoBe registro)
        {
            bool seGuardo = false;
            {
                try
                {
                    cn.Open();
                    seGuardo = notaDebitoDa.Anular(registro, cn);
                    cn.Close();
                }
                catch (Exception ex) { seGuardo = false; }
                finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            }
            return seGuardo;
        }

        public bool GuardarRespuestaSunatNotaDebito(NotaDebitoBe registro)
        {
            bool seGuardo = false;
            {
                try
                {
                    cn.Open();
                    seGuardo = notaDebitoDa.GuardarRespuestaSunat(registro, cn);
                    cn.Close();
                }
                catch (Exception ex) { seGuardo = false; }
                finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            }
            return seGuardo;
        }
    }
}
