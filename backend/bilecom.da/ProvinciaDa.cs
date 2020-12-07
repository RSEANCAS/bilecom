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
    public class ProvinciaDa
    {
        public List<ProvinciaBe> Listar(SqlConnection cn)
        {
            List<ProvinciaBe> lista = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_provincia_listar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            lista = new List<ProvinciaBe>();
                            while (dr.Read())
                            {
                                ProvinciaBe item = new ProvinciaBe();
                                item.DepartamentoId = dr.GetData<int>("DepartamentoId");
                                item.ProvinciaId = dr.GetData<int>("ProvinciaId");
                                item.CodigoUbigeo = dr.GetData<string>("CodigoUbigeo");
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
