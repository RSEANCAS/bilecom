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
    public class TipoAfectacionIgvBl:Conexion
    {
        TipoAfectacionIgvDa tipoAfectacionIgvDa = new TipoAfectacionIgvDa();
        TipoTributoDa tipoTributoDa = new TipoTributoDa();

        public List<TipoAfectacionIgvBe> ListarTipoAfectacionIgv()
        {
            List<TipoAfectacionIgvBe> respuesta = null;
            try
            {
                using (var cn = new SqlConnection(CadenaConexion))
                {
                    cn.Open();
                    respuesta = tipoAfectacionIgvDa.Listar(cn);
                    cn.Close();
                }
            }
            catch(Exception ex)
            {
                respuesta = null;
            }
            //finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return respuesta;
        }

        public List<TipoAfectacionIgvBe> ListarTipoAfectacionIgvPorEmpresa(int empresaId, bool withTipoTributo = false)
        {
            List<TipoAfectacionIgvBe> respuesta = null;
            try
            {
                using (var cn = new SqlConnection(CadenaConexion))
                {
                    cn.Open();
                    respuesta = new TipoAfectacionIgvDa().ListarPorEmpresa(empresaId, cn);
                    if (respuesta != null)
                    {
                        foreach (var item in respuesta)
                        {
                            item.TipoTributo = tipoTributoDa.Obtener(item.TipoTributoId, cn);
                        }
                    }
                    cn.Close();
                }
            }
            catch (Exception ex)
            {
                respuesta = null;
            }
            //finally { if (cn.State == ConnectionState.Open) cn.Close(); }
            return respuesta;
        }
    }
}
