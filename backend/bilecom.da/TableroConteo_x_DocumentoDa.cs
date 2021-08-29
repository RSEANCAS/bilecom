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
    public class TableroConteo_x_DocumentoDa
    {
        public TableroConteo_x_DocumentoBe Obtener(int empresaId, int anio, int mes, SqlConnection cn)
        {
            TableroConteo_x_DocumentoBe respuesta = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_tablero_conteo_x_documento", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empresaId", empresaId.GetNullable());
                    cmd.Parameters.AddWithValue("@anio", anio.GetNullable());
                    cmd.Parameters.AddWithValue("@mes", mes.GetNullable());
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            respuesta = new TableroConteo_x_DocumentoBe();
                            while (dr.Read())
                            {
                                respuesta.TotalDocumentoFa = dr.GetData<int>("TotalDocumentoFa");
                                respuesta.TotalAnuladoFa = dr.GetData<int>("TotalAnuladoFa");
                                respuesta.TotalDocumentoBo = dr.GetData<int>("TotalDocumentoBo");
                                respuesta.TotalAnuladoBo = dr.GetData<int>("TotalAnuladoBo");
                                respuesta.TotalDocumentoNC = dr.GetData<int>("TotalDocumentoNC");
                                respuesta.TotalAnuladoNC = dr.GetData<int>("TotalAnuladoNC");
                                respuesta.TotalDocumentoND = dr.GetData<int>("TotalDocumentoND");
                                respuesta.TotalAnuladoND = dr.GetData<int>("TotalAnuladoND");
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
