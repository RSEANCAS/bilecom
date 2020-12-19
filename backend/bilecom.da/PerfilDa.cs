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
    public class PerfilDa
    {
        public List<PerfilBe> ListarPorUsuario(int usuarioId, int empresaId, SqlConnection cn)
        {
            List<PerfilBe> lista = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_perfil_listar_x_usuario", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@usuarioId", usuarioId);
                    cmd.Parameters.AddWithValue("@empresaId", empresaId);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            lista = new List<PerfilBe>();
                            while (dr.Read())
                            {
                                PerfilBe item = new PerfilBe();
                                item.PerfilId = dr.GetData<int>("PerfilId");
                                item.EmpresaId = dr.GetData<int>("EmpresaId");
                                item.Nombre = dr.GetData<string>("Nombre");
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
