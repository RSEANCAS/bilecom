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
    public class TipoComprobanteFormatoDa
    {
        public List<TipoComprobanteFormatoBe> ListarPorTipoComprobante(int tipoComprobanteId, SqlConnection cn)
        {
            List<TipoComprobanteFormatoBe> lista = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_tipocomprobanteformato_listar_x_tipocomprobante", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tipoComprobanteId", tipoComprobanteId);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            lista = new List<TipoComprobanteFormatoBe>();
                            while (dr.Read())
                            {
                                TipoComprobanteFormatoBe item = new TipoComprobanteFormatoBe();
                                item.TipoComprobanteId = dr.GetData<int>("TipoComprobanteId");
                                item.FormatoId = dr.GetData<int>("FormatoId");
                                item.Nombre = dr.GetData<string>("Nombre");
                                item.Html = dr.GetData<string>("Html");
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
