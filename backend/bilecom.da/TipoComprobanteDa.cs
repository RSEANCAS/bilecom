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
    public class TipoComprobanteDa
    {
        public List<TipoComprobanteBe> Listar(SqlConnection cn)
        {
            List<TipoComprobanteBe> lista = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_tipocomprobante_listar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            lista = new List<TipoComprobanteBe>();
                            while (dr.Read())
                            {
                                TipoComprobanteBe item = new TipoComprobanteBe();
                                item.TipoComprobanteId = dr.GetData<int>("TipoComprobanteId");
                                item.Codigo = dr.GetData<string>("Codigo");
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

        public TipoComprobanteBe Obtener(int tipoComprobanteId, SqlConnection cn)
        {
            TipoComprobanteBe respuesta = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_tipocomprobante_obtener", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tipoComprobanteId", tipoComprobanteId.GetNullable());

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            if (dr.Read())
                            {
                                respuesta = new TipoComprobanteBe();
                                respuesta.TipoComprobanteId = dr.GetData<int>("TipoComprobanteId");
                                respuesta.Codigo = dr.GetData<string>("Codigo");
                                respuesta.Nombre = dr.GetData<string>("Nombre");
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
