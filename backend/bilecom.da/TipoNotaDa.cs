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
    public class TipoNotaDa
    {
        public List<TipoNotaBe> ListarPorTipoComprobante(int tipoComprobanteId, SqlConnection cn)
        {
            List<TipoNotaBe> lista = null;
            using (SqlCommand cmd = new SqlCommand("dbo.usp_tiponota_listar_x_tipocomprobante", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@tipoComprobanteId", tipoComprobanteId.GetNullable());

                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        lista = new List<TipoNotaBe>();
                        while (dr.Read())
                        {
                            TipoNotaBe item = new TipoNotaBe();
                            item.TipoNotaId = dr.GetData<int>("TipoNotaId");
                            item.TipoComprobanteId = dr.GetData<int>("TipoComprobanteId");
                            item.Descripcion = dr.GetData<string>("Descripcion");
                            item.CodigoSunat = dr.GetData<string>("CodigoSunat");
                            lista.Add(item);
                        }
                    }
                }
            }
            return lista;
        }
    }
}
