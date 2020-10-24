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
    public class EmpresaDa
    {
        public EmpresaBe Obtener(int empresaId, SqlConnection cn)
        {
            EmpresaBe item = null;

            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_empresa_obtener", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empresaId", empresaId);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            item = new EmpresaBe();
                            if (dr.Read())
                            {
                                item.EmpresaId = (int)dr["EmpresaId"];
                                item.Ruc = (string)dr["Ruc"];
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return item;
        }
    }
}
