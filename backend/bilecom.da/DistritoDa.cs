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
    public class DistritoDa
    {
        public List<DistritoBe> Listar(SqlConnection cn)
        {
            List<DistritoBe> lista = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_distrito_listar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            lista = new List<DistritoBe>();
                            while (dr.Read())
                            {
                                DistritoBe item = new DistritoBe();
                                item.DistritoId = dr.GetData<int>("DistritoId");
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
