using bilecom.be;
using bilecom.da;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.bl
{
    public class CotizacionBl:Conexion
    {
        
        //Como ultimo paso definir las Reglas de negocio (listar, insertar, eliminar)
        public List<CotizacionBe> Listar(int EmpresaId, string NombresCompletosPersonal, string RazonSocial, DateTime FechaHoraEmisionDesde, DateTime FechaHoraEmisionHasta)
        {
            List <CotizacionBe> lCotizacion = new List<CotizacionBe>();
            using (cn)
            {
                try
                {
                    using (TransactionScope scope = new TransactionScope())

                    cn.Open();
                    lCotizacion = new CotizacionDa().Listar(cn, EmpresaId, NombresCompletosPersonal, RazonSocial, FechaHoraEmisionDesde, FechaHoraEmisionHasta);
                    cn.Close();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return lCotizacion;
        }
        public bool CotizacionGuardar(CotizacionBe cotizacion, out int? cotizacionId)
        {
            cotizacionId = 0;
            bool seGuardo = false;           
            {
                try
                {

                    seGuardo = new CotizacionDa().CotizacionGuardar(cotizacion, cn, out cotizacionId);
                    // Si seGuardo es True entonces 
                    if (seGuardo)
                    {
                        // Si la Lista de Detalle es diferente de Null
                        if (cotizacion.ListaCotizacionDetalle != null)
                        {
                            //Entonces recorro la misma Lista de detalle con el Item
                            foreach(var item in cotizacion.ListaCotizacionDetalle)
                            {
                                item.CotizacionId = (int)cotizacionId;
                                seGuardo = new CotizacionDetalleDa().CotizacionDetalleGuardar(item, cn);
                            }
                        }
                    }
                    cn.Close();
                }
                catch (Exception ex)
                {
                    seGuardo = false;
                }
                finally
                {
                    if (cn.State == ConnectionState.Open) cn.Close();
                }
            }
            return seGuardo;
        }
    }
}
