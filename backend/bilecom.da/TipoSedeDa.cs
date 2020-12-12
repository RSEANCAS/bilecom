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
        public List<TipoSedeBe> Buscar(int empresaId, string nombre, int pagina, int cantidadRegistros, string columnaOrden, string ordenMax, SqlConnection cn, out int totalRegistros)
        {
            totalRegistros = 0;
            List<TipoSedeBe> lista = new List<TipoSedeBe>();

            using (SqlCommand cmd = new SqlCommand("dbo.usp_tiposede_Listar", cn))
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
    }
}
