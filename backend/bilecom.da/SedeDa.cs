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
    public class SedeDa
    {
        public List<SedeBe> Buscar(int empresaId, string nombre, int pagina, int cantidadRegistros, string columnaOrden, string ordenMax, SqlConnection cn, out int totalRegistros)
        {
            totalRegistros = 0;
            List<SedeBe> lista = new List<SedeBe>();
            using (SqlCommand cmd = new SqlCommand("dbo.usp_sede_buscar", cn))
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
                            SedeBe item = new SedeBe();
                            item.SedeId = dr.GetData<int>("Fila");
                            //item.SedeId = dr.GetData<int>("SedeId");
                            item.EmpresaId = dr.GetData<int>("EmpresaId");
                            item.TipoSedeId = dr.GetData<int>("TipoSedeId");
                            item.TipoSede = new TipoSedeBe();
                            item.TipoSede.Nombre = dr.GetData<string>("TipoSedeNombre");
                            item.Nombre = dr.GetData<string>("NombreSede");
                            item.DistritoId = dr.GetData<int>("DistritoId");
                            item.Direccion = dr.GetData<string>("Direccion");
                            item.FlagActivo = dr.GetData<bool>("SedeActivo");
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
