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

        //Como ultimo paso definir las Reglas de negocio (listar, insertar, eliminar)
        public List<CotizacionBe> BuscarCotizacion(int empresaId, string nombresCompletosPersonal, string razonSocial, DateTime fechaHoraEmisionDesde, DateTime fechaHoraEmisionHasta, int pagina, int cantidadRegistros, string columnaOrden, string ordenMax, out int totalRegistros)
        {
            totalRegistros = 0;
            List<CotizacionBe> lista = new List<CotizacionBe>();
            try
            {
                cn.Open();
                lista = cotizacionDa.Buscar(empresaId, nombresCompletosPersonal, razonSocial, fechaHoraEmisionDesde, fechaHoraEmisionHasta, pagina, cantidadRegistros, columnaOrden, ordenMax, cn, out totalRegistros);
                cn.Close();
            }
            catch (Exception) { lista = null; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return lista;
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
                            // Si la Lista de Detalle es diferente de Null
                            if (registro.ListaCotizacionDetalle != null)
                            {
                                //Entonces recorro la misma Lista de detalle con el Item
                                foreach (var item in registro.ListaCotizacionDetalle)
                                {
                                    item.CotizacionId = (int)cotizacionId;
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
    }
}
