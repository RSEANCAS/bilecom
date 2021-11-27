using bilecom.be;
using bilecom.da;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.bl
{
    public class SunatBl : Conexion
    {
        SunatPadronDa sunatPadronDa = new SunatPadronDa();

        public SunatPadronBe ObtenerPadronPorRuc(string ruc)
        {
            SunatPadronBe item = null;
            try
            {
                using (var cn = new SqlConnection(CadenaConexion))
                {
                    cn.Open();
                    item = sunatPadronDa.ObtenerPorRuc(ruc, cn);
                    cn.Close();
                }
            }
            catch (Exception ex) { item = null; }
            return item;
        }
    }
}
