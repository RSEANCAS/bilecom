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
    public class DepartamentoDa
    {
        public List<DepartamentoBe> Listar(SqlConnection cn)
        {
            List<DepartamentoBe> lista = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_departamento_listar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            lista = new List<DepartamentoBe>();
                            while (dr.Read())
                            {
                                DepartamentoBe item = new DepartamentoBe();
                                item.DepartamentoId = dr.GetData<int>("DepartamentoId");
                                item.PaisId = dr.GetData<int>("PaisId");
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
