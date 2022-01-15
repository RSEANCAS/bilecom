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
    public class ReporteDa
    {
        public List<ComprobanteBe> ComprobanteListar(SqlConnection cn, int empresaId, int sedeId, string fechaInicio, string fechaFin, string cat01_id, string serie_id, string numero, string razonSocialComprador, string usuario_id)
        {
            List<ComprobanteBe> lista = new List<ComprobanteBe>();
            ComprobanteBe item;
            using (SqlCommand cmd = new SqlCommand("dbo.usp_reporte_ventas_listar", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@EmpresaId", empresaId.GetNullable());
                cmd.Parameters.AddWithValue("@SedeId", sedeId.GetNullable());
                cmd.Parameters.AddWithValue("@FechaInicio", fechaInicio.GetNullable());
                cmd.Parameters.AddWithValue("@fechaFin", fechaFin.GetNullable());
                cmd.Parameters.AddWithValue("@SerieId", serie_id.GetNullable());
                cmd.Parameters.AddWithValue("@numero", numero.GetNullable());
                cmd.Parameters.AddWithValue("@razonSocialComprador", razonSocialComprador.GetNullable());
                cmd.Parameters.AddWithValue("@usuario_id", usuario_id.GetNullable());

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            item = new ComprobanteBe();
                        }
                    }
                }
            }
            return lista;
        }
    }
}
