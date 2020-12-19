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
    public class TipoSedeDa
    {
        public List<TipoSedeBe> Listar(int empresaId, SqlConnection cn)
        {
            List<TipoSedeBe> respuesta = new List<TipoSedeBe>();

            using (SqlCommand cmd = new SqlCommand("dbo.usp_tiposede_listar", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empresaId", empresaId.GetNullable());

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            TipoSedeBe item = new TipoSedeBe();
                            item.TipoSedeId = dr.GetData<int>("TipoSedeId");
                            item.EmpresaId = dr.GetData<int>("EmpresaId");
                            item.Nombre = dr.GetData<string>("Nombre");
                            item.FlagActivo = dr.GetData<bool>("FlagActivo");
                            respuesta.Add(item);
                        }
                    }
                }
            }
            return respuesta;
        }
        public List<TipoSedeBe> Buscar(int empresaId, string nombre, int pagina, int cantidadRegistros, string columnaOrden, string ordenMax, SqlConnection cn, out int totalRegistros)
        {
            totalRegistros = 0;
            List<TipoSedeBe> lista = new List<TipoSedeBe>();

            using (SqlCommand cmd = new SqlCommand("dbo.usp_tiposede_buscar", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empresaId", empresaId.GetNullable());
                cmd.Parameters.AddWithValue("@nombre", nombre.GetNullable());
                cmd.Parameters.AddWithValue("@pagina", pagina.GetNullable());
                cmd.Parameters.AddWithValue("@cantidadRegistros", cantidadRegistros.GetNullable());
                cmd.Parameters.AddWithValue("@columnaOrden", columnaOrden.GetNullable());
                cmd.Parameters.AddWithValue("@ordenMax", ordenMax.GetNullable());
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            TipoSedeBe item = new TipoSedeBe();
                            item.TipoSedeId = dr.GetData<int>("Fila");
                            item.EmpresaId = dr.GetData<int>("EmpresaId");
                            item.Nombre = dr.GetData<string>("Nombre");
                            item.FlagActivo = dr.GetData<bool>("FlagActivo");
                            lista.Add(item);

                            totalRegistros = dr.GetData<int>("Total");
                        }
                    }
                }
            }

            return lista;
        }

        public TipoSedeBe Obtener(int empresaId, int tiposedeId, SqlConnection cn)
        {
            TipoSedeBe respuesta = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_tiposede_obtener", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmpresaId", empresaId.GetNullable());
                    cmd.Parameters.AddWithValue("@TipoSedeId", tiposedeId.GetNullable());

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            if (dr.Read())
                            {
                                respuesta = new TipoSedeBe();
                                respuesta.Nombre = dr.GetData<string>("Nombre");
                                respuesta.FlagActivo = dr.GetData<bool>("FlagActivo");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = null;
            }
            return respuesta;
        }

        public bool Guardar(TipoSedeBe tipoSedeBe,SqlConnection cn)
        {
            bool seGuardo = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_tiposede_guardar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TipoSedeId", tipoSedeBe.TipoSedeId.GetNullable());
                    cmd.Parameters.AddWithValue("@EmpresaId", tipoSedeBe.EmpresaId.GetNullable());
                    cmd.Parameters.AddWithValue("@Nombre", tipoSedeBe.Nombre.GetNullable());
                    cmd.Parameters.AddWithValue("@FlagActivo", tipoSedeBe.FlagActivo.GetNullable());
                    cmd.Parameters.AddWithValue("@Usuario", tipoSedeBe.Usuario.GetNullable());
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
    }
}
