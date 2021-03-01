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
    public class EmpresaAmbienteSunatDa
    {
        public List<EmpresaAmbienteSunatBe> Listar(SqlConnection cn)
        {
            List<EmpresaAmbienteSunatBe> lista = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_ambientesunat_listar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            lista = new List<EmpresaAmbienteSunatBe>();
                            while (dr.Read())
                            {
                                EmpresaAmbienteSunatBe item = new EmpresaAmbienteSunatBe();
                                item.EmpresaId = dr.GetData<int>("EmpresaId");
                                item.AmbienteSunatId = dr.GetData<int>("AmbienteSunatId");
                                item.RucSOL = dr.GetData<string>("RucSOL");
                                item.UsuarioSOL = dr.GetData<string>("UsuarioSOL");
                                item.ClaveSOL = dr.GetData<string>("ClaveSOL");
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

        public EmpresaAmbienteSunatBe Obtener(int empresaId, int ambienteSunatId, SqlConnection cn)
        {
            EmpresaAmbienteSunatBe item = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_empresaambientesunat_obtener", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empresaId", empresaId.GetNullable());
                    cmd.Parameters.AddWithValue("@ambienteSunatId", ambienteSunatId.GetNullable());

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            item = new EmpresaAmbienteSunatBe();

                            if (dr.Read())
                            {
                                item.EmpresaId = dr.GetData<int>("EmpresaId");
                                item.AmbienteSunatId = dr.GetData<int>("AmbienteSunatId");
                                item.RucSOL = dr.GetData<string>("RucSOL");
                                item.UsuarioSOL = dr.GetData<string>("UsuarioSOL");
                                item.ClaveSOL = dr.GetData<string>("ClaveSOL");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                item = null;
            }
            return item;
        }
    }
}
