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

        public bool Guardar(FacturaBe registro, SqlConnection cn, out int? facturaId, out int? nroComprobante, out DateTime? fechaHoraEmision)
        {
            facturaId = null;
            nroComprobante = null;
            fechaHoraEmision = null;
            bool seGuardo = false;
            try
            {
                using (SqlCommand cmd = new SqlCommand("dbo.usp_factura_guardar", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@empresaId", registro.EmpresaId.GetNullable());
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@facturaId", SqlDbType = SqlDbType.Int, Value = registro.FacturaId.GetNullable(), Direction = ParameterDirection.InputOutput });
                    cmd.Parameters.AddWithValue("@serieId", registro.SerieId.GetNullable());
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@nroComprobante", SqlDbType = SqlDbType.Int, Value = registro.NroComprobante.GetNullable(), Direction = ParameterDirection.InputOutput });
                    cmd.Parameters.Add(new SqlParameter { ParameterName = "@fechaHoraEmision", SqlDbType = SqlDbType.DateTime, Value = DBNull.Value, Direction = ParameterDirection.InputOutput });
                    cmd.Parameters.AddWithValue("@fechaVencimiento", registro.FechaVencimiento.GetNullable());
                    cmd.Parameters.AddWithValue("@monedaId", registro.MonedaId.GetNullable());
                    cmd.Parameters.AddWithValue("@clienteID", registro.ClienteId.GetNullable());
                    cmd.Parameters.AddWithValue("@flagExportacion", registro.FlagExportacion.GetNullable());
                    cmd.Parameters.AddWithValue("@flagGratuito", registro.FlagGratuito.GetNullable());
                    cmd.Parameters.AddWithValue("@flagEmisorItinerante", registro.FlagEmisorItinerante.GetNullable());
                    cmd.Parameters.AddWithValue("@flagAnticipo", registro.FlagAnticipo.GetNullable());
                    cmd.Parameters.AddWithValue("@flagISC", registro.FlagISC.GetNullable());
                    cmd.Parameters.AddWithValue("@flagOtrosCargos", registro.FlagOtrosCargos.GetNullable());
                    cmd.Parameters.AddWithValue("@flagOtrosTributos", registro.FlagOtrosTributos.GetNullable());
                    cmd.Parameters.AddWithValue("@totalIgv", registro.TotalIgv.GetNullable());
                    cmd.Parameters.AddWithValue("@totalIsc", registro.TotalIsc.GetNullable());
                    cmd.Parameters.AddWithValue("@totalOtrosTributos", registro.TotalOtrosTributos.GetNullable());
                    cmd.Parameters.AddWithValue("@totalOtrosCargos", registro.TotalOtrosCargos.GetNullable());
                    cmd.Parameters.AddWithValue("@totalBaseImponible", registro.TotalBaseImponible.GetNullable());
                    cmd.Parameters.AddWithValue("@totalDescuentos", registro.TotalDescuentos.GetNullable());
                    cmd.Parameters.AddWithValue("@importeTotal", registro.ImporteTotal.GetNullable());
                    cmd.Parameters.AddWithValue("@usuario", registro.Usuario.GetNullable());

                    int filasAfectadas = cmd.ExecuteNonQuery();
                    seGuardo = filasAfectadas > 0;
                    if (seGuardo)
                    {
                        facturaId = (int?)cmd.Parameters["@facturaId"].Value;
                        nroComprobante = (int?)cmd.Parameters["@nroComprobante"].Value;
                        fechaHoraEmision = (DateTime?)cmd.Parameters["@fechaHoraEmision"].Value;
                    }
                }
            }
            catch (Exception ex)
            {
                seGuardo = false;
            }
            return seGuardo;
        }
    }
}
