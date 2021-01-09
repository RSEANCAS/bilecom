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
    public class MonedaBl : Conexion
    {
        MonedaDa monedaDa = new MonedaDa();

        public List<MonedaBe> ListarMonedaPorEmpresa(int empresaId)
        {
            List<MonedaBe> lista = new List<MonedaBe>();
            try
            {
                cn.Open();
                lista = monedaDa.ListarPorEmpresa(empresaId, cn);
                cn.Close();
            }
            catch (Exception) { lista = null; }
            finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return lista;
        }
    }
}
