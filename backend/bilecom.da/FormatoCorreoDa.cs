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
    public class FormatoCorreoDa
    {
        public FormatoCorreoBe Obtener(int tipoFormatoCorreoId, SqlConnection cn)
        {
            FormatoCorreoBe respuesta = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_formatocorreo_obtener", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tipoFormatoCorreoId", tipoFormatoCorreoId.GetNullable());

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            if (dr.Read())
                            {
                                respuesta = new FormatoCorreoBe();
                                respuesta.FormatoCorreoId = dr.GetData<int>("FormatoCorreoId");
                                respuesta.Nombre = dr.GetData<string>("Nombre");
                                respuesta.Html = dr.GetData<string>("Html");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta = null;
            }
            return respuesta;
        }
    }
}
