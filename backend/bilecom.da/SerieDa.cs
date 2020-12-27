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
        public List<SerieBe> Buscar(int empresaId, int? tipoComprobanteId, string serial, int pagina, int cantidadRegistros, string columnaOrden, string ordenMax, SqlConnection cn, out int totalRegistros)
        {
            totalRegistros = 0;
            List<SerieBe> lista = new List<SerieBe>();
            using (SqlCommand cmd = new SqlCommand("dbo.usp_serie_buscar", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empresaId", empresaId.GetNullable());
                cmd.Parameters.AddWithValue("@tipoComprobanteId", tipoComprobanteId.GetNullable());
                cmd.Parameters.AddWithValue("@serial", serial.GetNullable());
                cmd.Parameters.AddWithValue("@pagina", pagina.GetNullable());
                cmd.Parameters.AddWithValue("@cantidadRegistros", cantidadRegistros.GetNullable());
                cmd.Parameters.AddWithValue("@columnaOrden", columnaOrden.GetNullable());
                cmd.Parameters.AddWithValue("@ordenMax", ordenMax.GetNullable());
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            SerieBe item = new SerieBe();
                            item.SerieId = dr.GetData<int>("Fila");
                            item.EmpresaId = dr.GetData<int>("EmpresaId");
                            item.TipoComprobanteId = dr.GetData<int>("TipoComprobanteId");
                            item.TipoComprobante = new TipoComprobanteBe();
                            item.TipoComprobante.Nombre = dr.GetData<string>("NombreTipoComprobante");
                            item.Serial = dr.GetData<string>("Serial");
                            item.ValorInicial = dr.GetData<int>("ValorInicial");
                            item.ValorFinal = dr.GetData<int>("ValorFinal");
                            item.ValorActual = dr.GetData<int>("ValorActual");
                            lista.Add(item);

                            totalRegistros = dr.GetData<int>("Total");
                        }
                    }
                }
            }
            return lista;
        }
    }
}
