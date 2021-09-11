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
    public class AmbienteSunatBl : Conexion
    {
        AmbienteSunatDa ambienteSunatDa = new AmbienteSunatDa();

        public List<AmbienteSunatBe> ListarAmbienteSunat()
        {
            List<AmbienteSunatBe> lista = null;
            try
            {
                using (var cn = new SqlConnection(CadenaConexion))
                {
                    cn.Open();
                    lista = ambienteSunatDa.Listar(cn);
                    cn.Close();
                }
            }
            catch (Exception ex) { lista = null; }
            //finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return lista;
        }

        public AmbienteSunatBe ObtenerAmbienteSunat(int ambienteSunatId)
        {
            AmbienteSunatBe item = null;
            try
            {
                using (var cn = new SqlConnection(CadenaConexion))
                {
                    cn.Open();
                    item = ambienteSunatDa.Obtener(ambienteSunatId, cn);
                    cn.Close();
                }
            }
            catch (Exception ex) { item = null; }
            //finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return item;
        }
    }
}
