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
    public class TipoComprobanteTipoOperacionVentaDa
    {
        public List<TipoComprobanteTipoOperacionVentaBe> Listar(SqlConnection cn)
        {
            List<TipoComprobanteTipoOperacionVentaBe> lista = null;
            using (SqlCommand cmd = new SqlCommand("dbo.usp_tipocomprobantetipooperacionventa_listar", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        lista = new List<TipoComprobanteTipoOperacionVentaBe>();

                        while (dr.Read())
                        {
                            TipoComprobanteTipoOperacionVentaBe item = new TipoComprobanteTipoOperacionVentaBe();
                            item.TipoOperacionVentaId = dr.GetData<int>("TipoOperacionVentaId");
                            item.TipoComprobanteId = dr.GetData<int>("TipoComprobanteId");
                            item.FlagActivo = dr.GetData<bool>("FlagActivo");
                            lista.Add(item);

                        }
                    }
                }
            }
            return lista;
        }

        public List<TipoComprobanteTipoOperacionVentaBe> ListarPorEmpresa(int empresaId, SqlConnection cn)
        {
            List<TipoComprobanteTipoOperacionVentaBe> lista = null;
            using (SqlCommand cmd = new SqlCommand("dbo.usp_tipocomprobantetipooperacionventa_listar_x_empresa", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empresaId", empresaId);
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        lista = new List<TipoComprobanteTipoOperacionVentaBe>();

                        while (dr.Read())
                        {
                            TipoComprobanteTipoOperacionVentaBe item = new TipoComprobanteTipoOperacionVentaBe();
                            item.TipoOperacionVentaId = dr.GetData<int>("TipoOperacionVentaId");
                            item.TipoComprobanteId = dr.GetData<int>("TipoComprobanteId");
                            item.FlagActivo = dr.GetData<bool>("FlagActivo");
                            lista.Add(item);

                        }
                    }
                }
            }
            return lista;
        }
    }
}
