using bilecom.be;
using bilecom.da;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.bl
{
    public class MonedaBl : Conexion
    {
        MonedaDa monedaDa = new MonedaDa();

        public List<MonedaBe> ListarMoneda()
        {
            List<MonedaBe> lista = new List<MonedaBe>();
            try
            {
                using (var cn = new SqlConnection(CadenaConexion))
                {
                    cn.Open();
                    lista = monedaDa.Listar(cn);
                    cn.Close();
                }
            }
            catch (Exception) { lista = null; }
            //finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return lista;
        }

        public List<MonedaBe> ListarMonedaPorEmpresa(int empresaId)
        {
            List<MonedaBe> lista = new List<MonedaBe>();
            try
            {
                using (var cn = new SqlConnection(CadenaConexion))
                {
                    cn.Open();
                    lista = monedaDa.ListarPorEmpresa(empresaId, cn);
                    cn.Close();
                }
            }
            catch (Exception) { lista = null; }
            //finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return lista;
        }
    }
}
