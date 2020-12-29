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
    public class SerieDa
    {
        public List<SerieBe> ListarPorTipoComprobante(int tipoComprobanteId, SqlConnection cn)
        {
            List<SerieBe> lista = null;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_serie_listar_x_tipocomprobante", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@tipoComprobanteId", tipoComprobanteId);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            lista = new List<SerieBe>();
                            while (dr.Read())
                            {
                                SerieBe item = new SerieBe();
                                item.EmpresaId = dr.GetData<int>("EmpresaId");
                                item.SerieId = dr.GetData<int>("SerieId");
                                item.TipoComprobanteId = dr.GetData<int>("TipoComprobanteId");
                                item.Serial = dr.GetData<string>("Serial");
                                item.ValorInicial = dr.GetData<int>("ValorInicial");
                                item.ValorFinal = dr.GetData<int?>("ValorFinal");
                                item.FlagSinFinal = dr.GetData<bool>("FlagSinFinal");
                                item.ValorActual = dr.GetData<int>("ValorActual");
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
