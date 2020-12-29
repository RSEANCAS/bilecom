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
    public class MonedaDa
    {
        public List<MonedaBe> ListarPorEmpresa(int empresaId, SqlConnection cn)
        {
            List<MonedaBe> lista = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_moneda_listar_x_empresa", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empresaId", empresaId);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            lista = new List<MonedaBe>();
                            while (dr.Read())
                            {
                                MonedaBe item = new MonedaBe();
                                item.MonedaId = dr.GetData<int>("MonedaId");
                                item.Nombre = dr.GetData<string>("Nombre");
                                item.Simbolo = dr.GetData<string>("Simbolo");
                                item.Codigo = dr.GetData<string>("Codigo");
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
