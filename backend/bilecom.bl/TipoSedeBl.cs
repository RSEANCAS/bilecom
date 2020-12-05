using bilecom.be;
using bilecom.da;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.bl
{
    public class TipoSedeBl : Conexion
    {
        public List<TipoSedeBe> Listar(int empresaId, string nombre, int pagina, int cantidadRegistros, string columnaOrden, string ordenMax, out int totalRegistros)
        {
            totalRegistros = 0;
            List<TipoSedeBe> lTipoSede = new List<TipoSedeBe>();
            using (cn)
            {
                try
                {
                    cn.Open();
                    lTipoSede = new TipoSedeDa().fListar(cn, empresaId, nombre, pagina, cantidadRegistros, columnaOrden, ordenMax, out totalRegistros);
                    cn.Close();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return lTipoSede;
        }
    }
}
