﻿using bilecom.be;
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
    public class FacturaBl : Conexion
    {
        FacturaDa facturaDa = new FacturaDa();
        FacturaDetalleDa facturaDetalleDa = new FacturaDetalleDa();
        ClienteDa clienteDa = new ClienteDa();

        public List<FacturaBe> BuscarFactura(int empresaId, int ambienteSunatId, string nroDocumentoIdentidadCliente, string razonSocialCliente, DateTime fechaHoraEmisionDesde, DateTime fechaHoraEmisionHasta, int pagina, int cantidadRegistros, string columnaOrden, string ordenMax, out int totalRegistros)
        {
            totalRegistros = 0;
            List<FacturaBe> lista = null;
            try
            {
                cn.Open();
                lista = facturaDa.Buscar(empresaId, ambienteSunatId, nroDocumentoIdentidadCliente, razonSocialCliente, fechaHoraEmisionDesde, fechaHoraEmisionHasta, pagina, cantidadRegistros, columnaOrden, ordenMax, cn, out totalRegistros);
                cn.Close();
            }
            catch (Exception ex) { lista = null; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return lista;
        }

        public FacturaBe ObtenerFactura(int empresaId, int facturaId, bool conCliente = false, bool conDetalle = false)
        {
            FacturaBe item = null;
            try
            {
                cn.Open();
                item = facturaDa.Obtener(empresaId, facturaId, cn);

                if(item != null)
                {
                    if (conCliente) item.Cliente = clienteDa.Obtener(empresaId, item.ClienteId, cn);
                    if (conDetalle) item.ListaFacturaDetalle = facturaDetalleDa.Listar(empresaId, facturaId, cn);
                }
                cn.Close();
            }
            catch (SqlException ex) { item = null; }
            catch (Exception ex) { item = null; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return item;
        }

        public bool GuardarFactura(FacturaBe registro, out int? facturaId, out int? nroComprobante, out DateTime? fechaHoraEmision, out string totalImporteEnLetras)
        {
            facturaId = null;
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
                        seGuardo = facturaDa.Guardar(registro, cn, out facturaId, out nroComprobante, out fechaHoraEmision, out totalImporteEnLetras);
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

                            //if(registro.ListaFacturaGuiaRemision != null)
                            //{
                            //    foreach(var item in registro.ListaFacturaGuiaRemision)
                            //    {
                            //        int? facturaGuiaRemision = null;
                            //        item.FacturaId = (int)facturaId;
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

        public bool AnularFactura(FacturaBe registro)
        {
            bool seGuardo = false;
            {
                try
                {
                    cn.Open();
                    seGuardo = facturaDa.Anular(registro, cn);
                    cn.Close();
                }
                catch (Exception ex) { seGuardo = false; }
                finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            }
            return seGuardo;
        }

        public bool GuardarRespuestaSunatFactura(FacturaBe registro)
        {
            bool seGuardo = false;
            {
                try
                {
                    cn.Open();
                    seGuardo = facturaDa.GuardarRespuestaSunat(registro, cn);
                    cn.Close();
                }
                catch (Exception ex) { seGuardo = false; }
                finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            }
            return seGuardo;
        }
    }
}
