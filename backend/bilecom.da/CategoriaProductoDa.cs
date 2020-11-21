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
        public List<CategoriaProductoBe> fListar(SqlConnection cn, int empresaId, string nombre)
        {
            List<CategoriaProductoBe> lCategoriaProducto = null;
            CategoriaProductoBe oCategoriaProducto;

            using (SqlCommand cmd = new SqlCommand("dbo.usp_CategoriaProducto_Listar", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empresaId", empresaId.GetNullable());
                cmd.Parameters.AddWithValue("@nombre", nombre.GetNullable());

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        lCategoriaProducto = new List<CategoriaProductoBe>();
                        if (dr.Read())
                        {
                            oCategoriaProducto = new CategoriaProductoBe();
                            oCategoriaProducto.EmpresaId = dr.GetData<int>("EmpresaId");
                            oCategoriaProducto.Nombre = dr.GetData<string>("Nombre");
                            lCategoriaProducto.Add(oCategoriaProducto);
                        }
                    }
                }
            }
            return lCategoriaProducto;

        }
    }
}
