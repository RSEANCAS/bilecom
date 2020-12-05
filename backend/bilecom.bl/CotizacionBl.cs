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
            cotizacionId = null;
            bool seGuardo = false;
            using (cn)
            {
                try
                {
                    cn.Open();
                    //seGuardo = new CotizacionDa()(cotizacion, cn);
                    seGuardo = new CotizacionDa().CotizacionGuardar(cotizacion, cn, out cotizacionId);
                    if (seGuardo)
                    {
                        if (cotizacion.ListaCotizacionDetalle != null)
                        {
                            foreach(var item in cotizacion.ListaCotizacionDetalle)
                            {
                                seGuardo = new 
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
