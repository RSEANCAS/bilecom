using bilecom.be;
using bilecom.da;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace bilecom.bl
{
    public class BoletaBl : Conexion
    {
        BoletaDa boletaDa = new BoletaDa();
        BoletaDetalleDa boletaDetalleDa = new BoletaDetalleDa();
        ClienteDa clienteDa = new ClienteDa();

        public List<BoletaBe> BuscarBoleta(int empresaId, int ambienteSunatId, string nroDocumentoIdentidadCliente, string razonSocialCliente, DateTime fechaHoraEmisionDesde, DateTime fechaHoraEmisionHasta, int pagina, int cantidadRegistros, string columnaOrden, string ordenMax, out int totalRegistros)
        {
            totalRegistros = 0;
            List<BoletaBe> lista = null;
            try
            {
                cn.Open();
                lista = boletaDa.Buscar(empresaId, ambienteSunatId, nroDocumentoIdentidadCliente, razonSocialCliente, fechaHoraEmisionDesde, fechaHoraEmisionHasta, pagina, cantidadRegistros, columnaOrden, ordenMax, cn, out totalRegistros);
                cn.Close();
            }
            catch (Exception ex) { lista = null; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return lista;
        }

        public BoletaBe ObtenerBoleta(int empresaId, int boletaId, bool conCliente = false, bool conDetalle = false)
        {
            BoletaBe item = null;
            try
            {
                cn.Open();
                item = boletaDa.Obtener(empresaId, boletaId, cn);

                if (item != null)
                {
                    if (conCliente) item.Cliente = clienteDa.Obtener(empresaId, item.ClienteId, cn);
                    if (conDetalle) item.ListaBoletaDetalle = boletaDetalleDa.Listar(empresaId, boletaId, cn);
                }
                cn.Close();
            }
            catch (SqlException ex) { item = null; }
            catch (Exception ex) { item = null; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return item;
        }

        public bool GuardarBoleta(BoletaBe registro, out int? boletaId, out int? nroComprobante, out DateTime? fechaHoraEmision, out string totalImporteEnLetras)
        {
            boletaId = null;
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
                        seGuardo = boletaDa.Guardar(registro, cn, out boletaId, out nroComprobante, out fechaHoraEmision, out totalImporteEnLetras);
                        // Si seGuardo es True entonces 
                        if (seGuardo)
                        {
                            //if (registro.ListaBoletaDetalleEliminados != null)
                            //{
                            //    foreach (int boletaDetalleId in registro.ListaBoletaDetalleEliminados)
                            //    {
                            //        BoletaDetalleBe registroDetalleEliminar = new BoletaDetalleBe();
                            //        registroDetalleEliminar.EmpresaId = registro.EmpresaId;
                            //        registroDetalleEliminar.BoletaId = (int)boletaId;
                            //        registroDetalleEliminar.BoletaDetalleId = boletaDetalleId;
                            //        registroDetalleEliminar.Usuario = registro.Usuario;
                            //        seGuardo = boletaDetalleDa.Eliminar(registroDetalleEliminar, cn);

                            //        if (!seGuardo) break;
                            //    }
                            //}

                            // Si la Lista de Detalle es diferente de Null
                            if (registro.ListaBoletaDetalle != null)
                            {
                                //Entonces recorro la misma Lista de detalle con el Item
                                foreach (var item in registro.ListaBoletaDetalle)
                                {
                                    int? boletaDetalleId = null;

                                    item.BoletaId = (int)boletaId;
                                    item.EmpresaId = registro.EmpresaId;
                                    item.Usuario = registro.Usuario;
                                    seGuardo = boletaDetalleDa.Guardar(item, cn, out boletaDetalleId);
                                    //seGuardo = new 
                                    if (!seGuardo) break;
                                }
                            }

                            //if(registro.ListaBoletaGuiaRemision != null)
                            //{
                            //    foreach(var item in registro.ListaBoletaGuiaRemision)
                            //    {
                            //        int? boletaGuiaRemision = null;
                            //        item.BoletaId = (int)boletaId;
                            //        item.EmpresaId = registro.EmpresaId;
                            //        item.Usuario = registro.Usuario;
                            //        seg
                            //    }
                            //}
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

        public bool AnularBoleta(BoletaBe registro)
        {
            bool seGuardo = false;
            {
                try
                {
                    cn.Open();
                    seGuardo = boletaDa.Anular(registro, cn);
                    cn.Close();
                }
                catch (Exception ex) { seGuardo = false; }
                finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            }
            return seGuardo;
        }

        public bool GuardarRespuestaSunatBoleta(BoletaBe registro)
        {
            bool seGuardo = false;
            {
                try
                {
                    cn.Open();
                    seGuardo = boletaDa.GuardarRespuestaSunat(registro, cn);
                    cn.Close();
                }
                catch (Exception ex) { seGuardo = false; }
                finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            }
            return seGuardo;
        }
    }
}
