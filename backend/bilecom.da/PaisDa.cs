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
    public class PaisDa
    {
        public List<PaisBe> Listar(SqlConnection cn)
        {
            List<PaisBe> lista = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_pais_listar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            lista = new List<PaisBe>();
                            while (dr.Read())
                            {
                                PaisBe item = new PaisBe();
                                item.PaisId = dr.GetData<int>("PaisId");
                                item.Nombre = dr.GetData<string>("Nombre");
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
