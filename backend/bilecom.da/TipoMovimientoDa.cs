using bilecom.ut;
using bilecom.be;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bilecom.da
{
    public class TipoMovimientoDa
    {
        public List<TipoMovimientoBe> Listar(SqlConnection cn)
        {
            List<TipoMovimientoBe> resultado = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_tipomovimiento_listar",cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if(dr.HasRows)
                        {
                            resultado = new List<TipoMovimientoBe>();
                            while(dr.Read())
                            {
                                TipoMovimientoBe item = new TipoMovimientoBe();
                                item.Id = dr.GetData<int>("Id");
                                item.Descripcion= dr.GetData<string>("Descripcion");
                                resultado.Add(item);
                            }
                        }
                    }
                }
            }
            catch
            {
                resultado = null;
            }
            return resultado;
        }

    }
}
