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
        public List<TipoSedeBe> Listar(int empresaId)
        {
            List<TipoSedeBe> lTipoSede = new List<TipoSedeBe>();
            using (cn)
            {
                try
                {
                    cn.Open();
                    lTipoSede = new TipoSedeDa().fListar(cn, empresaId);
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
