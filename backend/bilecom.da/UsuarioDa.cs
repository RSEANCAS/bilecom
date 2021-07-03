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
    public class UsuarioDa
    {
        public UsuarioBe ObtenerPorNombre(string nombre, int? empresaId, SqlConnection cn)
        {
            UsuarioBe item = null;

            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_usuario_obtener_x_nombre", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre", nombre.GetNullable());
                    cmd.Parameters.AddWithValue("@empresaId", empresaId.GetNullable());

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            item = new UsuarioBe();
                            if (dr.Read())
                            {
                                item.UsuarioId = dr.GetData<int>("UsuarioId");
                                item.EmpresaId = dr.GetData<int>("EmpresaId");
                                item.Nombre = dr.GetData<string>("Nombre");
                                item.Contrasena = dr.GetData<byte[]>("Contraseña");
                                item.PersonalId = dr.GetData<int>("PersonalId");
                                item.Correo = dr.GetData<string>("Correo");
                                item.FlagCambiarContraseña = dr.GetData<bool>("FlagCambiarContraseña");
                                item.FlagActivo = dr.GetData<bool>("FlagActivo");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return item;
        }
        public UsuarioBe Obtener(int? empresaId,int? usuarioId, SqlConnection cn)
        {
            UsuarioBe item = null;

            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_usuario_obtener", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empresaId", empresaId.GetNullable());
                    cmd.Parameters.AddWithValue("@usuarioId", usuarioId.GetNullable());

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            item = new UsuarioBe();
                            if (dr.Read())
                            {
                                item.UsuarioId = dr.GetData<int>("UsuarioId");
                                item.EmpresaId = dr.GetData<int>("EmpresaId");
                                item.Nombre = dr.GetData<string>("Nombre");
                                item.Contrasena = dr.GetData<byte[]>("Contraseña");
                                item.PersonalId = dr.GetData<int>("PersonalId");
                                item.Correo = dr.GetData<string>("Correo");
                                item.FlagCambiarContraseña = dr.GetData<bool>("FlagCambiarContraseña");
                                item.FlagActivo = dr.GetData<bool>("FlagActivo");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return item;
        }
        public bool CambiarContrasena(int empresaId, int usuarioId, byte[] contrasena, string modificadoPor, SqlConnection cn)
        {
            bool seCambio = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_usuario_contraseña_cambiar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empresaId", empresaId.GetNullable());
                    cmd.Parameters.AddWithValue("@usuarioId", usuarioId.GetNullable());
                    cmd.Parameters.AddWithValue("@contraseña", contrasena.GetNullable());
                    cmd.Parameters.AddWithValue("@modificadoPor", modificadoPor.GetNullable());

                    int FilaAfectadas = cmd.ExecuteNonQuery();
                    seCambio = (FilaAfectadas != -1);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return seCambio;
        }
    }
}
