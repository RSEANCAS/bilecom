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
    public class FacturaDa
    {
        public List<FacturaBe> Buscar(int empresaId, string nroDocumentoIdentidadCliente, string razonSocialCliente, DateTime fechaHoraEmisionDesde, DateTime fechaHoraEmisionHasta, int pagina, int cantidadRegistros, string columnaOrden, string ordenMax, SqlConnection cn, out int totalRegistros)
        {
            totalRegistros = 0;
            List<FacturaBe> lista = null;
            using (SqlCommand cmd = new SqlCommand("usp_factura_buscar", cn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empresaId", empresaId.GetNullable());
                cmd.Parameters.AddWithValue("@nroDocumentoIdentidadCliente", nroDocumentoIdentidadCliente.GetNullable());
                cmd.Parameters.AddWithValue("@razonSocialCliente", razonSocialCliente.GetNullable());
                cmd.Parameters.AddWithValue("@fechaHoraEmisionDesde", fechaHoraEmisionDesde.GetNullable());
                cmd.Parameters.AddWithValue("@fechaHoraEmisionHasta", fechaHoraEmisionHasta.GetNullable());
                cmd.Parameters.AddWithValue("@pagina", pagina.GetNullable());
                cmd.Parameters.AddWithValue("@cantidadRegistros", cantidadRegistros.GetNullable());
                cmd.Parameters.AddWithValue("@columnaOrden", columnaOrden.GetNullable());
                cmd.Parameters.AddWithValue("@ordenMax", ordenMax.GetNullable());
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        lista = new List<FacturaBe>();

                        while (dr.Read())
                        {
                            FacturaBe item = new FacturaBe();
                            item.Fila = dr.GetData<int>("Fila");
                            item.FacturaId = dr.GetData<int>("FacturaId");
                            item.SerieId = dr.GetData<int>("SerieId");
                            item.Serie = new SerieBe();
                            item.Serie.SerieId = dr.GetData<int>("SerieId");
                            item.Serie.Serial = dr.GetData<string>("SerialSerie");
                            item.NroComprobante = dr.GetData<int>("NroComprobante");
                            item.FechaHoraEmision = dr.GetData<DateTime>("FechaHoraEmision");
                            item.FechaVencimiento = dr.GetData<DateTime>("FechaVencimiento");
                            item.Cliente = new ClienteBe();
                            item.Cliente.NroDocumentoIdentidad = dr.GetData<string>("NroDocumentoIdentidadCliente");
                            item.Cliente.RazonSocial = dr.GetData<string>("RazonSocialCliente");
                            item.ImporteTotal = dr.GetData<decimal>("ImporteTotal");
                            item.FlagAnulado = dr.GetData<bool>("FlagAnulado");
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
