using bilecom.be;
using bilecom.da;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.bl
{
    public class EmpresaBl : Conexion
    {
        EmpresaDa empresaDa = new EmpresaDa();

        public EmpresaBe Obtener(int empresaId)
        {
            EmpresaBe item = null;

            try
            {
                cn.Open();

                item = empresaDa.Obtener(empresaId, cn);
            }
            catch (Exception ex) { throw ex; }
            finally { cn.Close(); }

            return item;
        }

        public EmpresaBe ObtenerPorRuc(string ruc)
        {
            EmpresaBe item = null;

            try
            {
                cn.Open();

                item = empresaDa.ObtenerPorRuc(ruc, cn);
            }
            catch (Exception ex) { throw ex; }
            finally { cn.Close(); }

            return item;
        }
    }
}
