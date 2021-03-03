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
    public class FormatoDa
    {
        public List<FormatoBe> ListarPorTipoComprobante(int tipoComprobanteId, SqlConnection cn)
        {
            List<FormatoBe> lista = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_formato_listar_x_tipocomprobante", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tipoComprobanteId", tipoComprobanteId);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            lista = new List<FormatoBe>();
                            while (dr.Read())
                            {
                                FormatoBe item = new FormatoBe();
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

        public FormatoBe Obtener(int formatoId, SqlConnection cn)
        {
            FormatoBe respuesta = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_formato_obtener", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@formatoId", formatoId.GetNullable());

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            respuesta = new FormatoBe();

                            if (dr.Read())
                            {
                                respuesta.FormatoId = dr.GetData<int>("FormatoId");
                                respuesta.TipoComprobanteId = dr.GetData<int>("TipoComprobanteId");
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
