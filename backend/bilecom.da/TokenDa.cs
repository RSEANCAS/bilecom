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
    public class TokenDa
    {
        public bool Guardar(SqlConnection cn, TokenBe tokenBe)
        {
            bool seGuardo = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_token_guardar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@usuarioId", tokenBe.UsuarioId.GetNullable());
                    cmd.Parameters.AddWithValue("@empresaId", tokenBe.EmpresaId.GetNullable());
                    cmd.Parameters.AddWithValue("@codigoToken", tokenBe.CodigoToken.GetNullable());
                    cmd.Parameters.AddWithValue("@tipoTokenId", tokenBe.TipoTokenId.GetNullable());
                    cmd.Parameters.AddWithValue("@fechaInicio", tokenBe.FechaInicio.GetNullable());
                    cmd.Parameters.AddWithValue("@fechaFin", tokenBe.FechaFin.GetNullable());
                    cmd.Parameters.AddWithValue("@usuario", tokenBe.Usuario.GetNullable());
                    cmd.Parameters.AddWithValue("@fecha", tokenBe.Fecha.GetNullable());

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    seGuardo = filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {

                seGuardo = false;
            }
            return seGuardo;
        }
        public bool Validar(int usuarioId, int empresaId, string codigoToken, int tipoTokenId, SqlConnection cn)
        {
            bool esValido = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_token_validar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@usuarioId", usuarioId.GetNullable());
                    cmd.Parameters.AddWithValue("@empresaId", empresaId.GetNullable());
                    cmd.Parameters.AddWithValue("@codigoToken", codigoToken.GetNullable());
                    cmd.Parameters.AddWithValue("@tipoTokenId", tipoTokenId.GetNullable());

                    esValido = (bool)cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {

                esValido = false;
            }
            return esValido;
        }
        public TokenBe Obtener(int usuarioId, int empresaId, string codigoToken, int tipoTokenId, SqlConnection cn)
        {
            TokenBe token = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_token_obtener_x_codigotoken", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@usuarioId", usuarioId.GetNullable());
                    cmd.Parameters.AddWithValue("@empresaId", empresaId.GetNullable());
                    cmd.Parameters.AddWithValue("@codigoToken", codigoToken.GetNullable());
                    cmd.Parameters.AddWithValue("@tipoTokenId", tipoTokenId.GetNullable());

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                token = new TokenBe();           
                                token.UsuarioId = dr.GetData<int>("UsuarioId");
                                token.EmpresaId = dr.GetData<int>("EmpresaId");
                                token.CodigoToken = dr.GetData<string>("CodigoToken");
                                token.TipoTokenId = dr.GetData<int>("TipoTokenId");
                                token.FechaInicio = dr.GetData<DateTime>("FechaInicio");
                                token.FechaFin = dr.GetData<DateTime>("FechaFin");
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                token = null;
            }
            return token;
        }
    }
}
