using bilecom.be;
using bilecom.ut;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.da
{
    public class OpcionDa
    {
        public List<OpcionBe> ListarPorPerfil(int perfilId, int empresaId, SqlConnection cn)
        {
            List<OpcionBe> lista = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_opcion_listar_x_perfil", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@perfilId", perfilId);
                    cmd.Parameters.AddWithValue("@empresaId", empresaId);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            lista = new List<OpcionBe>();
                            while (dr.Read())
                            {
                                OpcionBe item = new OpcionBe();
                                item.OpcionId = dr.GetData<int>("OpcionId");
                                item.Nombre = dr.GetData<string>("Nombre");
                                item.Enlace = dr.GetData<string>("Enlace");
                                item.OpcionPadreId = dr.GetData<int?>("OpcionPadreId");
                                item.FlagActivo = dr.GetData<bool>("FlagActivo");
                                lista.Add(item);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lista = null;
            }
            return lista;
        }
    }
}
