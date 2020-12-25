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
    public class CategoriaProductoDa
    {
        public List<CategoriaProductoBe> Listar(int empresaId,SqlConnection cn)
        {
            
            List<CategoriaProductoBe> respuesta = null;

            using (SqlCommand cmd = new SqlCommand("dbo.usp_categoriaproducto_listar", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empresaId", empresaId.GetNullable());

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        respuesta  = new List<CategoriaProductoBe>();
                        while (dr.Read())
                        {
                            CategoriaProductoBe item = new CategoriaProductoBe();
                            item.CategoriaProductoId = dr.GetData<int>("CategoriaProductoId");
                            item.Nombre = dr.GetData<string>("Nombre");
                            respuesta.Add(item);
                        }
                    }
                }
            }
            return respuesta;
        }

        public List<CategoriaProductoBe> Buscar(int empresaId, string nombre, int pagina, int cantidadRegistros, string columnaOrden, string ordenMax, SqlConnection cn, out int totalRegistros)
        {
            totalRegistros = 0;
            List<CategoriaProductoBe> lista = null;

            using (SqlCommand cmd = new SqlCommand("dbo.usp_categoriaproducto_buscar", cn))
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
                        lista = new List<CategoriaProductoBe>();
                        while (dr.Read())
                        {
                            CategoriaProductoBe item = new CategoriaProductoBe();
                            item.CategoriaProductoId = dr.GetData<int>("Fila");
                            item.EmpresaId = dr.GetData<int>("EmpresaId");
                            item.Nombre = dr.GetData<string>("Nombre");
                            lista.Add(item);

                            totalRegistros = dr.GetData<int>("Total");
                        }
                    }
                }
            }
            return lista;

        }

        public bool Guardar(CategoriaProductoBe registro, SqlConnection cn)
        {
            bool seGuardo = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_categoriaproducto_guardar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empresaId", registro.EmpresaId.GetNullable());
                    cmd.Parameters.AddWithValue("@categoriaProductoId", registro.CategoriaProductoId.GetNullable());
                    cmd.Parameters.AddWithValue("@nombre", registro.Nombre.GetNullable());
                    cmd.Parameters.AddWithValue("@usuario", registro.Usuario.GetNullable());
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

        public CategoriaProductoBe Obtener(int empresaId,int categoriaProductoId,SqlConnection cn)
        {
            CategoriaProductoBe respuesta=null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_categoriaproducto_obtener", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmpresaId", empresaId.GetNullable());
                    cmd.Parameters.AddWithValue("@CategoriaProductoId", categoriaProductoId.GetNullable());

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            if (dr.Read())
                            {
                                respuesta = new CategoriaProductoBe();
                                respuesta.Nombre = dr.GetData<string>("Nombre");
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                respuesta = null;
            }
            return respuesta;
        }

        public bool Eliminar(int empresaId, int categoriaproductoId, string Usuario, SqlConnection cn)
        {
            bool seElimino = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_categoriaproducto_eliminar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@categoriaproductoId", categoriaproductoId.GetNullable());
                    cmd.Parameters.AddWithValue("@empresaId", empresaId.GetNullable());
                    cmd.Parameters.AddWithValue("@Usuario", Usuario.GetNullable());

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    seElimino = filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                seElimino = false;
            }
            return seElimino;
        }
    }
}
