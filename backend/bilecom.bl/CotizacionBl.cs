using bilecom.be;
using bilecom.da;
using System;
using System.Collections.Generic;
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
                    lCotizacion = new CotizacionDa().fListar(cn, EmpresaId, NombresCompletosPersonal, RazonSocial, FechaHoraEmisionDesde, FechaHoraEmisionHasta);
                    cn.Close();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return lCotizacion;
        }
    }
}
