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
        public List<CategoriaProductoBe> fListar(SqlConnection cn, int empresaId, string nombre, int pagina, int cantidadRegistros, string columnaOrden, string ordenMax, out int totalRegistros)
        {
            totalRegistros = 0;
            List<CategoriaProductoBe> lCategoriaProducto = null;
            CategoriaProductoBe oCategoriaProducto;

            using (SqlCommand cmd = new SqlCommand("dbo.usp_CategoriaProducto_Listar", cn))
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
                        lCategoriaProducto = new List<CategoriaProductoBe>();
                        while (dr.Read())
                        {
                            oCategoriaProducto = new CategoriaProductoBe();
                            oCategoriaProducto.CategoriaProductoId = dr.GetData<int>("Fila");
                            oCategoriaProducto.EmpresaId = dr.GetData<int>("EmpresaId");
                            oCategoriaProducto.Nombre = dr.GetData<string>("Nombre");
                            lCategoriaProducto.Add(oCategoriaProducto);

                            if (!DBNull.Value.Equals(dr["Total"])) totalRegistros = (int)dr["Total"];
                        }
                    }
                }
            }
            return lCategoriaProducto;

        }
        public bool CategoriaProductoGuardar(CategoriaProductoBe categoriaProductoBe, SqlConnection cn)
        {
            bool respuesta = false;
            try
            {
                using (SqlCommand oCommand = new SqlCommand("dbo.usp_categoriaproducto_guardar", cn))
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.Parameters.AddWithValue("@EmpresaId", categoriaProductoBe.EmpresaId.GetNullable());
                    oCommand.Parameters.AddWithValue("@CategoriaProductoId", categoriaProductoBe.CategoriaProductoId.GetNullable());
                    oCommand.Parameters.AddWithValue("@Nombre", categoriaProductoBe.Nombre.GetNullable());
                    oCommand.Parameters.AddWithValue("@Usuario", categoriaProductoBe.Usuario.GetNullable());
                    int result = oCommand.ExecuteNonQuery();
                    if (result > 0) respuesta = true;
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
            }
            return respuesta;
        }
        public bool CategoriaProductoActualizar(CategoriaProductoBe categoriaProductoBe, SqlConnection cn)
        {
            bool respuesta = false;
            try
            {
                using (SqlCommand oCommand = new SqlCommand("dbo.usp_categoriaproducto_guardar", cn))
                {
                    oCommand.CommandType = CommandType.StoredProcedure;
                    oCommand.Parameters.AddWithValue("@EmpresaId", categoriaProductoBe.EmpresaId);
                    oCommand.Parameters.AddWithValue("@CategoriaProductoId", categoriaProductoBe.CategoriaProductoId);
                    oCommand.Parameters.AddWithValue("@Nombre", categoriaProductoBe.Nombre);
                    oCommand.Parameters.AddWithValue("@ModificadoPor", categoriaProductoBe.Usuario);
                    oCommand.Parameters.AddWithValue("@FechaModificacio", categoriaProductoBe.Fecha);
                    int result = oCommand.ExecuteNonQuery();
                    if (result > 0) respuesta = true;
                }
            }
            catch (Exception ex)
            {
                respuesta = false;
            }
            return respuesta;
        }
    }
}
